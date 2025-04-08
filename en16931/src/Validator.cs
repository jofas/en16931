namespace dev.fassbender.en16931;

using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;

using net.sf.saxon.s9api;
using net.liberty_development.SaxonHE12s9apiExtensions;

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

        // UBL Invoice XRechnung Extension
        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/invoice:Invoice/cbc:CustomizationID[ . = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#conformant#urn:xeinkauf.de:kosit:extension:xrechnung_3.0']",
                ublNamespaceManager
            );
        }

        // UBL CreditNote XRechnung CIUS
        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/creditnote:CreditNote[ cbc:CustomizationID/text() = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0']",
                ublNamespaceManager
            );
            schema = Schema.UblCreditNote;
        }

        // CII XRechnung CIUS
        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/rsm:CrossIndustryInvoice[rsm:ExchangedDocumentContext/ram:GuidelineSpecifiedDocumentContextParameter/ram:ID/text() = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0']",
                ciiNamespaceManager
            );
            schema = Schema.CiiCrossIndustryInvoice;
        }

        // CII XRechnung Extension
        if (identifier == null)
        {
            identifier = root.SelectSingleNode(
                "/rsm:CrossIndustryInvoice[rsm:ExchangedDocumentContext/ram:GuidelineSpecifiedDocumentContextParameter/ram:ID/text() = 'urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#conformant#urn:xeinkauf.de:kosit:extension:xrechnung_3.0']",
                ciiNamespaceManager
            );
            schema = Schema.CiiCrossIndustryInvoice;
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

        XdmValue en16931FailedAsserts = xPath.evaluate(
            "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']",
            en16931Result!
        );

        // TODO: Extensions can extend code listings, making the BR-CL-* rules
        //   of the EN16931 Schematron fail early for code listings that are
        //   valid according to the extension.
        //
        //   How do we solve this problem?
        //
        //   * If we validate against an extension, we could ignore all BR-CL-*
        //     rules.
        //     This would be a rather terrible (and probably non-conformant),
        //     as we ignore every code linsting rule from the EN16931
        //     Schematron, not just the rules for code listings that are
        //     extended by the extension.
        //
        //   * Hard-code code listing extensions per extension.
        //     That'd require careful inspection upon updates to the extension,
        //     something I'd like to avoid.
        //
        //   * I don't see a way to tell from the XSLT source of the schematrons
        //     alone, when an EN16931 rule is overridden by an extension rule.
        //
        //   * I think the best we can do is add another resource to the bundle
        //     where we hard-code the EN16931 code listing rules that are
        //     overridden by an extension. That way it is at least capsuled and
        //     we can make "update the overridden rules resource" a step in our
        //     resource update workflow.
        //
        if(en16931FailedAsserts.size() > 0) {
            Console.WriteLine(en16931Result);
            Console.WriteLine(filepath);
            Console.WriteLine(schema);
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

        // TODO: failing XRechnung tests
        //       UBL Invoice Extension
        //       CII CrossIndustryInvoice Extension
        //
        // TODO: do I need to care about extension rules during schematron
        //       validation when invoice follows CIUS?
        //
        // TODO: suppress Saxon XSLT warnings to console

        XdmValue xRechnungFailedAsserts = xPath.evaluate(
            "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']",
            xRechnungResult!
        );

        if(xRechnungFailedAsserts.size() > 0) {
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

public class SchematronException : Exception {}

public class En16931SchematronException : SchematronException {}

public class XRechnungSchematronException : SchematronException {}
