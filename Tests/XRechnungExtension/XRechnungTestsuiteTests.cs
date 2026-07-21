using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungExtension;

public class XRechnungTestsuiteTests
{
    [Theory]
    [InlineData("Resources/Extern/xrechnung-testsuite/extension")]
    public void ValidateXRechnungTestsuite(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.Parse<S.XRechnungExtension>(test);
        }
    }
}
