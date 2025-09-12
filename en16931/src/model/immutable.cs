using Dev.Fassbender.En16931.Model.Immutable.Primitives;

namespace Dev.Fassbender.En16931.Model.Immutable;

public record Invoice
{
    // BT-1
    public required Identifier InvoiceNumber { get; init; }

    // BT-2
    public required Date InvoiceIssueDate { get; init; }

    // BT-3
    // UNTDID 1001
    public required Code InvoiceTypeCode { get; init; }

    // BT-5
    // ISO 4217 - Codes for the representation of currencies and funds - Alpha-3 representation
    public required Code InvoiceCurrencyCode { get; init; }

    // BT-6
    // ISO 4217 - Codes for the representation of currencies and funds - Alpha-3 representation
    public Code? VatAccountingCurrencyCode { get; init; }

    // BT-7
    public Date? ValueAddedTaxPointDate { get; init; }

    // BT-8
    // UNTDID 2005
    public Code? ValueAddedTaxPointDateCode { get; init; }

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
    public DocumentReference? DespatchAdviceReference { get; init; }

    // BT-17
    public DocumentReference? TenderOrLotReference { get; init; }

    // BT-18
    public Identifier? InvoicedObjectIdentifier { get; init; }

    // BT-19
    public Text? BuyerAccountingReference { get; init; }

    // BT-20
    public Text? PaymentTerms { get; init; }

    // BG-1
    public required Array<InvoiceNote> InvoiceNotes { get; init; }

    // BG-2
    public required ProcessControl ProcessControl { get; init; }

    // BG-3
    public required Array<PrecedingInvoiceReference> PrecedingInvoiceReferences { get; init; }

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
    public required Array<DocumentLevelAllowance> DocumentLevelAllowances { get; init; }

    // BG-21
    public required Array<DocumentLevelCharge> DocumentLevelCharges { get; init; }

    // BG-22
    public required DocumentTotals DocumentTotals { get; init; }

    // BG-23
    //
    // non-empty
    public required Array<VatBreakdown> VatBreakdown { get; init; }

    // BG-24
    public required Array<AdditionalSupportingDocument> AdditionalSupportingDocuments { get; init; }

    // BG-25
    //
    // non-empty
    public required Array<InvoiceLine> InvoiceLines { get; init; }
}

public record InvoiceNote
{
    // BT-21
    public Code? InvoiceNoteSubjectCode { get; init; }

    // BT-22
    public required Text Note { get; init; }
}

public record ProcessControl
{
    // BT-23
    public required Text BusinessProcessType { get; init; }

    // BT-24
    public required Identifier SpecificationIdentifier { get; init; }
}

public record PrecedingInvoiceReference
{
    // BT-25
    public required DocumentReference Reference { get; init; }

    // BT-26
    public Date? PrecedingInvoiceIssueDate { get; init; }
}

public record Seller
{
    // BT-27
    public required Text SellerName { get; init; }

    // BT-28
    public Text? SellerTradingName { get; init; }

    // BT-29
    public required Array<Identifier> SellerIdentifiers { get; init; }

    // BT-30
    public Identifier? SellerLegalRegistrationIdentifier { get; init; }

    // BT-31
    public Identifier? SellerVatIdentifier { get; init; }

    // BT-32
    public Identifier? SellerTaxRegistrationIdentifier { get; init; }

    // BT-33
    public Text? SellerAdditionalLegalInformation { get; init; }

    // BT-34
    public required Identifier SellerElectronicAddress { get; init; }

    // BG-5
    public required SellerPostalAddress SellerPostalAddress { get; init; }

    // BG-6
    public required SellerContact SellerContact { get; init; }

}

public record SellerPostalAddress
{
    // BT-35
    public Text? SellerAddressLine1 { get; init; }

    // BT-36
    public Text? SellerAddressLine2 { get; init; }

    // BT-162
    public Text? SellerAddressLine3 { get; init; }

    // BT-37
    public required Text SellerCity { get; init; }

    // BT-38
    public required Text SellerPostCode { get; init; }

    // BT-39
    public Text? SellerCountrySubdivision { get; init; }

    // BT-40
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code SellerCountryCode { get; init; }
}

public record SellerContact
{
    // BT-41
    public required Text SellerContactPoint { get; init; }

    // BT-42
    public required Text SellerContactTelephoneNumber { get; init; }

    // BT-43
    public required Text SellerContactEmailAddress { get; init; }
}

public record Buyer
{
    // BT-44
    public required Text BuyerName { get; init; }

    // BT-45
    public Text? BuyerTradingName { get; init; }

    // BT-46
    public Identifier? BuyerIdentifier { get; init; }

    // BT-47
    public Identifier? BuyerLegalRegistrationIdentifier { get; init; }

    // BT-48
    public Identifier? BuyerVatIdentifier { get; init; }

    // BT-49
    public Identifier? BuyerElectronicAddress { get; init; }

    // BG-8
    public required BuyerPostalAddress BuyerPostalAddress { get; init; }

    // BG-9
    public BuyerContact? BuyerContact { get; init; }
}

public record BuyerPostalAddress
{
    // BT-50
    public Text? BuyerAddressLine1 { get; init; }

    // BT-51
    public Text? BuyerAddressLine2 { get; init; }

    // BT-163
    public Text? BuyerAddressLine3 { get; init; }

    // BT-52
    public required Text BuyerCity { get; init; }

    // BT-53
    public required Text BuyerPostCode { get; init; }

    // BT-54
    public Text? BuyerCountrySubdivision { get; init; }

    // BT-55
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code BuyerCountryCode { get; init; }
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

public record Payee
{
    // BT-59
    public required Text PayeeName { get; init; }

    // BT-60
    public Identifier? PayeeIdentifier { get; init; }

    // BT-61
    public Identifier? PayeeLegalRegistrationIdentifier { get; init; }
}

public record SellerTaxRepresentativeParty
{
    // BT-62
    public required Text SellerTaxRepresentativeName { get; init; }

    // BT-63
    public required Identifier SellerTaxRepresentativeVatIdentifier { get; init; }

    // BG-12
    public required SellerTaxRepresentativePostalAddress SellerTaxRepresentativePostalAddress { get; init; }
}

public record SellerTaxRepresentativePostalAddress
{
    // BT-64
    public Text? TaxRepresentativeAddressLine1 { get; init; }

    // BT-65
    public Text? TaxRepresentativeAddressLine2 { get; init; }

    // BT-164
    public Text? TaxRepresentativeAddressLine3 { get; init; }

    // BT-66
    public required Text TaxRepresentativeCity { get; init; }

    // BT-67
    public required Text TaxRepresentativePostCode { get; init; }

    // BT-68
    public Text? TaxRepresentativeCountrySubdivision { get; init; }

    // BT-69
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code TaxRepresentativeCountryCode { get; init; }
}

public record DeliveryInformation
{
    // BT-70
    public Text? DeliverToPartyName { get; init; }

    // BT-71
    public Identifier? DeliverToLocationIdentifier { get; init; }

    // BT-72
    public Date? ActualDeliveryDate { get; init; }

    // BG-14
    public InvoicingPeriod? InvoicingPeriod { get; init; }

    // BG-15
    public DeliverToAddress? DeliverToAddress { get; init; }
}

public record InvoicingPeriod
{
    // BT-73
    public Date? InvoicingPeriodStartDate { get; init; }

    // BT-74
    public Date? InvoicingPeriodEndDate { get; init; }
}

public record DeliverToAddress
{
    // BT-75
    public required Text DeliverToAddressLine1 { get; init; }

    // BT-76
    public Text? DeliverToAddressLine2 { get; init; }

    // BT-165
    public Text? DeliverToAddressLine3 { get; init; }

    // BT-77
    public required Text DeliverToCity { get; init; }

    // BT-78
    public required Text DeliverToPostCode { get; init; }

    // BT-79
    public Text? DeliverToCountrySubdivision { get; init; }

    // BT-80
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code DeliverToCountryCode { get; init; }
}

public record PaymentInstructions
{
    // BT-81
    // UNTDID-4461
    public required Code PaymentMeansTypeCode { get; init; }

    // BT-82
    public Text? PaymentMeansText { get; init; }

    // BT-83
    public Text? RemittanceInformation { get; init; }

    // BG-17
    public required Array<CreditTransfer> CreditTransfers { get; init; }

    // BG-18
    public PaymentCardInformation? PaymentCardInformation { get; init; }

    // BG-19
    public DirectDebit? DirectDebit { get; init; }
}

public record CreditTransfer
{
    // BT-84
    public required Identifier PaymentAccountIdentifier { get; init; }

    // BT-85
    public Text? PaymentAccountName { get; init; }

    // BT-86
    public Identifier? PaymentServiceProviderIdentifier { get; init; }
}

public record PaymentCardInformation
{
    // BT-87
    public required Text PaymentCardPrimaryAccountNumber { get; init; }

    // BT-88
    public Text? PaymentCardHolderName { get; init; }
}

public record DirectDebit
{
    // BT-89
    public required Identifier MandateReferenceIdentifier { get; init; }

    // BT-90
    public required Identifier BankAssignedCreditorIdentifier { get; init; }

    // BT-91
    public required Identifier DebitedAccountIdentifier { get; init; }
}

public record DocumentLevelAllowance
{
    // BT-92
    public required Amount DocumentLevelAllowanceAmount { get; init; }

    // BT-93
    public Amount? DocumentLevelAllowanceBaseAmount { get; init; }

    // BT-94
    public Percentage? DocumentLevelAllowancePercentage { get; init; }

    // BT-95
    public required Code DocumentLevelAllowanceVatCategoryCode { get; init; }

    // BT-96
    public Percentage? DocumentLevelAllowanceVatRate { get; init; }

    // BT-97
    public Text? DocumentLevelAllowanceReason { get; init; }

    // BT-98
    public Code? DocumentLevelAllowanceReasonCode { get; init; }
}

public record DocumentLevelCharge
{
    // BT-99
    public required Amount DocumentLevelChargeAmount { get; init; }

    // BT-100
    public Amount? DocumentLevelChargeBaseAmount { get; init; }

    // BT-101
    public Percentage? DocumentLevelChargePercentage { get; init; }

    // BT-102
    public required Code DocumentLevelChargeVatCategoryCode { get; init; }

    // BT-103
    public Percentage? DocumentLevelChargeVatRate { get; init; }

    // BT-104
    public Text? DocumentLevelChargeReason { get; init; }

    // BT-105
    public Code? DocumentLevelChargeReasonCode { get; init; }
}

public record DocumentTotals
{
    // BT-106
    public required Amount SumOfInvoiceLineNetAmount { get; init; }

    // BT-107
    public Amount? SumOfAllowancesOnDocumentLevel { get; init; }

    // BT-108
    public Amount? SumOfChargesOnDocumentLevel { get; init; }

    // BT-109
    public required Amount InvoiceTotalAmountWithoutVat { get; init; }

    // BT-110
    public Amount? InvoiceTotalVatAmount { get; init; }

    // BT-111
    public Amount? InvoiceTotalVatAmountInAccountingCurrency { get; init; }

    // BT-112
    public required Amount InvoiceTotalAmountWithVat { get; init; }

    // BT-113
    public Amount? PaidAmount { get; init; }

    // BT-114
    public Amount? RoundingAmount { get; init; }

    // BT-115
    public required Amount AmountDueForPayment { get; init; }
}

public record VatBreakdown
{
    // BT-116
    public required Amount VatCategoryTaxableAmount { get; init; }

    // BT-117
    public required Amount VatCategoryTaxAmount { get; init; }

    // BT-118
    // UNTDID 5305
    public required Code VatCategoryCode { get; init; }

    // BT-119
    public required Percentage VatCategoryRate { get; init; }

    // BT-120
    public Text? VatExemptionReasonText { get; init; }

    // BT-121
    // VATEX Vat exemption reason code list
    public Code? VatExemptionReasonCode { get; init; }
}

public record AdditionalSupportingDocument
{
    // BT-122
    public required DocumentReference SupportingDocumentReference { get; init; }

    // BT-123
    public Text? SupportingDocumentDescription { get; init; }

    // BT-124
    public Text? ExternalDocumentLocation { get; init; }

    // BT-125
    public BinaryObject? AttachedDocument { get; init; }
}

public record InvoiceLine
{
    // BT-126
    public required Identifier InvoiceLineIdentifier { get; init; }

    // BT-127
    public Text? InvoiceLineNote { get; init; }

    // BT-128
    public Identifier? InvoiceLineObjectIdentifier { get; init; }

    // BT-129
    public required Quantity InvoicedQuantity { get; init; }

    // BT-130
    public required Code InvoicedQuantityUnitOfMeasure { get; init; }

    // BT-131
    public required Amount InvoiceLineNetAmount { get; init; }

    // BT-132
    public DocumentReference? ReferencedPurchaseOrderLineReference { get; init; }

    // BT-133
    public Text? InvoiceLineBuyerAccountingReference { get; init; }

    // BG-26
    public InvoiceLinePeriod? InvoiceLinePeriod { get; init; }

    // BG-27
    public required Array<InvoiceLineAllowance> InvoiceLineAllowances { get; init; }

    // BG-28
    public required Array<InvoiceLineCharge> InvoiceLineCharges { get; init; }

    // BG-29
    public required PriceDetails PriceDetails { get; init; }

    // BG-30
    public required LineVatInformation LineVatInformation { get; init; }

    // BG-31
    public required ItemInformation ItemInformation { get; init; }
}

public record InvoiceLinePeriod
{
    // BT-134
    public Date? InvoiceLinePeriodStartDate { get; init; }

    // BT-135
    public Date? InvoiceLinePeriodEndDate { get; init; }
}

public record InvoiceLineAllowance
{
    // BT-136
    public required Amount InvoiceLineAllowanceAmount { get; init; }

    // BT-137
    public Amount? InvoiceLineAllowanceBaseAmount { get; init; }

    // BT-138
    public Percentage? InvoiceLineAllowancePercentage { get; init; }

    // BT-139
    public Text? InvoiceLineAllowanceReason { get; init; }

    // BT-140
    public Code? InvoiceLineAllowanceReasonCode { get; init; }
}

public record InvoiceLineCharge
{
    // BT-141
    public required Amount InvoiceLineChargeAmount { get; init; }

    // BT-142
    public Amount? InvoiceLineChargeBaseAmount { get; init; }

    // BT-143
    public Percentage? InvoiceLineChargePercentage { get; init; }

    // BT-144
    public Text? InvoiceLineChargeReason { get; init; }

    // BT-145
    public Code? InvoiceLineChargeReasonCode { get; init; }
}

public record PriceDetails
{
    // BT-146
    public required UnitPriceAmount ItemNetPrice { get; init; }

    // BT-147
    public UnitPriceAmount? ItemPriceDiscount { get; init; }

    // BT-148
    public UnitPriceAmount? ItemGrossPrice { get; init; }

    // BT-149
    public Quantity? ItemPriceBaseQuantity { get; init; }

    // BT-150
    // UN/ECE Rec No 20,21
    public Code? ItemPriceBaseQuantityUnitOfMeasure { get; init; }
}

public record LineVatInformation
{
    // BT-151
    // UNTDID 5305
    public required Code InvoicedItemVatCategoryCode { get; init; }

    // BT-152
    public Percentage? InvoicedItemVatRate { get; init; }
}

public record ItemInformation
{
    // BT-153
    public required Text ItemName { get; init; }

    // BT-154
    public Text? ItemDescription { get; init; }

    // BT-155
    public Identifier? ItemSellersIdentifier { get; init; }

    // BT-156
    public Identifier? ItemBuyersIdentifier { get; init; }

    // BT-157
    public Identifier? ItemStandardIdentifier { get; init; }

    // BT-158
    // UNTDID 7143
    public required Array<Identifier> ItemClassificationIdentifiers { get; init; }

    // BT-159
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2 representation
    public Code? ItemCountryOfOrigin { get; init; }

    // BG-32
    public required Array<ItemAttribute> ItemAttributes { get; init; }
}

public record ItemAttribute
{
    // BT-160
    public required Text ItemAttributeName { get; init; }

    // BT-161
    public required Text ItemAttributeValue { get; init; }
}
