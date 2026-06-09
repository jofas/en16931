using System;
using System.IO;
using System.Xml;
using En16931;
using En16931.Collections.Immutable;
using En16931.Model.Immutable;
using En16931.Model.Immutable.Primitives;
using Xunit;

namespace Tests.IR;

public class IRTests
{
    [Fact]
    public void Schema() {
        string example = """
            <invoice xmlns="urn:todo">
                <invoice-number id="bt-1">
                    <content>1234567</content>
                </invoice-number>
                <invoice-issue-date id="bt-2">2018-04-13</invoice-issue-date>
                <invoice-type-code id="bt-3">380</invoice-type-code>
                <invoice-currency-code id="bt-5">EUR</invoice-currency-code>
                <vat-accounting-currency-code id="bt-6">GBP</vat-accounting-currency-code>
                <value-added-tax-point-date id="bt-7">2018-04-13</value-added-tax-point-date>
                <payment-due-date id="bt-9">2018-04-13</payment-due-date>
                <buyer-reference id="bt-10">90000000-03083-72</buyer-reference>
                <project-reference id="bt-11">PR12345678</project-reference>
                <contract-reference id="bt-12">0000000752</contract-reference>
                <purchase-order-reference id="bt-13">65002278</purchase-order-reference>
                <sales-order-reference id="bt-14">ABC123456789</sales-order-reference>
                <receiving-advice-reference id="bt-15">RAR123456789</receiving-advice-reference>
                <despatch-advice-reference id="bt-16">DAR123456789</despatch-advice-reference>
                <tender-or-lot-reference id="bt-17">ANG987654321</tender-or-lot-reference>
                <invoiced-object-identifier id="bt-18">
                    <content>OK987654321</content>
                </invoiced-object-identifier>
                <buyer-accounting-reference id="bt-19">Buchungscode1</buyer-accounting-reference>
                <payment-terms id="bt-20">Beschreibung 1: Bitte überweisen Sie bis zum 13.04.2018 auf das unten aufgeführte Konto.</payment-terms>
                <invoice-notes id="bg-1">
                    <invoice-note id="bg-1">
                        <invoice-note-subject-code id="bt-21">AAC</invoice-note-subject-code>
                        <invoice-note id="bt-22">Invoice Note Description</invoice-note>
                    </invoice-note>
                    <invoice-note id="bg-1">
                        <invoice-note-subject-code id="bt-21">AAC</invoice-note-subject-code>
                        <invoice-note id="bt-22">Invoice Note Description 2</invoice-note>
                        <invoice-note-extension id="bt-166">Extended Field</invoice-note-extension>
                    </invoice-note>
                </invoice-notes>
                <invoice-extension id="bt-167">Extended Field</invoice-extension>
            </invoice>
        """;

        using StringReader reader = new(example);
        using XmlTextReader xmlReader = new(reader);

        XmlDocument doc = new() {
            Schemas = Parser.SchemaSet,
        };

        doc.Load(xmlReader);

        doc.Validate(null);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Cii/Success/1.xml")]
    public void Cii1IR(string invoiceLocation)
    {
        Parser parser = new Parser();

        XmlDocument doc = parser.ParseFileToIR(invoiceLocation);

        using StringWriter writer = new();
        using XmlTextWriter xmlWriter = new(writer) {
            Formatting = Formatting.Indented,
        };

        doc.Save(xmlWriter);

        Console.WriteLine(writer.ToString());

        doc.Schemas = Parser.SchemaSet;

        doc.Validate(null);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success/1.xml")]
    public void UblInvoice1(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice1, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/1.xml")]
    public void UblCreditNote1(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Invoice expected = Data.Invoice1 with
        {
            InvoiceTypeCode = new Code("381"),
        };

        Assert.Equal(expected, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Cii/Success/1.xml")]
    public void Cii1(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        // CII only supports a single preceding invoice reference
        Invoice expected = Data.Invoice1 with
        {
            PrecedingInvoiceReferences = new Array<PrecedingInvoiceReference>([Data.Invoice1.PrecedingInvoiceReferences[0]]),
        };

        Assert.Equal(expected, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success/2.xml")]
    public void UblInvoice2(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice2, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/2.xml")]
    public void UblCreditNote2(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Invoice expected = Data.Invoice2 with
        {
            InvoiceTypeCode = new Code("381"),
        };

        Assert.Equal(expected, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Cii/Success/2.xml")]
    public void Cii2(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        // BT-8 is mapped to different code lists in CII (UNTDID 2475) and UBL (UNTDID 2005)
        Invoice expected = Data.Invoice2 with
        {
            ValueAddedTaxPointDateCode = new Code("5"),
        };

        Assert.Equal(expected, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success/3.xml")]
    public void UblInvoice3(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/3.xml")]
    public void UblCreditNote3(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Invoice expected = Data.Invoice3 with
        {
            InvoiceTypeCode = new Code("381"),
        };

        Assert.Equal(expected, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Cii/Success/3.xml")]
    public void Cii3(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success/4.xml")]
    public void UblInvoice4(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice4, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/4.xml")]
    public void UblCreditNote4(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Invoice expected = Data.Invoice4 with
        {
            InvoiceTypeCode = new Code("381"),
        };

        Assert.Equal(expected, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Cii/Success/4.xml")]
    public void Cii4(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice4, invoice);
    }
}
