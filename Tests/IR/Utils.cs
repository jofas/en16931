using System.Linq;
using En16931;
using En16931.Model;
using En16931.Spec;
using Xunit;

namespace Tests.IR;

public static class Utils
{
    public static void RefinedInvoiceComparison<T>(Invoice<T> expected, Invoice<T> invoice) where T : ISpecification
    {
        Assert.Equal(expected.InvoiceNumber, invoice.InvoiceNumber);
        Assert.Equal(expected.InvoiceIssueDate, invoice.InvoiceIssueDate);
        Assert.Equal(expected.InvoiceTypeCode, invoice.InvoiceTypeCode);
        Assert.Equal(expected.InvoiceCurrencyCode, invoice.InvoiceCurrencyCode);
        Assert.Equal(expected.VatAccountingCurrencyCode, invoice.VatAccountingCurrencyCode);
        Assert.Equal(expected.ValueAddedTaxPointDate, invoice.ValueAddedTaxPointDate);
        Assert.Equal(expected.ValueAddedTaxPointDateCode, invoice.ValueAddedTaxPointDateCode);
        Assert.Equal(expected.PaymentDueDate, invoice.PaymentDueDate);
        Assert.Equal(expected.BuyerReference, invoice.BuyerReference);
        Assert.Equal(expected.ProjectReference, invoice.ProjectReference);
        Assert.Equal(expected.ContractReference, invoice.ContractReference);
        Assert.Equal(expected.PurchaseOrderReference, invoice.PurchaseOrderReference);
        Assert.Equal(expected.SalesOrderReference, invoice.SalesOrderReference);
        Assert.Equal(expected.ReceivingAdviceReference, invoice.ReceivingAdviceReference);
        Assert.Equal(expected.DespatchAdviceReference, invoice.DespatchAdviceReference);
        Assert.Equal(expected.TenderOrLotReference, invoice.TenderOrLotReference);
        Assert.Equal(expected.InvoicedObjectIdentifier, invoice.InvoicedObjectIdentifier);
        Assert.Equal(expected.BuyerAccountingReference, invoice.BuyerAccountingReference);
        Assert.Equal(expected.PaymentTerms, invoice.PaymentTerms);
        Assert.Equal(expected.InvoiceNotes, invoice.InvoiceNotes);
        Assert.Equal(expected.ProcessControl, invoice.ProcessControl);
        Assert.Equal(expected.PrecedingInvoiceReferences, invoice.PrecedingInvoiceReferences);

        Assert.Equal(expected.Seller.SellerName, invoice.Seller.SellerName);
        Assert.Equal(expected.Seller.SellerTradingName, invoice.Seller.SellerTradingName);
        Assert.Equal(expected.Seller.SellerIdentifiers, invoice.Seller.SellerIdentifiers);
        Assert.Equal(expected.Seller.SellerLegalRegistrationIdentifier, invoice.Seller.SellerLegalRegistrationIdentifier);
        Assert.Equal(expected.Seller.SellerVatIdentifier, invoice.Seller.SellerVatIdentifier);
        Assert.Equal(expected.Seller.SellerTaxRegistrationIdentifier, invoice.Seller.SellerTaxRegistrationIdentifier);
        Assert.Equal(expected.Seller.SellerAdditionalLegalInformation, invoice.Seller.SellerAdditionalLegalInformation);
        Assert.Equal(expected.Seller.SellerElectronicAddress, invoice.Seller.SellerElectronicAddress);
        Assert.Equal(expected.Seller.SellerPostalAddress.SellerAddressLine1, invoice.Seller.SellerPostalAddress.SellerAddressLine1);
        Assert.Equal(expected.Seller.SellerPostalAddress.SellerAddressLine2, invoice.Seller.SellerPostalAddress.SellerAddressLine2);
        Assert.Equal(expected.Seller.SellerPostalAddress.SellerAddressLine3, invoice.Seller.SellerPostalAddress.SellerAddressLine3);
        Assert.Equal(expected.Seller.SellerPostalAddress, invoice.Seller.SellerPostalAddress);
        Assert.Equal(expected.Seller.SellerContact, invoice.Seller.SellerContact);
        Assert.Equal(expected.Seller, invoice.Seller);

        Assert.Equal(expected.Buyer.BuyerName, invoice.Buyer.BuyerName);
        Assert.Equal(expected.Buyer.BuyerTradingName, invoice.Buyer.BuyerTradingName);
        Assert.Equal(expected.Buyer.BuyerIdentifier, invoice.Buyer.BuyerIdentifier);
        Assert.Equal(expected.Buyer.BuyerLegalRegistrationIdentifier, invoice.Buyer.BuyerLegalRegistrationIdentifier);
        Assert.Equal(expected.Buyer.BuyerVatIdentifier, invoice.Buyer.BuyerVatIdentifier);
        Assert.Equal(expected.Buyer.BuyerElectronicAddress, invoice.Buyer.BuyerElectronicAddress);
        Assert.Equal(expected.Buyer.BuyerPostalAddress.BuyerAddressLine1, invoice.Buyer.BuyerPostalAddress.BuyerAddressLine1);
        Assert.Equal(expected.Buyer.BuyerPostalAddress.BuyerAddressLine2, invoice.Buyer.BuyerPostalAddress.BuyerAddressLine2);
        Assert.Equal(expected.Buyer.BuyerPostalAddress.BuyerAddressLine3, invoice.Buyer.BuyerPostalAddress.BuyerAddressLine3);
        Assert.Equal(expected.Buyer.BuyerPostalAddress, invoice.Buyer.BuyerPostalAddress);
        Assert.Equal(expected.Buyer.BuyerContact, invoice.Buyer.BuyerContact);
        Assert.Equal(expected.Buyer, invoice.Buyer);

        Assert.Equal(expected.Payee, invoice.Payee);
        Assert.Equal(expected.SellerTaxRepresentativeParty, invoice.SellerTaxRepresentativeParty);
        Assert.Equal(expected.DeliveryInformation, invoice.DeliveryInformation);
        Assert.Equal(expected.PaymentInstructions, invoice.PaymentInstructions);
        Assert.Equal(expected.DocumentLevelAllowances, invoice.DocumentLevelAllowances);
        Assert.Equal(expected.DocumentLevelCharges, invoice.DocumentLevelCharges);
        Assert.Equal(expected.DocumentTotals, invoice.DocumentTotals);
        Assert.Equal(expected.VatBreakdown, invoice.VatBreakdown);
        Assert.Equal(expected.AdditionalSupportingDocuments, invoice.AdditionalSupportingDocuments);

        Assert.Equal(expected.InvoiceLines.Length, invoice.InvoiceLines.Length);

        foreach ((InvoiceLine expectedLine, InvoiceLine invoiceLine) in expected.InvoiceLines.Zip(invoice.InvoiceLines))
        {
            Assert.Equal(expectedLine.InvoiceLineIdentifier, invoiceLine.InvoiceLineIdentifier);
            Assert.Equal(expectedLine.InvoiceLineNote, invoiceLine.InvoiceLineNote);
            Assert.Equal(expectedLine.InvoiceLineObjectIdentifier, invoiceLine.InvoiceLineObjectIdentifier);
            Assert.Equal(expectedLine.InvoicedQuantity, invoiceLine.InvoicedQuantity);
            Assert.Equal(expectedLine.InvoicedQuantityUnitOfMeasureCode, invoiceLine.InvoicedQuantityUnitOfMeasureCode);
            Assert.Equal(expectedLine.InvoiceLineNetAmount, invoiceLine.InvoiceLineNetAmount);
            Assert.Equal(expectedLine.ReferencedPurchaseOrderLineReference, invoiceLine.ReferencedPurchaseOrderLineReference);
            Assert.Equal(expectedLine.InvoiceLineBuyerAccountingReference, invoiceLine.InvoiceLineBuyerAccountingReference);
            Assert.Equal(expectedLine.InvoiceLinePeriod, invoiceLine.InvoiceLinePeriod);
            Assert.Equal(expectedLine.InvoiceLineAllowances, invoiceLine.InvoiceLineAllowances);
            Assert.Equal(expectedLine.InvoiceLineCharges, invoiceLine.InvoiceLineCharges);
            Assert.Equal(expectedLine.PriceDetails, invoiceLine.PriceDetails);
            Assert.Equal(expectedLine.LineVatInformation, invoiceLine.LineVatInformation);
            Assert.Equal(expectedLine.ItemInformation, invoiceLine.ItemInformation);
            Assert.Equal(expectedLine, invoiceLine);
        }

        Assert.Equal(expected.InvoiceLines, invoice.InvoiceLines);
    }
}
