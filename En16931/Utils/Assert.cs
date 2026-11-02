using System.Collections.Generic;
using System.Linq;

namespace En16931.Utils;

internal static class Assert
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

    public static void ArgContainsNoNullValues<T>(IEnumerable<T?> value, string message) where T : class
    {
        if (value.All(x => x is not null))
        {
            return;
        }

        ThrowHelper.ThrowArgumentException(message);
    }
}
