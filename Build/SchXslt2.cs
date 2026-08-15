using System;
using System.IO;
using System.Xml;
using Saxon.Api;
using static Build.Constants;

namespace Build;

public static class SchXslt2
{
    private static readonly DocumentBuilder _docBuilder;
    private static readonly XsltExecutable _executable;

    static SchXslt2()
    {
        Processor processor = new(false);

        XsltCompiler xsltCompiler = processor.NewXsltCompiler();

        _executable = xsltCompiler.Compile(new Uri(new FileInfo(SchXslt2Location).FullName));

        _docBuilder = processor.NewDocumentBuilder();
    }

    public static void Compile(string pathIn, string pathOut)
    {
        Xslt30Transformer transformer = _executable.Load30();

        XdmNode doc = _docBuilder.Build(new Uri(new FileInfo(pathIn).FullName));

        using XmlWriter writer = XmlWriter.Create(pathOut, new() { Indent = true });

        XmlWriterDestination destination = new(writer);

        transformer.GlobalContextItem = doc;

        transformer.ApplyTemplates(doc, destination);
    }
}
