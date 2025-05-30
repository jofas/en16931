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
        string path = "resources/xrechnung-testsuite/standard/01.01a-INVOICE_ubl.xml";
        Validator.ValidateFromFile(path);
    }

    [Fact]
    public void TestCorrectCii()
    {
        string path = "resources/xrechnung-testsuite/standard/01.01a-INVOICE_uncefact.xml";
        Validator.ValidateFromFile(path);
    }

    [Theory]
    [InlineData("resources/schemas/ubl/invoice/failure")]
    public void TestSchemaViolationUblInvoice(string testsLocation)
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XmlSchemaValidationException>(() =>
            {
                Validator.ValidateFromFile(test);
            });
        }
    }

    [Theory]
    [InlineData("resources/schemas/cii/cross-industry-invoice/failure")]
    public void TestSchemaViolationCiiCrossIndustryInvoice(string testsLocation)
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XmlSchemaValidationException>(() =>
            {
                Validator.ValidateFromFile(test);
            });
        }
    }

    [Theory]
    [InlineData("resources/schematrons/en16931/ubl/invoice/failure")]
    public void TestSchematronViolationEn16931UblInvoice(string testsLocation)
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<En16931SchematronException>(() =>
            {
                Validator.ValidateFromFile(test);
            });
        }
    }

    [Theory]
    [InlineData("resources/schematrons/en16931/cii/cross-industry-invoice/failure")]
    public void TestSchematronViolationEn16931CiiCrossIndustryInvoice(string testsLocation)
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<En16931SchematronException>(() =>
            {
                Validator.ValidateFromFile(test);
            });
        }
    }

    [Theory]
    [InlineData("resources/schematrons/xrechnung/cius/ubl/invoice/failure")]
    public void TestSchematronViolationXRechnungCiusUblInvoice(string testsLocation)
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                Validator.ValidateFromFile(test);
            });
        }
    }

    [Theory]
    [InlineData("resources/schematrons/xrechnung/cius/cii/cross-industry-invoice/failure")]
    public void TestSchematronViolationXRechnungCiusCiiCrossIndustryInvoice(string testsLocation)
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                Validator.ValidateFromFile(test);
            });
        }
    }

    [Theory]
    [InlineData("resources/schematrons/xrechnung/extension/ubl/invoice/failure")]
    public void TestSchematronViolationXRechnungExtensionUblInvoice(string testsLocation)
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                Validator.ValidateFromFile(test);
            });
        }
    }

    [Theory]
    [InlineData("resources/schematrons/xrechnung/extension/cii/cross-industry-invoice/failure")]
    public void TestSchematronViolationXRechnungExtensionCiiCrossIndustryInvoice(string testsLocation)
    {
        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                Validator.ValidateFromFile(test);
            });
        }
    }

    [Theory]
    [InlineData("resources/xrechnung-testsuite/standard")]
    [InlineData("resources/xrechnung-testsuite/extension")]
    [InlineData("resources/xrechnung-testsuite/technical-cases")]
    public void ValidateXRechnungTestsuite(string testDirectory)
    {
        string[] standardTests = Directory.GetFiles(testDirectory);

        foreach (string standardTest in standardTests)
        {
            Validator.ValidateFromFile(standardTest);
        }
    }
}
