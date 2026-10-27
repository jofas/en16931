using En16931;
using En16931.Model;
using Tests.FacturXEn16931.Invoices;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.FacturXEn16931;

public class UnitTests
{
    private static TestHarness _harness = new(S.FacturXEn16931.Instance);

    [Theory]
    [InlineData("Tests.Resources/FacturXEn16931/CiiD16b/Success")]
    public void CiiD16bsTest(string testsLocation)
    {
        _harness.UnitTest<CiiD16bs, Invoice<S.FacturXEn16931>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/FacturXEn16931/CiiD22b/Success")]
    public void CiiD22bsTest(string testsLocation)
    {
        _harness.UnitTest<CiiD22bs, Invoice<S.FacturXEn16931>>(testsLocation);
    }
}
