using System;

namespace En16931.Utils;

internal static class ThrowHelper
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
