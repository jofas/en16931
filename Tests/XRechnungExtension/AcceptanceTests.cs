using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using En16931.Model;
using Xunit;
using S = En16931.Specs;

namespace Tests.XRechnungExtension;

public class AcceptanceTests
{
    // TODO: Remove specialized schematronexceptions
    // TODO: Move failing En16931 tests to XRechnung

    // TODO: * these fail because of bt-24 mismatch
    //       * The current exception model will get overhauled eventually
    //       * We should move failed tests to a model where we check
    //         the exception's error list against the file name
    //       * Then we can copy all the En16931 tests to the
    //         respective specifications, updating bt-24 in the process
    /*
    [Theory]
    [InlineData("Resources/En16931/Ubl-Invoice/Failure")]
    [InlineData("Resources/En16931/Cii/Failure")]
    public void SchematronViolationEn16931(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<En16931SchematronException>(() =>
            {
                parser.Parse<S.XRechnungExtension>(test);
            });
        }
    }
    */

    [Theory]
    [InlineData("Resources/XRechnungExtension/UblInvoice/Failure")]
    [InlineData("Resources/XRechnungExtension/Cii/Failure")]
    public void SchematronViolationXRechnungExtension(string testsLocation)
    {
        Parser parser = new Parser();

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            Assert.Throws<XRechnungSchematronException>(() =>
            {
                parser.Parse<S.XRechnungExtension>(test);
            });
        }
    }
}
