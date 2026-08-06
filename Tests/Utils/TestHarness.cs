using System.IO;
using En16931;
using En16931.Model;
using Xunit;

namespace Tests.Utils;

public static class TestHarness {
    private static Parser parser = new Parser();

    public static void AcceptSuccess<I>(string testsLocation) where I: IInvoice {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.Parse<I>(test);
        }
    }

    public static void AcceptFailure<I>(string testsLocation) where I: IInvoice {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            ValidationException e = Assert.Throws<ValidationException>(() =>
            {
                parser.Parse<I>(test);
            });

            Assert.Contains(Path.GetFileNameWithoutExtension(test), e.Errors);
        }
    }

    public static void UnitTest<P, I>(string testsLocation) where I: IInvoice {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            string invoiceName = Path.GetFileNameWithoutExtension(test);

            I expected = InvoiceExtractor<P, I>.Invoice(invoiceName);

            I invoice = parser.Parse<I>(test);

            Assert.Equal(expected, invoice);
        }
    }

    public static void RoundTrip<P, I>(Schema schema) where I: IInvoice
    {
        foreach (I invoice in InvoiceExtractor<P, I>.Invoices)
        {
            using StringWriter writer = new();

            parser.Serialize(in invoice, schema, writer);

            using StringReader reader = new(writer.ToString());

            Assert.Equal(invoice, parser.Parse<I>(reader));
        }
    }
}
