using En16931;
using En16931.Model;
using Tests.Utils;
using Tests.XRechnung.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class RoundTripTests
{
    [Fact]
    public void RoundTrips()
    {
        TestHarness.RoundTrip<UblInvoices, Invoice<S.XRechnung>>(Schema.UblInvoice);
        TestHarness.RoundTrip<UblCreditNotes, Invoice<S.XRechnung>>(Schema.UblCreditNote);
        TestHarness.RoundTrip<CiiD16bs, Invoice<S.XRechnung>>(Schema.CiiD16b);
    }
}
