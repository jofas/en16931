using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Dev.Fassbender.En16931.Model.Immutable.Primitives;

public readonly record struct Amount(decimal Value);
public readonly record struct Percentage(decimal Value);
public readonly record struct Quantity(decimal Value);
public readonly record struct UnitPriceAmount(decimal Value);

public readonly record struct Date(DateTime Value);

public readonly record struct Code
{
    public required string Value
    {
        get;
        init
        {
            field = value ?? throw new ArgumentNullException(nameof(Value));
        }
    }

    [SetsRequiredMembers]
    public Code(string value)
    {
        Value = value;
    }
}

public readonly record struct DocumentReference
{
    public required string Value
    {
        get;
        init
        {
            field = value ?? throw new ArgumentNullException(nameof(Value));
        }
    }

    [SetsRequiredMembers]
    public DocumentReference(string value)
    {
        Value = value;
    }
}

public readonly record struct Text
{
    public required string Value
    {
        get;
        init
        {
            field = value ?? throw new ArgumentNullException(nameof(Value));
        }
    }

    [SetsRequiredMembers]
    public Text(string value)
    {
        Value = value;
    }
}

public readonly record struct BinaryObject
{
    public required Array<byte> Content { get; init; }

    public required string MimeCode
    {
        get;
        init
        {
            field = value ?? throw new ArgumentNullException(nameof(MimeCode));
        }
    }

    public required string Filename
    {
        get;
        init
        {
            field = value ?? throw new ArgumentNullException(nameof(Filename));
        }
    }

    [SetsRequiredMembers]
    public BinaryObject(Array<byte> content, string mimeCode, string filename)
    {
        Content = content;
        MimeCode = mimeCode;
        Filename = filename;
    }
}

public readonly record struct Identifier
{
    public required string Content
    {
        get;
        init
        {
            field = value ?? throw new ArgumentNullException(nameof(Content));
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
}

public readonly struct Array<T> : IEquatable<Array<T>> where T : struct
{
    public required ImmutableArray<T> Value { get; init; }

    [SetsRequiredMembers]
    public Array(IEnumerable<T> v) => Value = ImmutableArray.CreateRange(v);

    #region value-equality

    public override bool Equals(object? o) => o is Array<T> other && this.Equals(other);

    public override int GetHashCode() => Value.GetHashCode();

    public bool Equals(Array<T> other)
    {
        return Value.SequenceEqual(other.Value);
    }

    public static bool operator ==(Array<T> lhs, Array<T> rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Array<T> lhs, Array<T> rhs)
    {
        return !lhs.Equals(rhs);
    }

    #endregion

    public override String ToString()
    {
        string items = string.Join(", ", Value);
        return $"Array {{ Value = [{items}] }}";
    }
}
