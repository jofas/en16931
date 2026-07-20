using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.IR;
using En16931.Utils;

namespace En16931.Model.Primitives;

public readonly record struct Amount(decimal Value) : IIRDeserializable<Amount>, IIRSerializable
{
    public void Serialize(XmlWriter writer)
    {
        writer.WriteValue(Value);
    }

    public static Amount Deserialize(XmlReader reader)
    {
        return new Amount(reader.ReadContentAsDecimal());
    }
}

public readonly record struct Percentage(decimal Value) : IIRDeserializable<Percentage>, IIRSerializable
{
    public void Serialize(XmlWriter writer)
    {
        writer.WriteValue(Value);
    }

    public static Percentage Deserialize(XmlReader reader)
    {
        return new Percentage(reader.ReadContentAsDecimal());
    }
}

public readonly record struct Quantity(decimal Value) : IIRDeserializable<Quantity>, IIRSerializable
{
    public void Serialize(XmlWriter writer)
    {
        writer.WriteValue(Value);
    }

    public static Quantity Deserialize(XmlReader reader)
    {
        return new Quantity(reader.ReadContentAsDecimal());
    }
}

public readonly record struct UnitPriceAmount(decimal Value) : IIRDeserializable<UnitPriceAmount>, IIRSerializable
{
    public void Serialize(XmlWriter writer)
    {
        writer.WriteValue(Value);
    }

    public static UnitPriceAmount Deserialize(XmlReader reader)
    {
        return new UnitPriceAmount(reader.ReadContentAsDecimal());
    }
}

public readonly record struct Date(DateTime Value) : IIRDeserializable<Date>, IIRSerializable
{
    public void Serialize(XmlWriter writer)
    {
        writer.WriteString($"{Value.Year:D4}-{Value.Month:D2}-{Value.Day:D2}");
    }

    public static Date Deserialize(XmlReader reader)
    {
        return new Date(reader.ReadContentAsDateTime());
    }
}

public readonly record struct Code : IIRDeserializable<Code>, IIRSerializable
{
    public required string Value
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        init
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [SetsRequiredMembers]
    public Code(string value)
    {
        Value = value;
    }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteString(Value);
    }

    public static Code Deserialize(XmlReader reader)
    {
        return new Code(reader.ReadContentAsString());
    }
}

public readonly record struct DocumentReference : IIRDeserializable<DocumentReference>, IIRSerializable
{
    public required string Value
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        init
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [SetsRequiredMembers]
    public DocumentReference(string value)
    {
        Value = value;
    }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteString(Value);
    }

    public static DocumentReference Deserialize(XmlReader reader)
    {
        return new DocumentReference(reader.ReadContentAsString());
    }
}

public readonly record struct Text : IIRDeserializable<Text>, IIRSerializable
{
    public required string Value
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        init
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [SetsRequiredMembers]
    public Text(string value)
    {
        Value = value;
    }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteString(Value);
    }

    public static Text Deserialize(XmlReader reader)
    {
        return new Text(reader.ReadContentAsString());
    }
}

public readonly record struct BinaryObject : IIRDeserializable<BinaryObject>, IIRSerializable
{
    public required Array<byte> Content { get; init; }

    public required string MimeCode
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        init
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    public required string Filename
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        init
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    [SetsRequiredMembers]
    public BinaryObject(Array<byte> content, string mimeCode, string filename)
    {
        Content = content;
        MimeCode = mimeCode;
        Filename = filename;
    }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("content", IRConfig.NS);
        writer.WriteString(Convert.ToBase64String(Content.ToMutable()));
        writer.WriteEndElement();

        writer.WriteStartElement("mime-code", IRConfig.NS);
        writer.WriteString(MimeCode);
        writer.WriteEndElement();

        writer.WriteStartElement("filename", IRConfig.NS);
        writer.WriteString(Filename);
        writer.WriteEndElement();
    }

    public static BinaryObject Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("content", IRConfig.NS);
        reader.MoveToContent();

        Array<byte> content = new(Convert.FromBase64String(reader.ReadContentAsString()));

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("mime-code", IRConfig.NS);
        reader.MoveToContent();

        string mimeCode = reader.ReadContentAsString();

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("filename", IRConfig.NS);
        reader.MoveToContent();

        string filename = reader.ReadContentAsString();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new BinaryObject(content, mimeCode, filename);
    }
}

public readonly record struct Identifier : IIRDeserializable<Identifier>, IIRSerializable
{
    public required string Content
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
        init
        {
            Assert.ArgIsNotNull(value);
            field = value;
        }
    }

    public required string? SchemeIdentifier { get; init; }

    public required string? SchemeVersionIdentifier { get; init; }

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

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("content", IRConfig.NS);
        writer.WriteString(Content);
        writer.WriteEndElement();

        if (SchemeIdentifier is not null)
        {
            writer.WriteStartElement("scheme-identifier", IRConfig.NS);
            writer.WriteString(SchemeIdentifier);
            writer.WriteEndElement();
        }

        if (SchemeVersionIdentifier is not null)
        {
            writer.WriteStartElement("scheme-version-identifier", IRConfig.NS);
            writer.WriteString(SchemeVersionIdentifier);
            writer.WriteEndElement();
        }
    }

    public static Identifier Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("content", IRConfig.NS);
        reader.MoveToContent();

        string content = reader.ReadContentAsString();

        reader.ReadEndElement();
        reader.MoveToContent();

        string? schemeIdentifier = null;

        if (reader.IsStartElement("scheme-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            schemeIdentifier = reader.ReadContentAsString();

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        string? schemeVersionIdentifier = null;

        if (reader.IsStartElement("scheme-version-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            schemeVersionIdentifier = reader.ReadContentAsString();

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        return new Identifier(content, schemeIdentifier, schemeVersionIdentifier);
    }
}
