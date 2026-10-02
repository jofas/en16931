using System;
using En16931.Model;
using En16931.Model.Primitives;
using S = En16931.Specs;

namespace Tests.XRechnung.Invoices;

public partial class CiiD16bs
{
    public static readonly Invoice<S.XRechnung> Invoice5 = new Invoice<S.XRechnung>
    {
        InvoiceNumber = new Identifier("1"),
        InvoiceIssueDate = new Date(new DateTime(2026, 9, 9)),
        InvoiceTypeCode = new Code("380"),
        InvoiceCurrencyCode = new Code("EUR"),
        VatAccountingCurrencyCode = null,
        ValueAddedTaxPointDate = null,
        ValueAddedTaxPointDateCode = null,
        PaymentDueDate = null,
        BuyerReference = new Text("90000000-03083-72"),
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
        InvoiceNotes = [],
        ProcessControl = new ProcessControl<S.XRechnung>
        {
            BusinessProcessType = new Text("urn:fdc:peppol.eu:2017:poacc:billing:01:1.0"),
        },
        PrecedingInvoiceReferences = [],
        Seller = new Seller
        {
            SellerName = new Text("Seller"),
            SellerTradingName = null,
            SellerIdentifiers = [],
            SellerLegalRegistrationIdentifier = new Identifier("123456789", "0088"),
            SellerVatIdentifier = null,
            SellerTaxRegistrationIdentifier = null,
            SellerAdditionalLegalInformation = null,
            SellerElectronicAddress = new Identifier("rechnungsausgang@test.com", "EM"),
            SellerPostalAddress = new SellerPostalAddress
            {
                SellerAddressLine1 = null,
                SellerAddressLine2 = null,
                SellerAddressLine3 = null,
                SellerCity = new Text("[Seller city]"),
                SellerPostCode = new Text("12345"),
                SellerCountrySubdivision = null,
                SellerCountryCode = new Code("DE"),
            },
            SellerContact = new SellerContact
            {
                SellerContactPoint = new Text("Tim Tester"),
                SellerContactTelephoneNumber = new Text("012 3456789"),
                SellerContactEmailAddress = new Text("tim.tester@test.com"),
            },
        },
        Buyer = new Buyer
        {
            BuyerName = new Text("Buyer"),
            BuyerTradingName = null,
            BuyerIdentifier = null,
            BuyerLegalRegistrationIdentifier = null,
            BuyerVatIdentifier = null,
            BuyerElectronicAddress = new Identifier("rechnungseingang@test.de", "EM"),
            BuyerPostalAddress = new BuyerPostalAddress
            {
                BuyerAddressLine1 = null,
                BuyerAddressLine2 = null,
                BuyerAddressLine3 = null,
                BuyerCity = new Text("[Buyer city]"),
                BuyerPostCode = new Text("98765"),
                BuyerCountrySubdivision = null,
                BuyerCountryCode = new Code("DE"),
            },
            BuyerContact = null,
        },
        Payee = null,
        SellerTaxRepresentativeParty = null,
        DeliveryInformation = null,
        PaymentInstructions = new PaymentInstructions
        {
            PaymentMeansTypeCode = new Code("58"),
            PaymentMeansText = new Text("[Payment means text]"),
            RemittanceInformation = null,
            CreditTransfers = [
                new CreditTransfer {
                    PaymentAccountIdentifier = new Identifier("DE75512108001245126199"),
                    PaymentAccountName = new Text("[Payment account name]"),
                    PaymentServiceProviderIdentifier = new Identifier("[BIC]"),
                },
            ],
            PaymentCardInformation = null,
            DirectDebit = null,
        },
        DocumentLevelAllowances = [],
        DocumentLevelCharges = [],
        DocumentTotals = new DocumentTotals
        {
            SumOfInvoiceLineNetAmount = new Amount(1m),
            SumOfAllowancesOnDocumentLevel = null,
            SumOfChargesOnDocumentLevel = null,
            InvoiceTotalAmountWithoutVat = new Amount(1m),
            InvoiceTotalVatAmount = new Amount(0m),
            InvoiceTotalVatAmountInAccountingCurrency = null,
            InvoiceTotalAmountWithVat = new Amount(1m),
            PaidAmount = null,
            RoundingAmount = null,
            AmountDueForPayment = new Amount(1m),
        },
        VatBreakdown = [
            new VatBreakdown {
                VatCategoryTaxableAmount = new Amount(1m),
                VatCategoryTaxAmount = new Amount(0m),
                VatCategoryCode = new Code("O"),
                VatCategoryRate = new Percentage(0m),
                VatExemptionReasonText = null,
                VatExemptionReasonCode = new Code("VATEX-EU-132-1A"),
            },
        ],
        AdditionalSupportingDocuments = [],
        InvoiceLines = [
            new InvoiceLine {
                InvoiceLineIdentifier = new Identifier("1"),
                InvoiceLineNote = null,
                InvoiceLineObjectIdentifier = null,
                InvoicedQuantity = new Quantity(1m),
                InvoicedQuantityUnitOfMeasureCode = new Code("H87"),
                InvoiceLineNetAmount = new Amount(1m),
                ReferencedPurchaseOrderLineReference = null,
                InvoiceLineBuyerAccountingReference = null,
                InvoiceLinePeriod = null,
                InvoiceLineAllowances = [],
                InvoiceLineCharges = [],
                PriceDetails = new PriceDetails {
                    ItemNetPrice = new UnitPriceAmount(1m),
                    ItemPriceDiscount = null,
                    ItemGrossPrice = null,
                    ItemPriceBaseQuantity = null,
                    ItemPriceBaseQuantityUnitOfMeasureCode = null,
                },
                LineVatInformation = new LineVatInformation {
                    InvoicedItemVatCategoryCode = new Code("O"),
                    InvoicedItemVatRate = null,
                },
                ItemInformation = new ItemInformation {
                    ItemName = new Text("Some product"),
                    ItemDescription = null,
                    ItemSellersIdentifier = null,
                    ItemBuyersIdentifier = null,
                    ItemStandardIdentifier = null,
                    ItemClassificationIdentifiers = [],
                    ItemCountryOfOrigin = null,
                    ItemAttributes = [],
                },
            },
        ],
    };
}
