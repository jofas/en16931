using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using net.liberty_development.SaxonHE12s9apiExtensions;
using net.sf.saxon.s9api;

namespace Dev.Fassbender.En16931;

public class Validator
{
    public static void ValidateFromFile(string filepath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filepath);
        XmlNode root = doc.DocumentElement!;

        XmlNamespaceManager ublNamespaceManager = new XmlNamespaceManager(doc.NameTable);

        ublNamespaceManager.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
        ublNamespaceManager.AddNamespace("invoice", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
        ublNamespaceManager.AddNamespace("creditnote", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");

        XmlNamespaceManager ciiNamespaceManager = new XmlNamespaceManager(doc.NameTable);

        ciiNamespaceManager.AddNamespace("rsm", "urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100");
        ciiNamespaceManager.AddNamespace("ram", "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100");

        // UBL Invoice CIUS
        XmlNode? identifier = root.SelectSingleNode(
            "/invoice:Invoice/cbc:CustomizationID[ . = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0']",
            ublNamespaceManager
        );
        Schema? schema = Schema.UblInvoice;
        bool extension = false;

        // UBL Invoice XRechnung Extension
        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/invoice:Invoice/cbc:CustomizationID[ . = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#conformant#urn:xeinkauf.de:kosit:extension:xrechnung_3.0']",
                ublNamespaceManager
            );
            schema = Schema.UblInvoice;
            extension = true;
        }

        // UBL CreditNote XRechnung CIUS
        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/creditnote:CreditNote[ cbc:CustomizationID/text() = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0']",
                ublNamespaceManager
            );
            schema = Schema.UblCreditNote;
            extension = false;
        }

        // CII XRechnung CIUS
        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/rsm:CrossIndustryInvoice[rsm:ExchangedDocumentContext/ram:GuidelineSpecifiedDocumentContextParameter/ram:ID/text() = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0']",
                ciiNamespaceManager
            );
            schema = Schema.CiiCrossIndustryInvoice;
            extension = false;
        }

        // CII XRechnung Extension
        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/rsm:CrossIndustryInvoice[rsm:ExchangedDocumentContext/ram:GuidelineSpecifiedDocumentContextParameter/ram:ID/text() = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#conformant#urn:xeinkauf.de:kosit:extension:xrechnung_3.0']",
                ciiNamespaceManager
            );
            schema = Schema.CiiCrossIndustryInvoice;
            extension = true;
        }

        if (identifier == null)
        {
            throw new Exception("unable to find identifier");
        }

        doc.Schemas.XmlResolver = new XmlUrlResolver();

        string schemaFilePath = schema! switch
        {
            Schema.UblInvoice => "resources/ubl/2.1/maindoc/UBL-Invoice-2.1.xsd",
            Schema.UblCreditNote => "resources/ubl/2.1/maindoc/UBL-CreditNote-2.1.xsd",
            Schema.CiiCrossIndustryInvoice => "resources/cii/d16b/CrossIndustryInvoice_100pD16B.xsd",
            _ => throw new UnreachableException(),
        };

        doc.Schemas.Add(null, schemaFilePath);

        FileStream w3XmlSigSchemaFile = File.OpenRead("resources/w3/xmldsig-core-schema.xsd");

        XmlReaderSettings w3XmlSigSchemaReaderSettings = new XmlReaderSettings();
        w3XmlSigSchemaReaderSettings.DtdProcessing = DtdProcessing.Ignore;

        XmlReader w3XmlSigSchemaReader = XmlReader.Create(
            w3XmlSigSchemaFile,
            w3XmlSigSchemaReaderSettings
        );

        doc.Schemas.Add(
            "http://www.w3.org/2000/09/xmldsig#",
            w3XmlSigSchemaReader
        );

        doc.Schemas.Compile();

        doc.Validate(null);

        string en16931XsltPath = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => "resources/en16931/ubl/EN16931-UBL-validation.xslt",
            Schema.CiiCrossIndustryInvoice => "resources/en16931/cii/EN16931-CII-validation.xslt",
            _ => throw new UnreachableException(),
        };

        Processor processor = new Processor(false);
        processor.getUnderlyingConfiguration().setStandardErrorOutput(new java.io.PrintStream(new NullOutputStream()));

        var xPath = processor.newXPathCompiler();
        xPath.declareNamespace("svrl", "http://purl.oclc.org/dsdl/svrl");

        XdmNode node = processor.newDocumentBuilder().Build(new FileInfo(filepath));

        XsltTransformer en16931Transformer = processor.newXsltCompiler().Compile(
           new FileInfo(en16931XsltPath)
        ).load();

        XdmDestination en16931Destination = new XdmDestination();

        en16931Transformer.setDestination(en16931Destination);
        en16931Transformer.setInitialContextNode(node);
        en16931Transformer.transform();

        XdmNode? en16931Result = en16931Destination.getXdmNode().children().iterator().next()! as XdmNode;

        string en16931FailedAssertsQuery = "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']";

        if (extension)
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

        XdmValue en16931FailedAsserts = xPath.evaluate(
            en16931FailedAssertsQuery,
            en16931Result!
        );

        if (en16931FailedAsserts.size() > 0)
        {
            throw new En16931SchematronException();
        }

        string xRechnungXsltPath = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => "resources/xrechnung/ubl/XRechnung-UBL-validation.xsl",
            Schema.CiiCrossIndustryInvoice => "resources/xrechnung/cii/XRechnung-CII-validation.xsl",
            _ => throw new UnreachableException(),
        };

        XsltTransformer xRechnungTransformer = processor.newXsltCompiler().Compile(
           new FileInfo(xRechnungXsltPath)
        ).load();

        XdmDestination xRechnungDestination = new XdmDestination();

        xRechnungTransformer.setDestination(xRechnungDestination);
        xRechnungTransformer.setInitialContextNode(node);
        xRechnungTransformer.transform();

        XdmNode? xRechnungResult = xRechnungDestination.getXdmNode().children().iterator().next() as XdmNode;

        XdmValue xRechnungFailedAsserts = xPath.evaluate(
            "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']",
            xRechnungResult!
        );

        if (xRechnungFailedAsserts.size() > 0)
        {
            throw new XRechnungSchematronException();
        }
    }
}

enum Schema
{
    UblInvoice,
    UblCreditNote,
    CiiCrossIndustryInvoice,
}

public class SchematronException : Exception { }

public class En16931SchematronException : SchematronException { }

public class XRechnungSchematronException : SchematronException { }

class NullOutputStream : java.io.OutputStream
{
    public override void write(int b) { }
}
