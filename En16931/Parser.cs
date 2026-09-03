using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using En16931.Model;
using En16931.Model.Primitives;
using En16931.Spec;
using En16931.Specs;

namespace En16931;

public class Parser
{
    private readonly ImmutableDictionary<Identifier, ISpecificationParser> _specs;

    public Parser(IEnumerable<ISpecificationParser> specs)
    {
        _specs = specs
            .Select(s => KeyValuePair.Create(s.SpecificationIdentifier, s))
            .ToImmutableDictionary();
    }

    public Parser() : this(BuiltinSpecs.All) { }

    public IInvoice Parse(string filepath)
    {
        using StreamReader reader = new(filepath);
        return Parse(reader);
    }

    public IInvoice Parse(TextReader reader)
    {
        using XmlTextReader xmlReader = new(reader);
        return Parse(xmlReader);
    }

    public IInvoice Parse(XmlReader reader)
    {
        Document doc = new(reader);
        return Parse(in doc);
    }

    public IInvoice Parse(XDocument document)
    {
        Document doc = new(document);
        return Parse(in doc);
    }

    public IInvoice Parse(ref readonly Document document)
    {
        ISpecificationParser parser = _specs[document.Specification];
        parser.Validate(in document);
        return parser.Parse(in document);
    }

    public T Parse<T>(string filepath) where T : IInvoice
    {
        using StreamReader reader = new(filepath);
        return Parse<T>(reader);
    }

    public T Parse<T>(TextReader reader) where T : IInvoice
    {
        using XmlTextReader xmlReader = new(reader);
        return Parse<T>(xmlReader);
    }

    public T Parse<T>(XmlReader reader) where T : IInvoice
    {
        Document doc = new(reader);
        return Parse<T>(in doc);
    }

    public T Parse<T>(XDocument document) where T : IInvoice
    {
        Document doc = new(document);
        return Parse<T>(in doc);
    }

    public T Parse<T>(ref readonly Document document) where T : IInvoice
    {
        ISpecificationParser parser = _specs[document.Specification];
        ISpecificationParser<T> typedParser = (ISpecificationParser<T>)parser;

        parser.Validate(in document);

        return typedParser.Parse(in document);
    }

    public void Validate(string filepath)
    {
        using StreamReader reader = new(filepath);
        Validate(reader);
    }

    public void Validate(TextReader reader)
    {
        using XmlTextReader xmlReader = new(reader);
        Validate(xmlReader);
    }

    public void Validate(XmlReader reader)
    {
        Document doc = new(reader);
        Validate(in doc);
    }

    public void Validate(XDocument document)
    {
        Document doc = new Document(document);
        Validate(in doc);
    }

    public void Validate(ref readonly Document document)
    {
        ISpecificationParser parser = _specs[document.Specification];
        parser.Validate(in document);
    }

    public void Serialize(IInvoice invoice, Schema schema, string filepath)
    {
        using StreamWriter writer = new(filepath);
        Serialize(invoice, schema, writer);
    }

    public void Serialize(IInvoice invoice, Schema schema, TextWriter writer)
    {
        using XmlTextWriter xmlWriter = new(writer);
        Serialize(invoice, schema, xmlWriter);
    }

    public void Serialize(IInvoice invoice, Schema schema, XmlWriter writer)
    {
        Document doc = Serialize(invoice, schema);
        doc.WriteTo(writer);
    }

    public Document Serialize(IInvoice invoice, Schema schema)
    {
        ISpecificationParser parser = _specs[invoice.ProcessControl.SpecificationIdentifier];

        Document doc = parser.Serialize(invoice, schema);

        parser.Validate(in doc);

        return doc;
    }

    public void Serialize<T>(ref readonly T invoice, Schema schema, string filepath) where T : IInvoice
    {
        using StreamWriter writer = new(filepath);
        Serialize(in invoice, schema, writer);
    }

    public void Serialize<T>(ref readonly T invoice, Schema schema, TextWriter writer) where T : IInvoice
    {
        using XmlTextWriter xmlWriter = new(writer);
        Serialize(in invoice, schema, xmlWriter);
    }

    public void Serialize<T>(ref readonly T invoice, Schema schema, XmlWriter writer) where T : IInvoice
    {
        Document doc = Serialize(in invoice, schema);
        doc.WriteTo(writer);
    }

    public Document Serialize<T>(scoped ref readonly T invoice, Schema schema) where T : IInvoice
    {
        ISpecificationParser parser = _specs[invoice.ProcessControl.SpecificationIdentifier];
        ISpecificationParser<T> typedParser = (ISpecificationParser<T>)parser;

        Document doc = typedParser.Serialize(in invoice, schema);

        parser.Validate(in doc);

        return doc;
    }
}
