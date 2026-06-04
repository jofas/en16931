using System.Collections.Generic;
using System.Linq;

using En16931.Collections.Immutable;

namespace En16931.Model.Conversions;

public interface IToMutable<T>
{
    public T ToMutable();
}

public interface IToImmutable<T> where T : struct
{
    public T ToImmutable();
}

public static class ArrayToMutableExt
{
    public static TResult[] ToMutable<T, TResult>(this Array<T> self)
       where T : struct, IToMutable<TResult>
    {
        return self.Select(x => x.ToMutable()).ToArray();
    }

    public static T[] ToMutable<T>(this Array<T> self)
       where T : struct
    {
        return self.ToArray();
    }
}

public static class NonEmptyArrayToMutableExt
{
    public static TResult[] ToMutable<T, TResult>(this NonEmptyArray<T> self)
       where T : struct, IToMutable<TResult>
    {
        return self.Select(x => x.ToMutable()).ToArray();
    }

    public static T[] ToMutable<T>(this NonEmptyArray<T> self)
       where T : struct
    {
        return self.ToArray();
    }
}

public static class ToImmutableArrayExt
{
    public static Array<TResult> ToImmutable<T, TResult>(this IEnumerable<T> self)
    where T : IToImmutable<TResult> where TResult : struct
    {
        return new Array<TResult>(self.Select(x => x.ToImmutable()));
    }

    public static Array<T> ToImmutable<T>(this IEnumerable<T> self)
    where T : struct
    {
        return new Array<T>(self);
    }
}

public static class ToImmutableNonEmptyArrayExt
{
    public static NonEmptyArray<TResult> ToNonEmptyImmutable<T, TResult>(this IEnumerable<T> self)
    where T : IToImmutable<TResult> where TResult : struct
    {
        return new NonEmptyArray<TResult>(self.Select(x => x.ToImmutable()));
    }

    public static NonEmptyArray<T> ToNonEmptyImmutable<T>(this IEnumerable<T> self)
    where T : struct
    {
        return new NonEmptyArray<T>(self);
    }
}
