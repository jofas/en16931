using En16931;
using En16931.Model;
using Tests.Core.Invoices;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.Core;

public class RoundTripTests
{
    private static TestHarness _harness = new(S.Core.Instance);

    [Fact]
    public void RoundTrips()
    {
        _harness.RoundTrip<UblInvoices, Invoice<S.Core>>(Schema.UblInvoice);
        _harness.RoundTrip<UblCreditNotes, Invoice<S.Core>>(Schema.UblCreditNote);
        _harness.RoundTrip<CiiD16bs, Invoice<S.Core>>(Schema.CiiD16b);
    }
}
