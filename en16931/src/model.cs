using System;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Dev.Fassbender.En16931.Model.Primitives;
using Im = Dev.Fassbender.En16931.Model.Immutable;

namespace Dev.Fassbender.En16931.Model;

// TODO: ToImmutable() for mutable model
// TODO: IR test as immutable model

[XmlRoot(ElementName = "invoice", Namespace = "urn:todo")]
public class Invoice
{
    // BT-1
    [XmlElement(ElementName = "invoice-number")]
    public required Identifier InvoiceNumber { get; set; }

    // BT-2
    [XmlElement(ElementName = "invoice-issue-date")]
    public required Date InvoiceIssueDate { get; set; }

    // BT-3
    // UNTDID 1001
    [XmlElement(ElementName = "invoice-type-code")]
    public required Code InvoiceTypeCode { get; set; }

    // BT-5
    // ISO 4217 - Codes for the representation of currencies and funds - Alpha-3 representation
    [XmlElement(ElementName = "invoice-currency-code")]
    public required Code InvoiceCurrencyCode { get; set; }

    // BT-6
    // ISO 4217 - Codes for the representation of currencies and funds - Alpha-3 representation
    [XmlElement(ElementName = "vat-accounting-currency-code")]
    public Code? VatAccountingCurrencyCode { get; set; }

    // BT-7
    [XmlElement(ElementName = "value-added-tax-point-date")]
    public Date? ValueAddedTaxPointDate { get; set; }

    // BT-8
    // UNTDID 2005
    [XmlElement(ElementName = "value-added-tax-point-date-code")]
    public Code? ValueAddedTaxPointDateCode { get; set; }

    // BT-9
    [XmlElement(ElementName = "payment-due-date")]
    public Date? PaymentDueDate { get; set; }

    // BT-10
    [XmlElement(ElementName = "buyer-reference")]
    public required Text BuyerReference { get; set; }

    // BT-11
    [XmlElement(ElementName = "project-reference")]
    public DocumentReference? ProjectReference { get; set; }

    // BT-12
    [XmlElement(ElementName = "contract-reference")]
    public DocumentReference? ContractReference { get; set; }

    // BT-13
    [XmlElement(ElementName = "purchase-order-reference")]
    public DocumentReference? PurchaseOrderReference { get; set; }

    // BT-14
    [XmlElement(ElementName = "sales-order-reference")]
    public DocumentReference? SalesOrderReference { get; set; }

    // BT-15
    [XmlElement(ElementName = "receiving-advice-reference")]
    public DocumentReference? ReceivingAdviceReference { get; set; }

    // BT-16
    [XmlElement(ElementName = "despatch-advice-reference")]
    public DocumentReference? DespatchAdviceReference { get; set; }

    // BT-17
    [XmlElement(ElementName = "tender-or-lot-reference")]
    public DocumentReference? TenderOrLotReference { get; set; }

    // BT-18
    [XmlElement(ElementName = "invoiced-object-identifier")]
    public Identifier? InvoicedObjectIdentifier { get; set; }

    // BT-19
    [XmlElement(ElementName = "buyer-accounting-reference")]
    public Text? BuyerAccountingReference { get; set; }

    // BT-20
    [XmlElement(ElementName = "payment-terms")]
    public Text? PaymentTerms { get; set; }

    // BG-1
    [XmlArray(ElementName = "invoice-notes")]
    [XmlArrayItem(ElementName = "invoice-note")]
    public InvoiceNote[] InvoiceNotes { get; set; } = [];

    // BG-2
    [XmlElement(ElementName = "process-control")]
    public required ProcessControl ProcessControl { get; set; }

    // BG-3
    [XmlArray(ElementName = "preceding-invoice-references")]
    [XmlArrayItem(ElementName = "preceding-invoice-reference")]
    public PrecedingInvoiceReference[] PrecedingInvoiceReferences { get; set; } = [];

    // BG-4
    [XmlElement(ElementName = "seller")]
    public required Seller Seller { get; set; }

    // BG-7
    [XmlElement(ElementName = "buyer")]
    public required Buyer Buyer { get; set; }

    // BG-10
    [XmlElement(ElementName = "payee")]
    public Payee? Payee { get; set; }

    // BG-11
    [XmlElement(ElementName = "seller-tax-representative-party")]
    public SellerTaxRepresentativeParty? SellerTaxRepresentativeParty { get; set; }

    // BG-13
    [XmlElement(ElementName = "delivery-information")]
    public DeliveryInformation? DeliveryInformation { get; set; }

    // BG-16
    [XmlElement(ElementName = "payment-instructions")]
    public required PaymentInstructions PaymentInstructions { get; set; }

    // BG-20
    [XmlArray(ElementName = "document-level-allowances")]
    [XmlArrayItem(ElementName = "document-level-allowance")]
    public DocumentLevelAllowance[] DocumentLevelAllowances { get; set; } = [];

    // BG-21
    [XmlArray(ElementName = "document-level-charges")]
    [XmlArrayItem(ElementName = "document-level-charge")]
    public DocumentLevelCharge[] DocumentLevelCharges { get; set; } = [];

    // BG-22
    [XmlElement(ElementName = "document-totals")]
    public required DocumentTotals DocumentTotals { get; set; }

    // BG-23
    //
    // non-empty
    [XmlArray(ElementName = "vat-breakdown")]
    [XmlArrayItem(ElementName = "vat-breakdown")]
    public VatBreakdown[] VatBreakdown { get; set; } = [];

    // BG-24
    [XmlArray(ElementName = "additional-supporting-documents")]
    [XmlArrayItem(ElementName = "additional-supporting-document")]
    public AdditionalSupportingDocument[] AdditionalSupportingDocuments { get; set; } = [];

    // BG-25
    //
    // non-empty
    [XmlArray(ElementName = "invoice-lines")]
    [XmlArrayItem(ElementName = "invoice-line")]
    public InvoiceLine[] InvoiceLines { get; set; } = [];
}

public class InvoiceNote
{
    // BT-21
    [XmlElement(ElementName = "invoice-note-subject-code")]
    public Code? InvoiceNoteSubjectCode { get; set; }

    // BT-22
    [XmlElement(ElementName = "invoice-note")]
    public required Text Note { get; set; }
}

public class ProcessControl
{
    // BT-23
    [XmlElement(ElementName = "business-process-type")]
    public required Text BusinessProcessType { get; set; }

    // BT-24
    [XmlElement(ElementName = "specification-identifier")]
    public required Identifier SpecificationIdentifier { get; set; }
}

public class PrecedingInvoiceReference
{
    // BT-25
    [XmlElement(ElementName = "preceding-invoice-reference")]
    public required DocumentReference Reference { get; set; }

    // BT-26
    [XmlElement(ElementName = "preceding-invoice-issue-date")]
    public Date? PrecedingInvoiceIssueDate { get; set; }
}

public class Seller
{
    // BT-27
    [XmlElement(ElementName = "seller-name")]
    public required Text SellerName { get; set; }

    // BT-28
    [XmlElement(ElementName = "seller-trading-name")]
    public Text? SellerTradingName { get; set; }

    // BT-29
    [XmlArray(ElementName = "seller-identifiers")]
    [XmlArrayItem(ElementName = "seller-identifier")]
    public Identifier[] SellerIdentifiers { get; set; } = [];

    // BT-30
    [XmlElement(ElementName = "seller-legal-registration-identifier")]
    public Identifier? SellerLegalRegistrationIdentifier { get; set; }

    // BT-31
    [XmlElement(ElementName = "seller-vat-identifier")]
    public Identifier? SellerVatIdentifier { get; set; }

    // BT-32
    [XmlElement(ElementName = "seller-tax-registration-identifier")]
    public Identifier? SellerTaxRegistrationIdentifier { get; set; }

    // BT-33
    [XmlElement(ElementName = "seller-additional-legal-information")]
    public Text? SellerAdditionalLegalInformation { get; set; }

    // BT-34
    [XmlElement(ElementName = "seller-electronic-address")]
    public required Identifier SellerElectronicAddress { get; set; }

    // BG-5
    [XmlElement(ElementName = "seller-postal-address")]
    public required SellerPostalAddress SellerPostalAddress { get; set; }

    // BG-6
    [XmlElement(ElementName = "seller-contact")]
    public required SellerContact SellerContact { get; set; }

}

public class SellerPostalAddress
{
    // BT-35
    [XmlElement(ElementName = "seller-address-line-1")]
    public Text? SellerAddressLine1 { get; set; }

    // BT-36
    [XmlElement(ElementName = "seller-address-line-2")]
    public Text? SellerAddressLine2 { get; set; }

    // BT-162
    [XmlElement(ElementName = "seller-address-line-3")]
    public Text? SellerAddressLine3 { get; set; }

    // BT-37
    [XmlElement(ElementName = "seller-city")]
    public required Text SellerCity { get; set; }

    // BT-38
    [XmlElement(ElementName = "seller-post-code")]
    public required Text SellerPostCode { get; set; }

    // BT-39
    [XmlElement(ElementName = "seller-country-subdivision")]
    public Text? SellerCountrySubdivision { get; set; }

    // BT-40
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    [XmlElement(ElementName = "seller-country-code")]
    public required Code SellerCountryCode { get; set; }
}

public class SellerContact
{
    // BT-41
    [XmlElement(ElementName = "seller-contact-point")]
    public required Text SellerContactPoint { get; set; }

    // BT-42
    [XmlElement(ElementName = "seller-contact-telephone-number")]
    public required Text SellerContactTelephoneNumber { get; set; }

    // BT-43
    [XmlElement(ElementName = "seller-contact-email-address")]
    public required Text SellerContactEmailAddress { get; set; }
}

public class Buyer
{
    // BT-44
    [XmlElement(ElementName = "buyer-name")]
    public required Text BuyerName { get; set; }

    // BT-45
    [XmlElement(ElementName = "buyer-trading-name")]
    public Text? BuyerTradingName { get; set; }

    // BT-46
    [XmlElement(ElementName = "buyer-identifier")]
    public Identifier? BuyerIdentifier { get; set; }

    // BT-47
    [XmlElement(ElementName = "buyer-legal-registration-identifier")]
    public Identifier? BuyerLegalRegistrationIdentifier { get; set; }

    // BT-48
    [XmlElement(ElementName = "buyer-vat-identifier")]
    public Identifier? BuyerVatIdentifier { get; set; }

    // BT-49
    [XmlElement(ElementName = "buyer-electronic-address")]
    public Identifier? BuyerElectronicAddress { get; set; }

    // BG-8
    [XmlElement(ElementName = "buyer-postal-address")]
    public required BuyerPostalAddress BuyerPostalAddress { get; set; }

    // BG-9
    [XmlElement(ElementName = "buyer-contact")]
    public BuyerContact? BuyerContact { get; set; }
}

public class BuyerPostalAddress
{
    // BT-50
    [XmlElement(ElementName = "buyer-address-line-1")]
    public Text? BuyerAddressLine1 { get; set; }

    // BT-51
    [XmlElement(ElementName = "buyer-address-line-2")]
    public Text? BuyerAddressLine2 { get; set; }

    // BT-163
    [XmlElement(ElementName = "buyer-address-line-3")]
    public Text? BuyerAddressLine3 { get; set; }

    // BT-52
    [XmlElement(ElementName = "buyer-city")]
    public required Text BuyerCity { get; set; }

    // BT-53
    [XmlElement(ElementName = "buyer-post-code")]
    public required Text BuyerPostCode { get; set; }

    // BT-54
    [XmlElement(ElementName = "buyer-country-subdivision")]
    public Text? BuyerCountrySubdivision { get; set; }

    // BT-55
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    [XmlElement(ElementName = "buyer-country-code")]
    public required Code BuyerCountryCode { get; set; }
}

public class BuyerContact
{
    // BT-56
    [XmlElement(ElementName = "buyer-contact-point")]
    public Text? ContactPoint { get; set; }

    // BT-57
    [XmlElement(ElementName = "buyer-contact-telephone-number")]
    public Text? PhoneNumber { get; set; }

    // BT-58
    [XmlElement(ElementName = "buyer-contact-electronic-email")]
    public Text? EmailAddress { get; set; }
}

public class Payee
{
    // BT-59
    [XmlElement(ElementName = "payee-name")]
    public required Text PayeeName { get; set; }

    // BT-60
    [XmlElement(ElementName = "payee-identifier")]
    public Identifier? PayeeIdentifier { get; set; }

    // BT-61
    [XmlElement(ElementName = "payee-legal-registration-identifier")]
    public Identifier? PayeeLegalRegistrationIdentifier { get; set; }
}

public class SellerTaxRepresentativeParty
{
    // BT-62
    [XmlElement(ElementName = "seller-tax-representative-name")]
    public required Text SellerTaxRepresentativeName { get; set; }

    // BT-63
    [XmlElement(ElementName = "seller-tax-representative-vat-identifier")]
    public required Identifier SellerTaxRepresentativeVatIdentifier { get; set; }

    // BG-12
    [XmlElement(ElementName = "seller-tax-representative-postal-address")]
    public required SellerTaxRepresentativePostalAddress SellerTaxRepresentativePostalAddress { get; set; }
}

public class SellerTaxRepresentativePostalAddress
{
    // BT-64
    [XmlElement(ElementName = "tax-representative-address-line-1")]
    public Text? TaxRepresentativeAddressLine1 { get; set; }

    // BT-65
    [XmlElement(ElementName = "tax-representative-address-line-2")]
    public Text? TaxRepresentativeAddressLine2 { get; set; }

    // BT-164
    [XmlElement(ElementName = "tax-representative-address-line-3")]
    public Text? TaxRepresentativeAddressLine3 { get; set; }

    // BT-66
    [XmlElement(ElementName = "tax-representative-city")]
    public required Text TaxRepresentativeCity { get; set; }

    // BT-67
    [XmlElement(ElementName = "tax-representative-post-code")]
    public required Text TaxRepresentativePostCode { get; set; }

    // BT-68
    [XmlElement(ElementName = "tax-representative-country-subdivision")]
    public Text? TaxRepresentativeCountrySubdivision { get; set; }

    // BT-69
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    [XmlElement(ElementName = "tax-representative-country-code")]
    public required Code TaxRepresentativeCountryCode { get; set; }
}

public class DeliveryInformation
{
    // BT-70
    [XmlElement(ElementName = "deliver-to-party-name")]
    public Text? DeliverToPartyName { get; set; }

    // BT-71
    [XmlElement(ElementName = "deliver-to-location-identifier")]
    public Identifier? DeliverToLocationIdentifier { get; set; }

    // BT-72
    [XmlElement(ElementName = "actual-delivery-date")]
    public Date? ActualDeliveryDate { get; set; }

    // BG-14
    [XmlElement(ElementName = "invoicing-period")]
    public InvoicingPeriod? InvoicingPeriod { get; set; }

    // BG-15
    [XmlElement(ElementName = "deliver-to-address")]
    public DeliverToAddress? DeliverToAddress { get; set; }
}

public class InvoicingPeriod
{
    // BT-73
    [XmlElement(ElementName = "invoicing-period-start-date")]
    public Date? InvoicingPeriodStartDate { get; set; }

    // BT-74
    [XmlElement(ElementName = "invoicing-period-end-date")]
    public Date? InvoicingPeriodEndDate { get; set; }
}

public class DeliverToAddress
{
    // BT-75
    [XmlElement(ElementName = "deliver-to-address-line-1")]
    public required Text DeliverToAddressLine1 { get; set; }

    // BT-76
    [XmlElement(ElementName = "deliver-to-address-line-2")]
    public Text? DeliverToAddressLine2 { get; set; }

    // BT-165
    [XmlElement(ElementName = "deliver-to-address-line-3")]
    public Text? DeliverToAddressLine3 { get; set; }

    // BT-77
    [XmlElement(ElementName = "deliver-to-city")]
    public required Text DeliverToCity { get; set; }

    // BT-78
    [XmlElement(ElementName = "deliver-to-post-code")]
    public required Text DeliverToPostCode { get; set; }

    // BT-79
    [XmlElement(ElementName = "deliver-to-country-subdivision")]
    public Text? DeliverToCountrySubdivision { get; set; }

    // BT-80
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    [XmlElement(ElementName = "deliver-to-country-code")]
    public required Code DeliverToCountryCode { get; set; }
}

public class PaymentInstructions
{
    // BT-81
    // UNTDID-4461
    [XmlElement(ElementName = "payment-means-type-code")]
    public required Code PaymentMeansTypeCode { get; set; }

    // BT-82
    [XmlElement(ElementName = "payment-means-text")]
    public Text? PaymentMeansText { get; set; }

    // BT-83
    [XmlElement(ElementName = "remittance-information")]
    public Text? RemittanceInformation { get; set; }

    // BG-17
    [XmlArray(ElementName = "credit-transfers")]
    [XmlArrayItem(ElementName = "credit-transfer")]
    public CreditTransfer[] CreditTransfers { get; set; } = [];

    // BG-18
    [XmlElement(ElementName = "payment-card-information")]
    public PaymentCardInformation? PaymentCardInformation { get; set; }

    // BG-19
    [XmlElement(ElementName = "direct-debit")]
    public DirectDebit? DirectDebit { get; set; }
}

public class CreditTransfer
{
    // BT-84
    [XmlElement(ElementName = "payment-account-identifier")]
    public required Identifier PaymentAccountIdentifier { get; set; }

    // BT-85
    [XmlElement(ElementName = "payment-account-name")]
    public Text? PaymentAccountName { get; set; }

    // BT-86
    [XmlElement(ElementName = "payment-service-provider-identifier")]
    public Identifier? PaymentServiceProviderIdentifier { get; set; }
}

public class PaymentCardInformation
{
    // BT-87
    [XmlElement(ElementName = "payment-card-primary-account-number")]
    public required Text PaymentCardPrimaryAccountNumber { get; set; }

    // BT-88
    [XmlElement(ElementName = "payment-card-holder-name")]
    public Text? PaymentCardHolderName { get; set; }
}

public class DirectDebit
{
    // BT-89
    [XmlElement(ElementName = "mandate-reference-identifier")]
    public required Identifier MandateReferenceIdentifier { get; set; }

    // BT-90
    [XmlElement(ElementName = "bank-assigned-creditor-identifier")]
    public required Identifier BankAssignedCreditorIdentifier { get; set; }

    // BT-91
    [XmlElement(ElementName = "debited-account-identifier")]
    public required Identifier DebitedAccountIdentifier { get; set; }
}

public class DocumentLevelAllowance
{
    // BT-92
    [XmlElement(ElementName = "document-level-allowance-amount")]
    public required Amount DocumentLevelAllowanceAmount { get; set; }

    // BT-93
    [XmlElement(ElementName = "document-level-allowance-base-amount")]
    public Amount? DocumentLevelAllowanceBaseAmount { get; set; }

    // BT-94
    [XmlElement(ElementName = "document-level-allowance-percentage")]
    public Percentage? DocumentLevelAllowancePercentage { get; set; }

    // BT-95
    [XmlElement(ElementName = "document-level-allowance-vat-category-code")]
    public required Code DocumentLevelAllowanceVatCategoryCode { get; set; }

    // BT-96
    [XmlElement(ElementName = "document-level-allowance-vat-rate")]
    public Percentage? DocumentLevelAllowanceVatRate { get; set; }

    // BT-97
    [XmlElement(ElementName = "document-level-allowance-reason")]
    public Text? DocumentLevelAllowanceReason { get; set; }

    // BT-98
    [XmlElement(ElementName = "document-level-allowance-reason-code")]
    public Code? DocumentLevelAllowanceReasonCode { get; set; }
}

public class DocumentLevelCharge
{
    // BT-99
    [XmlElement(ElementName = "document-level-charge-amount")]
    public required Amount DocumentLevelChargeAmount { get; set; }

    // BT-100
    [XmlElement(ElementName = "document-level-charge-base-amount")]
    public Amount? DocumentLevelChargeBaseAmount { get; set; }

    // BT-101
    [XmlElement(ElementName = "document-level-charge-percentage")]
    public Percentage? DocumentLevelChargePercentage { get; set; }

    // BT-102
    [XmlElement(ElementName = "document-level-charge-vat-category-code")]
    public required Code DocumentLevelChargeVatCategoryCode { get; set; }

    // BT-103
    [XmlElement(ElementName = "document-level-charge-vat-rate")]
    public Percentage? DocumentLevelChargeVatRate { get; set; }

    // BT-104
    [XmlElement(ElementName = "document-level-charge-reason")]
    public Text? DocumentLevelChargeReason { get; set; }

    // BT-105
    [XmlElement(ElementName = "document-level-charge-reason-code")]
    public Code? DocumentLevelChargeReasonCode { get; set; }
}

public class DocumentTotals
{
    // BT-106
    [XmlElement(ElementName = "sum-of-invoice-line-net-amount")]
    public required Amount SumOfInvoiceLineNetAmount { get; set; }

    // BT-107
    [XmlElement(ElementName = "sum-of-allowances-on-document-level")]
    public Amount? SumOfAllowancesOnDocumentLevel { get; set; }

    // BT-108
    [XmlElement(ElementName = "sum-of-charges-on-document-level")]
    public Amount? SumOfChargesOnDocumentLevel { get; set; }

    // BT-109
    [XmlElement(ElementName = "invoice-total-amount-without-vat")]
    public required Amount InvoiceTotalAmountWithoutVat { get; set; }

    // BT-110
    [XmlElement(ElementName = "invoice-total-vat-amount")]
    public Amount? InvoiceTotalVatAmount { get; set; }

    // BT-111
    [XmlElement(ElementName = "invoice-total-vat-amount-in-accounting-currency")]
    public Amount? InvoiceTotalVatAmountInAccountingCurrency { get; set; }

    // BT-112
    [XmlElement(ElementName = "invoice-total-amount-with-vat")]
    public required Amount InvoiceTotalAmountWithVat { get; set; }

    // BT-113
    [XmlElement(ElementName = "paid-amount")]
    public Amount? PaidAmount { get; set; }

    // BT-114
    [XmlElement(ElementName = "rounding-amount")]
    public Amount? RoundingAmount { get; set; }

    // BT-115
    [XmlElement(ElementName = "amount-due-for-payment")]
    public required Amount AmountDueForPayment { get; set; }
}

public class VatBreakdown
{
    // BT-116
    [XmlElement(ElementName = "vat-category-taxable-amount")]
    public required Amount VatCategoryTaxableAmount { get; set; }

    // BT-117
    [XmlElement(ElementName = "vat-category-tax-amount")]
    public required Amount VatCategoryTaxAmount { get; set; }

    // BT-118
    // UNTDID 5305
    [XmlElement(ElementName = "vat-category-code")]
    public required Code VatCategoryCode { get; set; }

    // BT-119
    [XmlElement(ElementName = "vat-category-rate")]
    public required Percentage VatCategoryRate { get; set; }

    // BT-120
    [XmlElement(ElementName = "vat-exemption-reason")]
    public Text? VatExemptionReasonText { get; set; }

    // BT-121
    // VATEX Vat exemption reason code list
    [XmlElement(ElementName = "vat-exemption-reason-code")]
    public Code? VatExemptionReasonCode { get; set; }
}

public class AdditionalSupportingDocument
{
    // BT-122
    [XmlElement(ElementName = "supporting-document-reference")]
    public required DocumentReference SupportingDocumentReference { get; set; }

    // BT-123
    [XmlElement(ElementName = "supporting-document-description")]
    public Text? SupportingDocumentDescription { get; set; }

    // BT-124
    [XmlElement(ElementName = "external-document-location")]
    public Text? ExternalDocumentLocation { get; set; }

    // BT-125
    [XmlElement(ElementName = "attached-document")]
    public BinaryObject? AttachedDocument { get; set; }
}

public class InvoiceLine
{
    // BT-126
    [XmlElement(ElementName = "invoice-line-identifier")]
    public required Identifier InvoiceLineIdentifier { get; set; }

    // BT-127
    [XmlElement(ElementName = "invoice-line-note")]
    public Text? InvoiceLineNote { get; set; }

    // BT-128
    [XmlElement(ElementName = "invoice-line-object-identifier")]
    public Identifier? InvoiceLineObjectIdentifier { get; set; }

    // BT-129
    [XmlElement(ElementName = "invoiced-quantity")]
    public required Quantity InvoicedQuantity { get; set; }

    // BT-130
    [XmlElement(ElementName = "invoiced-quantity-unit-of-measure")]
    public required Code InvoicedQuantityUnitOfMeasure { get; set; }

    // BT-131
    [XmlElement(ElementName = "invoice-line-net-amount")]
    public required Amount InvoiceLineNetAmount { get; set; }

    // BT-132
    [XmlElement(ElementName = "referenced-purchase-order-line-reference")]
    public DocumentReference? ReferencedPurchaseOrderLineReference { get; set; }

    // BT-133
    [XmlElement(ElementName = "invoice-line-buyer-accounting-reference")]
    public Text? InvoiceLineBuyerAccountingReference { get; set; }

    // BG-26
    [XmlElement(ElementName = "invoice-line-period")]
    public InvoiceLinePeriod? InvoiceLinePeriod { get; set; }

    // BG-27
    [XmlArray(ElementName = "invoice-line-allowances")]
    [XmlArrayItem(ElementName = "invoice-line-allowance")]
    public InvoiceLineAllowance[] InvoiceLineAllowances { get; set; } = [];

    // BG-28
    [XmlArray(ElementName = "invoice-line-charges")]
    [XmlArrayItem(ElementName = "invoice-line-charge")]
    public InvoiceLineCharge[] InvoiceLineCharges { get; set; } = [];

    // BG-29
    [XmlElement(ElementName = "price-details")]
    public required PriceDetails PriceDetails { get; set; }

    // BG-30
    [XmlElement(ElementName = "line-vat-information")]
    public required LineVatInformation LineVatInformation { get; set; }

    // BG-31
    [XmlElement(ElementName = "item-information")]
    public required ItemInformation ItemInformation { get; set; }
}

public class InvoiceLinePeriod : IToImmutable<Im.InvoiceLinePeriod>
{
    // BT-134
    [XmlElement(ElementName = "invoice-line-period-start-date")]
    public Date? InvoiceLinePeriodStartDate { get; set; }

    // BT-135
    [XmlElement(ElementName = "invoice-line-period-end-date")]
    public Date? InvoiceLinePeriodEndDate { get; set; }

    public Im.InvoiceLinePeriod ToImmutable()
    {
        return new Im.InvoiceLinePeriod
        {
            InvoiceLinePeriodStartDate = InvoiceLinePeriodStartDate?.ToImmutable(),
            InvoiceLinePeriodEndDate = InvoiceLinePeriodEndDate?.ToImmutable(),
        };
    }
}

public class InvoiceLineAllowance : IToImmutable<Im.InvoiceLineAllowance>
{
    // BT-136
    [XmlElement(ElementName = "invoice-line-allowance-amount")]
    public required Amount InvoiceLineAllowanceAmount { get; set; }

    // BT-137
    [XmlElement(ElementName = "invoice-line-allowance-base-amount")]
    public Amount? InvoiceLineAllowanceBaseAmount { get; set; }

    // BT-138
    [XmlElement(ElementName = "invoice-line-allowance-percentage")]
    public Percentage? InvoiceLineAllowancePercentage { get; set; }

    // BT-139
    [XmlElement(ElementName = "invoice-line-allowance-reason")]
    public Text? InvoiceLineAllowanceReason { get; set; }

    // BT-140
    [XmlElement(ElementName = "invoice-line-allowance-reason-code")]
    public Code? InvoiceLineAllowanceReasonCode { get; set; }

    public Im.InvoiceLineAllowance ToImmutable()
    {
        return new Im.InvoiceLineAllowance
        {
            InvoiceLineAllowanceAmount = InvoiceLineAllowanceAmount.ToImmutable(),
            InvoiceLineAllowanceBaseAmount = InvoiceLineAllowanceBaseAmount?.ToImmutable(),
            InvoiceLineAllowancePercentage = InvoiceLineAllowancePercentage?.ToImmutable(),
            InvoiceLineAllowanceReason = InvoiceLineAllowanceReason?.ToImmutable(),
            InvoiceLineAllowanceReasonCode = InvoiceLineAllowanceReasonCode?.ToImmutable(),
        };
    }
}

public class InvoiceLineCharge : IToImmutable<Im.InvoiceLineCharge>
{
    // BT-141
    [XmlElement(ElementName = "invoice-line-charge-amount")]
    public required Amount InvoiceLineChargeAmount { get; set; }

    // BT-142
    [XmlElement(ElementName = "invoice-line-charge-base-amount")]
    public Amount? InvoiceLineChargeBaseAmount { get; set; }

    // BT-143
    [XmlElement(ElementName = "invoice-line-charge-percentage")]
    public Percentage? InvoiceLineChargePercentage { get; set; }

    // BT-144
    [XmlElement(ElementName = "invoice-line-charge-reason")]
    public Text? InvoiceLineChargeReason { get; set; }

    // BT-145
    [XmlElement(ElementName = "invoice-line-charge-reason-code")]
    public Code? InvoiceLineChargeReasonCode { get; set; }

    public Im.InvoiceLineCharge ToImmutable()
    {
        return new Im.InvoiceLineCharge
        {
            InvoiceLineChargeAmount = InvoiceLineChargeAmount.ToImmutable(),
            InvoiceLineChargeBaseAmount = InvoiceLineChargeBaseAmount?.ToImmutable(),
            InvoiceLineChargePercentage = InvoiceLineChargePercentage?.ToImmutable(),
            InvoiceLineChargeReason = InvoiceLineChargeReason?.ToImmutable(),
            InvoiceLineChargeReasonCode = InvoiceLineChargeReasonCode?.ToImmutable(),
        };
    }
}

public class PriceDetails : IToImmutable<Im.PriceDetails>
{
    // BT-146
    [XmlElement(ElementName = "item-net-price")]
    public required UnitPriceAmount ItemNetPrice { get; set; }

    // BT-147
    [XmlElement(ElementName = "item-price-discount")]
    public UnitPriceAmount? ItemPriceDiscount { get; set; }

    // BT-148
    [XmlElement(ElementName = "item-gross-price")]
    public UnitPriceAmount? ItemGrossPrice { get; set; }

    // BT-149
    [XmlElement(ElementName = "item-price-base-quantity")]
    public Quantity? ItemPriceBaseQuantity { get; set; }

    // BT-150
    // UN/ECE Rec No 20,21
    [XmlElement(ElementName = "item-price-base-quantity-unit-of-measure")]
    public Code? ItemPriceBaseQuantityUnitOfMeasure { get; set; }

    public Im.PriceDetails ToImmutable()
    {
        return new Im.PriceDetails
        {
            ItemNetPrice = ItemNetPrice.ToImmutable(),
            ItemPriceDiscount = ItemPriceDiscount?.ToImmutable(),
            ItemGrossPrice = ItemGrossPrice?.ToImmutable(),
            ItemPriceBaseQuantity = ItemPriceBaseQuantity?.ToImmutable(),
            ItemPriceBaseQuantityUnitOfMeasure = ItemPriceBaseQuantityUnitOfMeasure?.ToImmutable(),
        };
    }
}

public class LineVatInformation : IToImmutable<Im.LineVatInformation>
{
    // BT-151
    // UNTDID 5305
    [XmlElement(ElementName = "invoiced-item-vat-category-code")]
    public required Code InvoicedItemVatCategoryCode { get; set; }

    // BT-152
    [XmlElement(ElementName = "invoiced-item-vat-rate")]
    public Percentage? InvoicedItemVatRate { get; set; }

    public Im.LineVatInformation ToImmutable()
    {
        return new Im.LineVatInformation
        {
            InvoicedItemVatCategoryCode = InvoicedItemVatCategoryCode.ToImmutable(),
            InvoicedItemVatRate = InvoicedItemVatRate?.ToImmutable(),
        };
    }
}

public class ItemInformation : IToImmutable<Im.ItemInformation>
{
    // BT-153
    [XmlElement(ElementName = "item-name")]
    public required Text ItemName { get; set; }

    // BT-154
    [XmlElement(ElementName = "item-description")]
    public Text? ItemDescription { get; set; }

    // BT-155
    [XmlElement(ElementName = "item-sellers-identifier")]
    public Identifier? ItemSellersIdentifier { get; set; }

    // BT-156
    [XmlElement(ElementName = "item-buyers-identifier")]
    public Identifier? ItemBuyersIdentifier { get; set; }

    // BT-157
    [XmlElement(ElementName = "item-standard-identifier")]
    public Identifier? ItemStandardIdentifier { get; set; }

    // BT-158
    // UNTDID 7143
    [XmlArray(ElementName = "item-classification-identifiers")]
    [XmlArrayItem(ElementName = "item-classification-identifier")]
    public Identifier[] ItemClassificationIdentifiers { get; set; } = [];

    // BT-159
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2 representation
    [XmlElement(ElementName = "item-country-of-origin")]
    public Code? ItemCountryOfOrigin { get; set; }

    // BG-32
    [XmlArray(ElementName = "item-attributes")]
    [XmlArrayItem(ElementName = "item-attribute")]
    public ItemAttribute[] ItemAttributes { get; set; } = [];

    public Im.ItemInformation ToImmutable()
    {
        return new Im.ItemInformation
        {
            ItemName = ItemName.ToImmutable(),
            ItemDescription = ItemDescription?.ToImmutable(),
            ItemSellersIdentifier = ItemSellersIdentifier?.ToImmutable(),
            ItemBuyersIdentifier = ItemBuyersIdentifier?.ToImmutable(),
            ItemStandardIdentifier = ItemStandardIdentifier?.ToImmutable(),
            ItemClassificationIdentifiers = ItemClassificationIdentifiers.ToImmutable<Identifier, Im.Primitives.Identifier>(),
            ItemCountryOfOrigin = ItemCountryOfOrigin?.ToImmutable(),
            ItemAttributes = ItemAttributes.ToImmutable<ItemAttribute, Im.ItemAttribute>(),
        };
    }
}

public class ItemAttribute : IToImmutable<Im.ItemAttribute>
{
    // BT-160
    [XmlElement(ElementName = "item-attribute-name")]
    public required Text ItemAttributeName { get; set; }

    // BT-161
    [XmlElement(ElementName = "item-attribute-value")]
    public required Text ItemAttributeValue { get; set; }

    public Im.ItemAttribute ToImmutable()
    {
        return new Im.ItemAttribute
        {
            ItemAttributeName = ItemAttributeName.ToImmutable(),
            ItemAttributeValue = ItemAttributeValue.ToImmutable(),
        };
    }
}
