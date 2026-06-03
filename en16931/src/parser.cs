using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Saxon.Api;
using Im = Dev.Fassbender.En16931.Model.Immutable;
using Mut = Dev.Fassbender.En16931.Model;

namespace Dev.Fassbender.En16931;

public class Parser
{
    XmlNamespaceManager _namespaces;

    XmlSchemaSet _schemaSet;

    XmlSerializer _invoiceSerializer;

    Processor _processor;

    DocumentBuilder _docBuilder;

    XPathExecutable _failedAssertsQuery;

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
        _namespaces.AddNamespace("creditnote", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");

        // CII namespaces
        _namespaces.AddNamespace("rsm", "urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100");
        _namespaces.AddNamespace("ram", "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100");

        // Schema is DTD annotated, which is why we have to add it like this,
        // instead of adding the file directly with `_schemaSet.Add`
        FileStream w3XmlSigSchemaFile = File.OpenRead("resources/w3/xmldsig-core-schema.xsd");
        XmlSchema w3XmlSigSchema = XmlSchema.Read(w3XmlSigSchemaFile, null)!;

        _schemaSet = new XmlSchemaSet();
        _schemaSet.XmlResolver = new XmlUrlResolver();
        _schemaSet.Add(null, "resources/ubl/maindoc/UBL-Invoice-2.1.xsd");
        _schemaSet.Add(null, "resources/ubl/maindoc/UBL-CreditNote-2.1.xsd");
        _schemaSet.Add(null, "resources/cii/CrossIndustryInvoice_100pD16B.xsd");
        _schemaSet.Add(w3XmlSigSchema);
        _schemaSet.Compile();

        _invoiceSerializer = new XmlSerializer(typeof(Mut.Invoice));

        _processor = new Processor(false);
        _processor.ErrorWriter = TextWriter.Null;

        _docBuilder = _processor.NewDocumentBuilder();

        XPathCompiler xPathCompiler = _processor.NewXPathCompiler();
        xPathCompiler.DeclareNamespace("svrl", "http://purl.oclc.org/dsdl/svrl");

        _failedAssertsQuery = xPathCompiler.Compile("svrl:failed-assert[@flag = 'fatal']");

        XsltCompiler xsltCompiler = _processor.NewXsltCompiler();

        Uri en16931UblUri = new Uri(new FileInfo("resources/en16931/EN16931-UBL-validation.xslt").FullName);
        _en16931UblValidator = xsltCompiler.Compile(en16931UblUri);

        Uri en16931CiiUri = new Uri(new FileInfo("resources/en16931/EN16931-CII-validation.xslt").FullName);
        _en16931CiiValidator = xsltCompiler.Compile(en16931CiiUri);

        Uri xRechnungUblUri = new Uri(new FileInfo("resources/xrechnung/XRechnung-UBL-validation.xsl").FullName);
        _xRechnungUblValidator = xsltCompiler.Compile(xRechnungUblUri);

        Uri xRechnungCiiUri = new Uri(new FileInfo("resources/xrechnung/XRechnung-CII-validation.xsl").FullName);
        _xRechnungCiiValidator = xsltCompiler.Compile(xRechnungCiiUri);

        Uri irUri = new Uri(new FileInfo("resources/ir/ir.xslt").FullName);
        _irTransformer = xsltCompiler.Compile(irUri);
    }

    public Mut.Invoice ParseFile(string filepath)
    {
        XmlDocument ir = ParseFileToIR(filepath);

        XmlNodeReader reader = new(ir);

        Mut.Invoice invoice = (Mut.Invoice)_invoiceSerializer.Deserialize(reader)!;

        return invoice;
    }

    private XmlDocument ParseFileToIR(string filepath)
    {
        DocumentType docType = ValidateSchema(filepath);

        using StreamReader reader = new(filepath);
        XdmNode doc = _docBuilder.Build(reader);

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

    private DocumentType ValidateSchema(string filepath)
    {
        XmlDocument doc = new XmlDocument()
        {
            Schemas = _schemaSet,
        };

        doc.Load(filepath);

        doc.Validate(null);

        return GetDocumentType(doc);
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
        XdmValue result = transformer.ApplyTemplates(doc);

        XPathSelector selector = _failedAssertsQuery.Load();
        selector.ContextItem = (XdmItem)result;
        XdmValue failedAsserts = selector.Evaluate();

        if (failedAsserts.Count > 0)
        {
            string[] errors = failedAsserts
                .Select(i => ((XdmNode)i).GetAttributeValue("id"))
                .ToArray();

            throw new En16931SchematronException
            {
                Errors = errors,
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
        XdmValue result = transformer.ApplyTemplates(doc);

        XPathSelector selector = _failedAssertsQuery.Load();
        selector.ContextItem = (XdmItem)result;
        XdmValue failedAsserts = selector.Evaluate();

        if (failedAsserts.Count > 0)
        {
            string[] errors = failedAsserts
                .Select(i => ((XdmNode)i).GetAttributeValue("id"))
                .ToArray();

            throw new XRechnungSchematronException
            {
                Errors = errors,
            };
        }
    }

    private DocumentType GetDocumentType(XmlDocument doc)
    {
        XmlNode root = doc.DocumentElement!;

        XmlNode? identifier = root.SelectSingleNode(
            "/invoice:Invoice/cbc:CustomizationID[ . = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0']",
            _namespaces
        );
        Schema? schema = Schema.UblInvoice;
        Standard? standard = Standard.XRechnungCIUS;

        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/invoice:Invoice/cbc:CustomizationID[ . = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#conformant#urn:xeinkauf.de:kosit:extension:xrechnung_3.0']",
                _namespaces
            );
            schema = Schema.UblInvoice;
            standard = Standard.XRechnungExtension;
        }

        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/creditnote:CreditNote[ cbc:CustomizationID/text() = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0']",
                _namespaces
            );
            schema = Schema.UblCreditNote;
            standard = Standard.XRechnungCIUS;
        }

        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/rsm:CrossIndustryInvoice[rsm:ExchangedDocumentContext/ram:GuidelineSpecifiedDocumentContextParameter/ram:ID/text() = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0']",
                _namespaces
            );
            schema = Schema.CiiCrossIndustryInvoice;
            standard = Standard.XRechnungCIUS;
        }

        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/rsm:CrossIndustryInvoice[rsm:ExchangedDocumentContext/ram:GuidelineSpecifiedDocumentContextParameter/ram:ID/text() = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#conformant#urn:xeinkauf.de:kosit:extension:xrechnung_3.0']",
                _namespaces
            );
            schema = Schema.CiiCrossIndustryInvoice;
            standard = Standard.XRechnungExtension;
        }

        if (identifier == null)
        {
            throw new Exception("unable to find identifier");
        }

        return new DocumentType
        {
            Schema = schema.Value,
            Standard = standard.Value,
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
    XRechnungCIUS,
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
