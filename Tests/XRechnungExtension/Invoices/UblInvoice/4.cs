using System;
using En16931.Model.Primitives;
using En16931.Model.XRechnungExtension;
using M = En16931.Model;
using S = En16931.Specs;

namespace Tests.XRechnungExtension.Invoices;

public partial class UblInvoices
{
    public static readonly Invoice Invoice4 = new Invoice
    {
        InvoiceNumber = new Identifier("123456XX"),
        InvoiceIssueDate = new Date(new DateTime(2016, 4, 4)),
        InvoiceTypeCode = new Code("380"),
        InvoiceCurrencyCode = new Code("EUR"),
        VatAccountingCurrencyCode = null,
        ValueAddedTaxPointDate = null,
        ValueAddedTaxPointDateCode = null,
        PaymentDueDate = null,
        BuyerReference = new Text("04011000-12345-34"),
        ProjectReference = null,
        ContractReference = null,
        PurchaseOrderReference = null,
        SalesOrderReference = null,
        ReceivingAdviceReference = null,
        DespatchAdviceReference = null,
        TenderOrLotReference = null,
        InvoicedObjectIdentifier = null,
        BuyerAccountingReference = null,
        PaymentTerms = new Text("Zahlbar sofort ohne Abzug."),
        InvoiceNotes = [
            new M.InvoiceNote {
                InvoiceNoteSubjectCode = new Code("ADU"),
                Note = new Text("Es gelten unsere Allgem. Geschäftsbedingungen, die Sie unter […] finden."),
            },
        ],
        ProcessControl = new M.ProcessControl<S.XRechnungExtension>
        {
            BusinessProcessType = new Text("urn:fdc:peppol.eu:2017:poacc:billing:01:1.0"),
        },
        PrecedingInvoiceReferences = [],
        Seller = new M.Seller
        {
            SellerName = new Text("[Seller name]"),
            SellerTradingName = new Text("[Seller trading name]"),
            SellerIdentifiers = [],
            SellerLegalRegistrationIdentifier = new Identifier("[HRA-Eintrag]"),
            SellerVatIdentifier = new Identifier("DE 123456789"),
            SellerTaxRegistrationIdentifier = null,
            SellerAdditionalLegalInformation = new Text("123/456/7890, HRA-Eintrag in […]"),
            SellerElectronicAddress = new Identifier("seller@email.de", "EM"),
            SellerPostalAddress = new M.SellerPostalAddress
            {
                SellerAddressLine1 = new Text("[Seller address line 1]"),
                SellerAddressLine2 = null,
                SellerAddressLine3 = null,
                SellerCity = new Text("[Seller city]"),
                SellerPostCode = new Text("12345"),
                SellerCountrySubdivision = null,
                SellerCountryCode = new Code("DE"),
            },
            SellerContact = new M.SellerContact
            {
                SellerContactPoint = new Text("nicht vorhanden"),
                SellerContactTelephoneNumber = new Text("+49 1234-5678"),
                SellerContactEmailAddress = new Text("seller@email.de"),
            },
        },
        Buyer = new M.Buyer
        {
            BuyerName = new Text("[Buyer name]"),
            BuyerTradingName = null,
            BuyerIdentifier = new Identifier("[Buyer identifier]"),
            BuyerLegalRegistrationIdentifier = null,
            BuyerVatIdentifier = null,
            BuyerElectronicAddress = new Identifier("buyer@info.de", "EM"),
            BuyerPostalAddress = new M.BuyerPostalAddress
            {
                BuyerAddressLine1 = new Text("[Buyer address line 1]"),
                BuyerAddressLine2 = null,
                BuyerAddressLine3 = null,
                BuyerCity = new Text("[Buyer city]"),
                BuyerPostCode = new Text("12345"),
                BuyerCountrySubdivision = null,
                BuyerCountryCode = new Code("DE"),
            },
            BuyerContact = null,
        },
        Payee = null,
        SellerTaxRepresentativeParty = null,
        DeliveryInformation = null,
        PaymentInstructions = new M.PaymentInstructions
        {
            PaymentMeansTypeCode = new Code("58"),
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
            SumOfInvoiceLineNetAmount = new Amount(314.86m),
            SumOfAllowancesOnDocumentLevel = null,
            SumOfChargesOnDocumentLevel = null,
            InvoiceTotalAmountWithoutVat = new Amount(314.86m),
            InvoiceTotalVatAmount = new Amount(22.04m),
            InvoiceTotalVatAmountInAccountingCurrency = null,
            InvoiceTotalAmountWithVat = new Amount(336.9m),
            PaidAmount = null,
            RoundingAmount = null,
            AmountDueForPayment = new Amount(366.86m)
        },
        VatBreakdown = [
            new M.VatBreakdown {
                VatCategoryTaxableAmount = new Amount(314.86m),
                VatCategoryTaxAmount = new Amount(22.04m),
                VatCategoryCode = new Code("S"),
                VatCategoryRate = new Percentage(7m),
                VatExemptionReasonText = null,
                VatExemptionReasonCode = null,
            },
        ],
        AdditionalSupportingDocuments = [],
        InvoiceLines = [
            new InvoiceLine {
                InvoiceLineIdentifier = new Identifier("Zeitschrift [...]"),
                InvoiceLineNote = new Text("Die letzte Lieferung im Rahmen des abgerechneten Abonnements erfolgt in 12/2016 Lieferung erfolgt / erfolgte direkt vom Verlag"),
                InvoiceLineObjectIdentifier = null,
                InvoicedQuantity = new Quantity(1m),
                InvoicedQuantityUnitOfMeasureCode = new Code("XPP"),
                InvoiceLineNetAmount = new Amount(288.79m),
                ReferencedPurchaseOrderLineReference = new DocumentReference("6171175.1"),
                InvoiceLineBuyerAccountingReference = null,
                InvoiceLinePeriod = new M.InvoiceLinePeriod {
                    InvoiceLinePeriodStartDate = new Date(new DateTime(2016, 1, 1)),
                    InvoiceLinePeriodEndDate = new Date(new DateTime(2016, 12, 31)),
                },
                InvoiceLineAllowances = [],
                InvoiceLineCharges = [],
                PriceDetails = new M.PriceDetails {
                    ItemNetPrice = new UnitPriceAmount(288.79m),
                    ItemPriceDiscount = null,
                    ItemGrossPrice = null,
                    ItemPriceBaseQuantity = null,
                    ItemPriceBaseQuantityUnitOfMeasureCode = null,
                },
                LineVatInformation = new M.LineVatInformation {
                    InvoicedItemVatCategoryCode = new Code("S"),
                    InvoicedItemVatRate = new Percentage(7m),
                },
                ItemInformation = new M.ItemInformation {
                    ItemName = new Text("Zeitschrift [...]"),
                    ItemDescription = new Text("Zeitschrift Inland"),
                    ItemSellersIdentifier = new Identifier("246"),
                    ItemBuyersIdentifier = null,
                    ItemStandardIdentifier = null,
                    ItemClassificationIdentifiers = [
                        new Identifier("0721-880X", "IB"),
                    ],
                    ItemCountryOfOrigin = null,
                    ItemAttributes = [],
                },
                SubInvoiceLines = [],
            },
            new InvoiceLine {
                InvoiceLineIdentifier = new Identifier("Porto + Versandkosten"),
                InvoiceLineNote = null,
                InvoiceLineObjectIdentifier = null,
                InvoicedQuantity = new Quantity(1m),
                InvoicedQuantityUnitOfMeasureCode = new Code("XPP"),
                InvoiceLineNetAmount = new Amount(26.07m),
                ReferencedPurchaseOrderLineReference = null,
                InvoiceLineBuyerAccountingReference = null,
                InvoiceLinePeriod = null,
                InvoiceLineAllowances = [],
                InvoiceLineCharges = [],
                PriceDetails = new M.PriceDetails {
                    ItemNetPrice = new UnitPriceAmount(26.07m),
                    ItemPriceDiscount = null,
                    ItemGrossPrice = null,
                    ItemPriceBaseQuantity = null,
                    ItemPriceBaseQuantityUnitOfMeasureCode = null,
                },
                LineVatInformation = new M.LineVatInformation {
                    InvoicedItemVatCategoryCode = new Code("S"),
                    InvoicedItemVatRate = new Percentage(7m),
                },
                ItemInformation = new M.ItemInformation {
                    ItemName = new Text("Porto + Versandkosten"),
                    ItemDescription = null,
                    ItemSellersIdentifier = null,
                    ItemBuyersIdentifier = null,
                    ItemStandardIdentifier = null,
                    ItemClassificationIdentifiers = [],
                    ItemCountryOfOrigin = null,
                    ItemAttributes = [],
                },
                SubInvoiceLines = [],
            },
        ],
        ThirdPartyPayments = [
            new ThirdPartyPayment {
                ThirdPartyPaymentType = new Text("MobilesBezahlen"),
                ThirdPartyPaymentAmount = new Amount(19.96m),
                ThirdPartyPaymentDescription = new Text("Mobiles Bezahlen (Brutto-Forderung für Fremdleistungen)"),
            },
            new ThirdPartyPayment {
                ThirdPartyPaymentType = new Text("MobilesBezahlen"),
                ThirdPartyPaymentAmount = new Amount(10m),
                ThirdPartyPaymentDescription = new Text("Mobiles Bezahlen (Brutto-Forderung für Fremdleistungen)"),
            },
        ],
    };
}
