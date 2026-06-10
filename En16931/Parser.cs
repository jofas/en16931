using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Saxon.Api;
using Im = En16931.Model.Immutable;
using Mut = En16931.Model;

namespace En16931;

public class Parser
{
    XmlNamespaceManager _namespaces;

    XmlSchemaSet _schemaSet;

    XmlSerializer _invoiceSerializer;

    DocumentBuilder _docBuilder;

    XsltExecutable _en16931UblValidator;
    XsltExecutable _en16931CiiValidator;

    XsltExecutable _xRechnungUblValidator;
    XsltExecutable _xRechnungCiiValidator;

    XsltExecutable _irTransformer;

    public Parser()
    {
        _namespaces = new XmlNamespaceManager(new NameTable());

        // UBL namespaces
        _namespaces.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
        _namespaces.AddNamespace("invoice", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
        _namespaces.AddNamespace("credit-note", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");

        // CII namespaces
        _namespaces.AddNamespace("rsm", "urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100");
        _namespaces.AddNamespace("ram", "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100");

        // Schema is DTD annotated, which is why we have to add it like this,
        // instead of adding the file directly with `_schemaSet.Add`
        FileStream w3XmlSigSchemaFile = File.OpenRead("Resources/W3/xmldsig-core-schema.xsd");
        XmlSchema w3XmlSigSchema = XmlSchema.Read(w3XmlSigSchemaFile, null)!;

        _schemaSet = new XmlSchemaSet();
        _schemaSet.XmlResolver = new XmlUrlResolver();
        _schemaSet.Add(null, "Resources/Ubl/maindoc/UBL-Invoice-2.1.xsd");
        _schemaSet.Add(null, "Resources/Ubl/maindoc/UBL-CreditNote-2.1.xsd");
        _schemaSet.Add(null, "Resources/Cii/CrossIndustryInvoice_100pD16B.xsd");
        _schemaSet.Add(w3XmlSigSchema);
        _schemaSet.Compile();

        _invoiceSerializer = new XmlSerializer(typeof(Mut.Invoice));

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

        Uri irUri = new Uri(new FileInfo("Resources/IR/ir.xslt").FullName);
        _irTransformer = xsltCompiler.Compile(irUri);
    }

    public Mut.Invoice ParseFile(string filepath)
    {
        XmlDocument ir = ParseFileToIR(filepath);

        XmlNodeReader reader = new(ir);

        Mut.Invoice invoice = (Mut.Invoice)_invoiceSerializer.Deserialize(reader)!;

        return invoice;
    }

    internal XmlDocument ParseFileToIR(string filepath)
    {
        using StreamReader reader = new(filepath);

        XmlDocument xmlDoc = GetSchemaValidatedDocument(reader);

        DocumentType docType = GetDocumentType(xmlDoc);

        XdmNode doc = _docBuilder.Build(xmlDoc);

        try
        {
            ValidateEn16931(doc, docType);
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

        ValidateXRechnung(doc, docType);

        DomDestination destination = new();

        Xslt30Transformer transformer = _irTransformer.Load30();
        transformer.GlobalContextItem = doc;
        transformer.ApplyTemplates(doc, destination);

        return destination.XmlDocument;
    }

    private void ValidateEn16931(XdmNode doc, DocumentType docType)
    {
        XsltExecutable validator = docType.Schema switch
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

    private void ValidateXRechnung(XdmNode doc, DocumentType docType)
    {
        XsltExecutable validator = docType.Schema switch
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

    private XmlDocument GetSchemaValidatedDocument(TextReader reader)
    {
        XmlDocument doc = new XmlDocument()
        {
            Schemas = _schemaSet,
        };

        doc.Load(reader);

        doc.Validate(null);

        return doc;
    }

    private DocumentType GetDocumentType(XmlDocument doc)
    {
        XmlNode root = doc.DocumentElement ?? throw new Exception("Could not find root node.");

        Schema schema = (root.NamespaceURI, root.LocalName) switch
        {
            (string namespaceUri, "Invoice") when namespaceUri == _namespaces.LookupNamespace("invoice") => Schema.UblInvoice,
            (string namespaceUri, "CreditNote") when namespaceUri == _namespaces.LookupNamespace("credit-note") => Schema.UblCreditNote,
            (string namespaceUri, "CrossIndustryInvoice") when namespaceUri == _namespaces.LookupNamespace("rsm") => Schema.CiiCrossIndustryInvoice,
            (_, _) => throw new Exception($"Unknown root node: {root.Name}."),
        };

        string specificationIdentifier = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => root.SelectSingleNode("cbc:CustomizationID", _namespaces)?.InnerText ?? throw new Exception("Could not find specification identifier node."),
            Schema.CiiCrossIndustryInvoice => root.SelectSingleNode("rsm:ExchangedDocumentContext/ram:GuidelineSpecifiedDocumentContextParameter/ram:ID", _namespaces)?.InnerText ?? throw new Exception("Could not find specification identifier node."),
            _ => throw new UnreachableException(),
        };

        Standard standard = specificationIdentifier switch
        {
            "urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0" => Standard.XRechnungCius,
            "urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#conformant#urn:xeinkauf.de:kosit:extension:xrechnung_3.0" => Standard.XRechnungExtension,
            _ => throw new Exception($"Uncompatible specification identifier: {specificationIdentifier}."),
        };

        return new DocumentType
        {
            Schema = schema,
            Standard = standard,
        };
    }
}

enum Schema
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
