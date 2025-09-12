using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Dev.Fassbender.En16931.Model.Primitives;

public struct Amount : IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public Amount(decimal v) => Value = v;

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

public struct Code : IXmlSerializable
{
    public required string Value { get; set; }

    [SetsRequiredMembers]
    public Code(string v) => Value = v;

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

public struct Date : IXmlSerializable
{
    public required DateTime Value { get; set; }

    [SetsRequiredMembers]
    public Date(DateTime v) => Value = v;

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

public struct DocumentReference : IXmlSerializable
{
    public required string Value { get; set; }

    [SetsRequiredMembers]
    public DocumentReference(string v) => Value = v;

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

public struct Percentage : IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public Percentage(decimal v) => Value = v;

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

public struct Quantity : IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public Quantity(decimal v) => Value = v;

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

public struct Text : IXmlSerializable
{
    public required string Value { get; set; }

    [SetsRequiredMembers]
    public Text(string v) => Value = v;

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

public struct UnitPriceAmount : IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public UnitPriceAmount(decimal v) => Value = v;

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

public struct BinaryObject
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
}

public struct Identifier
{
    [XmlElement(ElementName = "content")]
    public required string Content { get; set; }

    [XmlElement(ElementName = "scheme-identifier")]
    public string? SchemeIdentifier { get; set; }

    [XmlElement(ElementName = "scheme-version-identifier")]
    public string? SchemeVersionIdentifier { get; set; }

    [SetsRequiredMembers]
    public Identifier(string content) => Content = content;

    [SetsRequiredMembers]
    public Identifier(string content, string schemeIdentifier) : this(content)
    {
        SchemeIdentifier = schemeIdentifier;
    }

    [SetsRequiredMembers]
    public Identifier(string content, string schemeIdentifier, string schemeVersionIdentifier) : this(content)
    {
        SchemeIdentifier = schemeIdentifier;
        SchemeVersionIdentifier = schemeVersionIdentifier;
    }
}
