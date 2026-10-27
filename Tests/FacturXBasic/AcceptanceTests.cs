using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.FacturXBasic;

public class AcceptanceTests
{
    private static TestHarness _harness = new(S.FacturXBasic.Instance);

    [Theory]
    [InlineData("Tests.Resources/FacturXBasic/CiiD16b/Success")]
    [InlineData("Tests.Resources/FacturXBasic/CiiD22b/Success")]
    public void Successes(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice<S.FacturXBasic>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/FacturXBasic/CiiD16b/Failure")]
    [InlineData("Tests.Resources/FacturXBasic/CiiD22b/Failure")]
    public void Failures(string testsLocation)
    {
        _harness.AcceptFailure<Invoice<S.FacturXBasic>>(testsLocation);
    }
}
