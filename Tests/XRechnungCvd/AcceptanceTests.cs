using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungCvd;

public class AcceptanceTests
{
    private static TestHarness _harness = new(S.XRechnungCvd.Instance);

    [Theory]
    [InlineData("Tests.Resources/XRechnungCvd/UblInvoice/Success")]
    [InlineData("Tests.Resources/XRechnungCvd/UblCreditNote/Success")]
    [InlineData("Tests.Resources/XRechnungCvd/Cii/Success")]
    public void Successes(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice<S.XRechnungCvd>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungCvd/UblInvoice/Failure")]
    [InlineData("Tests.Resources/XRechnungCvd/UblCreditNote/Failure")]
    [InlineData("Tests.Resources/XRechnungCvd/Cii/Failure")]
    public void Failures(string testsLocation)
    {
        _harness.AcceptFailure<Invoice<S.XRechnungCvd>>(testsLocation);
    }
}
