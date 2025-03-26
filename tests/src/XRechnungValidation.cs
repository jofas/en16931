using dev.fassbender.en16931;

namespace tests;

public class XRechnungValidation
{
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
}
