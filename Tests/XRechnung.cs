using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using En16931.Specs;
using Xunit;

namespace Tests;

public class XRechnungTests
{
    [Theory]
    [InlineData("Resources/Ubl-Invoice/Failure")]
    public void SchemaViolationUblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XmlSchemaValidationException>(() =>
            {
                parser.Parse<XRechnung>(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/Cii/Failure")]
    public void SchemaViolationCii(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XmlSchemaValidationException>(() =>
            {
                parser.Parse<XRechnung>(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/En16931/Ubl-Invoice/Failure")]
    public void SchematronViolationEn16931UblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<En16931SchematronException>(() =>
            {
                parser.Parse<XRechnung>(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/En16931/Cii/Failure")]
    public void SchematronViolationEn16931Cii(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<En16931SchematronException>(() =>
            {
                parser.Parse<XRechnung>(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success")]
    public void SuccessfulXRechnungCiusUblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.Parse<XRechnung>(test);
        }
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Failure")]
    public void SchematronViolationXRechnungCiusUblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                parser.Parse<XRechnung>(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success")]
    public void SuccessfulXRechnungCiusUblCreditNote(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.Parse<XRechnung>(test);
        }
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Cii/Failure")]
    public void SchematronViolationXRechnungCiusCii(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                parser.Parse<XRechnung>(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/XRechnung-Extension/Ubl-Invoice/Failure")]
    public void SchematronViolationXRechnungExtensionUblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                parser.Parse<XRechnungExtension>(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/XRechnung-Extension/Cii/Failure")]
    public void SchematronViolationXRechnungExtensionCii(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                parser.Parse<XRechnungExtension>(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/Extern/xrechnung-testsuite/standard")]
    [InlineData("Resources/Extern/xrechnung-testsuite/extension")]
    [InlineData("Resources/Extern/xrechnung-testsuite/technical-cases")]
    public void ValidateXRechnungTestsuite(string testDirectory)
    {
        Parser parser = new Parser();

        string[] standardTests = Directory.GetFiles(testDirectory);

        foreach (string standardTest in standardTests)
        {
            IInvoice invoice = parser.Parse(standardTest);
            Assert.True(invoice is Invoice<XRechnung> || invoice is Invoice<XRechnungExtension>);
        }
    }
}
