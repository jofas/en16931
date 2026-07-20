using System;
using System.Linq;

namespace En16931.Utils;

public static class Assert
{
    public static void IsTrue(bool value, string message)
    {
        if (value is true)
        {
            return;
        }

        ThrowHelper.ThrowArgumentException(message);
    }

    public static void IsFalse(bool value, string message)
    {
        if (value is false)
        {
            return;
        }

        ThrowHelper.ThrowArgumentException(message);
    }

    public static void IsNotNull<T>(T? value) where T : class
    {
        if (value is not null)
        {
            return;
        }

        ThrowHelper.ThrowNullReferenceException();
    }

    public static void ArgIsNotNull<T>(T? value) where T : class
    {
        if (value is not null)
        {
            return;
        }

        ThrowHelper.ThrowArgumentNullException();
    }

    public static void ArgIsNotEmpty<T>(T[] value, string message)
    {
        if (value.Length > 0)
        {
            return;
        }

        ThrowHelper.ThrowArgumentException(message);
    }

    public static void ArgContainsNoNullValues<T>(T?[] value, string message) where T : class
    {
        if (value.All(x => x is not null))
        {
            return;
        }

        ThrowHelper.ThrowArgumentException(message);
    }
}

public static class ThrowHelper
{
    public static void ThrowArgumentException(string message)
    {
        throw new ArgumentException(message);
    }

    public static void ThrowInvalidOperationException(string message)
    {
        throw new InvalidOperationException(message);
    }

    public static void ThrowArgumentNullException()
    {
        throw new ArgumentNullException();
    }

    public static void ThrowNullReferenceException()
    {
        throw new NullReferenceException();
    }

    public static void ThrowNotImplementedException()
    {
        throw new NotImplementedException();
    }
}
