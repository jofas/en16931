using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class AcceptanceTests
{
    [Theory]
    [InlineData("Resources/XRechnung/UblInvoice/Success")]
    [InlineData("Resources/XRechnung/UblCreditNote/Success")]
    [InlineData("Resources/XRechnung/Cii/Success")]
    public void Successes(string testsLocation)
    {
        TestHarness.AcceptSuccess<Invoice<S.XRechnung>>(testsLocation);
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblInvoice/Failure")]
    [InlineData("Resources/XRechnung/UblCreditNote/Failure")]
    [InlineData("Resources/XRechnung/Cii/Failure")]
    public void Failures(string testsLocation)
    {
        TestHarness.AcceptFailure<Invoice<S.XRechnung>>(testsLocation);
    }
}
