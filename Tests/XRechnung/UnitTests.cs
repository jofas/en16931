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
    [InlineData("Resources/XRechnung/UblInvoice/Success")]
    public void UblInvoicesTest(string testsLocation)
    {
        TestHarness.UnitTest<UblInvoices, Invoice<S.XRechnung>>(testsLocation);
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        TestHarness.UnitTest<UblCreditNotes, Invoice<S.XRechnung>>(testsLocation);
    }

    [Theory]
    [InlineData("Resources/XRechnung/Cii/Success")]
    public void CiisTest(string testsLocation)
    {
        TestHarness.UnitTest<Ciis, Invoice<S.XRechnung>>(testsLocation);
    }
}
