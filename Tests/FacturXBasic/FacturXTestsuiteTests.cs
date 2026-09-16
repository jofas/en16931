using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.FacturXBasic;

public class FacturXTestsuiteTests
{
    private static TestHarness _harness = new(S.FacturXBasic.Instance);

    [Theory]
    [InlineData("Tests.Resources.Extern/FacturX/Basic")]
    public void ValidateFacturXTestsuite(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice<S.FacturXBasic>>(testsLocation);
    }
}
