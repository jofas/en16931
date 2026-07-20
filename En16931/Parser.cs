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

    public Parser(IEnumerable<ISpecificationParser> specs) {
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

        ISpecificationParser parser = _specs[doc.Specification];

        parser.Validate(in doc);

        return parser.Parse(in doc);
    }

    public Invoice<T> Parse<T>(string filepath) where T : ISpecification
    {
        using StreamReader reader = new(filepath);
        return Parse<T>(reader);
    }

    public Invoice<T> Parse<T>(TextReader reader) where T : ISpecification
    {
        using XmlTextReader xmlReader = new(reader);
        return Parse<T>(xmlReader);
    }

    public Invoice<T> Parse<T>(XmlReader reader) where T : ISpecification
    {
        Document doc = new(reader);

        ISpecificationParser parser = _specs[doc.Specification];
        ISpecificationParser<Invoice<T>, T> typedParser = (ISpecificationParser<Invoice<T>, T>)parser;

        parser.Validate(in doc);

        return typedParser.Parse(in doc);
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

        ISpecificationParser parser = _specs[doc.Specification];

        parser.Validate(in doc);
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
        ISpecificationParser parser = _specs[invoice.ProcessControl.SpecificationIdentifier];

        XDocument rawDoc = new();

        using (XmlWriter docWriter = rawDoc.CreateWriter())
        {
            parser.Serialize(invoice, schema, docWriter);
        }

        Document doc = new(rawDoc);

        parser.Validate(in doc);

        doc.WriteTo(writer);
    }

    public void Serialize<T>(ref readonly Invoice<T> invoice, Schema schema, string filepath) where T : ISpecification
    {
        using StreamWriter writer = new(filepath);
        Serialize(in invoice, schema, writer);
    }

    public void Serialize<T>(ref readonly Invoice<T> invoice, Schema schema, TextWriter writer) where T : ISpecification
    {
        using XmlTextWriter xmlWriter = new(writer);
        Serialize(in invoice, schema, xmlWriter);
    }

    public void Serialize<T>(ref readonly Invoice<T> invoice, Schema schema, XmlWriter writer) where T : ISpecification
    {
        ISpecificationParser parser = _specs[invoice.ProcessControl.SpecificationIdentifier];
        ISpecificationParser<Invoice<T>, T> typedParser = (ISpecificationParser<Invoice<T>, T>)parser;

        XDocument rawDoc = new();

        using (XmlWriter docWriter = rawDoc.CreateWriter())
        {
            typedParser.Serialize(in invoice, schema, docWriter);
        }

        Document doc = new(rawDoc);

        parser.Validate(in doc);

        doc.WriteTo(writer);
    }
}

public class SchematronException : Exception
{
    public required string[] Errors { get; init; }
}

public class En16931SchematronException : SchematronException { }

public class XRechnungSchematronException : SchematronException { }
