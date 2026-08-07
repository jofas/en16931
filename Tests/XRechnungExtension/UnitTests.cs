using En16931;
using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Tests.XRechnungExtension.Invoices;
using Xunit;

namespace Tests.XRechnungExtension;

public class UnitTests
{
    [Theory]
    [InlineData("Resources/XRechnungExtension/UblInvoice/Success")]
    public void UblInvoicesTest(string testsLocation)
    {
        TestHarness.UnitTest<UblInvoices, Invoice>(testsLocation);
    }

    [Theory]
    [InlineData("Resources/XRechnungExtension/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        TestHarness.UnitTest<UblCreditNotes, Invoice>(testsLocation);
    }

    [Theory]
    [InlineData("Resources/XRechnungExtension/Cii/Success")]
    public void CiisTest(string testsLocation)
    {
        TestHarness.UnitTest<Ciis, Invoice>(testsLocation);
    }
}
