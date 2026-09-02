using En16931;
using En16931.Model;
using Tests.Utils;
using Tests.XRechnungCvd.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungCvd;

public class RoundTripTests
{
    [Fact]
    public void RoundTrips()
    {
        TestHarness.RoundTrip<UblInvoices, Invoice<S.XRechnungCvd>>(Schema.UblInvoice);
        TestHarness.RoundTrip<UblCreditNotes, Invoice<S.XRechnungCvd>>(Schema.UblCreditNote);
        TestHarness.RoundTrip<CiiD16bs, Invoice<S.XRechnungCvd>>(Schema.CiiD16b);
    }
}
