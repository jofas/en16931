using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Dev.Fassbender.En16931.Utils;

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
}

public readonly record struct DocumentReference
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
}

public readonly record struct Text
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
}

public readonly record struct BinaryObject
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
}

public readonly record struct Identifier
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
}

public readonly struct Array<T> : IEquatable<Array<T>> where T : struct
{
    public required ImmutableArray<T> Value
    {
        get
        {
            Assert.IsFalse(field.IsDefault, "Value can't be uninitialized");
            return field;
        }
        init
        {
            Assert.IsFalse(value.IsDefault, "Value can't be uninitialized");
            field = value;
        }
    }

    [SetsRequiredMembers]
    public Array(IEnumerable<T> v) => Value = ImmutableArray.CreateRange(v);

    public T this[int index] { get => Value[index]; }

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

    public override string ToString()
    {
        string items;

        if (Value.Length > 10) {
            string firstThree = string.Join(", ", Value[0..3]);
            string lastThree = string.Join(", ", Value[^3..^0]);
            items = $"{firstThree}, ..., {lastThree}";
        } else {
            items = string.Join(", ", Value);
        }

        return $"Array {{ Value = [{items}] }}";
    }
}

public readonly struct NonEmptyArray<T> : IEquatable<NonEmptyArray<T>> where T : struct
{
    public required ImmutableArray<T> Value
    {
        get
        {
            Assert.IsFalse(field.IsDefaultOrEmpty, "Value can't be uninitialized or empty");
            return field;
        }
        init
        {
            Assert.IsFalse(value.IsDefaultOrEmpty, "Value can't be uninitialized or empty");
            field = value;
        }
    }

    [SetsRequiredMembers]
    public NonEmptyArray(IEnumerable<T> v) => Value = ImmutableArray.CreateRange(v);

    public T this[int index] { get => Value[index]; }

    #region value-equality

    public override bool Equals(object? o) => o is NonEmptyArray<T> other && this.Equals(other);

    public override int GetHashCode() => Value.GetHashCode();

    public bool Equals(NonEmptyArray<T> other)
    {
        return Value.SequenceEqual(other.Value);
    }

    public static bool operator ==(NonEmptyArray<T> lhs, NonEmptyArray<T> rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(NonEmptyArray<T> lhs, NonEmptyArray<T> rhs)
    {
        return !lhs.Equals(rhs);
    }

    #endregion

    public override string ToString()
    {
        string items;

        if (Value.Length > 10) {
            string firstThree = string.Join(", ", Value[0..3]);
            string lastThree = string.Join(", ", Value[^3..^0]);
            items = $"{firstThree}, ..., {lastThree}";
        } else {
            items = string.Join(", ", Value);
        }

        return $"NonEmptyArray {{ Value = [{items}] }}";
    }
}
