using System.Collections.Generic;
using System.Xml.Linq;
using En16931.Collections.Immutable;

namespace En16931.Spec.Utils;

public readonly record struct SchematronResult
{
    public required RefArray<string> Infos { get; init; }
    public required RefArray<string> Warnings { get; init; }
    public required RefArray<string> Errors { get; init; }
}

public static class Svrl
{
    private static XNamespace _svrl = "http://purl.oclc.org/dsdl/svrl";

    public static SchematronResult Validate(XDocument doc, Transformer transformer)
    {
        XDocument validationOutput = transformer.Transform(doc);

        XElement root = validationOutput.Root ?? throw new System.Exception("Could not find root node.");

        List<string> infos = [];
        List<string> warnings = [];
        List<string> errors = [];

        foreach (XElement element in root.Elements(_svrl + "failed-assert"))
        {
            List<string> list = element.Attribute("flag")?.Value switch
            {
                "information" => infos,
                "warning" => warnings,
                "fatal" => errors,
                string flag => throw new System.Exception($"Unknown flag {flag}"),
                null => throw new System.Exception("Flag attribute not found"),
            };

            string businessRule = element.Attribute("id")?.Value ?? throw new System.Exception("Id attribute not found");

            list.Add(businessRule);
        }

        return new SchematronResult
        {
            Infos = new(infos),
            Warnings = new(warnings),
            Errors = new(errors),
        };
    }
}
