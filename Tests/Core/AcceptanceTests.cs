using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.Core;

public class AcceptanceTests
{
    private static TestHarness _harness = new(S.Core.Instance);

    [Theory]
    [InlineData("Tests.Resources/Core/UblInvoice/Success")]
    [InlineData("Tests.Resources/Core/UblCreditNote/Success")]
    [InlineData("Tests.Resources/Core/CiiD16b/Success")]
    public void Successes(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice<S.Core>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/Core/UblInvoice/Failure")]
    [InlineData("Tests.Resources/Core/UblCreditNote/Failure")]
    [InlineData("Tests.Resources/Core/CiiD16b/Failure")]
    public void Failures(string testsLocation)
    {
        _harness.AcceptFailure<Invoice<S.Core>>(testsLocation);
    }
}
