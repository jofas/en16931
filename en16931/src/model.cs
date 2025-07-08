using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Dev.Fassbender.En16931.Model;

public record struct Amount : IXmlSerializable
{
    private decimal _inner;

    public decimal Inner { get => _inner; }

    public Amount(decimal inner) => _inner = inner;

    public void ReadXml(XmlReader reader)
    {
        _inner = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public record struct Code : IXmlSerializable
{
    private string _inner;

    public string Inner { get => _inner; }

    public Code(string inner) => _inner = inner;

    public void ReadXml(XmlReader reader)
    {
        _inner = reader.ReadElementContentAsString();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public record struct Date : IXmlSerializable
{
    private DateTime _inner;

    public DateTime Inner { get => _inner; }

    public Date(DateTime inner) => _inner = inner;

    public void ReadXml(XmlReader reader)
    {
        _inner = reader.ReadElementContentAsDateTime();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public record struct DocumentReference : IXmlSerializable
{
    private string _inner;

    public string Inner { get => _inner; }

    public DocumentReference(string inner) => _inner = inner;

    public void ReadXml(XmlReader reader)
    {
        _inner = reader.ReadElementContentAsString();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public record struct Percentage : IXmlSerializable
{
    private decimal _inner;

    public decimal Inner { get => _inner; }

    public Percentage(decimal inner) => _inner = inner;

    public void ReadXml(XmlReader reader)
    {
        _inner = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public record struct Quantity : IXmlSerializable
{
    private decimal _inner;

    public decimal Inner { get => _inner; }

    public Quantity(decimal inner) => _inner = inner;

    public void ReadXml(XmlReader reader)
    {
        _inner = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public record struct Text : IXmlSerializable
{
    private string _inner;

    public string Inner { get => _inner; }

    public Text(string inner) => _inner = inner;

    public void ReadXml(XmlReader reader)
    {
        _inner = reader.ReadElementContentAsString();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public record struct UnitPriceAmount : IXmlSerializable
{
    private decimal _inner;

    public decimal Inner { get => _inner; }

    public UnitPriceAmount(decimal inner) => _inner = inner;

    public void ReadXml(XmlReader reader)
    {
        _inner = reader.ReadElementContentAsDecimal();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public record struct BinaryObject
{
    [XmlElement(ElementName = "content")]
    public required byte[] Content { get; init; }

    [XmlElement(ElementName = "mime-code")]
    public required string MimeCode { get; init; }

    [XmlElement(ElementName = "filename")]
    public required string Filename { get; init; }

    [SetsRequiredMembers]
    public BinaryObject(byte[] content, string mimeCode, string filename)
    {
        Content = content;
        MimeCode = mimeCode;
        Filename = filename;
    }
}

public record struct Identifier
{
    [XmlElement(ElementName = "content")]
    public required string Content { get; init; }

    [XmlElement(ElementName = "scheme-identifier")]
    public string? SchemeIdentifier { get; init; }

    [XmlElement(ElementName = "scheme-version-identifier")]
    public string? SchemeVersionIdentifier { get; init; }

    public Identifier() { }

    [SetsRequiredMembers]
    public Identifier(string content) => Content = content;

    [SetsRequiredMembers]
    public Identifier(string content, string schemeIdentifier) : this(content)
    {
        SchemeIdentifier = schemeIdentifier;
    }

    [SetsRequiredMembers]
    public Identifier(string content, string schemeIdentifier, string schemeVersionIdentifier) : this(content)
    {
        SchemeIdentifier = schemeIdentifier;
        SchemeVersionIdentifier = schemeVersionIdentifier;
    }
}

public struct Array<T> : IEnumerable<T>, IEquatable<Array<T>>
{
    private ImmutableArray<T>.Builder? __inner;
    private ImmutableArray<T>.Builder _inner => __inner ??= ImmutableArray.CreateBuilder<T>();

    public ImmutableArray<T> Inner { get => _inner.ToImmutableArray(); }

    public Array(List<T> inner)
    {
        _inner.AddRange(inner);
    }

    #region XML parsing

    public IEnumerator<T> GetEnumerator() => _inner.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _inner.GetEnumerator();

    public void Add(T item)
    {
        _inner.Add(item);
    }

    #endregion

    #region value-equality

    public override bool Equals(object? o) => o is Array<T> other && this.Equals(other);

    public bool Equals(Array<T> other)
    {
        return Inner.SequenceEqual(other.Inner);
    }

    public override int GetHashCode() => Inner.GetHashCode();

    public static bool operator ==(Array<T> lhs, Array<T> rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Array<T> lhs, Array<T> rhs)
    {
        return !lhs.Equals(rhs);
    }

    #endregion

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
    public required Array<Identifier> SellerIdentifiers { get; init; }

    // BT-30
    [XmlElement(ElementName = "seller-legal-registration-identifier")]
    public Identifier? SellerLegalRegistrationIdentifier { get; init; }

    // BT-31
    [XmlElement(ElementName = "seller-vat-identifier")]
    public Identifier? SellerVatIdentifier { get; init; }

    // BT-32
    [XmlElement(ElementName = "seller-tax-registration-identifier")]
    public Identifier? SellerTaxRegistrationIdentifier { get; init; }

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
    public required SellerContact SellerContact { get; init; }

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
    public Text? SellerAddressLine3 { get; init; }

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
    [XmlElement(ElementName = "payee-name")]
    public required Text PayeeName { get; init; }

    // BT-60
    [XmlElement(ElementName = "payee-identifier")]
    public Identifier? PayeeIdentifier { get; init; }

    // BT-61
    [XmlElement(ElementName = "payee-legal-registration-identifier")]
    public Identifier? PayeeLegalRegistrationIdentifier { get; init; }
}

public record SellerTaxRepresentativeParty
{
    // BT-62
    [XmlElement(ElementName = "seller-tax-representative-name")]
    public required Text SellerTaxRepresentativeName { get; init; }

    // BT-63
    [XmlElement(ElementName = "seller-tax-representative-vat-identifier")]
    public required Identifier SellerTaxRepresentativeVatIdentifier { get; init; }

    // BG-12
    [XmlElement(ElementName = "seller-tax-representative-postal-address")]
    public required SellerTaxRepresentativePostalAddress SellerTaxRepresentativePostalAddress { get; init; }
}

public record SellerTaxRepresentativePostalAddress
{
    // BT-64
    [XmlElement(ElementName = "tax-representative-address-line-1")]
    public Text? TaxRepresentativeAddressLine1 { get; init; }

    // BT-65
    [XmlElement(ElementName = "tax-representative-address-line-2")]
    public Text? TaxRepresentativeAddressLine2 { get; init; }

    // BT-164
    [XmlElement(ElementName = "tax-representative-address-line-3")]
    public Text? TaxRepresentativeAddressLine3 { get; init; }

    // BT-66
    [XmlElement(ElementName = "tax-representative-city")]
    public required Text TaxRepresentativeCity { get; init; }

    // BT-67
    [XmlElement(ElementName = "tax-representative-post-code")]
    public required Text TaxRepresentativePostCode { get; init; }

    // BT-68
    [XmlElement(ElementName = "tax-representative-country-subdivision")]
    public Text? TaxRepresentativeCountrySubdivision { get; init; }

    // BT-69
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    [XmlElement(ElementName = "tax-representative-country-code")]
    public required Code TaxRepresentativeCountryCode { get; init; }
}

public record DeliveryInformation
{
    // BT-70
    [XmlElement(ElementName = "deliver-to-party-name")]
    public Text? DeliverToPartyName { get; init; }

    // BT-71
    [XmlElement(ElementName = "deliver-to-location-identifier")]
    public Identifier? DeliverToLocationIdentifier { get; init; }

    // BT-72
    [XmlElement(ElementName = "actual-delivery-date")]
    public Date? ActualDeliveryDate { get; init; }

    // BG-14
    [XmlElement(ElementName = "invoicing-period")]
    public InvoicingPeriod? InvoicingPeriod { get; init; }

    // BG-15
    [XmlElement(ElementName = "deliver-to-address")]
    public DeliverToAddress? DeliverToAddress { get; init; }
}

public record InvoicingPeriod
{
    // BT-73
    [XmlElement(ElementName = "invoicing-period-start-date")]
    public Date? InvoicingPeriodStartDate { get; init; }

    // BT-74
    [XmlElement(ElementName = "invoicing-period-end-date")]
    public Date? InvoicingPeriodEndDate { get; init; }
}

public record DeliverToAddress
{
    // BT-75
    [XmlElement(ElementName = "deliver-to-address-line-1")]
    public required Text DeliverToAddressLine1 { get; init; }

    // BT-76
    [XmlElement(ElementName = "deliver-to-address-line-2")]
    public Text? DeliverToAddressLine2 { get; init; }

    // BT-165
    [XmlElement(ElementName = "deliver-to-address-line-3")]
    public Text? DeliverToAddressLine3 { get; init; }

    // BT-77
    [XmlElement(ElementName = "deliver-to-city")]
    public required Text DeliverToCity { get; init; }

    // BT-78
    [XmlElement(ElementName = "deliver-to-post-code")]
    public required Text DeliverToPostCode { get; init; }

    // BT-79
    [XmlElement(ElementName = "deliver-to-country-subdivision")]
    public Text? DeliverToCountrySubdivision { get; init; }

    // BT-80
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    [XmlElement(ElementName = "deliver-to-country-code")]
    public required Code DeliverToCountryCode { get; init; }
}

public record PaymentInstructions
{
    // BT-81
    // UNTDID-4461
    [XmlElement(ElementName = "payment-means-type-code")]
    public required Code PaymentMeansTypeCode { get; init; }

    // BT-82
    [XmlElement(ElementName = "payment-means-text")]
    public Text? PaymentMeansText { get; init; }

    // BT-83
    [XmlElement(ElementName = "remittance-information")]
    public Text? RemittanceInformation { get; init; }

    // BG-17
    [XmlArray(ElementName = "credit-transfers")]
    [XmlArrayItem(ElementName = "credit-transfer")]
    public required CreditTransfer[] CreditTransfers { get; init; }

    // BG-18
    [XmlElement(ElementName = "payment-card-information")]
    public PaymentCardInformation? PaymentCardInformation { get; init; }

    // BG-19
    [XmlElement(ElementName = "direct-debit")]
    public DirectDebit? DirectDebit { get; init; }
}

public record CreditTransfer
{
    // BT-84
    [XmlElement(ElementName = "payment-account-identifier")]
    public required Identifier PaymentAccountIdentifier { get; init; }

    // BT-85
    [XmlElement(ElementName = "payment-account-name")]
    public Text? PaymentAccountName { get; init; }

    // BT-86
    [XmlElement(ElementName = "payment-service-provider-identifier")]
    public Identifier? PaymentServiceProviderIdentifier { get; init; }
}

public record PaymentCardInformation
{
    // BT-87
    [XmlElement(ElementName = "payment-card-primary-account-number")]
    public required Text PaymentCardPrimaryAccountNumber { get; init; }

    // BT-88
    [XmlElement(ElementName = "payment-card-holder-name")]
    public Text? PaymentCardHolderName { get; init; }
}

public record DirectDebit
{
    // BT-89
    [XmlElement(ElementName = "mandate-reference-identifier")]
    public required Identifier MandateReferenceIdentifier { get; init; }

    // BT-90
    [XmlElement(ElementName = "bank-assigned-creditor-identifier")]
    public required Identifier BankAssignedCreditorIdentifier { get; init; }

    // BT-91
    [XmlElement(ElementName = "debited-account-identifier")]
    public required Identifier DebitedAccountIdentifier { get; init; }
}

public record DocumentLevelAllowance
{
    // BT-92
    [XmlElement(ElementName = "document-level-allowance-amount")]
    public required Amount DocumentLevelAllowanceAmount { get; init; }

    // BT-93
    [XmlElement(ElementName = "document-level-allowance-base-amount")]
    public Amount? DocumentLevelAllowanceBaseAmount { get; init; }

    // BT-94
    [XmlElement(ElementName = "document-level-allowance-percentage")]
    public Percentage? DocumentLevelAllowancePercentage { get; init; }

    // BT-95
    [XmlElement(ElementName = "document-level-allowance-vat-category-code")]
    public required Code DocumentLevelAllowanceVatCategoryCode { get; init; }

    // BT-96
    [XmlElement(ElementName = "document-level-allowance-vat-rate")]
    public Percentage? DocumentLevelAllowanceVatRate { get; init; }

    // BT-97
    [XmlElement(ElementName = "document-level-allowance-reason")]
    public Text? DocumentLevelAllowanceReason { get; init; }

    // BT-98
    [XmlElement(ElementName = "document-level-allowance-reason-code")]
    public Code? DocumentLevelAllowanceReasonCode { get; init; }
}

public record DocumentLevelCharge
{
    // BT-99
    [XmlElement(ElementName = "document-level-charge-amount")]
    public required Amount DocumentLevelChargeAmount { get; init; }

    // BT-100
    [XmlElement(ElementName = "document-level-charge-base-amount")]
    public Amount? DocumentLevelChargeBaseAmount { get; init; }

    // BT-101
    [XmlElement(ElementName = "document-level-charge-percentage")]
    public Percentage? DocumentLevelChargePercentage { get; init; }

    // BT-102
    [XmlElement(ElementName = "document-level-charge-vat-category-code")]
    public required Code DocumentLevelChargeVatCategoryCode { get; init; }

    // BT-103
    [XmlElement(ElementName = "document-level-charge-vat-rate")]
    public Percentage? DocumentLevelChargeVatRate { get; init; }

    // BT-104
    [XmlElement(ElementName = "document-level-charge-reason")]
    public Text? DocumentLevelChargeReason { get; init; }

    // BT-105
    [XmlElement(ElementName = "document-level-charge-reason-code")]
    public Code? DocumentLevelChargeReasonCode { get; init; }
}

public record DocumentTotals
{
    // BT-106
    [XmlElement(ElementName = "sum-of-invoice-line-net-amount")]
    public required Amount SumOfInvoiceLineNetAmount { get; init; }

    // BT-107
    [XmlElement(ElementName = "sum-of-allowances-on-document-level")]
    public Amount? SumOfAllowancesOnDocumentLevel { get; init; }

    // BT-108
    [XmlElement(ElementName = "sum-of-charges-on-document-level")]
    public Amount? SumOfChargesOnDocumentLevel { get; init; }

    // BT-109
    [XmlElement(ElementName = "invoice-total-amount-without-vat")]
    public required Amount InvoiceTotalAmountWithoutVat { get; init; }

    // BT-110
    [XmlElement(ElementName = "invoice-total-vat-amount")]
    public Amount? InvoiceTotalVatAmount { get; init; }

    // BT-111
    [XmlElement(ElementName = "invoice-total-vat-amount-in-accounting-currency")]
    public Amount? InvoiceTotalVatAmountInAccountingCurrency { get; init; }

    // BT-112
    [XmlElement(ElementName = "invoice-total-amount-with-vat")]
    public required Amount InvoiceTotalAmountWithVat { get; init; }

    // BT-113
    [XmlElement(ElementName = "paid-amount")]
    public Amount? PaidAmount { get; init; }

    // BT-114
    [XmlElement(ElementName = "rounding-amount")]
    public Amount? RoundingAmount { get; init; }

    // BT-115
    [XmlElement(ElementName = "amount-due-for-payment")]
    public required Amount AmountDueForPayment { get; init; }
}

public record VatBreakdown
{
    // BT-116
    [XmlElement(ElementName = "vat-category-taxable-amount")]
    public required Amount VatCategoryTaxableAmount { get; init; }

    // BT-117
    [XmlElement(ElementName = "vat-category-tax-amount")]
    public required Amount VatCategoryTaxAmount { get; init; }

    // BT-118
    // UNTDID 5305
    [XmlElement(ElementName = "vat-category-code")]
    public required Code VatCategoryCode { get; init; }

    // BT-119
    [XmlElement(ElementName = "vat-category-rate")]
    public required Percentage VatCategoryRate { get; init; }

    // BT-120
    [XmlElement(ElementName = "vat-exemption-reason")]
    public Text? VatExemptionReasonText { get; init; }

    // BT-121
    // VATEX Vat exemption reason code list
    [XmlElement(ElementName = "vat-exemption-reason-code")]
    public Code? VatExemptionReasonCode { get; init; }
}

public record AdditionalSupportingDocument
{
    // BT-122
    [XmlElement(ElementName = "supporting-document-reference")]
    public required DocumentReference SupportingDocumentReference { get; init; }

    // BT-123
    [XmlElement(ElementName = "supporting-document-description")]
    public Text? SupportingDocumentDescription { get; init; }

    // BT-124
    [XmlElement(ElementName = "external-document-location")]
    public Text? ExternalDocumentLocation { get; init; }

    // BT-125
    [XmlElement(ElementName = "attached-document")]
    public BinaryObject? AttachedDocument { get; init; }
}

public record InvoiceLine
{
    // BT-126
    [XmlElement(ElementName = "invoice-line-identifier")]
    public required Identifier InvoiceLineIdentifier { get; init; }

    // BT-127
    [XmlElement(ElementName = "invoice-line-note")]
    public Text? InvoiceLineNote { get; init; }

    // BT-128
    [XmlElement(ElementName = "invoice-line-object-identifier")]
    public Identifier? InvoiceLineObjectIdentifier { get; init; }

    // BT-129
    [XmlElement(ElementName = "invoiced-quantity")]
    public required Quantity InvoicedQuantity { get; init; }

    // BT-130
    [XmlElement(ElementName = "invoiced-quantity-unit-of-measure")]
    public required Code InvoicedQuantityUnitOfMeasure { get; init; }

    // BT-131
    [XmlElement(ElementName = "invoice-line-net-amount")]
    public required Amount InvoiceLineNetAmount { get; init; }

    // BT-132
    [XmlElement(ElementName = "referenced-purchase-order-line-reference")]
    public DocumentReference? ReferencedPurchaseOrderLineReference { get; init; }

    // BT-133
    [XmlElement(ElementName = "invoice-line-buyer-accounting-reference")]
    public Text? InvoiceLineBuyerAccountingReference { get; init; }

    // BG-26
    [XmlElement(ElementName = "invoice-line-period")]
    public InvoiceLinePeriod? InvoiceLinePeriod { get; init; }

    // BG-27
    [XmlArray(ElementName = "invoice-line-allowances")]
    [XmlArrayItem(ElementName = "invoice-line-allowance")]
    public required InvoiceLineAllowance[] InvoiceLineAllowances { get; init; }

    // BG-28
    [XmlArray(ElementName = "invoice-line-charges")]
    [XmlArrayItem(ElementName = "invoice-line-charge")]
    public required InvoiceLineCharge[] InvoiceLineCharges { get; init; }

    // BG-29
    [XmlElement(ElementName = "price-details")]
    public required PriceDetails PriceDetails { get; init; }

    // BG-30
    [XmlElement(ElementName = "line-vat-information")]
    public required LineVatInformation LineVatInformation { get; init; }

    // BG-31
    [XmlElement(ElementName = "item-information")]
    public required ItemInformation ItemInformation { get; init; }
}

public record InvoiceLinePeriod
{
    // BT-134
    [XmlElement(ElementName = "invoice-line-period-start-date")]
    public Date? InvoiceLinePeriodStartDate { get; init; }

    // BT-135
    [XmlElement(ElementName = "invoice-line-period-end-date")]
    public Date? InvoiceLinePeriodEndDate { get; init; }
}

public record InvoiceLineAllowance
{
    // BT-136
    [XmlElement(ElementName = "invoice-line-allowance-amount")]
    public required Amount InvoiceLineAllowanceAmount { get; init; }

    // BT-137
    [XmlElement(ElementName = "invoice-line-allowance-base-amount")]
    public Amount? InvoiceLineAllowanceBaseAmount { get; init; }

    // BT-138
    [XmlElement(ElementName = "invoice-line-allowance-percentage")]
    public Percentage? InvoiceLineAllowancePercentage { get; init; }

    // BT-139
    [XmlElement(ElementName = "invoice-line-allowance-reason")]
    public Text? InvoiceLineAllowanceReason { get; init; }

    // BT-140
    [XmlElement(ElementName = "invoice-line-allowance-reason-code")]
    public Code? InvoiceLineAllowanceReasonCode { get; init; }
}

public record InvoiceLineCharge
{
    // BT-141
    [XmlElement(ElementName = "invoice-line-charge-amount")]
    public required Amount InvoiceLineChargeAmount { get; init; }

    // BT-142
    [XmlElement(ElementName = "invoice-line-charge-base-amount")]
    public Amount? InvoiceLineChargeBaseAmount { get; init; }

    // BT-143
    [XmlElement(ElementName = "invoice-line-charge-percentage")]
    public Percentage? InvoiceLineChargePercentage { get; init; }

    // BT-144
    [XmlElement(ElementName = "invoice-line-charge-reason")]
    public Text? InvoiceLineChargeReason { get; init; }

    // BT-145
    [XmlElement(ElementName = "invoice-line-charge-reason-code")]
    public Code? InvoiceLineChargeReasonCode { get; init; }
}

public record PriceDetails
{
    // BT-146
    [XmlElement(ElementName = "item-net-price")]
    public required UnitPriceAmount ItemNetPrice { get; init; }

    // BT-147
    [XmlElement(ElementName = "item-price-discount")]
    public UnitPriceAmount? ItemPriceDiscount { get; init; }

    // BT-148
    [XmlElement(ElementName = "item-gross-price")]
    public UnitPriceAmount? ItemGrossPrice { get; init; }

    // BT-149
    [XmlElement(ElementName = "item-price-base-quantity")]
    public Quantity? ItemPriceBaseQuantity { get; init; }

    // BT-150
    // UN/ECE Rec No 20,21
    [XmlElement(ElementName = "item-price-base-quantity-unit-of-measure")]
    public Code? ItemPriceBaseQuantityUnitOfMeasure { get; init; }
}

public record LineVatInformation
{
    // BT-151
    // UNTDID 5305
    [XmlElement(ElementName = "invoiced-item-vat-category-code")]
    public required Code InvoicedItemVatCategoryCode { get; init; }

    // BT-152
    [XmlElement(ElementName = "invoiced-item-vat-rate")]
    public Percentage? InvoicedItemVatRate { get; init; }
}

public record ItemInformation
{
    // BT-153
    [XmlElement(ElementName = "item-name")]
    public required Text ItemName { get; init; }

    // BT-154
    [XmlElement(ElementName = "item-description")]
    public Text? ItemDescription { get; init; }

    // BT-155
    [XmlElement(ElementName = "item-sellers-identifier")]
    public Identifier? ItemSellersIdentifier { get; init; }

    // BT-156
    [XmlElement(ElementName = "item-buyers-identifier")]
    public Identifier? ItemBuyersIdentifier { get; init; }

    // BT-157
    [XmlElement(ElementName = "item-standard-identifier")]
    public Identifier? ItemStandardIdentifier { get; init; }

    // BT-158
    // UNTDID 7143
    [XmlArray(ElementName = "item-classification-identifiers")]
    [XmlArrayItem(ElementName = "item-classification-identifier")]
    public required Identifier[] ItemClassificationIdentifiers { get; init; }

    // BT-159
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2 representation
    [XmlElement(ElementName = "item-country-of-origin")]
    public Code? ItemCountryOfOrigin { get; init; }

    // BG-32
    [XmlArray(ElementName = "item-attributes")]
    [XmlArrayItem(ElementName = "item-attribute")]
    public required ItemAttribute[] ItemAttributes { get; init; }
}

public record ItemAttribute
{
    // BT-160
    [XmlElement(ElementName = "item-attribute-name")]
    public required Text ItemAttributeName { get; init; }

    // BT-161
    [XmlElement(ElementName = "item-attribute-value")]
    public required Text ItemAttributeValue { get; init; }
}
