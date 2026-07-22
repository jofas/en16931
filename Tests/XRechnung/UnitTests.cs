using System;
using System.Collections.Generic;
using System.IO;
using En16931;
using En16931.Model;
using En16931.Model.Primitives;
using Tests.XRechnung.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class UnitTests
{
    [Theory]
    [InlineData("Resources/XRechnung/UblInvoice/Success/1.xml")]
    public void UblInvoice1(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(UblInvoices.Invoice1, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblCreditNote/Success/1.xml")]
    public void UblCreditNote1(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(UblCreditNotes.Invoice1, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung/Cii/Success/1.xml")]
    public void Cii1(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(Ciis.Invoice1, invoice);
    }


    [Theory]
    [InlineData("Resources/XRechnung/UblInvoice/Success/2.xml")]
    public void UblInvoice2(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(UblInvoices.Invoice2, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblCreditNote/Success/2.xml")]
    public void UblCreditNote2(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(UblCreditNotes.Invoice2, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung/Cii/Success/2.xml")]
    public void Cii2(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(Ciis.Invoice2, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblInvoice/Success/3.xml")]
    public void UblInvoice3(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(UblInvoices.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblCreditNote/Success/3.xml")]
    public void UblCreditNote3(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(UblCreditNotes.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung/Cii/Success/3.xml")]
    public void Cii3(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(Ciis.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblInvoice/Success/4.xml")]
    public void UblInvoice4(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(UblInvoices.Invoice4, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblCreditNote/Success/4.xml")]
    public void UblCreditNote4(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(UblCreditNotes.Invoice4, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung/Cii/Success/4.xml")]
    public void Cii4(string invoiceLocation)
    {
        Parser parser = new Parser();
        Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(invoiceLocation);
        Assert.Equal(Ciis.Invoice4, invoice);
    }
}
