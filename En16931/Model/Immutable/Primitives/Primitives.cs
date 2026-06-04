using System;
using System.Diagnostics.CodeAnalysis;

using En16931.Collections.Immutable;
using En16931.Model.Conversions;
using En16931.Utils;

using Mut = En16931.Model;

namespace En16931.Model.Immutable.Primitives;

public readonly record struct Amount(decimal Value) : IToMutable<Mut.Primitives.Amount>
{
    public Mut.Primitives.Amount ToMutable()
    {
        return new Mut.Primitives.Amount(Value);
    }
}

public readonly record struct Percentage(decimal Value) : IToMutable<Mut.Primitives.Percentage>
{
    public Mut.Primitives.Percentage ToMutable()
    {
        return new Mut.Primitives.Percentage(Value);
    }
}

public readonly record struct Quantity(decimal Value) : IToMutable<Mut.Primitives.Quantity>
{
    public Mut.Primitives.Quantity ToMutable()
    {
        return new Mut.Primitives.Quantity(Value);
    }
}

public readonly record struct UnitPriceAmount(decimal Value) : IToMutable<Mut.Primitives.UnitPriceAmount>
{
    public Mut.Primitives.UnitPriceAmount ToMutable()
    {
        return new Mut.Primitives.UnitPriceAmount(Value);
    }
}

public readonly record struct Date(DateTime Value) : IToMutable<Mut.Primitives.Date>
{
    public Mut.Primitives.Date ToMutable()
    {
        return new Mut.Primitives.Date(Value);
    }
}

public readonly record struct Code : IToMutable<Mut.Primitives.Code>
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

    public Mut.Primitives.Code ToMutable()
    {
        return new Mut.Primitives.Code(Value);
    }
}

public readonly record struct DocumentReference : IToMutable<Mut.Primitives.DocumentReference>
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

    public Mut.Primitives.DocumentReference ToMutable()
    {
        return new Mut.Primitives.DocumentReference(Value);
    }
}

public readonly record struct Text : IToMutable<Mut.Primitives.Text>
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

    public Mut.Primitives.Text ToMutable()
    {
        return new Mut.Primitives.Text(Value);
    }
}

public readonly record struct BinaryObject : IToMutable<Mut.Primitives.BinaryObject>
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

    public Mut.Primitives.BinaryObject ToMutable()
    {
        return new Mut.Primitives.BinaryObject(Content.ToMutable<byte>(), MimeCode, Filename);
    }
}

public readonly record struct Identifier : IToMutable<Mut.Primitives.Identifier>
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

    public Mut.Primitives.Identifier ToMutable()
    {
        return new Mut.Primitives.Identifier(Content, SchemeIdentifier, SchemeVersionIdentifier);
    }
}
