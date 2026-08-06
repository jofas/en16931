using En16931;
using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Tests.XRechnungExtension.Invoices;
using Xunit;

namespace Tests.XRechnungExtension;

public class RoundTripTests
{
    [Fact]
    public void RoundTrips()
    {
        // TODO: TestHarness.RoundTrip<UblInvoices, Invoice>(Schema.UblInvoice);
        TestHarness.RoundTrip<UblCreditNotes, Invoice>(Schema.UblCreditNote);
        TestHarness.RoundTrip<Ciis, Invoice>(Schema.CiiCrossIndustryInvoice);
    }
}
