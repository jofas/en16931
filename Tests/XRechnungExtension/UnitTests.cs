using System;
using System.Collections.Generic;
using System.IO;
using En16931;
using En16931.Model.Primitives;
using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Tests.XRechnungExtension.Invoices;
using Xunit;

namespace Tests.XRechnungExtension;

public class UnitTests
{
    /* TODO
    [Theory]
    [InlineData("Resources/XRechnungExtension/UblInvoice/Success")]
    public void UblInvoicesTest(string testsLocation)
    {
        TestInvoices(testsLocation, typeof(UblInvoices));
    }
    */

    [Theory]
    [InlineData("Resources/XRechnungExtension/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        TestInvoices(testsLocation, typeof(UblCreditNotes));
    }

    [Theory]
    [InlineData("Resources/XRechnungExtension/Cii/Success")]
    public void CiisTest(string testsLocation)
    {
        TestInvoices(testsLocation, typeof(Ciis));
    }

    private void TestInvoices(string testsLocation, Type invoiceCollection)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            string invoiceName = Path.GetFileNameWithoutExtension(test);

            Invoice expected = InvoiceExtractor.Invoice<Invoice>(invoiceCollection, invoiceName);

            Invoice invoice = parser.Parse<Invoice>(test);

            Assert.Equal(expected, invoice);
        }
    }
}
