using En16931;
using En16931.Model;
using Tests.Utils;
using Tests.XRechnungCvd.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungCvd;

public class UnitTests
{
    [Theory]
    [InlineData("Tests.Resources/XRechnungCvd/UblInvoice/Success")]
    public void UblInvoicesTest(string testsLocation)
    {
        TestHarness.UnitTest<UblInvoices, Invoice<S.XRechnungCvd>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungCvd/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        TestHarness.UnitTest<UblCreditNotes, Invoice<S.XRechnungCvd>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnungCvd/CiiD16b/Success")]
    public void CiiD16bsTest(string testsLocation)
    {
        TestHarness.UnitTest<CiiD16bs, Invoice<S.XRechnungCvd>>(testsLocation);
    }
}
