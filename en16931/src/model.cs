using System;
using System.Xml.Serialization;

namespace Dev.Fassbender.En16931.Model;

public record Amount(decimal Inner);

// TODO: how to represent code lists?
public record Code(string Inner);

public record Date(System.DateTime Inner);

public record DocumentReference(string Inner);

public record Percentage(decimal Inner);

public record Quantity(decimal Inner);

public record Text(string Inner);

public record UnitPriceAmount(decimal Inner);

[XmlRoot(Namespace="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")]
public record BinaryObject
{
    public required byte[] Content { get; init; }
    public required string MimeCode { get; init; }
    public required string Filename { get; init; }
}

public record Identifier
{
    public required string Content { get; init; }
    public string? SchemeIdentifier { get; init; }
    public string? SchemeVersionIdentifier { get; init; }
}

public record Invoice
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

public record InvoiceNote
{
    // BT-21
    public Code? Subject { get; init; }

    // BT-22
    public required Text Note { get; init; }
}

public record ProcessControl
{
    // BT-23
    public required Text BusinessProcessType { get; init; }

    // BT-24
    public required Identifier Specification { get; init; }
}

public record PrecedingInvoiceReference
{
    // BT-25
    public required DocumentReference PrecedingInvoice { get; init; }

    // BT-26
    public Date? IssueDate { get; init; }
}

public record Seller
{
    // BT-27
    public required Text Name { get; init; }

    // BT-28
    public Text? TradingName { get; init; }

    // BT-29
    public required Identifier[] Identifiers { get; init; }

    // BT-30
    public Identifier? LegalRegistration { get; init; }

    // BT-31
    public Identifier? VatIdentifier { get; init; }

    // BT-32
    public Identifier? TaxRegistrationIdentifier { get; init; }

    // BT-33
    public Text? AdditionalLegalInformation { get; init; }

    // BT-34
    public required Identifier ElectronicAddress { get; init; }

    // BG-5
    public required SellerPostalAddress PostalAddress { get; init; }

    // BG-6
    public required SellerContact Contact { get; init; }

}

public record Buyer
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

public record Payee
{
    // BT-59
    public required Text Name { get; init; }

    // BT-60
    public Identifier? Identifier { get; init; }

    // BT-61
    public Identifier? LegalRegistration { get; init; }
}

public record SellerTaxRepresentativeParty
{
    // BT-62
    public required Text Name { get; init; }

    // BT-63
    public required Identifier VatIdentifier { get; init; }

    // BG-12
    public required SellerTaxRepresentativePostalAddress PostalAddress { get; init; }
}

public record DeliveryInformation
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

public record PaymentInstructions
{
    // BT-81
    // UNTDID-4461
    public required Code PaymentMeansType { get; init; }

    // BT-82
    public Text? PaymentMeans { get; init; }

    // BT-83
    public Text? RemittanceInformation { get; init; }

    // BG-17
    public required CreditTransfer[] CreditTransfers { get; init; }

    // BG-18
    public PaymentCardInformation? PaymentCardInformation { get; init; }

    // BG-19
    public DirectDebit? DirectDebit { get; init; }
}

public record DocumentLevelAllowance
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

public record DocumentLevelCharge
{
    // BT-99
    public required Amount Amount { get; init; }

    // BT-100
    public Amount? BaseAmount { get; init; }

    // BT-101
    public Percentage? Percentage { get; init; }

    // BT-102
    public required Code VatCategory { get; init; }

    // BT-103
    public Percentage? VatRate { get; init; }

    // BT-104
    public Text? Reason { get; init; }

    // BT-105
    public Code? ReasonCode { get; init; }
}

public record DocumentTotals
{
    // BT-106
    public required Amount SumInvoiceLinesNet { get; init; }

    // BT-107
    public Amount? SumAllowances { get; init; }

    // BT-108
    public Amount? SumCharges { get; init; }

    // BT-109
    public required Amount NetAmount { get; init; }

    // BT-110
    public Amount? Vat { get; init; }

    // BT-111
    public Amount? VatInAccountingCurrency { get; init; }

    // BT-112
    public required Amount GrossAmount { get; init; }

    // BT-113
    public Amount? Paid { get; init; }

    // BT-114
    public Amount? Rounding { get; init; }

    // BT-115
    public required Amount ToPay { get; init; }
}

public record VatBreakdown
{
    // BT-116
    public required Amount TaxableAmount { get; init; }

    // BT-117
    public required Amount TaxAmount { get; init; }

    // BT-118
    // UNTDID 5305
    public required Code Category { get; init; }

    // BT-119
    public required Percentage CategoryRate { get; init; }

    // BT-120
    public Text? ExemptionReason { get; init; }

    // BT-121
    // VATEX Vat exemption reason code list
    public Code? ExemptionReasonCode { get; init; }
}

public record AdditionalSupportingDocument
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

public record InvoiceLine
{
    // BT-126
    public required Identifier Identifier { get; init; }

    // BT-127
    public Text? Note { get; init; }

    // BT-128
    public Identifier? ObjectIdentifier { get; init; }

    // BT-129
    public required Quantity Quantity { get; init; }

    // BT-130
    public required Code UnitOfMeasure { get; init; }

    // BT-131
    public required Amount NetAmount { get; init; }

    // BT-132
    public DocumentReference? PurchaseOrderLineReference { get; init; }

    // BT-133
    public Text? BuyerAccountingReference { get; init; }

    // BG-26
    public InvoiceLinePeriod? Period { get; init; }

    // BG-27
    public required InvoiceLineAllowance[] Allowances { get; init; }

    // BG-28
    public required InvoiceLineCharge[] Charges { get; init; }

    // BG-29
    public required PriceDetails PriceDetails { get; init; }

    // BG-30
    public required LineVatInformation VatInformation { get; init; }

    // BG-31
    public required ItemInformation ItemInformation { get; init; }
}

public record SellerPostalAddress
{
    // BT-35
    public Text? AddressLine1 { get; init; }

    // BT-36
    public Text? AddressLine2 { get; init; }

    // BT-162
    public Text? AddressLine3 { get; init; }

    // BT-37
    public required Text City { get; init; }

    // BT-38
    public required Text PostCode { get; init; }

    // BT-39
    public Text? Subdivision { get; init; }

    // BT-40
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code Country { get; init; }
}

public record SellerContact
{
    // BT-41
    public required Text ContactPoint { get; init; }

    // BT-42
    public required Text PhoneNumber { get; init; }

    // BT-43
    public required Text EmailAddress { get; init; }
}

public record BuyerPostalAddress
{
    // BT-50
    public Text? AddressLine1 { get; init; }

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
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code Country { get; init; }
}

public record BuyerContact
{
    // BT-56
    public Text? ContactPoint { get; init; }

    // BT-57
    public Text? PhoneNumber { get; init; }

    // BT-58
    public Text? EmailAddress { get; init; }
}

public record SellerTaxRepresentativePostalAddress
{
    // BT-64
    public Text? AddressLine1 { get; init; }

    // BT-65
    public Text? AddressLine2 { get; init; }

    // BT-164
    public Text? AddressLine3 { get; init; }

    // BT-66
    public required Text City { get; init; }

    // BT-67
    public required Text PostCode { get; init; }

    // BT-68
    public Text? Subdivision { get; init; }

    // BT-69
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code Country { get; init; }
}

public record InvoicingPeriod
{
    // BT-73
    public Date? Start { get; init; }

    // BT-74
    public Date? End { get; init; }
}

public record DeliverToAddress
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

public record CreditTransfer
{
    // BT-84
    public required Identifier Account { get; init; }

    // BT-85
    public Text? AccountName { get; init; }

    // BT-86
    public Identifier? ServiceProvider { get; init; }
}

public record PaymentCardInformation
{
    // BT-87
    public required Text PrimaryAccountNumber { get; init; }

    // BT-88
    public Text? CardHolderName { get; init; }
}

public record DirectDebit
{
    // BT-89
    public required Identifier MandateReference { get; init; }

    // BT-90
    public required Identifier BankAssignedCreditor { get; init; }

    // BT-91
    public required Identifier DebitedAccount { get; init; }
}

public record InvoiceLinePeriod
{
    // BT-134
    public Date? Start { get; init; }

    // BT-135
    public Date? End { get; init; }
}

public record InvoiceLineAllowance
{
    // BT-136
    public required Amount Amount { get; init; }

    // BT-137
    public Amount? BaseAmount { get; init; }

    // BT-138
    public Percentage? Percentage { get; init; }

    // BT-139
    public Text? Reason { get; init; }

    // BT-140
    public Code? ReasonCode { get; init; }
}

public record InvoiceLineCharge
{
    // BT-141
    public required Amount Amount { get; init; }

    // BT-142
    public Amount? BaseAmount { get; init; }

    // BT-143
    public Percentage? Percentage { get; init; }

    // BT-144
    public Text? Reason { get; init; }

    // BT-145
    public Code? ReasonCode { get; init; }
}

public record PriceDetails
{
    // BT-146
    public required UnitPriceAmount Net { get; init; }

    // BT-147
    public UnitPriceAmount? Discount { get; init; }

    // BT-148
    public UnitPriceAmount? Gross { get; init; }

    // BT-149
    public Quantity? BaseQuantity { get; init; }

    // BT-150
    // UN/ECE Rec No 20,21
    public Code? BaseQuantityUnitOfMeasure { get; init; }
}

public record LineVatInformation
{
    // BT-151
    // UNTDID 5305
    public required Code Category { get; init; }

    // BT-152
    public Percentage? Rate { get; init; }
}

public record ItemInformation
{
    // BT-153
    public required Text Name { get; init; }

    // BT-154
    public Text? Description { get; init; }

    // BT-155
    public Identifier? Seller { get; init; }

    // BT-156
    public Identifier? Buyer { get; init; }

    // BT-157
    public Identifier? StandardIdentifier { get; init; }

    // BT-158
    // UNTDID 7143
    public required Identifier[] ClassificationIdentifiers { get; init; }

    // BT-159
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2 representation
    public Code? CountryOfOrigin { get; init; }

    // BG-32
    public required ItemAttribute[] Attributes { get; init; }
}

public record ItemAttribute
{
    // BT-160
    public required Text Name { get; init; }

    // BT-161
    public required Text Value { get; init; }
}
