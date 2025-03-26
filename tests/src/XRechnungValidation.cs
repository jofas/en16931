using dev.fassbender.en16931;

namespace tests;

public class XRechnungValidation
{
    [Theory]
    [InlineData("resources/invoices/standard")]
    [InlineData("resources/invoices/extension")]
    [InlineData("resources/invoices/technical-cases")]
    public void ValidateXRechnungTestsuite(string testDirectory)
    {
        string[] standardTests = Directory.GetFiles(testDirectory);

        foreach (string standardTest in standardTests)
        {
            Validator.ValidateFromFile(standardTest);
        }
    }
}
