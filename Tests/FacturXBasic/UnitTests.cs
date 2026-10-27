using En16931;
using En16931.Model;
using Tests.FacturXBasic.Invoices;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.FacturXBasic;

public class UnitTests
{
    private static TestHarness _harness = new(S.FacturXBasic.Instance);

    [Theory]
    [InlineData("Tests.Resources/FacturXBasic/CiiD16b/Success")]
    public void CiiD16bsTest(string testsLocation)
    {
        _harness.UnitTest<CiiD16bs, Invoice<S.FacturXBasic>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/FacturXBasic/CiiD22b/Success")]
    public void CiiD22bsTest(string testsLocation)
    {
        _harness.UnitTest<CiiD22bs, Invoice<S.FacturXBasic>>(testsLocation);
    }
}
