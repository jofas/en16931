using En16931;
using En16931.Model;
using Tests.Core.Invoices;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.Core;

public class UnitTests
{
    private static TestHarness _harness = new(S.Core.Instance);

    [Theory]
    [InlineData("Tests.Resources/Core/UblInvoice/Success")]
    public void UblInvoicesTest(string testsLocation)
    {
        _harness.UnitTest<UblInvoices, Invoice<S.Core>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/Core/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        _harness.UnitTest<UblCreditNotes, Invoice<S.Core>>(testsLocation);
    }

    [Theory]
    [InlineData("Tests.Resources/Core/CiiD16b/Success")]
    public void CiiD16bsTest(string testsLocation)
    {
        _harness.UnitTest<CiiD16bs, Invoice<S.Core>>(testsLocation);
    }
}
