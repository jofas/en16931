using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using Xunit;
using S = En16931.Specs;

namespace Tests.FacturXBasic;

public class FacturXTestsuiteTests
{
    [Theory]
    [InlineData("Tests.Resources.Extern/FacturX/Basic")]
    public void ValidateFacturXTestsuite(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            parser.Parse<Invoice<S.FacturXBasic>>(test);
        }
    }
}
