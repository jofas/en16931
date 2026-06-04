using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using En16931.Utils;

namespace En16931.Collections.Immutable;

public readonly struct Array<T> : IEquatable<Array<T>> where T : struct
{
    public required ImmutableArray<T> Values
    {
        get
        {
            Assert.IsFalse(field.IsDefault, "Values can't be uninitialized");
            return field;
        }
        init
        {
            Assert.IsFalse(value.IsDefault, "Values can't be uninitialized");
            field = value;
        }
    }

    [SetsRequiredMembers]
    public Array(IEnumerable<T> v) => Values = ImmutableArray.CreateRange(v);

    public T this[int index] { get => Values[index]; }

    #region value-equality

    public override bool Equals(object? o) => o is Array<T> other && this.Equals(other);

    public override int GetHashCode() => Values.GetHashCode();

    public bool Equals(Array<T> other)
    {
        return Values.SequenceEqual(other.Values);
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

        if (Values.Length > 10)
        {
            string firstThree = string.Join(", ", Values[0..3]);
            string lastThree = string.Join(", ", Values[^3..^0]);
            items = $"{firstThree}, ..., {lastThree}";
        }
        else
        {
            items = string.Join(", ", Values);
        }

        return $"Array {{ Values = [{items}] }}";
    }
}

public readonly struct NonEmptyArray<T> : IEquatable<NonEmptyArray<T>> where T : struct
{
    public required ImmutableArray<T> Values
    {
        get
        {
            Assert.IsFalse(field.IsDefaultOrEmpty, "Values can't be uninitialized or empty");
            return field;
        }
        init
        {
            Assert.IsFalse(value.IsDefaultOrEmpty, "Values can't be uninitialized or empty");
            field = value;
        }
    }

    [SetsRequiredMembers]
    public NonEmptyArray(IEnumerable<T> v) => Values = ImmutableArray.CreateRange(v);

    public T this[int index] { get => Values[index]; }

    #region value-equality

    public override bool Equals(object? o) => o is NonEmptyArray<T> other && this.Equals(other);

    public override int GetHashCode() => Values.GetHashCode();

    public bool Equals(NonEmptyArray<T> other)
    {
        return Values.SequenceEqual(other.Values);
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

        if (Values.Length > 10)
        {
            string firstThree = string.Join(", ", Values[0..3]);
            string lastThree = string.Join(", ", Values[^3..^0]);
            items = $"{firstThree}, ..., {lastThree}";
        }
        else
        {
            items = string.Join(", ", Values);
        }

        return $"NonEmptyArray {{ Values = [{items}] }}";
    }
}
