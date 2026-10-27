using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.FacturXEn16931;

public class FacturXTestsuiteTests
{
    private static TestHarness _harness = new(S.FacturXEn16931.Instance);

    [Theory]
    [InlineData("Tests.Resources.Extern/FacturX/En16931")]
    public void ValidateFacturXTestsuite(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice<S.FacturXEn16931>>(testsLocation);
    }
}
