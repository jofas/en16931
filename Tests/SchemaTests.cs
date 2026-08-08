using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using En16931;
using Xunit;

namespace Tests;

public class SchemaTests
{
    [Fact]
    public void UblInvoiceSchemaViolation()
    {
        string xml = $"""
            <?xml version="1.0" encoding="UTF-8"?>
            <ubl:Invoice xmlns:ubl="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"
                    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
                    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2">
                <cbc:CustomizationID>urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0</cbc:CustomizationID>
            </ubl:Invoice>
            """;

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        Assert.Throws<XmlSchemaValidationException>(() =>
        {
            Document document = new(xmlReader);
        });
    }

    [Fact]
    public void UblCreditNoteSchemaViolation()
    {
        string xml = $"""
            <?xml version="1.0" encoding="UTF-8"?>
            <ubl:CreditNote xmlns:ubl="urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2"
                    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
                    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2">
                <cbc:CustomizationID>urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0</cbc:CustomizationID>
            </ubl:CreditNote>
            """;

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        Assert.Throws<XmlSchemaValidationException>(() =>
        {
            Document document = new(xmlReader);
        });
    }

    [Fact]
    public void CiiSchemaViolation()
    {
        string xml = $"""
            <?xml version="1.0" encoding="UTF-8"?>
            <rsm:CrossIndustryInvoice xmlns:rsm="urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100"
                    xmlns:ram="urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100"
                    xmlns:qdt="urn:un:unece:uncefact:data:standard:QualifiedDataType:100"
                    xmlns:udt="urn:un:unece:uncefact:data:standard:UnqualifiedDataType:100">
                <rsm:ExchangedDocumentContext>
                    <ram:GuidelineSpecifiedDocumentContextParameter>
                        <ram:ID>urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0</ram:ID>
                    </ram:GuidelineSpecifiedDocumentContextParameter>
                </rsm:ExchangedDocumentContext>
            </rsm:CrossIndustryInvoice>
            """;

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        Assert.Throws<XmlSchemaValidationException>(() =>
        {
            Document document = new(xmlReader);
        });
    }
}
