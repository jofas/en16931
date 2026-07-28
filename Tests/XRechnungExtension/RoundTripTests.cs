using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using En16931;
using En16931.Collections.Immutable;
using En16931.Model.Primitives;
using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Tests.XRechnungExtension.Invoices;
using Xunit;

namespace Tests.XRechnungExtension;

public class RoundTripTests
{
    /* TODO
    [Fact]
    public void UblInvoiceRoundTrip()
    {
        Array<Invoice> invoices = InvoiceExtractor.Invoices<Invoice<S.XRechnung>>(typeof(UblInvoices));
        RoundTrip(invoices, Schema.UblInvoice);
    }

    [Fact]
    public void UblCreditNoteRoundTrip()
    {
        Array<Invoice> invoices = InvoiceExtractor.Invoices<Invoice<S.XRechnung>>(typeof(UblCreditNotes));
        RoundTrip(invoices, Schema.UblCreditNote);
    }
    */

    [Fact]
    public void CiiRoundTrip()
    {
        Array<Invoice> invoices = InvoiceExtractor.Invoices<Invoice>(typeof(Ciis));
        RoundTrip(invoices, Schema.CiiCrossIndustryInvoice);
    }

    private void RoundTrip(Array<Invoice> invoices, Schema schema)
    {
        Parser parser = new Parser();

        foreach (Invoice invoice in invoices)
        {
            using StringWriter writer = new();

            parser.Serialize(in invoice, schema, writer);

            using StringReader reader = new(writer.ToString());

            Assert.Equal(invoice, parser.Parse<Invoice>(reader));
        }
    }
}
