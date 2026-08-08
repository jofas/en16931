using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Xunit;

namespace Tests.XRechnungExtension;

public class AcceptanceTests
{
    [Theory]
    [InlineData("Tests.Resources/XRechnungExtension/UblInvoice/Success")]
    [InlineData("Tests.Resources/XRechnungExtension/UblCreditNote/Success")]
    [InlineData("Tests.Resources/XRechnungExtension/Cii/Success")]
    public void Successes(string testsLocation)
    {
        TestHarness.AcceptSuccess<Invoice>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungExtension/UblInvoice/Failure")]
    [InlineData("Tests.Resources/XRechnungExtension/UblCreditNote/Failure")]
    [InlineData("Tests.Resources/XRechnungExtension/Cii/Failure")]
    public void Failures(string testsLocation)
    {
        TestHarness.AcceptFailure<Invoice>(testsLocation);
    }
}
