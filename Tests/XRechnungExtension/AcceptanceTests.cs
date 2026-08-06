using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model.XRechnungExtension;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungExtension;

public class AcceptanceTests
{
    [Theory]
    [InlineData("Resources/XRechnungExtension/UblInvoice/Failure")]
    [InlineData("Resources/XRechnungExtension/UblCreditNote/Failure")]
    [InlineData("Resources/XRechnungExtension/Cii/Failure")]
    public void Failures(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            ValidationException e = Assert.Throws<ValidationException>(() =>
            {
                parser.Parse<Invoice>(test);
            });

            Assert.Contains(Path.GetFileNameWithoutExtension(test), e.Errors);
        }
    }

    [Theory]
    [InlineData("Resources/XRechnungExtension/UblCreditNote/Success")]
    [InlineData("Resources/XRechnungExtension/Cii/Success")]
    public void Successes(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.Parse<Invoice>(test);
        }
    }
}
