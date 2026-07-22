using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class AcceptanceTests
{
    [Theory]
    [InlineData("Resources/En16931/UblInvoice/Failure")]
    [InlineData("Resources/En16931/Cii/Failure")]
    public void SchematronViolationEn16931(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<En16931SchematronException>(() =>
            {
                parser.Parse<S.XRechnung>(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblInvoice/Failure")]
    [InlineData("Resources/XRechnung/Cii/Failure")]
    public void SchematronViolationXRechnung(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                parser.Parse<S.XRechnung>(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/XRechnung/UblInvoice/Success")]
    [InlineData("Resources/XRechnung/UblCreditNote/Success")]
    [InlineData("Resources/XRechnung/Cii/Success")]
    public void SuccessfulXRechnung(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.Parse<S.XRechnung>(test);
        }
    }
}
