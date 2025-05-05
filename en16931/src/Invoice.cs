namespace dev.fassbender.en16931;

using System;

using Amount = decimal;
// TODO: how to represent code lists?
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

public class Invoice
{
    // BT-1
    public required Identifier Number { get; init; }

    // BT-2
    public required Date IssueDate { get; init; }

    // BT-3
    // UNTDID 1001
    public required Code TypeCode { get; init; }

    // BT-5
    // ISO 4217 - Codes for the representation of currencies and funds - Alpha-3 representation
    public required Code CurrencyCode { get; init; }

    // BT-6
    // ISO 4217 - Codes for the representation of currencies and funds - Alpha-3 representation
    public Code? VatAccountingCurrencyCode { get; init; }

    // TODO: BT-7 and BT-8 are mutually exclusive
    // BT-7
    public Date? VatPointDate { get; init; }

    // BT-8
    // UNTDID 2005
    public Code? VatPointDateCode { get; init; }

    // BT-9
    public Date? PaymentDueDate { get; init; }

    // BT-10
    public required Text BuyerReference { get; init; }

    // BT-11
    public DocumentReference? ProjectReference { get; init; }

    // BT-12
    public DocumentReference? ContractReference { get; init; }

    // BT-13
    public DocumentReference? PurchaseOrderReference { get; init; }

    // BT-14
    public DocumentReference? SalesOrderReference { get; init; }

    // BT-15
    public DocumentReference? ReceivingAdviceReference { get; init; }

    // BT-16
    public DocumentReference? DispatchAdviceReference { get; init; }

    // BT-17
    public DocumentReference? TenderOrLotReference { get; init; }

    // BT-18
    public Identifier? InvoicedObjectIdentifier { get; init; }

    // BT-19
    public Text? BuyerAccountingReference { get; init; }

    // BT-20
    public Text? PaymentTerms { get; init; }
}
