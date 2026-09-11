using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class AcceptanceTests
{
    private static TestHarness _harness = new(S.XRechnung.Instance);

    [Theory]
    [InlineData("Tests.Resources/XRechnung/UblInvoice/Success")]
    [InlineData("Tests.Resources/XRechnung/UblCreditNote/Success")]
    [InlineData("Tests.Resources/XRechnung/CiiD16b/Success")]
    public void Successes(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice<S.XRechnung>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnung/UblInvoice/Failure")]
    [InlineData("Tests.Resources/XRechnung/UblCreditNote/Failure")]
    [InlineData("Tests.Resources/XRechnung/CiiD16b/Failure")]
    public void Failures(string testsLocation)
    {
        _harness.AcceptFailure<Invoice<S.XRechnung>>(testsLocation);
    }
}
