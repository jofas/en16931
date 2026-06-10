using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.IR;
using En16931.Model.Conversions;
using En16931.Utils;
using Mut = En16931.Model;

namespace En16931.Model.Immutable.Primitives;

public readonly record struct Amount(decimal Value) : IIRDeserializable<Amount>, IToMutable<Mut.Primitives.Amount>
{
    public static Amount Deserialize(XmlReader reader)
    {
        return new Amount(reader.ReadContentAsDecimal());
    }

    public Mut.Primitives.Amount ToMutable()
    {
        return new Mut.Primitives.Amount(Value);
    }
}

public readonly record struct Percentage(decimal Value) : IIRDeserializable<Percentage>, IToMutable<Mut.Primitives.Percentage>
{
    public static Percentage Deserialize(XmlReader reader)
    {
        return new Percentage(reader.ReadContentAsDecimal());
    }

    public Mut.Primitives.Percentage ToMutable()
    {
        return new Mut.Primitives.Percentage(Value);
    }
}

public readonly record struct Quantity(decimal Value) : IIRDeserializable<Quantity>, IToMutable<Mut.Primitives.Quantity>
{
    public static Quantity Deserialize(XmlReader reader)
    {
        return new Quantity(reader.ReadContentAsDecimal());
    }

    public Mut.Primitives.Quantity ToMutable()
    {
        return new Mut.Primitives.Quantity(Value);
    }
}

public readonly record struct UnitPriceAmount(decimal Value) : IIRDeserializable<UnitPriceAmount>, IToMutable<Mut.Primitives.UnitPriceAmount>
{
    public static UnitPriceAmount Deserialize(XmlReader reader)
    {
        return new UnitPriceAmount(reader.ReadContentAsDecimal());
    }

    public Mut.Primitives.UnitPriceAmount ToMutable()
    {
        return new Mut.Primitives.UnitPriceAmount(Value);
    }
}

public readonly record struct Date(DateTime Value) : IIRDeserializable<Date>, IToMutable<Mut.Primitives.Date>
{
    public static Date Deserialize(XmlReader reader)
    {
        return new Date(reader.ReadContentAsDateTime());
    }

    public Mut.Primitives.Date ToMutable()
    {
        return new Mut.Primitives.Date(Value);
    }
}

public readonly record struct Code : IIRDeserializable<Code>, IToMutable<Mut.Primitives.Code>
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

    public static Code Deserialize(XmlReader reader)
    {
        return new Code(reader.ReadContentAsString());
    }

    public Mut.Primitives.Code ToMutable()
    {
        return new Mut.Primitives.Code(Value);
    }
}

public readonly record struct DocumentReference : IIRDeserializable<DocumentReference>, IToMutable<Mut.Primitives.DocumentReference>
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

    public static DocumentReference Deserialize(XmlReader reader)
    {
        return new DocumentReference(reader.ReadContentAsString());
    }

    public Mut.Primitives.DocumentReference ToMutable()
    {
        return new Mut.Primitives.DocumentReference(Value);
    }
}

public readonly record struct Text : IIRDeserializable<Text>, IToMutable<Mut.Primitives.Text>
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

    public static Text Deserialize(XmlReader reader)
    {
        return new Text(reader.ReadContentAsString());
    }

    public Mut.Primitives.Text ToMutable()
    {
        return new Mut.Primitives.Text(Value);
    }
}

public readonly record struct BinaryObject : IIRDeserializable<BinaryObject>, IToMutable<Mut.Primitives.BinaryObject>
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

    public static BinaryObject Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("content", IRConfig.NS);
        reader.MoveToContent();

        const int size = 4096;

        var result = ImmutableArray.CreateBuilder<byte>();

        byte[] buffer = new byte[size];

        int bytesRead = -1;

        while (bytesRead != 0)
        {
            bytesRead = reader.ReadContentAsBase64(buffer, 0, size);
            result.AddRange(buffer, bytesRead);
        }

        Array<byte> content = new(result.ToImmutable());

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

    public Mut.Primitives.BinaryObject ToMutable()
    {
        return new Mut.Primitives.BinaryObject(Content.ToMutable<byte>(), MimeCode, Filename);
    }
}

public readonly record struct Identifier : IIRDeserializable<Identifier>, IToMutable<Mut.Primitives.Identifier>
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

    public Mut.Primitives.Identifier ToMutable()
    {
        return new Mut.Primitives.Identifier(Content, SchemeIdentifier, SchemeVersionIdentifier);
    }
}
