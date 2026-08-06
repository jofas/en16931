using System;
using En16931.Model.Primitives;
using En16931.Model.XRechnungExtension;
using M = En16931.Model;

namespace Tests.XRechnungExtension.Invoices;

public partial class Ciis
{
    public static readonly Invoice Invoice1 = new Invoice
    {
        InvoiceNumber = new Identifier("2020-1"),
        InvoiceIssueDate = new Date(new DateTime(2020, 1, 3)),
        InvoiceTypeCode = new Code("380"),
        InvoiceCurrencyCode = new Code("EUR"),
        VatAccountingCurrencyCode = null,
        ValueAddedTaxPointDate = null,
        ValueAddedTaxPointDateCode = null,
        PaymentDueDate = new Date(new DateTime(2020, 1, 17)),
        BuyerReference = new Text("Leitweg-ID"),
        ProjectReference = null,
        ContractReference = null,
        PurchaseOrderReference = null,
        SalesOrderReference = null,
        ReceivingAdviceReference = null,
        DespatchAdviceReference = null,
        TenderOrLotReference = null,
        InvoicedObjectIdentifier = null,
        BuyerAccountingReference = null,
        PaymentTerms = null,
        InvoiceNotes = [
            new M.InvoiceNote {
                InvoiceNoteSubjectCode = null,
                Note = new Text("Eine Testrechnung für das Projekt DiGA-Rechnung."),
            },
        ],
        ProcessControl = new ProcessControl
        {
            BusinessProcessType = new Text("urn:fdc:peppol.eu:2017:poacc:billing:01:1.0"),
        },
        PrecedingInvoiceReferences = [],
        Seller = new M.Seller
        {
            SellerName = new Text("Rechnungssteller"),
            SellerTradingName = null,
            SellerIdentifiers = [
                new Identifier("TEST_RECHNUNGSSTELLER"),
                new Identifier("987654321", "XR03"),
            ],
            SellerLegalRegistrationIdentifier = null,
            SellerVatIdentifier = new Identifier("DE 123 456 789"),
            SellerTaxRegistrationIdentifier = null,
            SellerAdditionalLegalInformation = null,
            SellerElectronicAddress = new Identifier("seller@email.de", "EM"),
            SellerPostalAddress = new M.SellerPostalAddress
            {
                SellerAddressLine1 = new Text("Musterstraße 1"),
                SellerAddressLine2 = null,
                SellerAddressLine3 = null,
                SellerCity = new Text("Berlin"),
                SellerPostCode = new Text("01234"),
                SellerCountrySubdivision = null,
                SellerCountryCode = new Code("DE"),
            },
            SellerContact = new M.SellerContact
            {
                SellerContactPoint = new Text("Max Mustermann"),
                SellerContactTelephoneNumber = new Text("+49 000 001 0001"),
                SellerContactEmailAddress = new Text("max.mustermann@rechnungssteller.de"),
            },
        },
        Buyer = new M.Buyer
        {
            BuyerName = new Text("Rechnungsempfänger"),
            BuyerTradingName = null,
            BuyerIdentifier = new Identifier("123456789", "XR03"),
            BuyerLegalRegistrationIdentifier = null,
            BuyerVatIdentifier = null,
            BuyerElectronicAddress = new Identifier("buyer@info.de", "EM"),
            BuyerPostalAddress = new M.BuyerPostalAddress
            {
                BuyerAddressLine1 = new Text("Musterstraße 2"),
                BuyerAddressLine2 = null,
                BuyerAddressLine3 = null,
                BuyerCity = new Text("Berlin"),
                BuyerPostCode = new Text("01234"),
                BuyerCountrySubdivision = null,
                BuyerCountryCode = new Code("DE"),
            },
            BuyerContact = null,
        },
        Payee = new M.Payee
        {
            PayeeName = new Text("Payee"),
            PayeeIdentifier = new Identifier("987654322", "XR03"),
            PayeeLegalRegistrationIdentifier = null,
        },
        SellerTaxRepresentativeParty = null,
        DeliveryInformation = new M.DeliveryInformation
        {
            DeliverToPartyName = null,
            DeliverToLocationIdentifier = null,
            ActualDeliveryDate = new Date(new DateTime(2020, 1, 2)),
            InvoicingPeriod = null,
            DeliverToAddress = null,
        },
        PaymentInstructions = new M.PaymentInstructions
        {
            PaymentMeansTypeCode = new Code("30"),
            PaymentMeansText = null,
            RemittanceInformation = null,
            CreditTransfers = [
                new M.CreditTransfer {
                    PaymentAccountIdentifier = new Identifier("DE75512108001245126199"),
                    PaymentAccountName = null,
                    PaymentServiceProviderIdentifier = null,
                },
            ],
            PaymentCardInformation = null,
            DirectDebit = null,
        },
        DocumentLevelAllowances = [],
        DocumentLevelCharges = [],
        DocumentTotals = new M.DocumentTotals
        {
            SumOfInvoiceLineNetAmount = new Amount(100m),
            SumOfAllowancesOnDocumentLevel = null,
            SumOfChargesOnDocumentLevel = null,
            InvoiceTotalAmountWithoutVat = new Amount(100m),
            InvoiceTotalVatAmount = new Amount(19m),
            InvoiceTotalVatAmountInAccountingCurrency = null,
            InvoiceTotalAmountWithVat = new Amount(119m),
            PaidAmount = null,
            RoundingAmount = null,
            AmountDueForPayment = new Amount(119m),
        },
        VatBreakdown = [
            new M.VatBreakdown {
                VatCategoryTaxableAmount = new Amount(100m),
                VatCategoryTaxAmount = new Amount(19m),
                VatCategoryCode = new Code("S"),
                VatCategoryRate = new Percentage(19m),
                VatExemptionReasonText = null,
                VatExemptionReasonCode = null,
            },
        ],
        AdditionalSupportingDocuments = [],
        InvoiceLines = [
            new InvoiceLine {
                InvoiceLineIdentifier = new Identifier("TEST_POSITION_01"),
                InvoiceLineNote = null,
                InvoiceLineObjectIdentifier = null,
                InvoicedQuantity = new Quantity(1m),
                InvoicedQuantityUnitOfMeasureCode = new Code("C62"),
                InvoiceLineNetAmount = new Amount(100m),
                ReferencedPurchaseOrderLineReference = null,
                InvoiceLineBuyerAccountingReference = null,
                InvoiceLinePeriod = null,
                InvoiceLineAllowances = [],
                InvoiceLineCharges = [],
                PriceDetails = new M.PriceDetails {
                    ItemNetPrice = new UnitPriceAmount(100m),
                    ItemPriceDiscount = null,
                    ItemGrossPrice = null,
                    ItemPriceBaseQuantity = null,
                    ItemPriceBaseQuantityUnitOfMeasureCode = null,
                },
                LineVatInformation = new M.LineVatInformation {
                    InvoicedItemVatCategoryCode = new Code("S"),
                    InvoicedItemVatRate = new Percentage(19m),
                },
                ItemInformation = new M.ItemInformation {
                    ItemName = new Text("Tinnitus Rex"),
                    ItemDescription = new Text("Ein Artikel für Testrechnungen."),
                    ItemSellersIdentifier = null,
                    ItemBuyersIdentifier = new Identifier("ABCDEFGHIJKLMNOP"),
                    ItemStandardIdentifier = new Identifier("12345678", "XR01"),
                    ItemClassificationIdentifiers = [],
                    ItemCountryOfOrigin = null,
                    ItemAttributes = [],
                },
                SubInvoiceLines = [],
            },
        ],
        ThirdPartyPayments = [],
    };
}
