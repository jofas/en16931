using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class AcceptanceTests
{
    [Theory]
    [InlineData("Tests.Resources/XRechnung/UblInvoice/Success")]
    [InlineData("Tests.Resources/XRechnung/UblCreditNote/Success")]
    [InlineData("Tests.Resources/XRechnung/Cii/Success")]
    public void Successes(string testsLocation)
    {
        TestHarness.AcceptSuccess<Invoice<S.XRechnung>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnung/UblInvoice/Failure")]
    [InlineData("Tests.Resources/XRechnung/UblCreditNote/Failure")]
    [InlineData("Tests.Resources/XRechnung/Cii/Failure")]
    public void Failures(string testsLocation)
    {
        TestHarness.AcceptFailure<Invoice<S.XRechnung>>(testsLocation);
    }
}
