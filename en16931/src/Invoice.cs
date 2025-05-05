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

    // BG-1
    public required InvoiceNote[] Notes { get; init; }

    // BG-2
    public required ProcessControl ProcessControl { get; init; }

    // BG-3
    public required PrecedingInvoiceReference[] PrecedingInvoiceReferences { get; init; }

    // BG-4
    public required Seller Seller { get; init; }

    // BG-7
    public required Buyer Buyer { get; init; }

    // BG-10
    public Payee? Payee { get; init; }

    // BG-11
    public SellerTaxRepresentativeParty? SellerTaxRepresentativeParty { get; init; }

    // BG-13
    public DeliveryInformation? DeliveryInformation { get; init; }

    // BG-16
    public required PaymentInstructions PaymentInstructions { get; init; }

    // BG-20
    public required DocumentLevelAllowance[] DocumentLevelAllowances { get; init; }

    // BG-21
    public required DocumentLevelCharge[] DocumentLevelCharges { get; init; }

    // BG-22
    public required DocumentTotals DocumentTotals { get; init; }

    // BG-23
    public required VatBreakdown[] VatBreakdowns
    {
        get;
        init
        {
            if (value.Length == 0)
            {
                throw new InvalidOperationException("Array can't be empty");
            }

            field = value;
        }
    }

    // BG-24
    public required AdditionalSupportingDocument[] AdditionalSupportingDocuments { get; init; }


    // BG-25
    public required InvoiceLine[] InvoiceLines
    {
        get;
        init
        {
            if (value.Length == 0)
            {
                throw new InvalidOperationException("Array can't be empty");
            }

            field = value;
        }
    }
}

public class InvoiceNote { }

public class ProcessControl { }

public class PrecedingInvoiceReference { }

public class Seller { }

public class Buyer { }

public class Payee { }

public class SellerTaxRepresentativeParty { }

public class DeliveryInformation { }

public class PaymentInstructions { }

public class DocumentLevelAllowance { }

public class DocumentLevelCharge { }

public class DocumentTotals { }

public class VatBreakdown { }

public class AdditionalSupportingDocument { }

public class InvoiceLine { }

public class SellerPostalAddress { }

public class SellerContact { }

public class BuyerPostalAddress { }

public class BuyerContact { }

public class SellerTaxRepresentativePostalAddress { }

public class InvoicingPeriod { }

public class DeliverToAddress { }

public class CreditTransfer { }

public class PaymentCardInformation { }

public class DirectDebit { }

public class InvoiceLinePeriod { }

public class InvoiceLineAllowance { }

public class InvoiceLineCharge { }

public class PriceDetails { }

public class LineVatInformation { }

public class ItemInformation { }

public class ItemAttribute { }
