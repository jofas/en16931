using System.Collections.Generic;
using System.Linq;
using En16931.Collections.Immutable;
using En16931.Model;
using Tests.XRechnung.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.Utils;

public class InvoiceExtractorTests
{
    [Fact]
    public void Invoices()
    {
        Array<Invoice<S.XRechnung>> invoices = InvoiceExtractor.Invoices<Invoice<S.XRechnung>>(typeof(UblInvoices));

        List<Invoice<S.XRechnung>> expected = [
            UblInvoices.Invoice1,
            UblInvoices.Invoice2,
            UblInvoices.Invoice3,
            UblInvoices.Invoice4,
        ];

        // Doesn't break the test when new invoices are added
        Assert.True(expected.All(i => invoices.Contains(i)));
    }

    [Fact]
    public void Invoice()
    {
        Invoice<S.XRechnung> invoice = InvoiceExtractor.Invoice<Invoice<S.XRechnung>>(typeof(UblInvoices), "1");
        Assert.Equal(UblInvoices.Invoice1, invoice);
    }
}
