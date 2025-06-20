using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Dev.Fassbender.En16931.Model;

public record NewType<T> : IXmlSerializable {
    public required T Inner;

    public void ReadXml(XmlReader reader) {
        Inner = (T)reader.ReadElementContentAs(typeof(T), null);
    }

    public void WriteXml(XmlWriter writer) {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema() {
        return null;
    }
}

public record Amount : NewType<decimal> {}

public record Code : NewType<string> {}

public record Date : NewType<DateTime> {}

public record DocumentReference : NewType<string> {}

public record Percentage : NewType<decimal> {}

public record Quantity : NewType<decimal> {}

public record Text : NewType<string> {}

public record UnitPriceAmount : NewType<decimal> {}

public record BinaryObject
{
    public required byte[] Content { get; init; }
    public required string MimeCode { get; init; }
    public required string Filename { get; init; }
}

public record Identifier
{
    [XmlElement(ElementName = "content")]
    public required string Content { get; init; }

    [XmlElement(ElementName = "scheme-identifier")]
    public string? SchemeIdentifier { get; init; }

    [XmlElement(ElementName = "scheme-version-identifier")]
    public string? SchemeVersionIdentifier { get; init; }
}

[XmlRoot(ElementName = "invoice", Namespace = "urn:todo")]
public record Invoice
{
    // BT-1
    [XmlElement(ElementName = "invoice-number")]
    public required Identifier InvoiceNumber { get; init; }

    // BT-2
    [XmlElement(ElementName = "invoice-issue-date")]
    public required Date InvoiceIssueDate { get; init; }

    // BT-3
    // UNTDID 1001
    [XmlElement(ElementName = "invoice-type-code")]
    public required Code InvoiceTypeCode { get; init; }

    // BT-5
    // ISO 4217 - Codes for the representation of currencies and funds - Alpha-3 representation
    [XmlElement(ElementName = "invoice-currency-code")]
    public required Code InvoiceCurrencyCode { get; init; }

    // BT-6
    // ISO 4217 - Codes for the representation of currencies and funds - Alpha-3 representation
    [XmlElement(ElementName = "vat-accounting-currency-code")]
    public Code? VatAccountingCurrencyCode { get; init; }

    // BT-7
    [XmlElement(ElementName = "value-added-tax-point-date")]
    public Date? ValueAddedTaxPointDate { get; init; }

    // BT-8
    // UNTDID 2005
    [XmlElement(ElementName = "value-added-tax-point-date-code")]
    public Code? ValueAddedTaxPointDateCode { get; init; }

    // BT-9
    [XmlElement(ElementName = "payment-due-date")]
    public Date? PaymentDueDate { get; init; }

    // BT-10
    [XmlElement(ElementName = "buyer-reference")]
    public required Text BuyerReference { get; init; }

    // BT-11
    [XmlElement(ElementName = "project-reference")]
    public DocumentReference? ProjectReference { get; init; }

    // BT-12
    [XmlElement(ElementName = "contract-reference")]
    public DocumentReference? ContractReference { get; init; }

    // BT-13
    [XmlElement(ElementName = "purchase-order-reference")]
    public DocumentReference? PurchaseOrderReference { get; init; }

    // BT-14
    [XmlElement(ElementName = "sales-order-reference")]
    public DocumentReference? SalesOrderReference { get; init; }

    // BT-15
    [XmlElement(ElementName = "receiving-advice-reference")]
    public DocumentReference? ReceivingAdviceReference { get; init; }

    // BT-16
    [XmlElement(ElementName = "despatch-advice-reference")]
    public DocumentReference? DespatchAdviceReference { get; init; }

    // BT-17
    [XmlElement(ElementName = "tender-or-lot-reference")]
    public DocumentReference? TenderOrLotReference { get; init; }

    // BT-18
    [XmlElement(ElementName = "invoiced-object-identifier")]
    public Identifier? InvoicedObjectIdentifier { get; init; }

    // BT-19
    [XmlElement(ElementName = "buyer-accounting-reference")]
    public Text? BuyerAccountingReference { get; init; }

    // BT-20
    [XmlElement(ElementName = "payment-terms")]
    public Text? PaymentTerms { get; init; }

    // BG-1
    [XmlArray(ElementName = "invoice-notes")]
    [XmlArrayItem(ElementName = "invoice-note")]
    public required InvoiceNote[] InvoiceNotes { get; init; }

    // BG-2
    [XmlElement(ElementName = "process-control")]
    public required ProcessControl ProcessControl { get; init; }

    // BG-3
    [XmlArray(ElementName = "preceding-invoice-references")]
    [XmlArrayItem(ElementName = "preceding-invoice-reference")]
    public required PrecedingInvoiceReference[] PrecedingInvoiceReferences { get; init; }

    // BG-4
    [XmlElement(ElementName = "seller")]
    public required Seller Seller { get; init; }

    // BG-7
    [XmlElement(ElementName = "buyer")]
    public required Buyer Buyer { get; init; }

    // BG-10
    [XmlElement(ElementName = "payee")]
    public Payee? Payee { get; init; }

    // BG-11
    [XmlElement(ElementName = "seller-tax-representative-party")]
    public SellerTaxRepresentativeParty? SellerTaxRepresentativeParty { get; init; }

    // BG-13
    [XmlElement(ElementName = "delivery-information")]
    public DeliveryInformation? DeliveryInformation { get; init; }

    // BG-16
    [XmlElement(ElementName = "payment-instructions")]
    public required PaymentInstructions PaymentInstructions { get; init; }

    // BG-20
    [XmlArray(ElementName = "document-level-allowances")]
    [XmlArrayItem(ElementName = "document-level-allowance")]
    public required DocumentLevelAllowance[] DocumentLevelAllowances { get; init; }

    // BG-21
    [XmlArray(ElementName = "document-level-charges")]
    [XmlArrayItem(ElementName = "document-level-charge")]
    public required DocumentLevelCharge[] DocumentLevelCharges { get; init; }

    // BG-22
    [XmlElement(ElementName = "document-totals")]
    public required DocumentTotals DocumentTotals { get; init; }

    // BG-23
    //
    // non-empty
    [XmlArray(ElementName = "vat-breakdown")]
    [XmlArrayItem(ElementName = "vat-breakdown")]
    public required VatBreakdown[] VatBreakdown { get; init; }

    // BG-24
    [XmlArray(ElementName = "additional-supporting-documents")]
    [XmlArrayItem(ElementName = "additional-supporting-document")]
    public required AdditionalSupportingDocument[] AdditionalSupportingDocuments { get; init; }

    // BG-25
    //
    // non-empty
    [XmlArray(ElementName = "invoice-lines")]
    [XmlArrayItem(ElementName = "invoice-line")]
    public required InvoiceLine[] InvoiceLines { get; init; }
}

public record InvoiceNote
{
    // BT-21
    [XmlElement(ElementName = "invoice-note-subject-code")]
    public Code? InvoiceNoteSubjectCode { get; init; }

    // BT-22
    [XmlElement(ElementName = "invoice-note")]
    public required Text Note { get; init; }
}

public record ProcessControl
{
    // BT-23
    [XmlElement(ElementName = "business-process-type")]
    public required Text BusinessProcessType { get; init; }

    // BT-24
    [XmlElement(ElementName = "specification-identifier")]
    public required Identifier SpecificationIdentifier { get; init; }
}

public record PrecedingInvoiceReference
{
    // BT-25
    [XmlElement(ElementName = "preceding-invoice-reference")]
    public required DocumentReference Reference { get; init; }

    // BT-26
    [XmlElement(ElementName = "preceding-invoice-issue-date")]
    public Date? PrecedingInvoiceIssueDate { get; init; }
}

public record Seller
{
    // BT-27
    [XmlElement(ElementName = "seller-name")]
    public required Text SellerName { get; init; }

    // BT-28
    [XmlElement(ElementName = "seller-trading-name")]
    public Text? SellerTradingName { get; init; }

    // BT-29
    [XmlArray(ElementName = "seller-identifiers")]
    [XmlArrayItem(ElementName = "seller-identifier")]
    public required Identifier[] SellerIdentifiers { get; init; }

    // BT-30
    [XmlElement(ElementName = "seller-legal-registration-identifier")]
    public Identifier? SellerLegalRegistrationIdentifier { get; init; }

    // BT-31
    [XmlElement(ElementName = "seller-vat-identifier")]
    public Identifier? SellerTaxRegistrationIdentifier { get; init; }

    // BT-32
    [XmlElement(ElementName = "seller-tax-registration-identifier")]
    public Identifier? SelerTaxRegistrationIdentifier { get; init; }

    // BT-33
    [XmlElement(ElementName = "seller-additional-legal-information")]
    public Text? SellerAdditionalLegalInformation { get; init; }

    // BT-34
    [XmlElement(ElementName = "seller-electronic-address")]
    public required Identifier SellerElectronicAddress { get; init; }

    // BG-5
    [XmlElement(ElementName = "seller-postal-address")]
    public required SellerPostalAddress SellerPostalAddress { get; init; }

    // BG-6
    [XmlElement(ElementName = "seller-contact")]
    public required SellerContact Contact { get; init; }

}

public record SellerPostalAddress
{
    // BT-35
    [XmlElement(ElementName = "seller-address-line-1")]
    public Text? SellerAddressLine1 { get; init; }

    // BT-36
    [XmlElement(ElementName = "seller-address-line-2")]
    public Text? SellerAddressLine2 { get; init; }

    // BT-162
    [XmlElement(ElementName = "seller-address-line-3")]
    public Text? AddressLine3 { get; init; }

    // BT-37
    [XmlElement(ElementName = "seller-city")]
    public required Text SellerCity { get; init; }

    // BT-38
    [XmlElement(ElementName = "seller-post-code")]
    public required Text SellerPostCode { get; init; }

    // BT-39
    [XmlElement(ElementName = "seller-country-subdivision")]
    public Text? SellerCountrySubdivision { get; init; }

    // BT-40
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    [XmlElement(ElementName = "seller-country-code")]
    public required Code SellerCountryCode { get; init; }
}

public record SellerContact
{
    // BT-41
    [XmlElement(ElementName = "seller-contact-point")]
    public required Text SellerContactPoint { get; init; }

    // BT-42
    [XmlElement(ElementName = "seller-contact-telephone-number")]
    public required Text SellerContactTelephoneNumber { get; init; }

    // BT-43
    [XmlElement(ElementName = "seller-contact-email-address")]
    public required Text SellerContactEmailAddress { get; init; }
}

public record Buyer
{
    // BT-44
    [XmlElement(ElementName = "buyer-name")]
    public required Text BuyerName { get; init; }

    // BT-45
    [XmlElement(ElementName = "buyer-trading-name")]
    public Text? BuyerTradingName { get; init; }

    // BT-46
    [XmlElement(ElementName = "buyer-identifier")]
    public Identifier? BuyerIdentifier { get; init; }

    // BT-47
    [XmlElement(ElementName = "buyer-legal-registration-identifier")]
    public Identifier? BuyerLegalRegistrationIdentifier { get; init; }

    // BT-48
    [XmlElement(ElementName = "buyer-vat-identifier")]
    public Identifier? BuyerVatIdentifier { get; init; }

    // BT-49
    [XmlElement(ElementName = "buyer-electronic-address")]
    public Identifier? BuyerElectronicAddress { get; init; }

    // BG-8
    [XmlElement(ElementName = "buyer-postal-address")]
    public required BuyerPostalAddress BuyerPostalAddress { get; init; }

    // BG-9
    [XmlElement(ElementName = "buyer-contact")]
    public BuyerContact? BuyerContact { get; init; }
}

public record BuyerPostalAddress
{
    // BT-50
    [XmlElement(ElementName = "buyer-address-line-1")]
    public Text? BuyerAddressLine1 { get; init; }

    // BT-51
    [XmlElement(ElementName = "buyer-address-line-2")]
    public Text? BuyerAddressLine2 { get; init; }

    // BT-163
    [XmlElement(ElementName = "buyer-address-line-3")]
    public Text? BuyerAddressLine3 { get; init; }

    // BT-52
    [XmlElement(ElementName = "buyer-city")]
    public required Text BuyerCity { get; init; }

    // BT-53
    [XmlElement(ElementName = "buyer-post-code")]
    public required Text BuyerPostCode { get; init; }

    // BT-54
    [XmlElement(ElementName = "buyer-country-subdivision")]
    public Text? BuyerCountrySubdivision { get; init; }

    // BT-55
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    [XmlElement(ElementName = "buyer-country-code")]
    public required Code BuyerCountryCode { get; init; }
}

public record BuyerContact
{
    // BT-56
    [XmlElement(ElementName = "buyer-contact-point")]
    public Text? ContactPoint { get; init; }

    // BT-57
    [XmlElement(ElementName = "buyer-contact-telephone-number")]
    public Text? PhoneNumber { get; init; }

    // BT-58
    [XmlElement(ElementName = "buyer-contact-electronic-email")]
    public Text? EmailAddress { get; init; }
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
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
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
