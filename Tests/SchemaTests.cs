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
    public void InvalidXml()
    {
        string notXml = """
            <Invoice>
            </NotInvoice>
            """;

        using StringReader reader = new(notXml);

        Assert.Throws<XmlException>(() =>
        {
            Document document = new(reader);
        });
    }

    [Fact]
    public void UnsupportedSchema()
    {
        string xml = """
            <?xml version="1.0" encoding="UTF-16"?>
            <Invoice>
            </Invoice>
            """;

        using StringReader reader = new(xml);

        Exception e = Assert.Throws<Exception>(() =>
        {
            Document document = new(reader);
        });

        Assert.Equal("Unknown root node: Invoice.", e.Message);
    }

    [Fact]
    public void UblInvoiceSchemaViolation()
    {
        string xml = """
            <?xml version="1.0" encoding="UTF-16"?>
            <ubl:Invoice xmlns:ubl="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"
                    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
                    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2">
                <cbc:CustomizationID>urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0</cbc:CustomizationID>
            </ubl:Invoice>
            """;

        using StringReader reader = new(xml);

        Assert.Throws<XmlSchemaValidationException>(() =>
        {
            Document document = new(reader);
        });
    }

    [Fact]
    public void UblCreditNoteSchemaViolation()
    {
        string xml = """
            <?xml version="1.0" encoding="UTF-16"?>
            <ubl:CreditNote xmlns:ubl="urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2"
                    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
                    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2">
                <cbc:CustomizationID>urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0</cbc:CustomizationID>
            </ubl:CreditNote>
            """;

        using StringReader reader = new(xml);

        Assert.Throws<XmlSchemaValidationException>(() =>
        {
            Document document = new(reader);
        });
    }

    [Fact]
    public void CiiSchemaViolation()
    {
        string xml = """
            <?xml version="1.0" encoding="UTF-16"?>
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

        Assert.Throws<XmlSchemaValidationException>(() =>
        {
            Document document = new(reader);
        });
    }

    [Fact]
    public void CiiD16b()
    {
        string xml = """
            <?xml version="1.0" encoding="UTF-16"?>
            <rsm:CrossIndustryInvoice xmlns:rsm="urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100"
                    xmlns:ram="urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100"
                    xmlns:qdt="urn:un:unece:uncefact:data:standard:QualifiedDataType:100"
                    xmlns:udt="urn:un:unece:uncefact:data:standard:UnqualifiedDataType:100">
                <rsm:ExchangedDocumentContext>
                    <ram:GuidelineSpecifiedDocumentContextParameter>
                        <ram:ID>urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0</ram:ID>
                    </ram:GuidelineSpecifiedDocumentContextParameter>
                </rsm:ExchangedDocumentContext>
                <rsm:ExchangedDocument>
                  <ram:ID>1</ram:ID>
                  <ram:IssueDateTime>
                    <udt:DateTimeString format="102">20180413</udt:DateTimeString>
                  </ram:IssueDateTime>
                </rsm:ExchangedDocument>
                <rsm:SupplyChainTradeTransaction>
                  <ram:ApplicableHeaderTradeAgreement>
                  </ram:ApplicableHeaderTradeAgreement>
                  <ram:ApplicableHeaderTradeDelivery>
                  </ram:ApplicableHeaderTradeDelivery>
                  <ram:ApplicableHeaderTradeSettlement>
                  </ram:ApplicableHeaderTradeSettlement>
                </rsm:SupplyChainTradeTransaction>
            </rsm:CrossIndustryInvoice>
            """;

        using StringReader reader = new(xml);

        Document document = new(reader);

        Assert.Equal(Schema.CiiD16b, document.Schema);
    }

    [Fact]
    public void CiiD22b()
    {
        string xml = """
            <?xml version="1.0" encoding="UTF-16"?>
            <rsm:CrossIndustryInvoice xmlns:rsm="urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100"
                    xmlns:ram="urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100"
                    xmlns:qdt="urn:un:unece:uncefact:data:standard:QualifiedDataType:100"
                    xmlns:udt="urn:un:unece:uncefact:data:standard:UnqualifiedDataType:100">
                <rsm:ExchangedDocumentContext>
                    <ram:GuidelineSpecifiedDocumentContextParameter>
                        <ram:ID>urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0</ram:ID>
                    </ram:GuidelineSpecifiedDocumentContextParameter>
                </rsm:ExchangedDocumentContext>
                <rsm:ExchangedDocument>
                  <ram:ID>3</ram:ID>
                  <ram:IssueDateTime>
                    <udt:DateTimeString format="102">20180413</udt:DateTimeString>
                  </ram:IssueDateTime>
                </rsm:ExchangedDocument>
                <rsm:SupplyChainTradeTransaction>
                  <ram:ApplicableHeaderTradeAgreement>
                  </ram:ApplicableHeaderTradeAgreement>
                  <ram:ApplicableHeaderTradeDelivery>
                  </ram:ApplicableHeaderTradeDelivery>
                  <ram:ApplicableHeaderTradeSettlement>
                    <ram:InvoiceReferencedDocument>
                      <ram:IssuerAssignedID>2</ram:IssuerAssignedID>
                      <ram:FormattedIssueDateTime>
                        <qdt:DateTimeString format="102">20180412</qdt:DateTimeString>
                      </ram:FormattedIssueDateTime>
                    </ram:InvoiceReferencedDocument>
                    <ram:InvoiceReferencedDocument>
                      <ram:IssuerAssignedID>1</ram:IssuerAssignedID>
                      <ram:FormattedIssueDateTime>
                        <qdt:DateTimeString format="102">20180411</qdt:DateTimeString>
                      </ram:FormattedIssueDateTime>
                    </ram:InvoiceReferencedDocument>
                  </ram:ApplicableHeaderTradeSettlement>
                </rsm:SupplyChainTradeTransaction>
            </rsm:CrossIndustryInvoice>
            """;

        using StringReader reader = new(xml);

        Document document = new(reader);

        Assert.Equal(Schema.CiiD22b, document.Schema);
    }
}
