using En16931;
using En16931.Model;
using Tests.FacturXEn16931.Invoices;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.FacturXEn16931;

public class RoundTripTests
{
    private static TestHarness _harness = new(S.FacturXEn16931.Instance);

    [Fact]
    public void RoundTrips()
    {
        _harness.RoundTrip<CiiD16bs, Invoice<S.FacturXEn16931>>(Schema.CiiD16b);
        _harness.RoundTrip<CiiD22bs, Invoice<S.FacturXEn16931>>(Schema.CiiD22b);
    }
}
