using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model.XRechnungExtension;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungExtension;

public class XRechnungTestsuiteTests
{
    private static TestHarness _harness = new(S.XRechnungExtension.Instance);

    [Theory]
    [InlineData("Tests.Resources.Extern/xrechnung-testsuite/extension")]
    public void ValidateXRechnungTestsuite(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice>(testsLocation);
    }
}
