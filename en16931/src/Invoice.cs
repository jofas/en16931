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
    public required string Filename { get; init; }

    public BinaryObject(byte[] content, string mimeCode, string filename)
    {
        Content = content;
        MimeCode = mimeCode;
        Filename = filename;
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
    // TODO: AttachedDocument?.Filename non-case sensitive unique per invoice
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

public class Buyer
{
    // BT-44
    public required Text Name { get; init; }

    // BT-45
    public Text? TradingName { get; init; }

    // BT-46
    public Identifier? Identifier { get; init; }

    // BT-47
    public Identifier? LegalRegistrationIdentifier { get; init; }

    // BT-48
    public Identifier? VatIdentifier { get; init; }

    // BT-49
    public Identifier? ElectronicAddress { get; init; }

    // BG-8
    public required BuyerPostalAddress PostalAddress { get; init; }

    // BG-9
    public BuyerContact? Contact { get; init; }
}

public class Payee { }

public class SellerTaxRepresentativeParty { }

public class DeliveryInformation
{
    // BT-70
    public Text? PartyName { get; init; }

    // BT-71
    public Identifier? LocationIdentifier { get; init; }

    // BT-72
    public Date? ActualDeliveryDate { get; init; }

    // BG-14
    public InvoicingPeriod? InvoicingPeriod { get; init; }

    // BG-15
    public DeliverToAddress? DeliverToAddress { get; init; }
}

public class PaymentInstructions { }

public class DocumentLevelAllowance
{
    // BT-92
    public required Amount Amount { get; init; }

    // BT-93
    public Amount? BaseAmount { get; init; }

    // BT-94
    public Percentage? Percentage { get; init; }

    // BT-95
    public required Code VatCategory { get; init; }

    // BT-96
    public Percentage? VatRate { get; init; }

    // BT-97
    public Text? Reason { get; init; }

    // BT-98
    public Code? ReasonCode { get; init; }
}

public class DocumentLevelCharge { }

public class DocumentTotals { }

public class VatBreakdown { }

public class AdditionalSupportingDocument
{
    // BT-122
    public required DocumentReference Reference { get; init; }

    // BT-123
    public Text? Description { get; init; }

    // BT-124
    public Text? Location { get; init; }

    // BT-125
    public BinaryObject? AttachedDocument { get; init; }
}

public class InvoiceLine { }

public class SellerPostalAddress { }

public class SellerContact { }

public class BuyerPostalAddress
{
    // BT-50
    public required Text AddressLine1 { get; init; }

    // BT-51
    public Text? AddressLine2 { get; init; }

    // BT-163
    public Text? AddressLine3 { get; init; }

    // BT-52
    public required Text City { get; init; }

    // BT-53
    public required Text PostCode { get; init; }

    // BT-54
    public Text? Subdivision { get; init; }

    // BT-55
    public required Code Country { get; init; }
}

public class BuyerContact
{
    // BT-56
    public Text? ContactPoint { get; init; }

    // BT-57
    public Text? PhoneNumber { get; init; }

    // BT-58
    public Text? EmailAddress { get; init; }
}

public class SellerTaxRepresentativePostalAddress { }

public class InvoicingPeriod { }

public class DeliverToAddress
{
    // BT-75
    public required Text AddressLine1 { get; init; }

    // BT-76
    public Text? AddressLine2 { get; init; }

    // BT-165
    public Text? AddressLine3 { get; init; }

    // BT-77
    public required Text City { get; init; }

    // BT-78
    public required Text PostCode { get; init; }

    // BT-79
    public Text? Subdivision { get; init; }

    // BT-80
    public required Code Country { get; init; }
}

public class CreditTransfer
{
    // BT-84
    public required Identifier Account { get; init; }

    // BT-85
    public Text? AccountName { get; init; }

    // BT-86
    public Identifier? ServiceProvider { get; init; }
}

public class PaymentCardInformation { }

public class DirectDebit
{
    // BT-89
    public required Identifier MandateReference { get; init; }

    // BT-90
    public required Identifier BankAssignedCreditor { get; init; }

    // BT-91
    public required Identifier DebitedAccount { get; init; }
}

public class InvoiceLinePeriod { }

public class InvoiceLineAllowance { }

public class InvoiceLineCharge { }

public class PriceDetails { }

public class LineVatInformation { }

public class ItemInformation { }

public class ItemAttribute { }
