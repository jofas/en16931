using System;
using En16931.Model;
using En16931.Model.Primitives;
using S = En16931.Specs;

namespace Tests.FacturXEn16931.Invoices;

public partial class CiiD22bs
{
    public static readonly Invoice<S.FacturXEn16931> Invoice1 = new Invoice<S.FacturXEn16931>
    {
        InvoiceNumber = new Identifier("1"),
        InvoiceIssueDate = new Date(new DateTime(2026, 10, 27)),
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
        InvoiceNotes = [
            new InvoiceNote {
                InvoiceNoteSubjectCode = new Code("REG"),
                Note = new Text("[Invoice note 1]"),
            },
            new InvoiceNote {
                InvoiceNoteSubjectCode = null,
                Note = new Text("[Invoice note 2]"),
            },
        ],
        ProcessControl = new ProcessControl<S.FacturXEn16931>
        {
            BusinessProcessType = null,
        },
        PrecedingInvoiceReferences = [],
        Seller = new Seller
        {
            SellerName = new Text("Seller"),
            SellerTradingName = null,
            SellerIdentifiers = [],
            SellerLegalRegistrationIdentifier = null,
            SellerVatIdentifier = new Identifier("DE123456789"),
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
            InvoiceTotalVatAmount = new Amount(0.19m),
            InvoiceTotalVatAmountInAccountingCurrency = null,
            InvoiceTotalAmountWithVat = new Amount(1.19m),
            PaidAmount = null,
            RoundingAmount = null,
            AmountDueForPayment = new Amount(1.19m),
        },
        VatBreakdown = [
            new VatBreakdown {
                VatCategoryTaxableAmount = new Amount(1m),
                VatCategoryTaxAmount = new Amount(0.19m),
                VatCategoryCode = new Code("S"),
                VatCategoryRate = new Percentage(19m),
                VatExemptionReasonText = null,
                VatExemptionReasonCode = null,
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
                    InvoicedItemVatCategoryCode = new Code("S"),
                    InvoicedItemVatRate = new Percentage(19m),
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
