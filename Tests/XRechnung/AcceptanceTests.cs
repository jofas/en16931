using System;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class AcceptanceTests
{
    [Theory]
    [InlineData("Resources/XRechnung/UblInvoice/Failure")]
    [InlineData("Resources/XRechnung/Cii/Failure")]
    public void Failures(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            ValidationException e = Assert.Throws<ValidationException>(() =>
            {
                parser.Parse<S.XRechnung>(test);
            });

            Assert.Contains(Path.GetFileNameWithoutExtension(test), e.Errors);
        }
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblInvoice/Success")]
    [InlineData("Resources/XRechnung/UblCreditNote/Success")]
    [InlineData("Resources/XRechnung/Cii/Success")]
    public void Successes(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.Parse<S.XRechnung>(test);
        }
    }
}
