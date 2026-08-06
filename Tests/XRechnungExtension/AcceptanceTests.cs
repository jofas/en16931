using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Xunit;

namespace Tests.XRechnungExtension;

public class AcceptanceTests
{
    [Theory]
    [InlineData("Resources/XRechnungExtension/UblCreditNote/Success")]
    [InlineData("Resources/XRechnungExtension/Cii/Success")]
    public void Successes(string testsLocation)
    {
        TestHarness.AcceptSuccess<Invoice>(testsLocation);
    }

    [Theory]
    [InlineData("Resources/XRechnungExtension/UblInvoice/Failure")]
    [InlineData("Resources/XRechnungExtension/UblCreditNote/Failure")]
    [InlineData("Resources/XRechnungExtension/Cii/Failure")]
    public void Failures(string testsLocation)
    {
        TestHarness.AcceptFailure<Invoice>(testsLocation);
    }
}
