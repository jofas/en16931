using System;
using System.Collections.Generic;
using System.IO;
using En16931;
using En16931.Model;
using En16931.Model.Primitives;
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
        TestInvoices(testsLocation, typeof(UblInvoices));
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblCreditNote/Success")]
    public void UblCreditNotesTest(string testsLocation)
    {
        TestInvoices(testsLocation, typeof(UblCreditNotes));
    }

    [Theory]
    [InlineData("Resources/XRechnung/Cii/Success")]
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

            Invoice<S.XRechnung> expected = InvoiceExtractor.Invoice<Invoice<S.XRechnung>>(invoiceCollection, invoiceName);

            Invoice<S.XRechnung> invoice = parser.Parse<S.XRechnung>(test);

            Assert.Equal(expected, invoice);
        }
    }
}
