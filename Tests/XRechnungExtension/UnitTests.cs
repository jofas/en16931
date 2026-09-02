using En16931;
using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Tests.XRechnungExtension.Invoices;
using Xunit;

namespace Tests.XRechnungExtension;

public class UnitTests
{
    [Theory]
    [InlineData("Tests.Resources/XRechnungExtension/UblInvoice/Success")]
    public void UblInvoicesTest(string testsLocation)
    {
        TestHarness.UnitTest<UblInvoices, Invoice>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungExtension/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        TestHarness.UnitTest<UblCreditNotes, Invoice>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungExtension/CiiD16b/Success")]
    public void CiiD16bsTest(string testsLocation)
    {
        TestHarness.UnitTest<CiiD16bs, Invoice>(testsLocation);
    }
}
