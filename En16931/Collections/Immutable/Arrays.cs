using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using En16931.Utils;

namespace En16931.Collections.Immutable;

public readonly struct Array<T> : IEquatable<Array<T>>, IEnumerable<T> where T : struct
{
    private ImmutableArray<T> _values
    {
        get
        {
            Assert.IsFalse(field.IsDefault, "Array can't be uninitialized");
            return field;
        }
        init
        {
            Assert.IsFalse(value.IsDefault, "Array can't be uninitialized");
            field = value;
        }
    }

    [SetsRequiredMembers]
    public Array(ImmutableArray<T> v) => _values = v;

    [SetsRequiredMembers]
    public Array(IEnumerable<T> v) => _values = ImmutableArray.CreateRange(v);

    public int Length => _values.Length;

    public T this[int index] { get => _values[index]; }

    public IEnumerator<T> GetEnumerator()
    {
        return (_values as IEnumerable<T>).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Array<T> Slice(int start, int length)
    {
        return new(_values.Slice(start, length));
    }

    #region value-equality

    public override bool Equals(object? o) => o is Array<T> other && this.Equals(other);

    public override int GetHashCode() => _values.GetHashCode();

    public bool Equals(Array<T> other)
    {
        return this.SequenceEqual(other);
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

        if (Length > 10)
        {
            string firstThree = string.Join(", ", this[0..3]);
            string lastThree = string.Join(", ", this[^3..^0]);
            items = $"{firstThree}, ..., {lastThree}";
        }
        else
        {
            items = string.Join(", ", this);
        }

        return $"[{items}]";
    }
}

public readonly struct NonEmptyArray<T> : IEquatable<NonEmptyArray<T>>, IEnumerable<T> where T : struct
{
    private ImmutableArray<T> _values
    {
        get
        {
            Assert.IsFalse(field.IsDefaultOrEmpty, "NonEmptyArray can't be uninitialized or empty");
            return field;
        }
        init
        {
            Assert.IsFalse(value.IsDefaultOrEmpty, "NonEmptyArray can't be uninitialized or empty");
            field = value;
        }
    }

    [SetsRequiredMembers]
    public NonEmptyArray(ImmutableArray<T> v) => _values = v;

    [SetsRequiredMembers]
    public NonEmptyArray(IEnumerable<T> v) => _values = ImmutableArray.CreateRange(v);

    public int Length => _values.Length;

    public T this[int index] { get => _values[index]; }

    public IEnumerator<T> GetEnumerator()
    {
        return (_values as IEnumerable<T>).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public NonEmptyArray<T> Slice(int start, int length)
    {
        return new(_values.Slice(start, length));
    }

    #region value-equality

    public override bool Equals(object? o) => o is NonEmptyArray<T> other && this.Equals(other);

    public override int GetHashCode() => _values.GetHashCode();

    public bool Equals(NonEmptyArray<T> other)
    {
        return this.SequenceEqual(other);
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

        if (Length > 10)
        {
            string firstThree = string.Join(", ", this[0..3]);
            string lastThree = string.Join(", ", this[^3..^0]);
            items = $"{firstThree}, ..., {lastThree}";
        }
        else
        {
            items = string.Join(", ", this);
        }

        return $"[{items}]";
    }
}
