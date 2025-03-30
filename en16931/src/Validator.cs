namespace dev.fassbender.en16931;

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

using System.Xml.Xsl;

public class Validator
{
    public static void ValidateFromFile(string filepath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filepath);
        XmlNode root = doc.DocumentElement;

        XmlNamespaceManager ublNamespaceManager = new XmlNamespaceManager(doc.NameTable);

        ublNamespaceManager.AddNamespace("ubl", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
        ublNamespaceManager.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
        ublNamespaceManager.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
        ublNamespaceManager.AddNamespace("invoice", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
        ublNamespaceManager.AddNamespace("creditnote", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");

        XmlNamespaceManager ciiNamespaceManager = new XmlNamespaceManager(doc.NameTable);

        ciiNamespaceManager.AddNamespace("rsm", "urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100");
        ciiNamespaceManager.AddNamespace("ram", "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100");
        ciiNamespaceManager.AddNamespace("qdt", "urn:un:unece:uncefact:data:standard:QualifiedDataType:100");
        ciiNamespaceManager.AddNamespace("udt", "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:100");

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

        schema = schema!;

        doc.Schemas.XmlResolver = new XmlUrlResolver();

        string schemaFilePath = schema switch
        {
            Schema.UblInvoice => "resources/ubl/2.1/maindoc/UBL-Invoice-2.1.xsd",
            Schema.UblCreditNote => "resources/ubl/2.1/maindoc/UBL-CreditNote-2.1.xsd",
            Schema.CiiCrossIndustryInvoice => "resources/cii/d16b/CrossIndustryInvoice_100pD16B.xsd",
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
        };


        XsltSettings settings = new XsltSettings(true, true);
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(en16931XsltPath, settings, new XmlUrlResolver());

        MemoryStream buf = new MemoryStream();
        XmlWriter output = new XmlTextWriter(buf, null);

        xslt.Transform(doc, output);

        Console.WriteLine("made it so far");
        Console.WriteLine(buf);
        /*
        Processor processor = new Processor(false);
        XsltExecutable en16931Xslt = processor.NewXsltCompiler().LoadExecutablePackage(new Uri(en16931XsltPath));
        */
        // TODO: Saxon-HE: validate against en16931 schematron
        // TODO: Saxon-HE: validate against xrechnung schematron
    }
}

enum Schema
{
    UblInvoice,
    UblCreditNote,
    CiiCrossIndustryInvoice,
}
