using En16931;
using En16931.Model;
using Tests.Utils;
using Tests.XRechnung.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class RoundTripTests
{
    private static TestHarness _harness = new(S.XRechnung.Instance);

    [Fact]
    public void RoundTrips()
    {
        _harness.RoundTrip<UblInvoices, Invoice<S.XRechnung>>(Schema.UblInvoice);
        _harness.RoundTrip<UblCreditNotes, Invoice<S.XRechnung>>(Schema.UblCreditNote);
        _harness.RoundTrip<CiiD16bs, Invoice<S.XRechnung>>(Schema.CiiD16b);
    }
}
