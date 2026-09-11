using En16931;
using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Tests.XRechnungExtension.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungExtension;

public class RoundTripTests
{
    private static TestHarness _harness = new(S.XRechnungExtension.Instance);

    [Fact]
    public void RoundTrips()
    {
        _harness.RoundTrip<UblInvoices, Invoice>(Schema.UblInvoice);
        _harness.RoundTrip<UblCreditNotes, Invoice>(Schema.UblCreditNote);
        _harness.RoundTrip<CiiD16bs, Invoice>(Schema.CiiD16b);
    }
}
