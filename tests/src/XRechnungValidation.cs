namespace tests;

using System;
using System.IO;
using System.Xml.Schema;

using Xunit;

using dev.fassbender.en16931;

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
    public void TestIncorrectUbl(string testsLocation)
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
    public void TestIncorrectCii(string testsLocation)
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

    /*
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
    */
}
