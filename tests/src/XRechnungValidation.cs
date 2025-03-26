using dev.fassbender.en16931;

namespace tests;

public class XRechnungValidation
{
    [Fact]
    public void Test()
    {
        string pathUbl = "resources/xrechnung-testsuite/standard/01.01a-INVOICE_ubl.xml";
        Validator.ValidateFromFile(pathUbl);

        string pathCii = "resources/xrechnung-testsuite/standard/01.01a-INVOICE_uncefact.xml";
        Validator.ValidateFromFile(pathCii);
    }
    /*
    [Theory]
    [InlineData("resources/xrechnung-testsuite/standard")]
    [InlineData("resources/xrechnung-testsuite/extension")]
    [InlineData("resources/xrechnung-testsuite/technical-cases")]
    public void ValidateXRechnungTestsuite(string testDirectory)
    {
        string[] standardTests = Directory.GetFiles(testDirectory);

        foreach (string standardTest in standardTests)
        {
            Validator.ValidateFromFile(standardTest);
        }
    }
    */
}
