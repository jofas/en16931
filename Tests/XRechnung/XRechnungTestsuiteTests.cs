using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnung;

public class XRechnungTestsuiteTests
{
    private static TestHarness _harness = new(S.XRechnung.Instance);

    [Theory]
    [InlineData("Tests.Resources.Extern/xrechnung-testsuite/standard")]
    [InlineData("Tests.Resources.Extern/xrechnung-testsuite/technical-cases")]
    public void ValidateXRechnungTestsuite(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice<S.XRechnung>>(testsLocation);
    }
}
