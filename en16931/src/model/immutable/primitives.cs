using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Dev.Fassbender.En16931.Model.Immutable.Primitives;

public readonly record struct Amount(decimal Value);
public readonly record struct Code(string Value);
public readonly record struct Date(DateTime Value);
public readonly record struct DocumentReference(string Value);
public readonly record struct Percentage(decimal Value);
public readonly record struct Quantity(decimal Value);
public readonly record struct Text(string Value);
public readonly record struct UnitPriceAmount(decimal Value);

public record struct BinaryObject(Array<byte> Content, string MimeCode, string Filename);

public record struct Identifier
{
    public required string Content { get; init; }

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
    public Identifier(string content, string schemeIdentifier)
    {
        Content = content;
        SchemeIdentifier = schemeIdentifier;
        SchemeVersionIdentifier = null;
    }

    [SetsRequiredMembers]
    public Identifier(string content, string schemeIdentifier, string schemeVersionIdentifier)
    {
        Content = content;
        SchemeIdentifier = schemeIdentifier;
        SchemeVersionIdentifier = schemeVersionIdentifier;
    }
}

public struct Array<T> : IEquatable<Array<T>>
{
    public required ImmutableArray<T> Value { get; init; }

    [SetsRequiredMembers]
    public Array(IEnumerable<T> v) => Value = ImmutableArray.CreateRange(v);

    #region value-equality

    public override bool Equals(object? o) => o is Array<T> other && this.Equals(other);

    public bool Equals(Array<T> other)
    {
        return Value.SequenceEqual(other.Value);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Array<T> lhs, Array<T> rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Array<T> lhs, Array<T> rhs)
    {
        return !lhs.Equals(rhs);
    }

    #endregion
}
