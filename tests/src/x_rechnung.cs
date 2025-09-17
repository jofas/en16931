using System;
using System.IO;
using System.Xml.Schema;
using Dev.Fassbender.En16931;
using Xunit;

namespace Tests;

public class XRechnungValidation
{
    [Fact]
    public void TestCorrectUbl()
    {
        Parser parser = new Parser();
        parser.ParseFile("resources/xrechnung-testsuite/standard/01.01a-INVOICE_ubl.xml");
    }

    /* TODO: uncomment once CII parsing is implemented
    [Fact]
    public void TestCorrectCii()
    {
        Parser parser = new Parser();
        parser.ParseFile("resources/xrechnung-testsuite/standard/01.01a-INVOICE_uncefact.xml");
    }
    */

    [Theory]
    [InlineData("resources/schemas/ubl/invoice/failure")]
    public void TestSchemaViolationUblInvoice(string testsLocation)
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
    [InlineData("resources/schemas/cii/cross-industry-invoice/failure")]
    public void TestSchemaViolationCiiCrossIndustryInvoice(string testsLocation)
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
    [InlineData("resources/schematrons/en16931/ubl/invoice/failure")]
    public void TestSchematronViolationEn16931UblInvoice(string testsLocation)
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
    [InlineData("resources/schematrons/en16931/cii/cross-industry-invoice/failure")]
    public void TestSchematronViolationEn16931CiiCrossIndustryInvoice(string testsLocation)
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
    [InlineData("resources/schematrons/xrechnung/cius/ubl/invoice/success")]
    public void TestSuccessfulXRechnungCiusUblInvoice(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.ParseFile(test);
        }
    }

    [Theory]
    [InlineData("resources/schematrons/xrechnung/cius/ubl/invoice/failure")]
    public void TestSchematronViolationXRechnungCiusUblInvoice(string testsLocation)
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
    [InlineData("resources/schematrons/xrechnung/cius/cii/cross-industry-invoice/failure")]
    public void TestSchematronViolationXRechnungCiusCiiCrossIndustryInvoice(string testsLocation)
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
    [InlineData("resources/schematrons/xrechnung/extension/ubl/invoice/failure")]
    public void TestSchematronViolationXRechnungExtensionUblInvoice(string testsLocation)
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
    [InlineData("resources/schematrons/xrechnung/extension/cii/cross-industry-invoice/failure")]
    public void TestSchematronViolationXRechnungExtensionCiiCrossIndustryInvoice(string testsLocation)
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

    /* TODO: uncomment once CII parsing is implemented
    [Theory]
    [InlineData("resources/xrechnung-testsuite/standard")]
    [InlineData("resources/xrechnung-testsuite/extension")]
    [InlineData("resources/xrechnung-testsuite/technical-cases")]
    public void ValidateXRechnungTestsuite(string testDirectory)
    {
        Parser parser = new Parser();

        string[] standardTests = Directory.GetFiles(testDirectory);

        foreach (string standardTest in standardTests)
        {
            parser.ParseFile(standardTest);
        }
    }
    */
}
