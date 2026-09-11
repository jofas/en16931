using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungExtension;

public class AcceptanceTests
{
    private static TestHarness _harness = new(S.XRechnungExtension.Instance);

    [Theory]
    [InlineData("Tests.Resources/XRechnungExtension/UblInvoice/Success")]
    [InlineData("Tests.Resources/XRechnungExtension/UblCreditNote/Success")]
    [InlineData("Tests.Resources/XRechnungExtension/CiiD16b/Success")]
    public void Successes(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungExtension/UblInvoice/Failure")]
    [InlineData("Tests.Resources/XRechnungExtension/UblCreditNote/Failure")]
    [InlineData("Tests.Resources/XRechnungExtension/CiiD16b/Failure")]
    public void Failures(string testsLocation)
    {
        _harness.AcceptFailure<Invoice>(testsLocation);
    }
}
