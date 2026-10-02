using System.IO;
using En16931;
using En16931.Model;
using En16931.Spec;
using Xunit;

namespace Tests.Utils;

public class TestHarness
{
    public readonly Parser Parser;

    public TestHarness(ISpecificationParser spec)
    {
        Parser = Parser.Create(spec);
    }

    public void AcceptSuccess<I>(string testsLocation) where I : IInvoice
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Parser.Parse<I>(test);
        }
    }

    public void AcceptFailure<I>(string testsLocation) where I : IInvoice
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            ValidationException e = Assert.Throws<ValidationException>(() =>
            {
                Parser.Parse<I>(test);
            });

            Assert.Contains(Path.GetFileNameWithoutExtension(test), e.Errors);
        }
    }

    public void UnitTest<P, I>(string testsLocation) where I : IInvoice
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            string invoiceName = Path.GetFileNameWithoutExtension(test);

            I expected = InvoiceExtractor<P, I>.Invoice(invoiceName);

            I invoice = Parser.Parse<I>(test);

            Assert.Equal(expected, invoice);
        }
    }

    public void RoundTrip<P, I>(Schema schema) where I : IInvoice
    {
        foreach (I invoice in InvoiceExtractor<P, I>.Invoices)
        {
            using StringWriter writer = new();

            Parser.Serialize(in invoice, schema, writer);

            using StringReader reader = new(writer.ToString());

            Assert.Equal(invoice, Parser.Parse<I>(reader));
        }
    }
}
