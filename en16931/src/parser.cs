using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using net.liberty_development.SaxonHE12s9apiExtensions;
using net.sf.saxon.s9api;
using Im = Dev.Fassbender.En16931.Model.Immutable;
using Mut = Dev.Fassbender.En16931.Model;

namespace Dev.Fassbender.En16931;

public class Parser
{
    XmlNamespaceManager _namespaces;

    XmlSchemaSet _schemaSet;

    XmlSerializer _invoiceSerializer;

    Processor _processor;

    XPathCompiler _xPathCompiler;
    XsltCompiler _xsltCompiler;

    XsltTransformer _en16931UblValidator;
    XsltTransformer _en16931CiiValidator;

    XsltTransformer _xRechnungUblValidator;
    XsltTransformer _xRechnungCiiValidator;

    XsltTransformer _irTransformer;

    public Parser()
    {
        _namespaces = new XmlNamespaceManager(new NameTable());

        // UBL namespces
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
        _schemaSet.Add(null, "resources/ubl/2.1/maindoc/UBL-Invoice-2.1.xsd");
        _schemaSet.Add(null, "resources/ubl/2.1/maindoc/UBL-CreditNote-2.1.xsd");
        _schemaSet.Add(null, "resources/cii/d16b/CrossIndustryInvoice_100pD16B.xsd");
        _schemaSet.Add(w3XmlSigSchema);
        _schemaSet.Compile();

        _invoiceSerializer = new XmlSerializer(typeof(Mut.Invoice));

        _processor = new Processor(false);
        _processor.getUnderlyingConfiguration().setStandardErrorOutput(new java.io.PrintStream(new NullOutputStream()));

        _xPathCompiler = _processor.newXPathCompiler();
        _xPathCompiler.declareNamespace("svrl", "http://purl.oclc.org/dsdl/svrl");

        _xsltCompiler = _processor.newXsltCompiler();

        _en16931UblValidator = _xsltCompiler.Compile(new FileInfo("resources/en16931/ubl/EN16931-UBL-validation.xslt")).load();
        _en16931CiiValidator = _xsltCompiler.Compile(new FileInfo("resources/en16931/cii/EN16931-CII-validation.xslt")).load();

        _xRechnungUblValidator = _xsltCompiler.Compile(new FileInfo("resources/xrechnung/ubl/XRechnung-UBL-validation.xsl")).load();
        _xRechnungCiiValidator = _xsltCompiler.Compile(new FileInfo("resources/xrechnung/cii/XRechnung-CII-validation.xsl")).load();

        _irTransformer = _xsltCompiler.Compile(new FileInfo("resources/ir/ir.xslt")).load();
    }

    public Mut.Invoice ParseFile(string filepath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Schemas = _schemaSet;

        doc.Load(filepath);

        DocumentType docType = GetDocumentType(doc);

        doc.Validate(null);

        XdmNode node = _processor.newDocumentBuilder().Build(new FileInfo(filepath));

        XsltTransformer en16931Validator = docType.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => _en16931UblValidator,
            Schema.CiiCrossIndustryInvoice => _en16931CiiValidator,
            _ => throw new UnreachableException(),
        };

        XdmDestination en16931Destination = new XdmDestination();

        en16931Validator.setDestination(en16931Destination);
        en16931Validator.setInitialContextNode(node);
        en16931Validator.transform();

        XdmNode? en16931Result = en16931Destination.getXdmNode().children().iterator().next()! as XdmNode;

        string en16931FailedAssertsQuery = "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']";

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
            en16931FailedAssertsQuery = $"{en16931FailedAssertsQuery}[not(contains(' BR-CL-10 BR-CL-11 BR-CL-21 BR-CL-25 BR-CL-26 BR-CO-16 ', concat(' ', normalize-space(@id), ' ')))]";
        }

        XdmValue en16931FailedAsserts = _xPathCompiler.evaluate(
            en16931FailedAssertsQuery,
            en16931Result!
        );

        if (en16931FailedAsserts.size() > 0)
        {
            throw new En16931SchematronException();
        }

        XsltTransformer xRechnungValidator = docType.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => _xRechnungUblValidator,
            Schema.CiiCrossIndustryInvoice => _xRechnungCiiValidator,
            _ => throw new UnreachableException(),
        };

        XdmDestination xRechnungDestination = new XdmDestination();

        xRechnungValidator.setDestination(xRechnungDestination);
        xRechnungValidator.setInitialContextNode(node);
        xRechnungValidator.transform();

        XdmNode? xRechnungResult = xRechnungDestination.getXdmNode().children().iterator().next() as XdmNode;

        XdmValue xRechnungFailedAsserts = _xPathCompiler.evaluate(
            "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']",
            xRechnungResult!
        );

        if (xRechnungFailedAsserts.size() > 0)
        {
            throw new XRechnungSchematronException();
        }

        XdmDestination irDestination = new XdmDestination();

        _irTransformer.setDestination(irDestination);
        _irTransformer.setInitialContextNode(node);
        _irTransformer.transform();

        Mut.Invoice invoice = (Mut.Invoice)_invoiceSerializer.Deserialize(
            new StringReader(irDestination.getXdmNode().ToString())
        )!;

        return invoice;
    }

    DocumentType GetDocumentType(XmlDocument doc)
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

        return new DocumentType((Schema)schema, (Standard)standard);
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

record DocumentType(Schema Schema, Standard Standard)
{
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

public class SchematronException : Exception { }

public class En16931SchematronException : SchematronException { }

public class XRechnungSchematronException : SchematronException { }

// For throwing away stdout output from Saxon
class NullOutputStream : java.io.OutputStream
{
    public override void write(int b) { }
}
