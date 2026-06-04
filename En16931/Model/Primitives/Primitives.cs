using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using En16931.Collections.Immutable;
using En16931.Model.Conversions;
using En16931.Utils;

using Im = En16931.Model.Immutable;

namespace En16931.Model.Primitives;

public struct Amount : IToImmutable<Im.Primitives.Amount>, IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public Amount(decimal v) => Value = v;

    public Im.Primitives.Amount ToImmutable()
    {
        return new Im.Primitives.Amount(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        ThrowHelper.ThrowNotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct Percentage : IToImmutable<Im.Primitives.Percentage>, IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public Percentage(decimal v) => Value = v;

    public Im.Primitives.Percentage ToImmutable()
    {
        return new Im.Primitives.Percentage(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        ThrowHelper.ThrowNotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct Quantity : IToImmutable<Im.Primitives.Quantity>, IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public Quantity(decimal v) => Value = v;

    public Im.Primitives.Quantity ToImmutable()
    {
        return new Im.Primitives.Quantity(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        ThrowHelper.ThrowNotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct UnitPriceAmount : IToImmutable<Im.Primitives.UnitPriceAmount>, IXmlSerializable
{
    public required decimal Value { get; set; }

    [SetsRequiredMembers]
    public UnitPriceAmount(decimal v) => Value = v;

    public Im.Primitives.UnitPriceAmount ToImmutable()
    {
        return new Im.Primitives.UnitPriceAmount(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        ThrowHelper.ThrowNotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct Date : IToImmutable<Im.Primitives.Date>, IXmlSerializable
{
    public required DateTime Value { get; set; }

    [SetsRequiredMembers]
    public Date(DateTime v) => Value = v;

    public Im.Primitives.Date ToImmutable()
    {
        return new Im.Primitives.Date(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsDateTime();
    }

    public void WriteXml(XmlWriter writer)
    {
        ThrowHelper.ThrowNotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct Code : IToImmutable<Im.Primitives.Code>, IXmlSerializable
{
    public required string Value
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        set
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [SetsRequiredMembers]
    public Code(string v) => Value = v;

    public Im.Primitives.Code ToImmutable()
    {
        return new Im.Primitives.Code(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsString();
    }

    public void WriteXml(XmlWriter writer)
    {
        ThrowHelper.ThrowNotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct DocumentReference : IToImmutable<Im.Primitives.DocumentReference>, IXmlSerializable
{
    public required string Value
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        set
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [SetsRequiredMembers]
    public DocumentReference(string v) => Value = v;

    public Im.Primitives.DocumentReference ToImmutable()
    {
        return new Im.Primitives.DocumentReference(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsString();
    }

    public void WriteXml(XmlWriter writer)
    {
        ThrowHelper.ThrowNotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct Text : IToImmutable<Im.Primitives.Text>, IXmlSerializable
{
    public required string Value
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        set
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [SetsRequiredMembers]
    public Text(string v) => Value = v;

    public Im.Primitives.Text ToImmutable()
    {
        return new Im.Primitives.Text(Value);
    }

    public void ReadXml(XmlReader reader)
    {
        Value = reader.ReadElementContentAsString();
    }

    public void WriteXml(XmlWriter writer)
    {
        ThrowHelper.ThrowNotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public struct BinaryObject : IToImmutable<Im.Primitives.BinaryObject>
{
    [XmlElement(ElementName = "content")]
    public required byte[] Content
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        set
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [XmlElement(ElementName = "mime-code")]
    public required string MimeCode
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        set
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [XmlElement(ElementName = "filename")]
    public required string Filename
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        set
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [SetsRequiredMembers]
    public BinaryObject(byte[] content, string mimeCode, string filename)
    {
        Content = content;
        MimeCode = mimeCode;
        Filename = filename;
    }

    public Im.Primitives.BinaryObject ToImmutable()
    {
        return new Im.Primitives.BinaryObject(new Array<byte>(Content), MimeCode, Filename);
    }
}

public struct Identifier : IToImmutable<Im.Primitives.Identifier>
{
    [XmlElement(ElementName = "content")]
    public required string Content
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        set
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [XmlElement(ElementName = "scheme-identifier")]
    public required string? SchemeIdentifier { get; set; }

    [XmlElement(ElementName = "scheme-version-identifier")]
    public required string? SchemeVersionIdentifier { get; set; }

    [SetsRequiredMembers]
    public Identifier(string content)
    {
        Content = content;
        SchemeIdentifier = null;
        SchemeVersionIdentifier = null;
    }

    [SetsRequiredMembers]
    public Identifier(string content, string? schemeIdentifier)
    {
        Content = content;
        SchemeIdentifier = schemeIdentifier;
        SchemeVersionIdentifier = null;
    }

    [SetsRequiredMembers]
    public Identifier(string content, string? schemeIdentifier, string? schemeVersionIdentifier)
    {
        Content = content;
        SchemeIdentifier = schemeIdentifier;
        SchemeVersionIdentifier = schemeVersionIdentifier;
    }

    public Im.Primitives.Identifier ToImmutable()
    {
        return new Im.Primitives.Identifier(Content, SchemeIdentifier, SchemeVersionIdentifier);
    }
}
