using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using En16931;
using En16931.Collections.Immutable;
using En16931.Model;
using En16931.Model.Primitives;
using Tests.Utils;
using Tests.XRechnung.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class RoundTripTests
{
    [Fact]
    public void UblInvoiceRoundTrip()
    {
        Array<Invoice<S.XRechnung>> invoices = InvoiceExtractor.Invoices<Invoice<S.XRechnung>>(typeof(UblInvoices));
        RoundTrip(invoices, Schema.UblInvoice);
    }

    [Fact]
    public void UblCreditNoteRoundTrip()
    {
        Array<Invoice<S.XRechnung>> invoices = InvoiceExtractor.Invoices<Invoice<S.XRechnung>>(typeof(UblCreditNotes));
        RoundTrip(invoices, Schema.UblCreditNote);
    }

    [Fact]
    public void CiiRoundTrip()
    {
        Array<Invoice<S.XRechnung>> invoices = InvoiceExtractor.Invoices<Invoice<S.XRechnung>>(typeof(Ciis));
        RoundTrip(invoices, Schema.CiiCrossIndustryInvoice);
    }

    private void RoundTrip(Array<Invoice<S.XRechnung>> invoices, Schema schema)
    {
        Parser parser = new Parser();

        foreach (Invoice<S.XRechnung> invoice in invoices)
        {
            using StringWriter writer = new();

            parser.Serialize(in invoice, schema, writer);

            using StringReader reader = new(writer.ToString());

            Assert.Equal(invoice, parser.Parse<S.XRechnung>(reader));
        }
    }
}
