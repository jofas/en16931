namespace dev.fassbender.en16931;

using System;

using Amount = decimal;
using Code = string;
using Date = System.DateTime;
using DocumentReference = string;
using Percentage = decimal;
using Quantity = decimal;
using Text = string;
using UnitPriceAmount = decimal;

public class BinaryObject
{
    public required byte[] Content { get; init; }
    public required string MimeCode { get; init; }

    public BinaryObject(byte[] content, string mimeCode)
    {
        Content = content;
        MimeCode = mimeCode;
    }
}

public class Identifier
{
    public required string Content { get; init; }
    public string? SchemeIdentifier { get; init; }
    public string? SchemeVersionIdentifier { get; init; }

    public Identifier(string content, string? schemeIdentifier, string? schemeVersionIdentifier)
    {
        Content = content;
        SchemeIdentifier = SchemeIdentifier;
        SchemeVersionIdentifier = schemeVersionIdentifier;
    }
}

