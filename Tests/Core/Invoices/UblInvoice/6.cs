using System;
using En16931.Model;
using En16931.Model.Primitives;
using S = En16931.Specs;

namespace Tests.Core.Invoices;

public partial class UblInvoices
{
    public static readonly Invoice<S.Core> Invoice6 = new Invoice<S.Core>
    {
        InvoiceNumber = new Identifier("1"),
        InvoiceIssueDate = new Date(new DateTime(2026, 9, 9)),
        InvoiceTypeCode = new Code("380"),
        InvoiceCurrencyCode = new Code("EUR"),
        VatAccountingCurrencyCode = null,
        ValueAddedTaxPointDate = null,
        ValueAddedTaxPointDateCode = null,
        PaymentDueDate = null,
        BuyerReference = null,
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
        ProcessControl = new ProcessControl<S.Core>
        {
            BusinessProcessType = null,
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
            SellerElectronicAddress = null,
            SellerPostalAddress = new SellerPostalAddress
            {
                SellerAddressLine1 = null,
                SellerAddressLine2 = null,
                SellerAddressLine3 = null,
                SellerCity = null,
                SellerPostCode = null,
                SellerCountrySubdivision = null,
                SellerCountryCode = new Code("DE"),
            },
            SellerContact = null,
        },
        Buyer = new Buyer
        {
            BuyerName = new Text("Buyer"),
            BuyerTradingName = null,
            BuyerIdentifier = null,
            BuyerLegalRegistrationIdentifier = null,
            BuyerVatIdentifier = null,
            BuyerElectronicAddress = null,
            BuyerPostalAddress = new BuyerPostalAddress
            {
                BuyerAddressLine1 = null,
                BuyerAddressLine2 = null,
                BuyerAddressLine3 = null,
                BuyerCity = null,
                BuyerPostCode = null,
                BuyerCountrySubdivision = null,
                BuyerCountryCode = new Code("DE"),
            },
            BuyerContact = null,
        },
        Payee = null,
        SellerTaxRepresentativeParty = null,
        DeliveryInformation = null,
        PaymentInstructions = null,
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
                VatCategoryRate = null,
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
