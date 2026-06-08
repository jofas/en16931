using System;
using En16931;
using En16931.Collections.Immutable;
using En16931.Model.Immutable;
using En16931.Model.Immutable.Primitives;
using Xunit;

namespace Tests.IR;

public class IRTests
{
    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success/1.xml")]
    public void UblInvoice1(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

        Assert.Equal(Data.Invoice1, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/1.xml")]
    public void UblCreditNote1(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

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

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

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

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

        Assert.Equal(Data.Invoice2, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/2.xml")]
    public void UblCreditNote2(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

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

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

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

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

        Assert.Equal(Data.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/3.xml")]
    public void UblCreditNote3(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

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

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

        Assert.Equal(Data.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success/4.xml")]
    public void UblInvoice4(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

        Assert.Equal(Data.Invoice4, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/4.xml")]
    public void UblCreditNote4(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

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

        Invoice invoice = parser.ParseFileToImmutable(invoiceLocation).Unwrap();

        Assert.Equal(Data.Invoice4, invoice);
    }
}
