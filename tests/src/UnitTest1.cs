using dev.fassbender.en16931;

namespace tests;

public class UnitTest1
{
    [Theory]
    [InlineData("resources/invoices/standard")]
    [InlineData("resources/invoices/extension")]
    [InlineData("resources/invoices/technical-cases")]
    public void givenPathOfValidXRechnung_validationShouldSucceed(string testDirectory)
    {
        string[] standardTests = Directory.GetFiles(testDirectory);

        foreach (string standardTest in standardTests)
        {
            Class1.validateXRechnungFromFile(standardTest);
        }
    }

}
