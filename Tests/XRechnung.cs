using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model.Immutable;
using Xunit;

namespace Tests;

public class XRechnung
{
    [Theory]
    [InlineData("Resources/Ubl-Invoice/Failure")]
    [InlineData("Resources/Cii/Failure")]
    public void SchemaViolations(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Result<Invoice> result = parser.ParseFileToImmutable(test);

            Assert.True(result.HasErrors());
            Assert.True(result.Report.SchemaViolation is not null);
        }
    }

    [Theory]
    [InlineData("Resources/En16931/Ubl-Invoice/Failure")]
    [InlineData("Resources/En16931/Cii/Failure")]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Failure")]
    [InlineData("Resources/XRechnung-Cius/Cii/Failure")]
    [InlineData("Resources/XRechnung-Extension/Ubl-Invoice/Failure")]
    [InlineData("Resources/XRechnung-Extension/Cii/Failure")]
    public void SchematronViolations(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Result<Invoice> result = parser.ParseFileToImmutable(test);

            Assert.True(result.HasErrors());
            Assert.True(result.Report.Errors.Count > 0);
        }
    }

    [Theory]
    [InlineData("Resources/XRechnung-Cius/Ubl-Invoice/Success")]
    [InlineData("Resources/XRechnung-Cius/Ubl-Credit-Note/Success")]
    public void NoViolations(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Result<Invoice> result = parser.ParseFileToImmutable(test);

            Assert.True(result.IsOk());
            Assert.True(result.Value is not null);
        }
    }

    [Theory]
    [InlineData("Resources/Extern/xrechnung-testsuite/standard")]
    [InlineData("Resources/Extern/xrechnung-testsuite/extension")]
    [InlineData("Resources/Extern/xrechnung-testsuite/technical-cases")]
    public void ValidateXRechnungTestsuite(string testDirectory)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testDirectory);

        foreach (string test in testFiles)
        {
            Result<Invoice> result = parser.ParseFileToImmutable(test);

            Assert.True(result.IsOk());
            Assert.True(result.Value is not null);
        }
    }
}
