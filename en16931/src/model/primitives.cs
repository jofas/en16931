using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Im = Dev.Fassbender.En16931.Model.Immutable.Primitives;

namespace Dev.Fassbender.En16931.Model.Primitives;

public struct Amount : IToImmutable<Im.Amount>, IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public Amount(decimal v) => Value = v;

    public Im.Amount ToImmutable()
    {
        return new Im.Amount(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct Code : IToImmutable<Im.Code>, IXmlSerializable
{
    public required string Value { get; set; }

    [SetsRequiredMembers]
    public Code(string v) => Value = v;

    public Im.Code ToImmutable()
    {
        return new Im.Code(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsString();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct Date : IToImmutable<Im.Date>, IXmlSerializable
{
    public required DateTime Value { get; set; }

    [SetsRequiredMembers]
    public Date(DateTime v) => Value = v;

    public Im.Date ToImmutable()
    {
        return new Im.Date(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsDateTime();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct DocumentReference : IToImmutable<Im.DocumentReference>, IXmlSerializable
{
    public required string Value { get; set; }

    [SetsRequiredMembers]
    public DocumentReference(string v) => Value = v;

    public Im.DocumentReference ToImmutable()
    {
        return new Im.DocumentReference(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsString();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct Percentage : IToImmutable<Im.Percentage>, IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public Percentage(decimal v) => Value = v;

    public Im.Percentage ToImmutable()
    {
        return new Im.Percentage(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct Quantity : IToImmutable<Im.Quantity>, IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public Quantity(decimal v) => Value = v;

    public Im.Quantity ToImmutable()
    {
        return new Im.Quantity(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct Text : IToImmutable<Im.Text>, IXmlSerializable
{
    public required string Value { get; set; }

    [SetsRequiredMembers]
    public Text(string v) => Value = v;

    public Im.Text ToImmutable()
    {
        return new Im.Text(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsString();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct UnitPriceAmount : IToImmutable<Im.UnitPriceAmount>, IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public UnitPriceAmount(decimal v) => Value = v;

    public Im.UnitPriceAmount ToImmutable()
    {
        return new Im.UnitPriceAmount(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct BinaryObject : IToImmutable<Im.BinaryObject>
{
    [XmlElement(ElementName = "content")]
    public required byte[] Content { get; set; }

    [XmlElement(ElementName = "mime-code")]
    public required string MimeCode { get; set; }

    [XmlElement(ElementName = "filename")]
    public required string Filename { get; set; }

    [SetsRequiredMembers]
    public BinaryObject(byte[] content, string mimeCode, string filename)
    {
        Content = content;
        MimeCode = mimeCode;
        Filename = filename;
    }

    public Im.BinaryObject ToImmutable()
    {
        return new Im.BinaryObject(new Im.Array<byte>(Content), MimeCode, Filename);
    }
}

public struct Identifier : IToImmutable<Im.Identifier>
{
    [XmlElement(ElementName = "content")]
    public required string Content { get; set; }

    [XmlElement(ElementName = "scheme-identifier")]
    public string? SchemeIdentifier { get; set; }

    [XmlElement(ElementName = "scheme-version-identifier")]
    public string? SchemeVersionIdentifier { get; set; }

    [SetsRequiredMembers]
    public Identifier(string content)
    {
        Content = content;
    }

    [SetsRequiredMembers]
    public Identifier(string content, string? schemeIdentifier)
    {
        Content = content;
        SchemeIdentifier = schemeIdentifier;
    }

    [SetsRequiredMembers]
    public Identifier(string content, string? schemeIdentifier, string? schemeVersionIdentifier)
    {
        Content = content;
        SchemeIdentifier = schemeIdentifier;
        SchemeVersionIdentifier = schemeVersionIdentifier;
    }

    public Im.Identifier ToImmutable()
    {
        return new Im.Identifier(Content, SchemeIdentifier, SchemeVersionIdentifier);
    }
}

// TODO: To own module?

public interface IToImmutable<T>
{
    public T ToImmutable();
}

public static class ArrayToImmutableExt
{
    public static Im.Array<TResult> ToImmutable<T, TResult>(this IEnumerable<T> self)
    where T : IToImmutable<TResult>
    {
        return new Im.Array<TResult>(self.Select(x => x.ToImmutable()));
    }
}
