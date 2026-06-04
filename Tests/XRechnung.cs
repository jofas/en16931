using System;
using System.IO;
using System.Xml.Schema;
using Dev.Fassbender.En16931;
using Xunit;

namespace Tests;

public class XRechnung
{
    [Fact]
    public void CorrectUbl()
    {
        Parser parser = new Parser();
        parser.ParseFile("Resources/xrechnung-testsuite/standard/01.01a-INVOICE_ubl.xml");
    }

    [Fact]
    public void CorrectCii()
    {
        Parser parser = new Parser();
        parser.ParseFile("Resources/xrechnung-testsuite/standard/01.01a-INVOICE_uncefact.xml");
    }

    [Theory]
    [InlineData("Resources/schemas/ubl/invoice/failure")]
    public void SchemaViolationUblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XmlSchemaValidationException>(() =>
            {
                parser.ParseFile(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/schemas/cii/cross-industry-invoice/failure")]
    public void SchemaViolationCiiCrossIndustryInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XmlSchemaValidationException>(() =>
            {
                parser.ParseFile(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/schematrons/en16931/ubl/invoice/failure")]
    public void SchematronViolationEn16931UblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<En16931SchematronException>(() =>
            {
                parser.ParseFile(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/schematrons/en16931/cii/cross-industry-invoice/failure")]
    public void SchematronViolationEn16931CiiCrossIndustryInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<En16931SchematronException>(() =>
            {
                parser.ParseFile(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/invoice/success")]
    public void SuccessfulXRechnungCiusUblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.ParseFile(test);
        }
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/invoice/failure")]
    public void SchematronViolationXRechnungCiusUblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                parser.ParseFile(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/cius/ubl/credit-note/success")]
    public void SuccessfulXRechnungCiusUblCreditNote(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.ParseFile(test);
        }
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/cius/cii/cross-industry-invoice/failure")]
    public void SchematronViolationXRechnungCiusCiiCrossIndustryInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                parser.ParseFile(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/extension/ubl/invoice/failure")]
    public void SchematronViolationXRechnungExtensionUblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                parser.ParseFile(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/schematrons/xrechnung/extension/cii/cross-industry-invoice/failure")]
    public void SchematronViolationXRechnungExtensionCiiCrossIndustryInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                parser.ParseFile(test);
            });
        }
    }

    [Theory]
    [InlineData("Resources/xrechnung-testsuite/standard")]
    [InlineData("Resources/xrechnung-testsuite/extension")]
    [InlineData("Resources/xrechnung-testsuite/technical-cases")]
    public void ValidateXRechnungTestsuite(string testDirectory)
    {
        Parser parser = new Parser();

        string[] standardTests = Directory.GetFiles(testDirectory);

        foreach (string standardTest in standardTests)
        {
            parser.ParseFile(standardTest);
        }
    }
}
