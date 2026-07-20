using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using En16931.Utils;
using Saxon.Api;

namespace En16931.Spec.Utils;

public class TransformerSet<K> where K : notnull
{
    private readonly ImmutableDictionary<K, Transformer> _transformers;

    public TransformerSet(IDictionary<K, string> transformers)
    {
        _transformers = transformers
            .Select(kvp => KeyValuePair.Create(kvp.Key, new Transformer(kvp.Value)))
            .ToImmutableDictionary();
    }

    public Transformer this[K key]
    {
        get => _transformers[key];
    }
}

public class Transformer
{
    private readonly static DocumentBuilder _docBuilder;
    private readonly static XsltCompiler _xsltCompiler;

    static Transformer()
    {
        Processor processor = new(false);
        processor.ErrorWriter = TextWriter.Null;

        _docBuilder = processor.NewDocumentBuilder();
        _xsltCompiler = processor.NewXsltCompiler();
    }

    private readonly XsltExecutable _executable;

    public Transformer(string path)
    {
        Assert.ArgIsNotNull(path);

        _executable = _xsltCompiler.Compile(new Uri(new FileInfo(path).FullName));
    }

    public void Transform(XDocument doc, XmlWriter writer, string? initialMode = null)
    {
        XmlWriterDestination destination = new(writer);
        Transform(doc, destination, initialMode);
    }

    public XDocument Transform(XDocument doc, string? initialMode = null)
    {
        LinqDestination destination = new();
        Transform(doc, destination, initialMode);
        return destination.XDocument;
    }

    private void Transform(XDocument doc, IDestination destination, string? initialMode)
    {
        Xslt30Transformer transformer = _executable.Load30();

        transformer.InitialMode = initialMode is null ? null : new QName(initialMode);

        XdmNode xdm = _docBuilder.Build(doc);

        transformer.GlobalContextItem = xdm;

        transformer.ApplyTemplates(xdm, destination);
    }
}
