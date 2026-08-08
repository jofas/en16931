using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungCvd;

public class AcceptanceTests
{
    [Theory]
    [InlineData("Tests.Resources/XRechnungCvd/UblInvoice/Success")]
    [InlineData("Tests.Resources/XRechnungCvd/UblCreditNote/Success")]
    [InlineData("Tests.Resources/XRechnungCvd/Cii/Success")]
    public void Successes(string testsLocation)
    {
        TestHarness.AcceptSuccess<Invoice<S.XRechnungCvd>>(testsLocation);
    }

    /* TODO
    [Theory]
    [InlineData("Tests.Resources/XRechnungCvd/UblInvoice/Failure")]
    [InlineData("Tests.Resources/XRechnungCvd/UblCreditNote/Failure")]
    [InlineData("Tests.Resources/XRechnungCvd/Cii/Failure")]
    public void Failures(string testsLocation)
    {
        TestHarness.AcceptFailure<Invoice<S.XRechnungCvd>>(testsLocation);
    }
    */
}
