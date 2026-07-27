using System.Collections.Generic;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Utils;
using S = En16931.Specs;

namespace En16931.Model.XRechnungExtension;

public readonly record struct Invoice : IInvoice, IIRDeserializable<Invoice>, IIRSerializable
{
    IProcessControl IInvoice.ProcessControl { get => ProcessControl; }

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
    public required Code? VatAccountingCurrencyCode { get; init; }

    // BT-7
    public required Date? ValueAddedTaxPointDate { get; init; }

    // BT-8
    // UNTDID 2005
    public required Code? ValueAddedTaxPointDateCode { get; init; }

    // BT-9
    public required Date? PaymentDueDate { get; init; }

    // BT-10
    public required Text BuyerReference { get; init; }

    // BT-11
    public required DocumentReference? ProjectReference { get; init; }

    // BT-12
    public required DocumentReference? ContractReference { get; init; }

    // BT-13
    public required DocumentReference? PurchaseOrderReference { get; init; }

    // BT-14
    public required DocumentReference? SalesOrderReference { get; init; }

    // BT-15
    public required DocumentReference? ReceivingAdviceReference { get; init; }

    // BT-16
    public required DocumentReference? DespatchAdviceReference { get; init; }

    // BT-17
    public required DocumentReference? TenderOrLotReference { get; init; }

    // BT-18
    public required Identifier? InvoicedObjectIdentifier { get; init; }

    // BT-19
    public required Text? BuyerAccountingReference { get; init; }

    // BT-20
    public required Text? PaymentTerms { get; init; }

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
    public required Payee? Payee { get; init; }

    // BG-11
    public required SellerTaxRepresentativeParty? SellerTaxRepresentativeParty { get; init; }

    // BG-13
    public required DeliveryInformation? DeliveryInformation { get; init; }

    // BG-16
    public required PaymentInstructions PaymentInstructions { get; init; }

    // BG-20
    public required Array<DocumentLevelAllowance> DocumentLevelAllowances { get; init; }

    // BG-21
    public required Array<DocumentLevelCharge> DocumentLevelCharges { get; init; }

    // BG-22
    public required DocumentTotals DocumentTotals { get; init; }

    // BG-23
    public required NonEmptyArray<VatBreakdown> VatBreakdown { get; init; }

    // BG-24
    public required Array<AdditionalSupportingDocument> AdditionalSupportingDocuments { get; init; }

    // BG-25
    public required NonEmptyArray<InvoiceLine> InvoiceLines { get; init; }

    // BG-DEX-09
    public required Array<ThirdPartyPayment> ThirdPartyPayments { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartDocument();

        writer.WriteStartElement("invoice", IRConfig.NS);

        writer.WriteStartElement("invoice-number", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-1");
        InvoiceNumber.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("invoice-issue-date", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-2");
        InvoiceIssueDate.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("invoice-type-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-3");
        InvoiceTypeCode.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("invoice-currency-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-5");
        InvoiceCurrencyCode.Serialize(writer);
        writer.WriteEndElement();

        if (VatAccountingCurrencyCode is not null)
        {
            writer.WriteStartElement("vat-accounting-currency-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-6");
            VatAccountingCurrencyCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ValueAddedTaxPointDate is not null)
        {
            writer.WriteStartElement("value-added-tax-point-date", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-7");
            ValueAddedTaxPointDate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ValueAddedTaxPointDateCode is not null)
        {
            writer.WriteStartElement("value-added-tax-point-date-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-8");
            ValueAddedTaxPointDateCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (PaymentDueDate is not null)
        {
            writer.WriteStartElement("payment-due-date", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-9");
            PaymentDueDate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("buyer-reference", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-10");
        BuyerReference.Serialize(writer);
        writer.WriteEndElement();

        if (ProjectReference is not null)
        {
            writer.WriteStartElement("project-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-11");
            ProjectReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ContractReference is not null)
        {
            writer.WriteStartElement("contract-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-12");
            ContractReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (PurchaseOrderReference is not null)
        {
            writer.WriteStartElement("purchase-order-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-13");
            PurchaseOrderReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SalesOrderReference is not null)
        {
            writer.WriteStartElement("sales-order-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-14");
            SalesOrderReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ReceivingAdviceReference is not null)
        {
            writer.WriteStartElement("receiving-advice-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-15");
            ReceivingAdviceReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DespatchAdviceReference is not null)
        {
            writer.WriteStartElement("despatch-advice-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-16");
            DespatchAdviceReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (TenderOrLotReference is not null)
        {
            writer.WriteStartElement("tender-or-lot-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-17");
            TenderOrLotReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoicedObjectIdentifier is not null)
        {
            writer.WriteStartElement("invoiced-object-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-18");
            InvoicedObjectIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerAccountingReference is not null)
        {
            writer.WriteStartElement("buyer-accounting-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-19");
            BuyerAccountingReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (PaymentTerms is not null)
        {
            writer.WriteStartElement("payment-terms", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-20");
            PaymentTerms.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceNotes.Length > 0)
        {
            writer.WriteStartElement("invoice-notes", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-1");

            foreach (InvoiceNote i in InvoiceNotes)
            {
                i.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        ProcessControl.Serialize(writer);

        if (PrecedingInvoiceReferences.Length > 0)
        {
            writer.WriteStartElement("preceding-invoice-references", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-3");

            foreach (PrecedingInvoiceReference pir in PrecedingInvoiceReferences)
            {
                pir.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        Seller.Serialize(writer);

        Buyer.Serialize(writer);

        Payee?.Serialize(writer);

        SellerTaxRepresentativeParty?.Serialize(writer);

        DeliveryInformation?.Serialize(writer);

        PaymentInstructions.Serialize(writer);

        if (DocumentLevelAllowances.Length > 0)
        {
            writer.WriteStartElement("document-level-allowances", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-20");

            foreach (DocumentLevelAllowance dla in DocumentLevelAllowances)
            {
                dla.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        if (DocumentLevelCharges.Length > 0)
        {
            writer.WriteStartElement("document-level-charges", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-21");

            foreach (DocumentLevelCharge dlc in DocumentLevelCharges)
            {
                dlc.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        DocumentTotals.Serialize(writer);

        writer.WriteStartElement("vat-breakdown", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-23");

        foreach (VatBreakdown vb in VatBreakdown)
        {
            vb.Serialize(writer);
        }

        writer.WriteEndElement();

        if (AdditionalSupportingDocuments.Length > 0)
        {
            writer.WriteStartElement("additional-supporting-documents", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-24");

            foreach (AdditionalSupportingDocument asd in AdditionalSupportingDocuments)
            {
                asd.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        writer.WriteStartElement("invoice-lines", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-25");

        foreach (InvoiceLine il in InvoiceLines)
        {
            il.Serialize(writer);
        }

        writer.WriteEndElement();

        if (ThirdPartyPayments.Length > 0)
        {
            writer.WriteStartElement("third-party-payments", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-dex-09");

            foreach (ThirdPartyPayment tpp in ThirdPartyPayments)
            {
                tpp.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        writer.WriteEndElement();

        writer.WriteEndDocument();
    }

    public static Invoice Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("invoice", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("invoice-number", IRConfig.NS);
        reader.MoveToContent();

        Identifier invoiceNumber = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("invoice-issue-date", IRConfig.NS);
        reader.MoveToContent();

        Date invoiceIssueDate = Date.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("invoice-type-code", IRConfig.NS);
        reader.MoveToContent();

        Code invoiceTypeCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("invoice-currency-code", IRConfig.NS);
        reader.MoveToContent();

        Code invoiceCurrencyCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Code? vatAccountingCurrencyCode = null;

        if (reader.IsStartElement("vat-accounting-currency-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            vatAccountingCurrencyCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Date? valueAddedTaxPointDate = null;

        if (reader.IsStartElement("value-added-tax-point-date", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            valueAddedTaxPointDate = Date.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? valueAddedTaxPointDateCode = null;

        if (reader.IsStartElement("value-added-tax-point-date-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            valueAddedTaxPointDateCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Date? paymentDueDate = null;

        if (reader.IsStartElement("payment-due-date", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paymentDueDate = Date.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("buyer-reference", IRConfig.NS);
        reader.MoveToContent();

        Text buyerReference = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        DocumentReference? projectReference = null;

        if (reader.IsStartElement("project-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            projectReference = DocumentReference.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        DocumentReference? contractReference = null;

        if (reader.IsStartElement("contract-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            contractReference = DocumentReference.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        DocumentReference? purchaseOrderReference = null;

        if (reader.IsStartElement("purchase-order-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            purchaseOrderReference = DocumentReference.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        DocumentReference? salesOrderReference = null;

        if (reader.IsStartElement("sales-order-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            salesOrderReference = DocumentReference.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        DocumentReference? receivingAdviceReference = null;

        if (reader.IsStartElement("receiving-advice-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            receivingAdviceReference = DocumentReference.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        DocumentReference? despatchAdviceReference = null;

        if (reader.IsStartElement("despatch-advice-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            despatchAdviceReference = DocumentReference.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        DocumentReference? tenderOrLotReference = null;

        if (reader.IsStartElement("tender-or-lot-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            tenderOrLotReference = DocumentReference.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? invoicedObjectIdentifier = null;

        if (reader.IsStartElement("invoiced-object-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoicedObjectIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerAccountingReference = null;

        if (reader.IsStartElement("buyer-accounting-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerAccountingReference = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? paymentTerms = null;

        if (reader.IsStartElement("payment-terms", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paymentTerms = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Array<InvoiceNote> invoiceNotes = Array<InvoiceNote>.Empty;

        if (reader.IsStartElement("invoice-notes", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<InvoiceNote> invoiceNotesBuilder = [];
            while (reader.IsStartElement("invoice-note", IRConfig.NS))
            {
                invoiceNotesBuilder.Add(InvoiceNote.Deserialize(reader));
            }

            invoiceNotes = new(invoiceNotesBuilder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        ProcessControl processControl = ProcessControl.Deserialize(reader);

        Array<PrecedingInvoiceReference> precedingInvoiceReferences = Array<PrecedingInvoiceReference>.Empty;

        if (reader.IsStartElement("preceding-invoice-references", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<PrecedingInvoiceReference> precedingInvoiceReferencesBuilder = [];
            while (reader.IsStartElement("preceding-invoice-reference", IRConfig.NS))
            {
                precedingInvoiceReferencesBuilder.Add(PrecedingInvoiceReference.Deserialize(reader));
            }

            precedingInvoiceReferences = new(precedingInvoiceReferencesBuilder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Seller seller = Seller.Deserialize(reader);

        Buyer buyer = Buyer.Deserialize(reader);

        Payee? payee = null;

        if (reader.IsStartElement("payee", IRConfig.NS))
        {
            payee = Model.Payee.Deserialize(reader);
        }

        SellerTaxRepresentativeParty? sellerTaxRepresentativeParty = null;

        if (reader.IsStartElement("seller-tax-representative-party", IRConfig.NS))
        {
            sellerTaxRepresentativeParty = Model.SellerTaxRepresentativeParty.Deserialize(reader);
        }

        DeliveryInformation? deliveryInformation = null;

        if (reader.IsStartElement("delivery-information", IRConfig.NS))
        {
            deliveryInformation = Model.DeliveryInformation.Deserialize(reader);
        }

        PaymentInstructions paymentInstructions = PaymentInstructions.Deserialize(reader);

        Array<DocumentLevelAllowance> documentLevelAllowances = Array<DocumentLevelAllowance>.Empty;

        if (reader.IsStartElement("document-level-allowances", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<DocumentLevelAllowance> documentLevelAllowancesBuilder = [];
            while (reader.IsStartElement("document-level-allowance", IRConfig.NS))
            {
                documentLevelAllowancesBuilder.Add(DocumentLevelAllowance.Deserialize(reader));
            }

            documentLevelAllowances = new(documentLevelAllowancesBuilder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Array<DocumentLevelCharge> documentLevelCharges = Array<DocumentLevelCharge>.Empty;

        if (reader.IsStartElement("document-level-charges", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<DocumentLevelCharge> documentLevelChargesBuilder = [];
            while (reader.IsStartElement("document-level-charge", IRConfig.NS))
            {
                documentLevelChargesBuilder.Add(DocumentLevelCharge.Deserialize(reader));
            }

            documentLevelCharges = new(documentLevelChargesBuilder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        DocumentTotals documentTotals = DocumentTotals.Deserialize(reader);

        reader.ReadStartElement("vat-breakdown", IRConfig.NS);
        reader.MoveToContent();

        List<VatBreakdown> vatBreadownBuilder = [];
        while (reader.IsStartElement("vat-breakdown", IRConfig.NS))
        {
            vatBreadownBuilder.Add(Model.VatBreakdown.Deserialize(reader));
        }

        NonEmptyArray<VatBreakdown> vatBreakdown = new(vatBreadownBuilder);

        reader.ReadEndElement();
        reader.MoveToContent();

        Array<AdditionalSupportingDocument> additionalSupportingDocuments = Array<AdditionalSupportingDocument>.Empty;

        if (reader.IsStartElement("additional-supporting-documents", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<AdditionalSupportingDocument> additionalSupportingDocumentsBuilder = [];
            while (reader.IsStartElement("additional-supporting-document", IRConfig.NS))
            {
                additionalSupportingDocumentsBuilder.Add(AdditionalSupportingDocument.Deserialize(reader));
            }

            additionalSupportingDocuments = new(additionalSupportingDocumentsBuilder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("invoice-lines", IRConfig.NS);
        reader.MoveToContent();

        List<InvoiceLine> invoiceLinesBuilder = [];
        while (reader.IsStartElement("invoice-line", IRConfig.NS))
        {
            invoiceLinesBuilder.Add(InvoiceLine.Deserialize(reader));
        }

        NonEmptyArray<InvoiceLine> invoiceLines = new(invoiceLinesBuilder);

        reader.ReadEndElement();
        reader.MoveToContent();

        Array<ThirdPartyPayment> thirdPartyPayments = Array<ThirdPartyPayment>.Empty;

        if (reader.IsStartElement("third-party-payments", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<ThirdPartyPayment> thirdPartyPaymentsBuilder = [];
            while (reader.IsStartElement("third-party-payment", IRConfig.NS))
            {
                thirdPartyPaymentsBuilder.Add(ThirdPartyPayment.Deserialize(reader));
            }

            thirdPartyPayments = new(thirdPartyPaymentsBuilder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new Invoice
        {
            InvoiceNumber = invoiceNumber,
            InvoiceIssueDate = invoiceIssueDate,
            InvoiceTypeCode = invoiceTypeCode,
            InvoiceCurrencyCode = invoiceCurrencyCode,
            VatAccountingCurrencyCode = vatAccountingCurrencyCode,
            ValueAddedTaxPointDate = valueAddedTaxPointDate,
            ValueAddedTaxPointDateCode = valueAddedTaxPointDateCode,
            PaymentDueDate = paymentDueDate,
            BuyerReference = buyerReference,
            ProjectReference = projectReference,
            ContractReference = contractReference,
            PurchaseOrderReference = purchaseOrderReference,
            SalesOrderReference = salesOrderReference,
            ReceivingAdviceReference = receivingAdviceReference,
            DespatchAdviceReference = despatchAdviceReference,
            TenderOrLotReference = tenderOrLotReference,
            InvoicedObjectIdentifier = invoicedObjectIdentifier,
            BuyerAccountingReference = buyerAccountingReference,
            PaymentTerms = paymentTerms,
            InvoiceNotes = invoiceNotes,
            ProcessControl = processControl,
            PrecedingInvoiceReferences = precedingInvoiceReferences,
            Seller = seller,
            Buyer = buyer,
            Payee = payee,
            SellerTaxRepresentativeParty = sellerTaxRepresentativeParty,
            DeliveryInformation = deliveryInformation,
            PaymentInstructions = paymentInstructions,
            DocumentLevelAllowances = documentLevelAllowances,
            DocumentLevelCharges = documentLevelCharges,
            DocumentTotals = documentTotals,
            VatBreakdown = vatBreakdown,
            AdditionalSupportingDocuments = additionalSupportingDocuments,
            InvoiceLines = invoiceLines,
            ThirdPartyPayments = thirdPartyPayments,
        };
    }
}

public readonly record struct ProcessControl : IProcessControl, IIRDeserializable<ProcessControl>, IIRSerializable
{
    // BT-23
    public required Text BusinessProcessType { get; init; }

    // BT-24
    public Identifier SpecificationIdentifier { get => S.XRechnungExtension.SpecificationIdentifier; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("process-control", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-2");

        writer.WriteStartElement("business-process-type", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-23");
        BusinessProcessType.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("specification-identifier", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-24");
        SpecificationIdentifier.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static ProcessControl Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("process-control", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("business-process-type", IRConfig.NS);
        reader.MoveToContent();

        Text businessProcessType = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("specification-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier specificationIdentifier = Identifier.Deserialize(reader);

        if (specificationIdentifier != S.XRechnungExtension.SpecificationIdentifier)
        {
            ThrowHelper.ThrowInvalidOperationException($"`specification-identifier` field value `{specificationIdentifier.Content}` from the xml document does not match the identifier from the specification {nameof(S.XRechnungExtension)}, which is: {S.XRechnungExtension.SpecificationIdentifier.Content}");
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new ProcessControl
        {
            BusinessProcessType = businessProcessType,
        };
    }
}

public readonly record struct ThirdPartyPayment : IIRDeserializable<ThirdPartyPayment>, IIRSerializable
{
    // BT-DEX-001
    public required Text ThirdPartyPaymentType { get; init; }

    // BT-DEX-002
    public required Amount ThirdPartyPaymentAmount { get; init; }

    // BT-DEX-003
    public required Text ThirdPartyPaymentDescription { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("third-party-payment", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-dex-09");

        writer.WriteStartElement("third-party-payment-type", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-dex-001");
        ThirdPartyPaymentType.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("third-party-payment-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-dex-002");
        ThirdPartyPaymentAmount.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("third-party-payment-description", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-dex-003");
        ThirdPartyPaymentDescription.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static ThirdPartyPayment Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("third-party-payment", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("third-party-payment-type", IRConfig.NS);
        reader.MoveToContent();

        Text thirdPartyPaymentType = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("third-party-payment-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount thirdPartyPaymentAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("third-party-payment-description", IRConfig.NS);
        reader.MoveToContent();

        Text thirdPartyPaymentDescription = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new ThirdPartyPayment
        {
            ThirdPartyPaymentType = thirdPartyPaymentType,
            ThirdPartyPaymentAmount = thirdPartyPaymentAmount,
            ThirdPartyPaymentDescription = thirdPartyPaymentDescription,
        };
    }
}

public readonly record struct InvoiceLine : IIRDeserializable<InvoiceLine>, IIRSerializable
{
    // BT-126
    public required Identifier InvoiceLineIdentifier { get; init; }

    // BT-127
    public required Text? InvoiceLineNote { get; init; }

    // BT-128
    public required Identifier? InvoiceLineObjectIdentifier { get; init; }

    // BT-129
    public required Quantity InvoicedQuantity { get; init; }

    // BT-130
    public required Code InvoicedQuantityUnitOfMeasureCode { get; init; }

    // BT-131
    public required Amount InvoiceLineNetAmount { get; init; }

    // BT-132
    public required DocumentReference? ReferencedPurchaseOrderLineReference { get; init; }

    // BT-133
    public required Text? InvoiceLineBuyerAccountingReference { get; init; }

    // BG-26
    public required InvoiceLinePeriod? InvoiceLinePeriod { get; init; }

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

    // BG-DEX-01
    public required RefArray<SubInvoiceLine> SubInvoiceLines { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("invoice-line", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-25");

        writer.WriteStartElement("invoice-line-identifier", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-126");
        InvoiceLineIdentifier.Serialize(writer);
        writer.WriteEndElement();

        if (InvoiceLineNote is not null)
        {
            writer.WriteStartElement("invoice-line-note", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-127");
            InvoiceLineNote.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLineObjectIdentifier is not null)
        {
            writer.WriteStartElement("invoice-line-object-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-128");
            InvoiceLineObjectIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("invoiced-quantity", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-129");
        InvoicedQuantity.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("invoiced-quantity-unit-of-measure-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-130");
        InvoicedQuantityUnitOfMeasureCode.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("invoice-line-net-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-131");
        InvoiceLineNetAmount.Serialize(writer);
        writer.WriteEndElement();

        if (ReferencedPurchaseOrderLineReference is not null)
        {
            writer.WriteStartElement("referenced-purchase-order-line-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-132");
            ReferencedPurchaseOrderLineReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLineBuyerAccountingReference is not null)
        {
            writer.WriteStartElement("invoice-line-buyer-accounting-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-133");
            InvoiceLineBuyerAccountingReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLinePeriod is not null)
        {
            InvoiceLinePeriod.Value.Serialize(writer);
        }

        if (InvoiceLineAllowances.Length > 0)
        {
            writer.WriteStartElement("invoice-line-allowances", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-27");

            foreach (InvoiceLineAllowance ila in InvoiceLineAllowances)
            {
                ila.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        if (InvoiceLineCharges.Length > 0)
        {
            writer.WriteStartElement("invoice-line-charges", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-28");

            foreach (InvoiceLineCharge ilc in InvoiceLineCharges)
            {
                ilc.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        PriceDetails.Serialize(writer);

        LineVatInformation.Serialize(writer);

        ItemInformation.Serialize(writer);

        if (SubInvoiceLines.Length > 0)
        {
            writer.WriteStartElement("sub-invoice-lines", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-dex-01");

            foreach (SubInvoiceLine il in SubInvoiceLines)
            {
                il.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static InvoiceLine Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("invoice-line", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("invoice-line-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier invoiceLineIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? invoiceLineNote = null;

        if (reader.IsStartElement("invoice-line-note", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineNote = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? invoiceLineObjectIdentifier = null;

        if (reader.IsStartElement("invoice-line-object-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineObjectIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("invoiced-quantity", IRConfig.NS);
        reader.MoveToContent();

        Quantity invoicedQuantity = Quantity.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("invoiced-quantity-unit-of-measure-code", IRConfig.NS);
        reader.MoveToContent();

        Code invoicedQuantityUnitOfMeasureCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("invoice-line-net-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount invoiceLineNetAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        DocumentReference? referencedPurchaseOrderLineReference = null;

        if (reader.IsStartElement("referenced-purchase-order-line-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            referencedPurchaseOrderLineReference = DocumentReference.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? invoiceLineBuyerAccountingReference = null;

        if (reader.IsStartElement("invoice-line-buyer-accounting-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineBuyerAccountingReference = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        InvoiceLinePeriod? invoiceLinePeriod = null;

        if (reader.IsStartElement("invoice-line-period", IRConfig.NS))
        {
            invoiceLinePeriod = Model.InvoiceLinePeriod.Deserialize(reader);
        }

        Array<InvoiceLineAllowance> invoiceLineAllowances = Array<InvoiceLineAllowance>.Empty;

        if (reader.IsStartElement("invoice-line-allowances", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<InvoiceLineAllowance> builder = [];
            while (reader.IsStartElement("invoice-line-allowance", IRConfig.NS))
            {
                builder.Add(InvoiceLineAllowance.Deserialize(reader));
            }

            invoiceLineAllowances = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Array<InvoiceLineCharge> invoiceLineCharges = Array<InvoiceLineCharge>.Empty;

        if (reader.IsStartElement("invoice-line-charges", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<InvoiceLineCharge> builder = [];
            while (reader.IsStartElement("invoice-line-charge", IRConfig.NS))
            {
                builder.Add(InvoiceLineCharge.Deserialize(reader));
            }

            invoiceLineCharges = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        PriceDetails priceDetails = PriceDetails.Deserialize(reader);

        LineVatInformation lineVatInformation = LineVatInformation.Deserialize(reader);

        ItemInformation itemInformation = ItemInformation.Deserialize(reader);

        RefArray<SubInvoiceLine> subInvoiceLines = RefArray<SubInvoiceLine>.Empty;

        if (reader.IsStartElement("sub-invoice-lines", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<SubInvoiceLine> builder = [];
            while (reader.IsStartElement("sub-invoice-line", IRConfig.NS))
            {
                builder.Add(SubInvoiceLine.Deserialize(reader));
            }

            subInvoiceLines = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new InvoiceLine
        {
            InvoiceLineIdentifier = invoiceLineIdentifier,
            InvoiceLineNote = invoiceLineNote,
            InvoiceLineObjectIdentifier = invoiceLineObjectIdentifier,
            InvoicedQuantity = invoicedQuantity,
            InvoicedQuantityUnitOfMeasureCode = invoicedQuantityUnitOfMeasureCode,
            InvoiceLineNetAmount = invoiceLineNetAmount,
            ReferencedPurchaseOrderLineReference = referencedPurchaseOrderLineReference,
            InvoiceLineBuyerAccountingReference = invoiceLineBuyerAccountingReference,
            InvoiceLinePeriod = invoiceLinePeriod,
            InvoiceLineAllowances = invoiceLineAllowances,
            InvoiceLineCharges = invoiceLineCharges,
            PriceDetails = priceDetails,
            LineVatInformation = lineVatInformation,
            ItemInformation = itemInformation,
            SubInvoiceLines = subInvoiceLines,
        };
    }
}

public record class SubInvoiceLine : IIRDeserializable<SubInvoiceLine>, IIRSerializable
{
    // BT-126
    public required Identifier InvoiceLineIdentifier { get; init; }

    // BT-127
    public required Text? InvoiceLineNote { get; init; }

    // BT-128
    public required Identifier? InvoiceLineObjectIdentifier { get; init; }

    // BT-129
    public required Quantity InvoicedQuantity { get; init; }

    // BT-130
    public required Code InvoicedQuantityUnitOfMeasureCode { get; init; }

    // BT-131
    public required Amount InvoiceLineNetAmount { get; init; }

    // BT-132
    public required DocumentReference? ReferencedPurchaseOrderLineReference { get; init; }

    // BT-133
    public required Text? InvoiceLineBuyerAccountingReference { get; init; }

    // BG-DEX-02
    public required SubInvoiceLineItemInformation SubInvoiceLineItemInformation { get; init; }

    // BG-DEX-03
    public required Array<SubInvoiceLineAllowance> SubInvoiceLineAllowances { get; init; }

    // BG-DEX-04
    public required Array<SubInvoiceLineCharge> SubInvoiceLineCharges { get; init; }

    // BG-DEX-05
    public required SubInvoiceLinePeriod? SubInvoiceLinePeriod { get; init; }

    // BG-DEX-06
    public required SubInvoiceLineVatInformation SubInvoiceLineVatInformation { get; init; }

    // BG-DEX-07
    public required SubInvoiceLinePriceDetails SubInvoiceLinePriceDetails { get; init; }

    // BG-DEX-01
    public required RefArray<SubInvoiceLine> SubInvoiceLines { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("sub-invoice-line", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-dex-01");

        writer.WriteStartElement("invoice-line-identifier", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-126");
        InvoiceLineIdentifier.Serialize(writer);
        writer.WriteEndElement();

        if (InvoiceLineNote is not null)
        {
            writer.WriteStartElement("invoice-line-note", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-127");
            InvoiceLineNote.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLineObjectIdentifier is not null)
        {
            writer.WriteStartElement("invoice-line-object-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-128");
            InvoiceLineObjectIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("invoiced-quantity", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-129");
        InvoicedQuantity.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("invoiced-quantity-unit-of-measure-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-130");
        InvoicedQuantityUnitOfMeasureCode.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("invoice-line-net-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-131");
        InvoiceLineNetAmount.Serialize(writer);
        writer.WriteEndElement();

        if (ReferencedPurchaseOrderLineReference is not null)
        {
            writer.WriteStartElement("referenced-purchase-order-line-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-132");
            ReferencedPurchaseOrderLineReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLineBuyerAccountingReference is not null)
        {
            writer.WriteStartElement("invoice-line-buyer-accounting-reference", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-133");
            InvoiceLineBuyerAccountingReference.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        SubInvoiceLineItemInformation.Serialize(writer);

        if (SubInvoiceLineAllowances.Length > 0)
        {
            writer.WriteStartElement("sub-invoice-line-allowances", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-dex-03");

            foreach (SubInvoiceLineAllowance ila in SubInvoiceLineAllowances)
            {
                ila.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        if (SubInvoiceLineCharges.Length > 0)
        {
            writer.WriteStartElement("sub-invoice-line-charges", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-dex-04");

            foreach (SubInvoiceLineCharge ilc in SubInvoiceLineCharges)
            {
                ilc.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        if (SubInvoiceLinePeriod is not null)
        {
            SubInvoiceLinePeriod.Value.Serialize(writer);
        }

        SubInvoiceLineVatInformation.Serialize(writer);

        SubInvoiceLinePriceDetails.Serialize(writer);

        if (SubInvoiceLines.Length > 0)
        {
            writer.WriteStartElement("sub-invoice-lines", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-dex-01");

            foreach (SubInvoiceLine il in SubInvoiceLines)
            {
                il.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static SubInvoiceLine Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("sub-invoice-line", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("invoice-line-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier invoiceLineIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? invoiceLineNote = null;

        if (reader.IsStartElement("invoice-line-note", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineNote = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? invoiceLineObjectIdentifier = null;

        if (reader.IsStartElement("invoice-line-object-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineObjectIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("invoiced-quantity", IRConfig.NS);
        reader.MoveToContent();

        Quantity invoicedQuantity = Quantity.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("invoiced-quantity-unit-of-measure-code", IRConfig.NS);
        reader.MoveToContent();

        Code invoicedQuantityUnitOfMeasureCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("invoice-line-net-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount invoiceLineNetAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        DocumentReference? referencedPurchaseOrderLineReference = null;

        if (reader.IsStartElement("referenced-purchase-order-line-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            referencedPurchaseOrderLineReference = DocumentReference.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? invoiceLineBuyerAccountingReference = null;

        if (reader.IsStartElement("invoice-line-buyer-accounting-reference", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineBuyerAccountingReference = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        SubInvoiceLineItemInformation subInvoiceLineItemInformation = SubInvoiceLineItemInformation.Deserialize(reader);

        Array<SubInvoiceLineAllowance> subInvoiceLineAllowances = Array<SubInvoiceLineAllowance>.Empty;

        if (reader.IsStartElement("sub-invoice-line-allowances", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<SubInvoiceLineAllowance> builder = [];
            while (reader.IsStartElement("sub-invoice-line-allowance", IRConfig.NS))
            {
                builder.Add(SubInvoiceLineAllowance.Deserialize(reader));
            }

            subInvoiceLineAllowances = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Array<SubInvoiceLineCharge> subInvoiceLineCharges = Array<SubInvoiceLineCharge>.Empty;

        if (reader.IsStartElement("sub-invoice-line-charges", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<SubInvoiceLineCharge> builder = [];
            while (reader.IsStartElement("sub-invoice-line-charge", IRConfig.NS))
            {
                builder.Add(SubInvoiceLineCharge.Deserialize(reader));
            }

            subInvoiceLineCharges = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        SubInvoiceLinePeriod? subInvoiceLinePeriod = null;

        if (reader.IsStartElement("sub-invoice-line-period", IRConfig.NS))
        {
            subInvoiceLinePeriod = XRechnungExtension.SubInvoiceLinePeriod.Deserialize(reader);
        }

        SubInvoiceLineVatInformation subInvoiceLineVatInformation = SubInvoiceLineVatInformation.Deserialize(reader);

        SubInvoiceLinePriceDetails subInvoiceLinePriceDetails = SubInvoiceLinePriceDetails.Deserialize(reader);

        RefArray<SubInvoiceLine> subInvoiceLines = RefArray<SubInvoiceLine>.Empty;

        if (reader.IsStartElement("sub-invoice-lines", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<SubInvoiceLine> builder = [];
            while (reader.IsStartElement("sub-invoice-line", IRConfig.NS))
            {
                builder.Add(SubInvoiceLine.Deserialize(reader));
            }

            subInvoiceLines = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SubInvoiceLine
        {
            InvoiceLineIdentifier = invoiceLineIdentifier,
            InvoiceLineNote = invoiceLineNote,
            InvoiceLineObjectIdentifier = invoiceLineObjectIdentifier,
            InvoicedQuantity = invoicedQuantity,
            InvoicedQuantityUnitOfMeasureCode = invoicedQuantityUnitOfMeasureCode,
            InvoiceLineNetAmount = invoiceLineNetAmount,
            ReferencedPurchaseOrderLineReference = referencedPurchaseOrderLineReference,
            InvoiceLineBuyerAccountingReference = invoiceLineBuyerAccountingReference,
            SubInvoiceLineItemInformation = subInvoiceLineItemInformation,
            SubInvoiceLineAllowances = subInvoiceLineAllowances,
            SubInvoiceLineCharges = subInvoiceLineCharges,
            SubInvoiceLinePeriod = subInvoiceLinePeriod,
            SubInvoiceLineVatInformation = subInvoiceLineVatInformation,
            SubInvoiceLinePriceDetails = subInvoiceLinePriceDetails,
            SubInvoiceLines = subInvoiceLines,
        };
    }
}

public readonly record struct SubInvoiceLineItemInformation : IIRDeserializable<SubInvoiceLineItemInformation>, IIRSerializable
{
    // BT-153
    public required Text ItemName { get; init; }

    // BT-154
    public required Text? ItemDescription { get; init; }

    // BT-155
    public required Identifier? ItemSellersIdentifier { get; init; }

    // BT-156
    public required Identifier? ItemBuyersIdentifier { get; init; }

    // BT-157
    public required Identifier? ItemStandardIdentifier { get; init; }

    // BT-158
    // UNTDID 7143
    public required Array<Identifier> ItemClassificationIdentifiers { get; init; }

    // BT-159
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2 representation
    public required Code? ItemCountryOfOrigin { get; init; }

    // BG-DEX-08
    public required Array<SubInvoiceLineItemAttribute> SubInvoiceLineItemAttributes { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("sub-invoice-line-item-information", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-dex-02");

        writer.WriteStartElement("item-name", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-153");
        ItemName.Serialize(writer);
        writer.WriteEndElement();

        if (ItemDescription is not null)
        {
            writer.WriteStartElement("item-description", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-154");
            ItemDescription.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemSellersIdentifier is not null)
        {
            writer.WriteStartElement("item-sellers-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-155");
            ItemSellersIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemBuyersIdentifier is not null)
        {
            writer.WriteStartElement("item-buyers-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-156");
            ItemBuyersIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemStandardIdentifier is not null)
        {
            writer.WriteStartElement("item-standard-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-157");
            ItemStandardIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemClassificationIdentifiers.Length > 0)
        {
            writer.WriteStartElement("item-classification-identifiers", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-158");

            foreach (Identifier i in ItemClassificationIdentifiers)
            {
                writer.WriteStartElement("item-classification-identifier", IRConfig.NS);
                writer.WriteAttributeString("id", "bt-158");
                i.Serialize(writer);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        if (ItemCountryOfOrigin is not null)
        {
            writer.WriteStartElement("item-country-of-origin", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-159");
            ItemCountryOfOrigin.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SubInvoiceLineItemAttributes.Length > 0)
        {
            writer.WriteStartElement("sub-invoice-line-item-attributes", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-dex-08");

            foreach (SubInvoiceLineItemAttribute ia in SubInvoiceLineItemAttributes)
            {
                ia.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static SubInvoiceLineItemInformation Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("sub-invoice-line-item-information", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("item-name", IRConfig.NS);
        reader.MoveToContent();

        Text itemName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? itemDescription = null;

        if (reader.IsStartElement("item-description", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            itemDescription = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? itemSellersIdentifier = null;

        if (reader.IsStartElement("item-sellers-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            itemSellersIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? itemBuyersIdentifier = null;

        if (reader.IsStartElement("item-buyers-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            itemBuyersIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? itemStandardIdentifier = null;

        if (reader.IsStartElement("item-standard-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            itemStandardIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Array<Identifier> itemClassificationIdentifiers = Array<Identifier>.Empty;

        if (reader.IsStartElement("item-classification-identifiers", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<Identifier> builder = [];
            while (reader.IsStartElement("item-classification-identifier", IRConfig.NS))
            {
                reader.ReadStartElement();
                reader.MoveToContent();

                builder.Add(Identifier.Deserialize(reader));

                reader.ReadEndElement();
                reader.MoveToContent();
            }

            itemClassificationIdentifiers = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? itemCountryOfOrigin = null;

        if (reader.IsStartElement("item-country-of-origin", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            itemCountryOfOrigin = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Array<SubInvoiceLineItemAttribute> subInvoiceLineItemAttributes = Array<SubInvoiceLineItemAttribute>.Empty;

        if (reader.IsStartElement("sub-invoice-line-item-attributes", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<SubInvoiceLineItemAttribute> builder = [];
            while (reader.IsStartElement("sub-invoice-line-item-attribute", IRConfig.NS))
            {
                builder.Add(SubInvoiceLineItemAttribute.Deserialize(reader));
            }

            subInvoiceLineItemAttributes = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SubInvoiceLineItemInformation
        {
            ItemName = itemName,
            ItemDescription = itemDescription,
            ItemSellersIdentifier = itemSellersIdentifier,
            ItemBuyersIdentifier = itemBuyersIdentifier,
            ItemStandardIdentifier = itemStandardIdentifier,
            ItemClassificationIdentifiers = itemClassificationIdentifiers,
            ItemCountryOfOrigin = itemCountryOfOrigin,
            SubInvoiceLineItemAttributes = subInvoiceLineItemAttributes,
        };
    }
}

public readonly record struct SubInvoiceLineItemAttribute : IIRDeserializable<SubInvoiceLineItemAttribute>, IIRSerializable
{
    // BT-160
    public required Text ItemAttributeName { get; init; }

    // BT-161
    public required Text ItemAttributeValue { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("sub-invoice-line-item-attribute", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-dex-08");

        writer.WriteStartElement("item-attribute-name", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-160");
        ItemAttributeName.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("item-attribute-value", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-161");
        ItemAttributeValue.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static SubInvoiceLineItemAttribute Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("sub-invoice-line-item-attribute", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("item-attribute-name", IRConfig.NS);
        reader.MoveToContent();

        Text itemAttributeName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("item-attribute-value", IRConfig.NS);
        reader.MoveToContent();

        Text itemAttributeValue = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SubInvoiceLineItemAttribute
        {
            ItemAttributeName = itemAttributeName,
            ItemAttributeValue = itemAttributeValue,
        };
    }
}

public readonly record struct SubInvoiceLineAllowance : IIRDeserializable<SubInvoiceLineAllowance>, IIRSerializable
{
    // BT-136
    public required Amount InvoiceLineAllowanceAmount { get; init; }

    // BT-137
    public required Amount? InvoiceLineAllowanceBaseAmount { get; init; }

    // BT-138
    public required Percentage? InvoiceLineAllowancePercentage { get; init; }

    // BT-139
    public required Text? InvoiceLineAllowanceReason { get; init; }

    // BT-140
    public required Code? InvoiceLineAllowanceReasonCode { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("sub-invoice-line-allowance", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-dex-03");

        writer.WriteStartElement("invoice-line-allowance-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-136");
        InvoiceLineAllowanceAmount.Serialize(writer);
        writer.WriteEndElement();

        if (InvoiceLineAllowanceBaseAmount is not null)
        {
            writer.WriteStartElement("invoice-line-allowance-base-amount", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-137");
            InvoiceLineAllowanceBaseAmount.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLineAllowancePercentage is not null)
        {
            writer.WriteStartElement("invoice-line-allowance-percentage", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-138");
            InvoiceLineAllowancePercentage.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLineAllowanceReason is not null)
        {
            writer.WriteStartElement("invoice-line-allowance-reason", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-139");
            InvoiceLineAllowanceReason.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLineAllowanceReasonCode is not null)
        {
            writer.WriteStartElement("invoice-line-allowance-reason-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-140");
            InvoiceLineAllowanceReasonCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static SubInvoiceLineAllowance Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("sub-invoice-line-allowance", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("invoice-line-allowance-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount invoiceLineAllowanceAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? invoiceLineAllowanceBaseAmount = null;

        if (reader.IsStartElement("invoice-line-allowance-base-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineAllowanceBaseAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Percentage? invoiceLineAllowancePercentage = null;

        if (reader.IsStartElement("invoice-line-allowance-percentage", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineAllowancePercentage = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? invoiceLineAllowanceReason = null;

        if (reader.IsStartElement("invoice-line-allowance-reason", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineAllowanceReason = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? invoiceLineAllowanceReasonCode = null;

        if (reader.IsStartElement("invoice-line-allowance-reason-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineAllowanceReasonCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SubInvoiceLineAllowance
        {
            InvoiceLineAllowanceAmount = invoiceLineAllowanceAmount,
            InvoiceLineAllowanceBaseAmount = invoiceLineAllowanceBaseAmount,
            InvoiceLineAllowancePercentage = invoiceLineAllowancePercentage,
            InvoiceLineAllowanceReason = invoiceLineAllowanceReason,
            InvoiceLineAllowanceReasonCode = invoiceLineAllowanceReasonCode,
        };
    }
}

public readonly record struct SubInvoiceLineCharge : IIRDeserializable<SubInvoiceLineCharge>, IIRSerializable
{
    // BT-141
    public required Amount InvoiceLineChargeAmount { get; init; }

    // BT-142
    public required Amount? InvoiceLineChargeBaseAmount { get; init; }

    // BT-143
    public required Percentage? InvoiceLineChargePercentage { get; init; }

    // BT-144
    public required Text? InvoiceLineChargeReason { get; init; }

    // BT-145
    public required Code? InvoiceLineChargeReasonCode { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("sub-invoice-line-charge", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-dex-04");

        writer.WriteStartElement("invoice-line-charge-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-141");
        InvoiceLineChargeAmount.Serialize(writer);
        writer.WriteEndElement();

        if (InvoiceLineChargeBaseAmount is not null)
        {
            writer.WriteStartElement("invoice-line-charge-base-amount", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-142");
            InvoiceLineChargeBaseAmount.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLineChargePercentage is not null)
        {
            writer.WriteStartElement("invoice-line-charge-percentage", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-143");
            InvoiceLineChargePercentage.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLineChargeReason is not null)
        {
            writer.WriteStartElement("invoice-line-charge-reason", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-144");
            InvoiceLineChargeReason.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLineChargeReasonCode is not null)
        {
            writer.WriteStartElement("invoice-line-charge-reason-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-145");
            InvoiceLineChargeReasonCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static SubInvoiceLineCharge Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("sub-invoice-line-charge", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("invoice-line-charge-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount invoiceLineChargeAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? invoiceLineChargeBaseAmount = null;

        if (reader.IsStartElement("invoice-line-charge-base-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineChargeBaseAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Percentage? invoiceLineChargePercentage = null;

        if (reader.IsStartElement("invoice-line-charge-percentage", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineChargePercentage = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? invoiceLineChargeReason = null;

        if (reader.IsStartElement("invoice-line-charge-reason", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineChargeReason = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? invoiceLineChargeReasonCode = null;

        if (reader.IsStartElement("invoice-line-charge-reason-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLineChargeReasonCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SubInvoiceLineCharge
        {
            InvoiceLineChargeAmount = invoiceLineChargeAmount,
            InvoiceLineChargeBaseAmount = invoiceLineChargeBaseAmount,
            InvoiceLineChargePercentage = invoiceLineChargePercentage,
            InvoiceLineChargeReason = invoiceLineChargeReason,
            InvoiceLineChargeReasonCode = invoiceLineChargeReasonCode,
        };
    }
}

public readonly record struct SubInvoiceLinePeriod : IIRDeserializable<SubInvoiceLinePeriod>, IIRSerializable
{
    // BT-134
    public required Date? InvoiceLinePeriodStartDate { get; init; }

    // BT-135
    public required Date? InvoiceLinePeriodEndDate { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("sub-invoice-line-period", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-dex-05");

        if (InvoiceLinePeriodStartDate is not null)
        {
            writer.WriteStartElement("invoice-line-period-start-date");
            writer.WriteAttributeString("id", "bt-134");
            InvoiceLinePeriodStartDate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceLinePeriodEndDate is not null)
        {
            writer.WriteStartElement("invoice-line-period-end-date");
            writer.WriteAttributeString("id", "bt-135");
            InvoiceLinePeriodEndDate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static SubInvoiceLinePeriod Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("sub-invoice-line-period", IRConfig.NS);
        reader.MoveToContent();

        Date? invoiceLinePeriodStartDate = null;

        if (reader.IsStartElement("invoice-line-period-start-date", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLinePeriodStartDate = Date.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Date? invoiceLinePeriodEndDate = null;

        if (reader.IsStartElement("invoice-line-period-end-date", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceLinePeriodEndDate = Date.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SubInvoiceLinePeriod
        {
            InvoiceLinePeriodStartDate = invoiceLinePeriodStartDate,
            InvoiceLinePeriodEndDate = invoiceLinePeriodEndDate,
        };
    }
}

public readonly record struct SubInvoiceLineVatInformation : IIRDeserializable<SubInvoiceLineVatInformation>, IIRSerializable
{
    // BT-151
    // UNTDID 5305
    public required Code InvoicedItemVatCategoryCode { get; init; }

    // BT-152
    public required Percentage? InvoicedItemVatRate { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("sub-invoice-line-vat-information", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-dex-06");

        writer.WriteStartElement("invoiced-item-vat-category-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-151");
        InvoicedItemVatCategoryCode.Serialize(writer);
        writer.WriteEndElement();

        if (InvoicedItemVatRate is not null)
        {
            writer.WriteStartElement("invoiced-item-vat-rate", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-152");
            InvoicedItemVatRate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static SubInvoiceLineVatInformation Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("sub-invoice-line-vat-information", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("invoiced-item-vat-category-code", IRConfig.NS);
        reader.MoveToContent();

        Code invoicedItemVatCategoryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Percentage? invoicedItemVatRate = null;

        if (reader.IsStartElement("invoiced-item-vat-rate", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoicedItemVatRate = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SubInvoiceLineVatInformation
        {
            InvoicedItemVatCategoryCode = invoicedItemVatCategoryCode,
            InvoicedItemVatRate = invoicedItemVatRate,
        };
    }
}

public readonly record struct SubInvoiceLinePriceDetails : IIRDeserializable<SubInvoiceLinePriceDetails>, IIRSerializable
{
    // BT-146
    public required UnitPriceAmount ItemNetPrice { get; init; }

    // BT-147
    public required UnitPriceAmount? ItemPriceDiscount { get; init; }

    // BT-148
    public required UnitPriceAmount? ItemGrossPrice { get; init; }

    // BT-149
    public required Quantity? ItemPriceBaseQuantity { get; init; }

    // BT-150
    // UN/ECE Rec No 20,21
    public required Code? ItemPriceBaseQuantityUnitOfMeasureCode { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("sub-invoice-line-price-details", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-dex-07");

        writer.WriteStartElement("item-net-price", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-146");
        ItemNetPrice.Serialize(writer);
        writer.WriteEndElement();

        if (ItemPriceDiscount is not null)
        {
            writer.WriteStartElement("item-price-discount", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-147");
            ItemPriceDiscount.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemGrossPrice is not null)
        {
            writer.WriteStartElement("item-gross-price", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-148");
            ItemGrossPrice.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemPriceBaseQuantity is not null)
        {
            writer.WriteStartElement("item-price-base-quantity", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-149");
            ItemPriceBaseQuantity.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemPriceBaseQuantityUnitOfMeasureCode is not null)
        {
            writer.WriteStartElement("item-price-base-quantity-unit-of-measure-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-150");
            ItemPriceBaseQuantityUnitOfMeasureCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static SubInvoiceLinePriceDetails Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("sub-invoice-line-price-details", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("item-net-price", IRConfig.NS);
        reader.MoveToContent();

        UnitPriceAmount itemNetPrice = UnitPriceAmount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        UnitPriceAmount? itemPriceDiscount = null;

        if (reader.IsStartElement("item-price-discount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            itemPriceDiscount = UnitPriceAmount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        UnitPriceAmount? itemGrossPrice = null;

        if (reader.IsStartElement("item-gross-price", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            itemGrossPrice = UnitPriceAmount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Quantity? itemPriceBaseQuantity = null;

        if (reader.IsStartElement("item-price-base-quantity", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            itemPriceBaseQuantity = Quantity.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? itemPriceBaseQuantityUnitOfMeasureCode = null;

        if (reader.IsStartElement("item-price-base-quantity-unit-of-measure-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            itemPriceBaseQuantityUnitOfMeasureCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SubInvoiceLinePriceDetails
        {
            ItemNetPrice = itemNetPrice,
            ItemPriceDiscount = itemPriceDiscount,
            ItemGrossPrice = itemGrossPrice,
            ItemPriceBaseQuantity = itemPriceBaseQuantity,
            ItemPriceBaseQuantityUnitOfMeasureCode = itemPriceBaseQuantityUnitOfMeasureCode,
        };
    }
}
