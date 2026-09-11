using En16931;
using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Tests.XRechnungExtension.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungExtension;

public class UnitTests
{
    private static TestHarness _harness = new(S.XRechnungExtension.Instance);

    [Theory]
    [InlineData("Tests.Resources/XRechnungExtension/UblInvoice/Success")]
    public void UblInvoicesTest(string testsLocation)
    {
        _harness.UnitTest<UblInvoices, Invoice>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungExtension/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        _harness.UnitTest<UblCreditNotes, Invoice>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungExtension/CiiD16b/Success")]
    public void CiiD16bsTest(string testsLocation)
    {
        _harness.UnitTest<CiiD16bs, Invoice>(testsLocation);
    }
}
