using En16931;
using En16931.Model;
using Tests.Utils;
using Tests.XRechnungCvd.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungCvd;

public class RoundTripTests
{
    private static TestHarness _harness = new(S.XRechnungCvd.Instance);

    [Fact]
    public void RoundTrips()
    {
        _harness.RoundTrip<UblInvoices, Invoice<S.XRechnungCvd>>(Schema.UblInvoice);
        _harness.RoundTrip<UblCreditNotes, Invoice<S.XRechnungCvd>>(Schema.UblCreditNote);
        _harness.RoundTrip<CiiD16bs, Invoice<S.XRechnungCvd>>(Schema.CiiD16b);
    }
}
