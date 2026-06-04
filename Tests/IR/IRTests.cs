using System;
using Dev.Fassbender.En16931;
using Dev.Fassbender.En16931.Collections.Immutable;
using Dev.Fassbender.En16931.Model.Immutable;
using Dev.Fassbender.En16931.Model.Immutable.Primitives;
using Xunit;

namespace Tests.IR;

public class IRTests
{
    [Theory]
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/invoice/success/1.xml")]
    public void UblInvoice1(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice1, invoice);
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/credit-note/success/1.xml")]
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
    [InlineData("Resources/schematrons/xrechnung/cius/cii/cross-industry-invoice/success/1.xml")]
    public void CiiCrossIndustryInvoice1(string invoiceLocation)
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
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/invoice/success/2.xml")]
    public void UblInvoice2(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice2, invoice);
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/credit-note/success/2.xml")]
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
    [InlineData("Resources/schematrons/xrechnung/cius/cii/cross-industry-invoice/success/2.xml")]
    public void CiiCrossIndustryInvoice2(string invoiceLocation)
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
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/invoice/success/3.xml")]
    public void UblInvoice3(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/credit-note/success/3.xml")]
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
    [InlineData("Resources/schematrons/xrechnung/cius/cii/cross-industry-invoice/success/3.xml")]
    public void CiiCrossIndustryInvoice3(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/invoice/success/4.xml")]
    public void UblInvoice4(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice4, invoice);
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/credit-note/success/4.xml")]
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
    [InlineData("Resources/schematrons/xrechnung/cius/cii/cross-industry-invoice/success/4.xml")]
    public void CiiCrossIndustryInvoice4(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFile(invoiceLocation).ToImmutable();

        Assert.Equal(Data.Invoice4, invoice);
    }
}
