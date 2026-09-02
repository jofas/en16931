using En16931;
using En16931.Model;
using Tests.Utils;
using Tests.XRechnung.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class UnitTests
{
    [Theory]
    [InlineData("Tests.Resources/XRechnung/UblInvoice/Success")]
    public void UblInvoicesTest(string testsLocation)
    {
        TestHarness.UnitTest<UblInvoices, Invoice<S.XRechnung>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnung/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        TestHarness.UnitTest<UblCreditNotes, Invoice<S.XRechnung>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnung/CiiD16b/Success")]
    public void CiiD16bsTest(string testsLocation)
    {
        TestHarness.UnitTest<CiiD16bs, Invoice<S.XRechnung>>(testsLocation);
    }
}
