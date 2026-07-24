using System.Collections.Generic;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Utils;
using S = En16931.Specs;

namespace En16931.Model.XRechnungExtension;

// TODO: add missing types and fields:
//  * Third Party Payment (field in invoice) (BG-DEX-09)
//  * Sub Invoice Line (BG-DEX-01)
//    - Sub Invoice Line Item Information (BG-DEX-02)
//    - Sub Invoice Line Allowance (BG-DEX-03)
//    - Sub Invoice Line Charge (BG-DEX-04)
//    - Sub Invoice Line Period (BG-DEX-05)
//    - Sub Invoice Line Vat Information (BG-DEX-06)
//    - Sub Invoice Line Price Details (BG-DEX-07)
//    - Sub Invoice Line Item Attributes (BG-DEX-08)

public readonly record struct XRechnungExtensionInvoice : IInvoice, IIRDeserializable<XRechnungExtensionInvoice>, IIRSerializable
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
    public required XRechnungExtensionProcessControl ProcessControl { get; init; }

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
    // TODO: Serialization
    public required Array<XRechnungExtensionThirdPartyPayment> ThirdPartyPayments { get; init; }

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

        writer.WriteEndElement();

        writer.WriteEndDocument();
    }

    public static XRechnungExtensionInvoice Deserialize(XmlReader reader)
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

        XRechnungExtensionProcessControl processControl = XRechnungExtensionProcessControl.Deserialize(reader);

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

        reader.ReadEndElement();
        reader.MoveToContent();

        return new XRechnungExtensionInvoice
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
        };
    }
}

public readonly record struct XRechnungExtensionProcessControl : IProcessControl, IIRDeserializable<XRechnungExtensionProcessControl>, IIRSerializable
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

    public static XRechnungExtensionProcessControl Deserialize(XmlReader reader)
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

        return new XRechnungExtensionProcessControl
        {
            BusinessProcessType = businessProcessType,
        };
    }
}

public readonly record struct XRechnungExtensionThirdPartyPayment : IIRDeserializable<XRechnungExtensionThirdPartyPayment>, IIRSerializable
{
    public void Serialize(XmlWriter writer)
    {
        throw new System.NotImplementedException();
    }

    public static XRechnungExtensionThirdPartyPayment Deserialize(XmlReader reader)
    {
        throw new System.NotImplementedException();
    }
}
