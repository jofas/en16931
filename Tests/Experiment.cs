using System;
using System.IO;
using System.Xml.Schema;
using En16931;
using Xunit;
using S = En16931.Specs;

namespace Tests;

public class Experiment
{
    [Theory]
    [InlineData("Tests.Resources.Extern/Experiment")]
    public void MultipleBg18AndBg19InCii(string testsLocation)
    {
        Parser parser = Parser.Create(S.Core.Instance);

        string[] testFiles = Directory.GetFiles(testsLocation);

        foreach (string test in testFiles)
        {
            try {
                parser.Validate(test);
            } catch (ValidationException e) {
                Console.WriteLine($"{test}: {e.Errors}");
            }
        }

        Assert.True(false);
    }
}
