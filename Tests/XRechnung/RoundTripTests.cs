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

public class RoundTripTests
{
    [Fact]
    public void UblInvoiceRoundTrip()
    {
        Parser parser = new Parser();

        List<Invoice<S.XRechnung>> invoices = [
            UblInvoices.Invoice1,
            UblInvoices.Invoice2,
            UblInvoices.Invoice3,
            UblInvoices.Invoice4,
        ];

        foreach (Invoice<S.XRechnung> invoice in invoices)
        {
            using StringWriter writer = new();

            parser.Serialize(in invoice, Schema.UblInvoice, writer);

            using StringReader reader = new(writer.ToString());

            Assert.Equal(invoice, parser.Parse<S.XRechnung>(reader));
        }
    }

    [Fact]
    public void UblCreditNoteRoundTrip()
    {
        Parser parser = new Parser();

        List<Invoice<S.XRechnung>> invoices = [
            UblInvoices.Invoice1 with
            {
                InvoiceTypeCode = new Code("381"),
            },
            UblInvoices.Invoice2 with
            {
                InvoiceTypeCode = new Code("381"),
            },
            UblInvoices.Invoice3 with
            {
                InvoiceTypeCode = new Code("381"),
            },
            UblInvoices.Invoice4 with
            {
                InvoiceTypeCode = new Code("381"),
            },
        ];

        foreach (Invoice<S.XRechnung> invoice in invoices)
        {
            using StringWriter writer = new();

            parser.Serialize(in invoice, Schema.UblCreditNote, writer);

            using StringReader reader = new(writer.ToString());

            Assert.Equal(invoice, parser.Parse<S.XRechnung>(reader));
        }
    }

    [Fact]
    public void CiiRoundTrip()
    {
        Parser parser = new Parser();

        List<Invoice<S.XRechnung>> invoices = [
            // CII only supports a single preceding invoice reference
            UblInvoices.Invoice1 with
            {
                PrecedingInvoiceReferences = [UblInvoices.Invoice1.PrecedingInvoiceReferences[0]],
            },
            UblInvoices.Invoice2,
            UblInvoices.Invoice3,
            UblInvoices.Invoice4,
        ];

        foreach (Invoice<S.XRechnung> invoice in invoices)
        {
            using StringWriter writer = new();

            parser.Serialize(in invoice, Schema.CiiCrossIndustryInvoice, writer);

            using StringReader reader = new(writer.ToString());

            Assert.Equal(invoice, parser.Parse<S.XRechnung>(reader));
        }
    }
}
