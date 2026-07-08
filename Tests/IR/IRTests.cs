using System;
using System.Collections.Generic;
using System.IO;
using En16931;
using En16931.Model;
using En16931.Model.Primitives;
using Xunit;

namespace Tests.IR;

public class IRTests
{
    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success/1.xml")]
    public void UblInvoice1(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.Parse(invoiceLocation);

        Assert.Equal(Data.Invoice1, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/1.xml")]
    public void UblCreditNote1(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.Parse(invoiceLocation);

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

        Invoice invoice = parser.Parse(invoiceLocation);

        // CII only supports a single preceding invoice reference
        Invoice expected = Data.Invoice1 with
        {
            PrecedingInvoiceReferences = [Data.Invoice1.PrecedingInvoiceReferences[0]],
        };

        Assert.Equal(expected, invoice);
    }


    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success/2.xml")]
    public void UblInvoice2(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.Parse(invoiceLocation);

        Assert.Equal(Data.Invoice2, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/2.xml")]
    public void UblCreditNote2(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.Parse(invoiceLocation);

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

        Invoice invoice = parser.Parse(invoiceLocation);

        Assert.Equal(Data.Invoice2, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success/3.xml")]
    public void UblInvoice3(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.Parse(invoiceLocation);

        Assert.Equal(Data.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/3.xml")]
    public void UblCreditNote3(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.Parse(invoiceLocation);

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

        Invoice invoice = parser.Parse(invoiceLocation);

        Assert.Equal(Data.Invoice3, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success/4.xml")]
    public void UblInvoice4(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.Parse(invoiceLocation);

        Assert.Equal(Data.Invoice4, invoice);
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success/4.xml")]
    public void UblCreditNote4(string invoiceLocation)
    {
        Parser parser = new Parser();

        Invoice invoice = parser.Parse(invoiceLocation);

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

        Invoice invoice = parser.Parse(invoiceLocation);

        Assert.Equal(Data.Invoice4, invoice);
    }

    [Fact]
    public void UblInvoiceRoundTrip() {
        Parser parser = new Parser();

        List<Invoice> invoices = [
            Data.Invoice1,
            Data.Invoice2,
            Data.Invoice3,
            Data.Invoice4,
        ];

        foreach (Invoice invoice in invoices)
        {
            using StringWriter writer = new();

            // TODO: remove once test works
            using System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(writer, new() {
                Indent = true,
            });

            parser.Serialize(in invoice, xmlWriter, Schema.UblInvoice);

            Console.WriteLine(writer.ToString());

            using StringReader reader = new(writer.ToString());

            Assert.Equal(invoice, parser.Parse(reader));
        }
    }

    [Fact]
    public void UblCreditNoteRoundTrip() {
        Parser parser = new Parser();

        List<Invoice> invoices = [
            Data.Invoice1 with
            {
                InvoiceTypeCode = new Code("381"),
            },
            Data.Invoice2 with
            {
                InvoiceTypeCode = new Code("381"),
            },
            Data.Invoice3 with
            {
                InvoiceTypeCode = new Code("381"),
            },
            Data.Invoice4 with
            {
                InvoiceTypeCode = new Code("381"),
            },
        ];

        foreach (Invoice invoice in invoices)
        {
            using StringWriter writer = new();

            // TODO: remove once test works
            using System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(writer, new() {
                Indent = true,
            });

            parser.Serialize(in invoice, xmlWriter, Schema.UblCreditNote);

            Console.WriteLine(writer.ToString());

            using StringReader reader = new(writer.ToString());

            Assert.Equal(invoice, parser.Parse(reader));
        }
    }

    [Fact]
    public void CiiRoundTrip()
    {
        Parser parser = new Parser();

        List<Invoice> invoices = [
            // CII only supports a single preceding invoice reference
            Data.Invoice1 with
            {
                PrecedingInvoiceReferences = [Data.Invoice1.PrecedingInvoiceReferences[0]],
            },
            Data.Invoice2,
            Data.Invoice3,
            Data.Invoice4,
        ];

        foreach (Invoice invoice in invoices)
        {
            using StringWriter writer = new();

            parser.Serialize(in invoice, writer, Schema.CiiCrossIndustryInvoice);

            using StringReader reader = new(writer.ToString());

            Assert.Equal(invoice, parser.Parse(reader));
        }
    }
}
