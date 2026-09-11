using En16931;
using En16931.Model;
using Tests.Utils;
using Tests.XRechnungCvd.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungCvd;

public class UnitTests
{
    private static TestHarness _harness = new(S.XRechnungCvd.Instance);

    [Theory]
    [InlineData("Tests.Resources/XRechnungCvd/UblInvoice/Success")]
    public void UblInvoicesTest(string testsLocation)
    {
        _harness.UnitTest<UblInvoices, Invoice<S.XRechnungCvd>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungCvd/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        _harness.UnitTest<UblCreditNotes, Invoice<S.XRechnungCvd>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungCvd/CiiD16b/Success")]
    public void CiiD16bsTest(string testsLocation)
    {
        _harness.UnitTest<CiiD16bs, Invoice<S.XRechnungCvd>>(testsLocation);
    }
}
