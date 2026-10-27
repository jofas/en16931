using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.FacturXEn16931;

public class AcceptanceTests
{
    private static TestHarness _harness = new(S.FacturXEn16931.Instance);

    [Theory]
    [InlineData("Tests.Resources/FacturXEn16931/CiiD16b/Success")]
    [InlineData("Tests.Resources/FacturXEn16931/CiiD22b/Success")]
    public void Successes(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice<S.FacturXEn16931>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/FacturXEn16931/CiiD16b/Failure")]
    [InlineData("Tests.Resources/FacturXEn16931/CiiD22b/Failure")]
    public void Failures(string testsLocation)
    {
        _harness.AcceptFailure<Invoice<S.FacturXEn16931>>(testsLocation);
    }
}
