using System;
using System.Text;
using En16931.Collections.Immutable;

namespace Tests.IR;

public readonly record struct Blob
{
    public readonly string Content;
    public readonly Array<byte> RawContent;
    public readonly string Base64Content;

    public Blob(string content)
    {
        Content = content;

        byte[] raw = Encoding.UTF8.GetBytes(content);

        RawContent = [.. raw];
        Base64Content = Convert.ToBase64String(raw);
    }
}
