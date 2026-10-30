using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using Tests.Utils;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungCvd;

public class XRechnungTestsuiteTests
{
    private static TestHarness _harness = new(S.XRechnungCvd.Instance);

    [Theory]
    [InlineData("Tests.Resources.Extern/XRechnung/technical-cases/cvd")]
    public void ValidateXRechnungTestsuite(string testsLocation)
    {
        _harness.AcceptSuccess<Invoice<S.XRechnungCvd>>(testsLocation);
    }
}
