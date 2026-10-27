using En16931;
using En16931.Model;
using Tests.FacturXBasic.Invoices;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.FacturXBasic;

public class RoundTripTests
{
    private static TestHarness _harness = new(S.FacturXBasic.Instance);

    [Fact]
    public void RoundTrips()
    {
        _harness.RoundTrip<CiiD16bs, Invoice<S.FacturXBasic>>(Schema.CiiD16b);
        _harness.RoundTrip<CiiD22bs, Invoice<S.FacturXBasic>>(Schema.CiiD22b);
    }
}
