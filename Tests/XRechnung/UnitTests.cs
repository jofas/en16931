using En16931;
using En16931.Model;
using Tests.Utils;
using Tests.XRechnung.Invoices;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class UnitTests
{
    private static TestHarness _harness = new(S.XRechnung.Instance);

    [Theory]
    [InlineData("Tests.Resources/XRechnung/UblInvoice/Success")]
    public void UblInvoicesTest(string testsLocation)
    {
        _harness.UnitTest<UblInvoices, Invoice<S.XRechnung>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnung/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        _harness.UnitTest<UblCreditNotes, Invoice<S.XRechnung>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/XRechnung/CiiD16b/Success")]
    public void CiiD16bsTest(string testsLocation)
    {
        _harness.UnitTest<CiiD16bs, Invoice<S.XRechnung>>(testsLocation);
    }
}
