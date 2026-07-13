using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using En16931.Model;
using Saxon.Api;

namespace En16931;

static class Urn
{
    public const string UblInvoice = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
    public const string UblCreditNote = "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2";
    public const string UblCbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";

    public const string Cii = "urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100";
    public const string CiiRam = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100";

    public const string XRechnungV3 = "urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0";
    public const string XRechnungExtensionV3 = "urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#conformant#urn:xeinkauf.de:kosit:extension:xrechnung_3.0";
}

public class Parser
{
    private static QName _ublInvoice = new QName(Urn.UblInvoice, "Invoice");
    private static QName _ublCreditNote = new QName(Urn.UblCreditNote, "CreditNote");
    private static QName _cii = new QName(Urn.Cii, "CrossIndustryInvoice");

    private XmlSchemaSet _schemaSet;

    private DocumentBuilder _docBuilder;

    private XsltExecutable _en16931UblValidator;
    private XsltExecutable _en16931CiiValidator;

    private XsltExecutable _xRechnungUblValidator;
    private XsltExecutable _xRechnungCiiValidator;

    private XsltExecutable _ublToIRTransformer;
    private XsltExecutable _ciiToIRTransformer;

    private XsltExecutable _irToCiiTransformer;
    private XsltExecutable _irToUblTransformer;

    public Parser()
    {
        _schemaSet = new XmlSchemaSet();
        _schemaSet.XmlResolver = new XmlUrlResolver();

        _schemaSet.Add(null, "Resources/Ubl/maindoc/UBL-Invoice-2.1.xsd");
        _schemaSet.Add(null, "Resources/Ubl/maindoc/UBL-CreditNote-2.1.xsd");
        _schemaSet.Add(null, "Resources/Cii/CrossIndustryInvoice_100pD16B.xsd");

        // Schema is DTD annotated, which is why we have to add it like this,
        // instead of adding the file directly with `_schemaSet.Add`
        FileStream w3XmlSigSchemaFile = File.OpenRead("Resources/W3/xmldsig-core-schema.xsd");
        XmlSchema w3XmlSigSchema = XmlSchema.Read(w3XmlSigSchemaFile, null)!;
        _schemaSet.Add(w3XmlSigSchema);

        _schemaSet.Compile();

        Processor processor = new Processor(false);
        processor.ErrorWriter = TextWriter.Null;

        _docBuilder = processor.NewDocumentBuilder();

        XsltCompiler xsltCompiler = processor.NewXsltCompiler();

        Uri en16931UblUri = new Uri(new FileInfo("Resources/En16931/EN16931-UBL-validation.xslt").FullName);
        _en16931UblValidator = xsltCompiler.Compile(en16931UblUri);

        Uri en16931CiiUri = new Uri(new FileInfo("Resources/En16931/EN16931-CII-validation.xslt").FullName);
        _en16931CiiValidator = xsltCompiler.Compile(en16931CiiUri);

        Uri xRechnungUblUri = new Uri(new FileInfo("Resources/XRechnung/XRechnung-UBL-validation.xsl").FullName);
        _xRechnungUblValidator = xsltCompiler.Compile(xRechnungUblUri);

        Uri xRechnungCiiUri = new Uri(new FileInfo("Resources/XRechnung/XRechnung-CII-validation.xsl").FullName);
        _xRechnungCiiValidator = xsltCompiler.Compile(xRechnungCiiUri);

        Uri ublToIRUri = new Uri(new FileInfo("Resources/IR/ubl2ir.xslt").FullName);
        _ublToIRTransformer = xsltCompiler.Compile(ublToIRUri);

        Uri ciiToIRUri = new Uri(new FileInfo("Resources/IR/cii2ir.xslt").FullName);
        _ciiToIRTransformer = xsltCompiler.Compile(ciiToIRUri);

        Uri irToCiiUri = new Uri(new FileInfo("Resources/IR/ir2cii.xslt").FullName);
        _irToCiiTransformer = xsltCompiler.Compile(irToCiiUri);

        Uri irToUblUri = new Uri(new FileInfo("Resources/IR/ir2ubl.xslt").FullName);
        _irToUblTransformer = xsltCompiler.Compile(irToUblUri);
    }

    public Invoice Parse(string filepath)
    {
        using StreamReader reader = new(filepath);
        return Parse(reader);
    }

    public Invoice Parse(TextReader reader)
    {
        using XmlTextReader xmlReader = new(reader);
        return Parse(xmlReader);
    }

    public Invoice Parse(XmlReader reader)
    {
        XdmNode doc = GetSchemaValidatedDocument(reader);
        DocumentType docType = GetDocumentType(doc);

        Validate(doc, in docType);

        XdmNode ir = SchemaToIR(doc, docType.Schema);

        using XmlNodeReader irReader = new(ir.GetUnderlyingXmlNode());
        return Invoice.Deserialize(irReader);
    }

    public void Validate(string filepath)
    {
        using StreamReader reader = new(filepath);
        Validate(reader);
    }

    public void Validate(TextReader reader)
    {
        using XmlTextReader xmlReader = new(reader);
        Validate(xmlReader);
    }

    public void Validate(XmlReader reader)
    {
        XdmNode doc = GetSchemaValidatedDocument(reader);
        DocumentType docType = GetDocumentType(doc);

        Validate(doc, in docType);
    }

    public void Serialize(ref readonly Invoice invoice, string filepath, Schema schema)
    {
        using StreamWriter writer = new(filepath);
        Serialize(in invoice, writer, schema);
    }

    public void Serialize(ref readonly Invoice invoice, TextWriter writer, Schema schema)
    {
        using XmlTextWriter xmlWriter = new(writer);
        Serialize(in invoice, xmlWriter, schema);
    }

    public void Serialize(ref readonly Invoice invoice, XmlWriter writer, Schema schema)
    {
        XdmNode irDoc = SerializeToIR(in invoice);

        XdmNode doc = IRToSchema(irDoc, schema);

        DocumentType docType = GetDocumentType(doc);

        Validate(doc, in docType);

        doc.WriteTo(writer);
    }

    private void Validate(XdmNode doc, ref readonly DocumentType docType)
    {
        try
        {
            ValidateEn16931(doc, docType.Schema);
        }
        catch (En16931SchematronException e)
        {
            if (docType.Standard == Standard.XRechnungExtension)
            {
                // Extensions can extend code listings and otherwise add elements
                // or override rules of the EN16931 specification.
                // These overridden rules of the EN16931 Schematron can fail early,
                // even when the invoice is valid according to the extension.
                // Here we remove these failed asserts from the query and continue
                // executing the rules that override the code listings.
                //
                // The XRechnung Extension overrides the following rules:
                //
                // * BR-CL-10 => BR-DEX-04
                // * BR-CL-11 => BR-DEX-05
                // * BR-CL-21 => BR-DEX-06
                // * BR-CL-25 => BR-DEX-07
                // * BR-CL-26 => BR-DEX-08
                // * BR-CO-16 => BR-DEX-09
                //
                if (!e.Errors.All(e =>
                {
                    return ((string[])[
                        "BR-CL-10",
                        "BR-CL-11",
                        "BR-CL-21",
                        "BR-CL-25",
                        "BR-CL-26",
                        "BR-CO-16",
                    ]).Contains(e);
                }))
                {
                    throw;
                }
            }
            else
            {
                throw;
            }
        }

        ValidateXRechnung(doc, docType.Schema);
    }

    private void ValidateEn16931(XdmNode doc, Schema schema)
    {
        XsltExecutable validator = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => _en16931UblValidator,
            Schema.CiiCrossIndustryInvoice => _en16931CiiValidator,
            _ => throw new UnreachableException(),
        };

        Xslt30Transformer transformer = validator.Load30();
        transformer.GlobalContextItem = doc;
        XdmNode result = (XdmNode)transformer.ApplyTemplates(doc);

        List<string> infos = [];
        List<string> warnings = [];
        List<string> errors = [];

        foreach (XdmNode node in result.Children("failed-assert"))
        {
            List<string> list = node.GetAttributeValue("flag") switch
            {
                "information" => infos,
                "warning" => warnings,
                "fatal" => errors,
                _ => throw new UnreachableException(),
            };

            list.Add(node.GetAttributeValue("id"));
        }

        if (errors.Count > 0)
        {
            throw new En16931SchematronException
            {
                Errors = errors.ToArray(),
            };
        }
    }

    private void ValidateXRechnung(XdmNode doc, Schema schema)
    {
        XsltExecutable validator = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => _xRechnungUblValidator,
            Schema.CiiCrossIndustryInvoice => _xRechnungCiiValidator,
            _ => throw new UnreachableException(),
        };

        Xslt30Transformer transformer = validator.Load30();
        transformer.GlobalContextItem = doc;
        XdmNode result = (XdmNode)transformer.ApplyTemplates(doc);

        List<string> infos = [];
        List<string> warnings = [];
        List<string> errors = [];

        foreach (XdmNode node in result.Children("failed-assert"))
        {
            List<string> list = node.GetAttributeValue("flag") switch
            {
                "information" => infos,
                "warning" => warnings,
                "fatal" => errors,
                _ => throw new UnreachableException(),
            };

            list.Add(node.GetAttributeValue("id"));
        }

        if (errors.Count > 0)
        {
            throw new XRechnungSchematronException
            {
                Errors = errors.ToArray(),
            };
        }
    }

    private XdmNode SchemaToIR(XdmNode doc, Schema schema)
    {
        XsltExecutable executable = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => _ublToIRTransformer,
            Schema.CiiCrossIndustryInvoice => _ciiToIRTransformer,
            _ => throw new UnreachableException(),
        };

        DomDestination destination = new();

        Xslt30Transformer transformer = executable.Load30();
        transformer.GlobalContextItem = doc;
        transformer.ApplyTemplates(doc, destination);

        // TODO: validate against IR schema, once it exists

        return _docBuilder.Wrap(destination.XmlDocument);
    }

    private XdmNode IRToSchema(XdmNode ir, Schema schema)
    {
        XsltExecutable executable = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => _irToUblTransformer,
            Schema.CiiCrossIndustryInvoice => _irToCiiTransformer,
            _ => throw new UnreachableException(),
        };

        DomDestination destination = new();

        Xslt30Transformer transformer = executable.Load30();

        transformer.InitialMode = schema switch
        {
            Schema.UblInvoice => new QName("invoice"),
            Schema.UblCreditNote => new QName("credit-note"),
            _ => null,
        };

        transformer.GlobalContextItem = ir;

        transformer.ApplyTemplates(ir, destination);

        destination.XmlDocument.Schemas = _schemaSet;
        destination.XmlDocument.Validate(null);

        return _docBuilder.Wrap(destination.XmlDocument);
    }

    private XdmNode SerializeToIR(ref readonly Invoice invoice)
    {
        XmlDocument result = new();

        using (XmlWriter irWriter = result.CreateNavigator()!.AppendChild())
        {
            invoice.Serialize(irWriter);
        }

        // TODO: validate against IR schema, once it exists

        return _docBuilder.Wrap(result);
    }

    private XdmNode GetSchemaValidatedDocument(XmlReader reader)
    {
        XmlDocument result = new()
        {
            Schemas = _schemaSet,
        };

        result.Load(reader);

        result.Validate(null);

        return _docBuilder.Wrap(result);
    }

    private DocumentType GetDocumentType(XdmNode doc)
    {
        doc = doc ?? throw new ArgumentNullException();

        XdmNode root = doc.OutermostElement ?? throw new Exception("Could not find root node.");

        Schema schema = root.NodeName switch
        {
            QName n when n.Equals(_ublInvoice) => Schema.UblInvoice,
            QName n when n.Equals(_ublCreditNote) => Schema.UblCreditNote,
            QName n when n.Equals(_cii) => Schema.CiiCrossIndustryInvoice,
            _ => throw new Exception($"Unknown root node: {root.NodeName?.EQName ?? "null?"}."),
        };

        string specificationIdentifier = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => root
                .Children(Urn.UblCbc, "CustomizationID")
                .Single()
                .GetUnderlyingXmlNode()?
                .InnerText ?? throw new Exception("Could not find specification identifier node."),
            Schema.CiiCrossIndustryInvoice => root
                .Children(Urn.Cii, "ExchangedDocumentContext")
                .Single()
                .Children(Urn.CiiRam, "GuidelineSpecifiedDocumentContextParameter")
                .Single()
                .Children(Urn.CiiRam, "ID")
                .Single()
                .GetUnderlyingXmlNode()?
                .InnerText ?? throw new Exception("Could not find specification identifier node."),
            _ => throw new UnreachableException(),
        };

        Standard standard = specificationIdentifier switch
        {
            Urn.XRechnungV3 => Standard.XRechnungCius,
            Urn.XRechnungExtensionV3 => Standard.XRechnungExtension,
            _ => throw new Exception($"Uncompatible specification identifier: {specificationIdentifier}."),
        };

        return new DocumentType
        {
            Schema = schema,
            Standard = standard,
        };
    }
}

public enum Schema
{
    UblInvoice,
    UblCreditNote,
    CiiCrossIndustryInvoice,
}

enum Standard
{
    XRechnungCius,
    XRechnungExtension,
}

readonly ref struct DocumentType
{
    public required Schema Schema { get; init; }
    public required Standard Standard { get; init; }

    public string SchemaFilePath()
    {
        return Schema switch
        {
            Schema.UblInvoice => "resources/ubl/2.1/maindoc/UBL-Invoice-2.1.xsd",
            Schema.UblCreditNote => "resources/ubl/2.1/maindoc/UBL-CreditNote-2.1.xsd",
            Schema.CiiCrossIndustryInvoice => "resources/cii/d16b/CrossIndustryInvoice_100pD16B.xsd",
            _ => throw new UnreachableException(),
        };
    }
}

public class SchematronException : Exception
{
    public required string[] Errors { get; init; }
}

public class En16931SchematronException : SchematronException { }

public class XRechnungSchematronException : SchematronException { }
