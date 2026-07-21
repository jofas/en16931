using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class XRechnungTestsuiteTests
{
    [Theory]
    [InlineData("Resources/Extern/xrechnung-testsuite/standard")]
    [InlineData("Resources/Extern/xrechnung-testsuite/technical-cases")]
    public void ValidateXRechnungTestsuite(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.Parse<S.XRechnung>(test);
        }
    }
}
