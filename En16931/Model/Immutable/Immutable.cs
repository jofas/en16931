using System.Collections.Generic;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.IR;
using En16931.Model.Conversions;
using En16931.Model.Immutable.Primitives;
using Mut = En16931.Model;

namespace En16931.Model.Immutable;

public readonly record struct Invoice : IIRDeserializable<Invoice>, IToMutable<Mut.Invoice>
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
            payee = Immutable.Payee.Deserialize(reader);
        }

        SellerTaxRepresentativeParty? sellerTaxRepresentativeParty = null;

        if (reader.IsStartElement("seller-tax-representative-party", IRConfig.NS))
        {
            sellerTaxRepresentativeParty = Immutable.SellerTaxRepresentativeParty.Deserialize(reader);
        }

        DeliveryInformation? deliveryInformation = null;

        if (reader.IsStartElement("delivery-information", IRConfig.NS))
        {
            deliveryInformation = Immutable.DeliveryInformation.Deserialize(reader);
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
            vatBreadownBuilder.Add(Immutable.VatBreakdown.Deserialize(reader));
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
        };
    }

    public Mut.Invoice ToMutable()
    {
        return new Mut.Invoice
        {
            InvoiceNumber = InvoiceNumber.ToMutable(),
            InvoiceIssueDate = InvoiceIssueDate.ToMutable(),
            InvoiceTypeCode = InvoiceTypeCode.ToMutable(),
            InvoiceCurrencyCode = InvoiceCurrencyCode.ToMutable(),
            VatAccountingCurrencyCode = VatAccountingCurrencyCode?.ToMutable(),
            ValueAddedTaxPointDate = ValueAddedTaxPointDate?.ToMutable(),
            ValueAddedTaxPointDateCode = ValueAddedTaxPointDateCode?.ToMutable(),
            PaymentDueDate = PaymentDueDate?.ToMutable(),
            BuyerReference = BuyerReference.ToMutable(),
            ProjectReference = ProjectReference?.ToMutable(),
            ContractReference = ContractReference?.ToMutable(),
            PurchaseOrderReference = PurchaseOrderReference?.ToMutable(),
            SalesOrderReference = SalesOrderReference?.ToMutable(),
            ReceivingAdviceReference = ReceivingAdviceReference?.ToMutable(),
            DespatchAdviceReference = DespatchAdviceReference?.ToMutable(),
            TenderOrLotReference = TenderOrLotReference?.ToMutable(),
            InvoicedObjectIdentifier = InvoicedObjectIdentifier?.ToMutable(),
            BuyerAccountingReference = BuyerAccountingReference?.ToMutable(),
            PaymentTerms = PaymentTerms?.ToMutable(),
            InvoiceNotes = InvoiceNotes.ToMutable<InvoiceNote, Mut.InvoiceNote>(),
            ProcessControl = ProcessControl.ToMutable(),
            PrecedingInvoiceReferences = PrecedingInvoiceReferences.ToMutable<PrecedingInvoiceReference, Mut.PrecedingInvoiceReference>(),
            Seller = Seller.ToMutable(),
            Buyer = Buyer.ToMutable(),
            Payee = Payee?.ToMutable(),
            SellerTaxRepresentativeParty = SellerTaxRepresentativeParty?.ToMutable(),
            DeliveryInformation = DeliveryInformation?.ToMutable(),
            PaymentInstructions = PaymentInstructions.ToMutable(),
            DocumentLevelAllowances = DocumentLevelAllowances.ToMutable<DocumentLevelAllowance, Mut.DocumentLevelAllowance>(),
            DocumentLevelCharges = DocumentLevelCharges.ToMutable<DocumentLevelCharge, Mut.DocumentLevelCharge>(),
            DocumentTotals = DocumentTotals.ToMutable(),
            VatBreakdown = VatBreakdown.ToMutable<VatBreakdown, Mut.VatBreakdown>(),
            AdditionalSupportingDocuments = AdditionalSupportingDocuments.ToMutable<AdditionalSupportingDocument, Mut.AdditionalSupportingDocument>(),
            InvoiceLines = InvoiceLines.ToMutable<InvoiceLine, Mut.InvoiceLine>(),
        };
    }
}

public readonly record struct InvoiceNote : IIRDeserializable<InvoiceNote>, IToMutable<Mut.InvoiceNote>
{
    // BT-21
    public required Code? InvoiceNoteSubjectCode { get; init; }

    // BT-22
    public required Text Note { get; init; }

    public static InvoiceNote Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("invoice-note", IRConfig.NS);
        reader.MoveToContent();

        Code? invoiceNoteSubjectCode = null;

        if (reader.IsStartElement("invoice-note-subject-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceNoteSubjectCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("invoice-note", IRConfig.NS);
        reader.MoveToContent();

        Text note = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new InvoiceNote
        {
            InvoiceNoteSubjectCode = invoiceNoteSubjectCode,
            Note = note,
        };
    }

    public Mut.InvoiceNote ToMutable()
    {
        return new Mut.InvoiceNote
        {
            InvoiceNoteSubjectCode = InvoiceNoteSubjectCode?.ToMutable(),
            Note = Note.ToMutable(),
        };
    }
}

public readonly record struct ProcessControl : IIRDeserializable<ProcessControl>, IToMutable<Mut.ProcessControl>
{
    // BT-23
    public required Text BusinessProcessType { get; init; }

    // BT-24
    public required Identifier SpecificationIdentifier { get; init; }

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

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new ProcessControl
        {
            BusinessProcessType = businessProcessType,
            SpecificationIdentifier = specificationIdentifier,
        };
    }

    public Mut.ProcessControl ToMutable()
    {
        return new Mut.ProcessControl
        {
            BusinessProcessType = BusinessProcessType.ToMutable(),
            SpecificationIdentifier = SpecificationIdentifier.ToMutable(),
        };
    }
}

public readonly record struct PrecedingInvoiceReference : IIRDeserializable<PrecedingInvoiceReference>, IToMutable<Mut.PrecedingInvoiceReference>
{
    // BT-25
    public required DocumentReference Reference { get; init; }

    // BT-26
    public required Date? PrecedingInvoiceIssueDate { get; init; }

    public static PrecedingInvoiceReference Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("preceding-invoice-reference", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("preceding-invoice-reference", IRConfig.NS);
        reader.MoveToContent();

        DocumentReference reference = DocumentReference.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Date? precedingInvoiceIssueDate = null;

        if (reader.IsStartElement("preceding-invoice-issue-date", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            precedingInvoiceIssueDate = Date.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new PrecedingInvoiceReference
        {
            Reference = reference,
            PrecedingInvoiceIssueDate = precedingInvoiceIssueDate,
        };
    }

    public Mut.PrecedingInvoiceReference ToMutable()
    {
        return new Mut.PrecedingInvoiceReference
        {
            Reference = Reference.ToMutable(),
            PrecedingInvoiceIssueDate = PrecedingInvoiceIssueDate?.ToMutable(),
        };
    }
}

public readonly record struct Seller : IIRDeserializable<Seller>, IToMutable<Mut.Seller>
{
    // BT-27
    public required Text SellerName { get; init; }

    // BT-28
    public required Text? SellerTradingName { get; init; }

    // BT-29
    public required Array<Identifier> SellerIdentifiers { get; init; }

    // BT-30
    public required Identifier? SellerLegalRegistrationIdentifier { get; init; }

    // BT-31
    public required Identifier? SellerVatIdentifier { get; init; }

    // BT-32
    public required Identifier? SellerTaxRegistrationIdentifier { get; init; }

    // BT-33
    public required Text? SellerAdditionalLegalInformation { get; init; }

    // BT-34
    public required Identifier SellerElectronicAddress { get; init; }

    // BG-5
    public required SellerPostalAddress SellerPostalAddress { get; init; }

    // BG-6
    public required SellerContact SellerContact { get; init; }

    public static Seller Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("seller", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("seller-name", IRConfig.NS);
        reader.MoveToContent();

        Text sellerName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? sellerTradingName = null;

        if (reader.IsStartElement("seller-trading-name", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerTradingName = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Array<Identifier> sellerIdentifiers = Array<Identifier>.Empty;

        if (reader.IsStartElement("seller-identifiers", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<Identifier> builder = [];
            while (reader.IsStartElement("seller-identifier", IRConfig.NS))
            {
                reader.ReadStartElement();
                reader.MoveToContent();

                builder.Add(Identifier.Deserialize(reader));

                reader.ReadEndElement();
                reader.MoveToContent();
            }

            sellerIdentifiers = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? sellerLegalRegistrationIdentifier = null;

        if (reader.IsStartElement("seller-legal-registration-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerLegalRegistrationIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? sellerVatIdentifier = null;

        if (reader.IsStartElement("seller-vat-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerVatIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? sellerTaxRegistrationIdentifier = null;

        if (reader.IsStartElement("seller-tax-registration-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerTaxRegistrationIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? sellerAdditionalLegalInformation = null;

        if (reader.IsStartElement("seller-additional-legal-information", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerAdditionalLegalInformation = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("seller-electronic-address", IRConfig.NS);
        reader.MoveToContent();

        Identifier sellerElectronicAddress = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        SellerPostalAddress sellerPostalAddress = SellerPostalAddress.Deserialize(reader);

        SellerContact sellerContact = SellerContact.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        return new Seller
        {
            SellerName = sellerName,
            SellerTradingName = sellerTradingName,
            SellerIdentifiers = sellerIdentifiers,
            SellerLegalRegistrationIdentifier = sellerLegalRegistrationIdentifier,
            SellerVatIdentifier = sellerVatIdentifier,
            SellerTaxRegistrationIdentifier = sellerTaxRegistrationIdentifier,
            SellerAdditionalLegalInformation = sellerAdditionalLegalInformation,
            SellerElectronicAddress = sellerElectronicAddress,
            SellerPostalAddress = sellerPostalAddress,
            SellerContact = sellerContact,
        };
    }

    public Mut.Seller ToMutable()
    {
        return new Mut.Seller
        {
            SellerName = SellerName.ToMutable(),
            SellerTradingName = SellerTradingName?.ToMutable(),
            SellerIdentifiers = SellerIdentifiers.ToMutable<Identifier, Mut.Primitives.Identifier>(),
            SellerLegalRegistrationIdentifier = SellerLegalRegistrationIdentifier?.ToMutable(),
            SellerVatIdentifier = SellerVatIdentifier?.ToMutable(),
            SellerTaxRegistrationIdentifier = SellerTaxRegistrationIdentifier?.ToMutable(),
            SellerAdditionalLegalInformation = SellerAdditionalLegalInformation?.ToMutable(),
            SellerElectronicAddress = SellerElectronicAddress.ToMutable(),
            SellerPostalAddress = SellerPostalAddress.ToMutable(),
            SellerContact = SellerContact.ToMutable(),
        };
    }

}

public readonly record struct SellerPostalAddress : IIRDeserializable<SellerPostalAddress>, IToMutable<Mut.SellerPostalAddress>
{
    // BT-35
    public required Text? SellerAddressLine1 { get; init; }

    // BT-36
    public required Text? SellerAddressLine2 { get; init; }

    // BT-162
    public required Text? SellerAddressLine3 { get; init; }

    // BT-37
    public required Text SellerCity { get; init; }

    // BT-38
    public required Text SellerPostCode { get; init; }

    // BT-39
    public required Text? SellerCountrySubdivision { get; init; }

    // BT-40
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code SellerCountryCode { get; init; }

    public static SellerPostalAddress Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("seller-postal-address", IRConfig.NS);
        reader.MoveToContent();

        Text? sellerAddressLine1 = null;

        if (reader.IsStartElement("seller-address-line-1", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerAddressLine1 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? sellerAddressLine2 = null;

        if (reader.IsStartElement("seller-address-line-2", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerAddressLine2 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? sellerAddressLine3 = null;

        if (reader.IsStartElement("seller-address-line-3", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerAddressLine3 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("seller-city", IRConfig.NS);
        reader.MoveToContent();

        Text sellerCity = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("seller-post-code", IRConfig.NS);
        reader.MoveToContent();

        Text sellerPostCode = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? sellerCountrySubdivision = null;

        if (reader.IsStartElement("seller-country-subdivision", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerCountrySubdivision = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("seller-country-code", IRConfig.NS);
        reader.MoveToContent();

        Code sellerCountryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SellerPostalAddress
        {
            SellerAddressLine1 = sellerAddressLine1,
            SellerAddressLine2 = sellerAddressLine2,
            SellerAddressLine3 = sellerAddressLine3,
            SellerCity = sellerCity,
            SellerPostCode = sellerPostCode,
            SellerCountrySubdivision = sellerCountrySubdivision,
            SellerCountryCode = sellerCountryCode,
        };
    }

    public Mut.SellerPostalAddress ToMutable()
    {
        return new Mut.SellerPostalAddress
        {
            SellerAddressLine1 = SellerAddressLine1?.ToMutable(),
            SellerAddressLine2 = SellerAddressLine2?.ToMutable(),
            SellerAddressLine3 = SellerAddressLine3?.ToMutable(),
            SellerCity = SellerCity.ToMutable(),
            SellerPostCode = SellerPostCode.ToMutable(),
            SellerCountrySubdivision = SellerCountrySubdivision?.ToMutable(),
            SellerCountryCode = SellerCountryCode.ToMutable(),
        };
    }
}

public readonly record struct SellerContact : IIRDeserializable<SellerContact>, IToMutable<Mut.SellerContact>
{
    // BT-41
    public required Text SellerContactPoint { get; init; }

    // BT-42
    public required Text SellerContactTelephoneNumber { get; init; }

    // BT-43
    public required Text SellerContactEmailAddress { get; init; }

    public static SellerContact Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("seller-contact", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("seller-contact-point", IRConfig.NS);
        reader.MoveToContent();

        Text sellerContactPoint = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("seller-contact-telephone-number", IRConfig.NS);
        reader.MoveToContent();

        Text sellerContactTelephoneNumber = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("seller-contact-email-address", IRConfig.NS);
        reader.MoveToContent();

        Text sellerContactEmailAddress = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SellerContact
        {
            SellerContactPoint = sellerContactPoint,
            SellerContactTelephoneNumber = sellerContactTelephoneNumber,
            SellerContactEmailAddress = sellerContactEmailAddress,
        };
    }

    public Mut.SellerContact ToMutable()
    {
        return new Mut.SellerContact
        {
            SellerContactPoint = SellerContactPoint.ToMutable(),
            SellerContactTelephoneNumber = SellerContactTelephoneNumber.ToMutable(),
            SellerContactEmailAddress = SellerContactEmailAddress.ToMutable(),
        };
    }
}

public readonly record struct Buyer : IIRDeserializable<Buyer>, IToMutable<Mut.Buyer>
{
    // BT-44
    public required Text BuyerName { get; init; }

    // BT-45
    public required Text? BuyerTradingName { get; init; }

    // BT-46
    public required Identifier? BuyerIdentifier { get; init; }

    // BT-47
    public required Identifier? BuyerLegalRegistrationIdentifier { get; init; }

    // BT-48
    public required Identifier? BuyerVatIdentifier { get; init; }

    // BT-49
    public required Identifier? BuyerElectronicAddress { get; init; }

    // BG-8
    public required BuyerPostalAddress BuyerPostalAddress { get; init; }

    // BG-9
    public required BuyerContact? BuyerContact { get; init; }

    public static Buyer Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("buyer", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("buyer-name", IRConfig.NS);
        reader.MoveToContent();

        Text buyerName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? buyerTradingName = null;

        if (reader.IsStartElement("buyer-trading-name", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerTradingName = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? buyerIdentifier = null;

        if (reader.IsStartElement("buyer-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? buyerLegalRegistrationIdentifier = null;

        if (reader.IsStartElement("buyer-legal-registration-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerLegalRegistrationIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? buyerVatIdentifier = null;

        if (reader.IsStartElement("buyer-vat-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerVatIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? buyerElectronicAddress = null;

        if (reader.IsStartElement("buyer-electronic-address", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerElectronicAddress = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        BuyerPostalAddress buyerPostalAddress = BuyerPostalAddress.Deserialize(reader);

        BuyerContact? buyerContact = null;

        if (reader.IsStartElement("buyer-contact", IRConfig.NS))
        {
            buyerContact = Immutable.BuyerContact.Deserialize(reader);
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new Buyer
        {
            BuyerName = buyerName,
            BuyerTradingName = buyerTradingName,
            BuyerIdentifier = buyerIdentifier,
            BuyerLegalRegistrationIdentifier = buyerLegalRegistrationIdentifier,
            BuyerVatIdentifier = buyerVatIdentifier,
            BuyerElectronicAddress = buyerElectronicAddress,
            BuyerPostalAddress = buyerPostalAddress,
            BuyerContact = buyerContact,
        };
    }

    public Mut.Buyer ToMutable()
    {
        return new Mut.Buyer
        {
            BuyerName = BuyerName.ToMutable(),
            BuyerTradingName = BuyerTradingName?.ToMutable(),
            BuyerIdentifier = BuyerIdentifier?.ToMutable(),
            BuyerLegalRegistrationIdentifier = BuyerLegalRegistrationIdentifier?.ToMutable(),
            BuyerVatIdentifier = BuyerVatIdentifier?.ToMutable(),
            BuyerElectronicAddress = BuyerElectronicAddress?.ToMutable(),
            BuyerPostalAddress = BuyerPostalAddress.ToMutable(),
            BuyerContact = BuyerContact?.ToMutable(),
        };
    }
}

public readonly record struct BuyerPostalAddress : IIRDeserializable<BuyerPostalAddress>, IToMutable<Mut.BuyerPostalAddress>
{
    // BT-50
    public required Text? BuyerAddressLine1 { get; init; }

    // BT-51
    public required Text? BuyerAddressLine2 { get; init; }

    // BT-163
    public required Text? BuyerAddressLine3 { get; init; }

    // BT-52
    public required Text BuyerCity { get; init; }

    // BT-53
    public required Text BuyerPostCode { get; init; }

    // BT-54
    public required Text? BuyerCountrySubdivision { get; init; }

    // BT-55
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code BuyerCountryCode { get; init; }

    public static BuyerPostalAddress Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("buyer-postal-address", IRConfig.NS);
        reader.MoveToContent();

        Text? buyerAddressLine1 = null;

        if (reader.IsStartElement("buyer-address-line-1", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerAddressLine1 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerAddressLine2 = null;

        if (reader.IsStartElement("buyer-address-line-2", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerAddressLine2 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerAddressLine3 = null;

        if (reader.IsStartElement("buyer-address-line-3", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerAddressLine3 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("buyer-city", IRConfig.NS);
        reader.MoveToContent();

        Text buyerCity = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("buyer-post-code", IRConfig.NS);
        reader.MoveToContent();

        Text buyerPostCode = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? buyerCountrySubdivision = null;

        if (reader.IsStartElement("buyer-country-subdivision", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerCountrySubdivision = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("buyer-country-code", IRConfig.NS);
        reader.MoveToContent();

        Code buyerCountryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new BuyerPostalAddress
        {
            BuyerAddressLine1 = buyerAddressLine1,
            BuyerAddressLine2 = buyerAddressLine2,
            BuyerAddressLine3 = buyerAddressLine3,
            BuyerCity = buyerCity,
            BuyerPostCode = buyerPostCode,
            BuyerCountrySubdivision = buyerCountrySubdivision,
            BuyerCountryCode = buyerCountryCode,
        };
    }

    public Mut.BuyerPostalAddress ToMutable()
    {
        return new Mut.BuyerPostalAddress
        {
            BuyerAddressLine1 = BuyerAddressLine1?.ToMutable(),
            BuyerAddressLine2 = BuyerAddressLine2?.ToMutable(),
            BuyerAddressLine3 = BuyerAddressLine3?.ToMutable(),
            BuyerCity = BuyerCity.ToMutable(),
            BuyerPostCode = BuyerPostCode.ToMutable(),
            BuyerCountrySubdivision = BuyerCountrySubdivision?.ToMutable(),
            BuyerCountryCode = BuyerCountryCode.ToMutable(),
        };
    }
}

public readonly record struct BuyerContact : IIRDeserializable<BuyerContact>, IToMutable<Mut.BuyerContact>
{
    // BT-56
    public required Text? BuyerContactPoint { get; init; }

    // BT-57
    public required Text? BuyerContactTelephoneNumber { get; init; }

    // BT-58
    public required Text? BuyerContactEmailAddress { get; init; }

    public static BuyerContact Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("buyer-contact", IRConfig.NS);
        reader.MoveToContent();

        Text? buyerContactPoint = null;

        if (reader.IsStartElement("buyer-contact-point", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerContactPoint = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerContactTelephoneNumber = null;

        if (reader.IsStartElement("buyer-contact-telephone-number", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerContactTelephoneNumber = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerContactEmailAddress = null;

        if (reader.IsStartElement("buyer-contact-email-address", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerContactEmailAddress = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new BuyerContact
        {
            BuyerContactPoint = buyerContactPoint,
            BuyerContactTelephoneNumber = buyerContactTelephoneNumber,
            BuyerContactEmailAddress = buyerContactEmailAddress,
        };
    }

    public Mut.BuyerContact ToMutable()
    {
        return new Mut.BuyerContact
        {
            BuyerContactPoint = BuyerContactPoint?.ToMutable(),
            BuyerContactTelephoneNumber = BuyerContactTelephoneNumber?.ToMutable(),
            BuyerContactEmailAddress = BuyerContactEmailAddress?.ToMutable(),
        };
    }
}

public readonly record struct Payee : IIRDeserializable<Payee>, IToMutable<Mut.Payee>
{
    // BT-59
    public required Text PayeeName { get; init; }

    // BT-60
    public required Identifier? PayeeIdentifier { get; init; }

    // BT-61
    public required Identifier? PayeeLegalRegistrationIdentifier { get; init; }

    public static Payee Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("payee", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("payee-name", IRConfig.NS);
        reader.MoveToContent();

        Text payeeName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Identifier? payeeIdentifier = null;

        if (reader.IsStartElement("payee-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            payeeIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? payeeLegalRegistrationIdentifier = null;

        if (reader.IsStartElement("payee-legal-registration-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            payeeLegalRegistrationIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new Payee
        {
            PayeeName = payeeName,
            PayeeIdentifier = payeeIdentifier,
            PayeeLegalRegistrationIdentifier = payeeLegalRegistrationIdentifier,
        };
    }

    public Mut.Payee ToMutable()
    {
        return new Mut.Payee
        {
            PayeeName = PayeeName.ToMutable(),
            PayeeIdentifier = PayeeIdentifier?.ToMutable(),
            PayeeLegalRegistrationIdentifier = PayeeLegalRegistrationIdentifier?.ToMutable(),
        };
    }
}

public readonly record struct SellerTaxRepresentativeParty : IIRDeserializable<SellerTaxRepresentativeParty>, IToMutable<Mut.SellerTaxRepresentativeParty>
{
    // BT-62
    public required Text SellerTaxRepresentativeName { get; init; }

    // BT-63
    public required Identifier SellerTaxRepresentativeVatIdentifier { get; init; }

    // BG-12
    public required SellerTaxRepresentativePostalAddress SellerTaxRepresentativePostalAddress { get; init; }

    public static SellerTaxRepresentativeParty Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("seller-tax-representative-party", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("seller-tax-representative-name", IRConfig.NS);
        reader.MoveToContent();

        Text sellerTaxRepresentativeName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("seller-tax-representative-vat-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier sellerTaxRepresentativeVatIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        SellerTaxRepresentativePostalAddress sellerTaxRepresentativePostalAddress = SellerTaxRepresentativePostalAddress.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SellerTaxRepresentativeParty
        {
            SellerTaxRepresentativeName = sellerTaxRepresentativeName,
            SellerTaxRepresentativeVatIdentifier = sellerTaxRepresentativeVatIdentifier,
            SellerTaxRepresentativePostalAddress = sellerTaxRepresentativePostalAddress,
        };
    }

    public Mut.SellerTaxRepresentativeParty ToMutable()
    {
        return new Mut.SellerTaxRepresentativeParty
        {
            SellerTaxRepresentativeName = SellerTaxRepresentativeName.ToMutable(),
            SellerTaxRepresentativeVatIdentifier = SellerTaxRepresentativeVatIdentifier.ToMutable(),
            SellerTaxRepresentativePostalAddress = SellerTaxRepresentativePostalAddress.ToMutable(),
        };
    }
}

public readonly record struct SellerTaxRepresentativePostalAddress : IIRDeserializable<SellerTaxRepresentativePostalAddress>, IToMutable<Mut.SellerTaxRepresentativePostalAddress>
{
    // BT-64
    public required Text? TaxRepresentativeAddressLine1 { get; init; }

    // BT-65
    public required Text? TaxRepresentativeAddressLine2 { get; init; }

    // BT-164
    public required Text? TaxRepresentativeAddressLine3 { get; init; }

    // BT-66
    public required Text? TaxRepresentativeCity { get; init; }

    // BT-67
    public required Text? TaxRepresentativePostCode { get; init; }

    // BT-68
    public required Text? TaxRepresentativeCountrySubdivision { get; init; }

    // BT-69
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code TaxRepresentativeCountryCode { get; init; }

    public static SellerTaxRepresentativePostalAddress Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("seller-tax-representative-postal-address", IRConfig.NS);
        reader.MoveToContent();

        Text? taxRepresentativeAddressLine1 = null;

        if (reader.IsStartElement("tax-representative-address-line-1", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativeAddressLine1 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? taxRepresentativeAddressLine2 = null;

        if (reader.IsStartElement("tax-representative-address-line-2", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativeAddressLine2 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? taxRepresentativeAddressLine3 = null;

        if (reader.IsStartElement("tax-representative-address-line-3", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativeAddressLine3 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? taxRepresentativeCity = null;

        if (reader.IsStartElement("tax-representative-city", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativeCity = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? taxRepresentativePostCode = null;

        if (reader.IsStartElement("tax-representative-post-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativePostCode = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? taxRepresentativeCountrySubdivision = null;

        if (reader.IsStartElement("tax-representative-country-subdivision", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativeCountrySubdivision = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("tax-representative-country-code", IRConfig.NS);
        reader.MoveToContent();

        Code taxRepresentativeCountryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SellerTaxRepresentativePostalAddress
        {
            TaxRepresentativeAddressLine1 = taxRepresentativeAddressLine1,
            TaxRepresentativeAddressLine2 = taxRepresentativeAddressLine2,
            TaxRepresentativeAddressLine3 = taxRepresentativeAddressLine3,
            TaxRepresentativeCity = taxRepresentativeCity,
            TaxRepresentativePostCode = taxRepresentativePostCode,
            TaxRepresentativeCountrySubdivision = taxRepresentativeCountrySubdivision,
            TaxRepresentativeCountryCode = taxRepresentativeCountryCode,
        };
    }

    public Mut.SellerTaxRepresentativePostalAddress ToMutable()
    {
        return new Mut.SellerTaxRepresentativePostalAddress
        {
            TaxRepresentativeAddressLine1 = TaxRepresentativeAddressLine1?.ToMutable(),
            TaxRepresentativeAddressLine2 = TaxRepresentativeAddressLine2?.ToMutable(),
            TaxRepresentativeAddressLine3 = TaxRepresentativeAddressLine3?.ToMutable(),
            TaxRepresentativeCity = TaxRepresentativeCity?.ToMutable(),
            TaxRepresentativePostCode = TaxRepresentativePostCode?.ToMutable(),
            TaxRepresentativeCountrySubdivision = TaxRepresentativeCountrySubdivision?.ToMutable(),
            TaxRepresentativeCountryCode = TaxRepresentativeCountryCode.ToMutable(),
        };
    }
}

public readonly record struct DeliveryInformation : IIRDeserializable<DeliveryInformation>, IToMutable<Mut.DeliveryInformation>
{
    // BT-70
    public required Text? DeliverToPartyName { get; init; }

    // BT-71
    public required Identifier? DeliverToLocationIdentifier { get; init; }

    // BT-72
    public required Date? ActualDeliveryDate { get; init; }

    // BG-14
    public required InvoicingPeriod? InvoicingPeriod { get; init; }

    // BG-15
    public required DeliverToAddress? DeliverToAddress { get; init; }

    public static DeliveryInformation Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("delivery-information", IRConfig.NS);
        reader.MoveToContent();

        Text? deliverToPartyName = null;

        if (reader.IsStartElement("deliver-to-party-name", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToPartyName = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? deliverToLocationIdentifier = null;

        if (reader.IsStartElement("deliver-to-location-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToLocationIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Date? actualDeliverDate = null;

        if (reader.IsStartElement("actual-delivery-date", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            actualDeliverDate = Date.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        InvoicingPeriod? invoicingPeriod = null;

        if (reader.IsStartElement("invoicing-period", IRConfig.NS))
        {
            invoicingPeriod = Immutable.InvoicingPeriod.Deserialize(reader);
        }

        DeliverToAddress? deliverToAddress = null;

        if (reader.IsStartElement("deliver-to-address", IRConfig.NS))
        {
            deliverToAddress = Immutable.DeliverToAddress.Deserialize(reader);
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DeliveryInformation
        {
            DeliverToPartyName = deliverToPartyName,
            DeliverToLocationIdentifier = deliverToLocationIdentifier,
            ActualDeliveryDate = actualDeliverDate,
            InvoicingPeriod = invoicingPeriod,
            DeliverToAddress = deliverToAddress,
        };
    }

    public Mut.DeliveryInformation ToMutable()
    {
        return new Mut.DeliveryInformation
        {
            DeliverToPartyName = DeliverToPartyName?.ToMutable(),
            DeliverToLocationIdentifier = DeliverToLocationIdentifier?.ToMutable(),
            ActualDeliveryDate = ActualDeliveryDate?.ToMutable(),
            InvoicingPeriod = InvoicingPeriod?.ToMutable(),
            DeliverToAddress = DeliverToAddress?.ToMutable(),
        };
    }
}

public readonly record struct InvoicingPeriod : IIRDeserializable<InvoicingPeriod>, IToMutable<Mut.InvoicingPeriod>
{
    // BT-73
    public required Date? InvoicingPeriodStartDate { get; init; }

    // BT-74
    public required Date? InvoicingPeriodEndDate { get; init; }

    public static InvoicingPeriod Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("invoicing-period", IRConfig.NS);
        reader.MoveToContent();

        Date? invoicingPeriodStartDate = null;

        if (reader.IsStartElement("invoicing-period-start-date", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoicingPeriodStartDate = Date.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Date? invoicingPeriodEndDate = null;

        if (reader.IsStartElement("invoicing-period-end-date", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoicingPeriodEndDate = Date.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new InvoicingPeriod
        {
            InvoicingPeriodStartDate = invoicingPeriodStartDate,
            InvoicingPeriodEndDate = invoicingPeriodEndDate,
        };
    }

    public Mut.InvoicingPeriod ToMutable()
    {
        return new Mut.InvoicingPeriod
        {
            InvoicingPeriodStartDate = InvoicingPeriodStartDate?.ToMutable(),
            InvoicingPeriodEndDate = InvoicingPeriodEndDate?.ToMutable(),
        };
    }
}

public readonly record struct DeliverToAddress : IIRDeserializable<DeliverToAddress>, IToMutable<Mut.DeliverToAddress>
{
    // BT-75
    public required Text? DeliverToAddressLine1 { get; init; }

    // BT-76
    public required Text? DeliverToAddressLine2 { get; init; }

    // BT-165
    public required Text? DeliverToAddressLine3 { get; init; }

    // BT-77
    public required Text DeliverToCity { get; init; }

    // BT-78
    public required Text DeliverToPostCode { get; init; }

    // BT-79
    public required Text? DeliverToCountrySubdivision { get; init; }

    // BT-80
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code DeliverToCountryCode { get; init; }

    public static DeliverToAddress Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("deliver-to-address", IRConfig.NS);
        reader.MoveToContent();

        Text? deliverToAddressLine1 = null;

        if (reader.IsStartElement("deliver-to-address-line-1", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToAddressLine1 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? deliverToAddressLine2 = null;

        if (reader.IsStartElement("deliver-to-address-line-2", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToAddressLine2 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? deliverToAddressLine3 = null;

        if (reader.IsStartElement("deliver-to-address-line-3", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToAddressLine3 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("deliver-to-city", IRConfig.NS);
        reader.MoveToContent();

        Text deliverToCity = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("deliver-to-post-code", IRConfig.NS);
        reader.MoveToContent();

        Text deliverToPostCode = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? deliverToCountrySubdivision = null;

        if (reader.IsStartElement("deliver-to-country-subdivision", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToCountrySubdivision = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("deliver-to-country-code", IRConfig.NS);
        reader.MoveToContent();

        Code deliverToCountryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DeliverToAddress
        {
            DeliverToAddressLine1 = deliverToAddressLine1,
            DeliverToAddressLine2 = deliverToAddressLine2,
            DeliverToAddressLine3 = deliverToAddressLine3,
            DeliverToCity = deliverToCity,
            DeliverToPostCode = deliverToPostCode,
            DeliverToCountrySubdivision = deliverToCountrySubdivision,
            DeliverToCountryCode = deliverToCountryCode,
        };
    }

    public Mut.DeliverToAddress ToMutable()
    {
        return new Mut.DeliverToAddress
        {
            DeliverToAddressLine1 = DeliverToAddressLine1?.ToMutable(),
            DeliverToAddressLine2 = DeliverToAddressLine2?.ToMutable(),
            DeliverToAddressLine3 = DeliverToAddressLine3?.ToMutable(),
            DeliverToCity = DeliverToCity.ToMutable(),
            DeliverToPostCode = DeliverToPostCode.ToMutable(),
            DeliverToCountrySubdivision = DeliverToCountrySubdivision?.ToMutable(),
            DeliverToCountryCode = DeliverToCountryCode.ToMutable(),
        };
    }
}

public readonly record struct PaymentInstructions : IIRDeserializable<PaymentInstructions>, IToMutable<Mut.PaymentInstructions>
{
    // BT-81
    // UNTDID-4461
    public required Code PaymentMeansTypeCode { get; init; }

    // BT-82
    public required Text? PaymentMeansText { get; init; }

    // BT-83
    public required Text? RemittanceInformation { get; init; }

    // BG-17
    public required Array<CreditTransfer> CreditTransfers { get; init; }

    // BG-18
    public required PaymentCardInformation? PaymentCardInformation { get; init; }

    // BG-19
    public required DirectDebit? DirectDebit { get; init; }

    public static PaymentInstructions Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("payment-instructions", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("payment-means-type-code", IRConfig.NS);
        reader.MoveToContent();

        Code paymentMeansTypeCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? paymentMeansText = null;

        if (reader.IsStartElement("payment-means-text", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paymentMeansText = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? remittanceInformation = null;

        if (reader.IsStartElement("remittance-information", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            remittanceInformation = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Array<CreditTransfer> creditTransfers = Array<CreditTransfer>.Empty;

        if (reader.IsStartElement("credit-transfers", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<CreditTransfer> builder = [];
            while (reader.IsStartElement("credit-transfer", IRConfig.NS))
            {
                builder.Add(CreditTransfer.Deserialize(reader));
            }

            creditTransfers = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        PaymentCardInformation? paymentCardInformation = null;

        if (reader.IsStartElement("payment-card-information", IRConfig.NS))
        {
            paymentCardInformation = Immutable.PaymentCardInformation.Deserialize(reader);
        }

        DirectDebit? directDebit = null;

        if (reader.IsStartElement("direct-debit", IRConfig.NS))
        {
            directDebit = Immutable.DirectDebit.Deserialize(reader);
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new PaymentInstructions
        {
            PaymentMeansTypeCode = paymentMeansTypeCode,
            PaymentMeansText = paymentMeansText,
            RemittanceInformation = remittanceInformation,
            CreditTransfers = creditTransfers,
            PaymentCardInformation = paymentCardInformation,
            DirectDebit = directDebit,
        };
    }

    public Mut.PaymentInstructions ToMutable()
    {
        return new Mut.PaymentInstructions
        {
            PaymentMeansTypeCode = PaymentMeansTypeCode.ToMutable(),
            PaymentMeansText = PaymentMeansText?.ToMutable(),
            RemittanceInformation = RemittanceInformation?.ToMutable(),
            CreditTransfers = CreditTransfers.ToMutable<CreditTransfer, Mut.CreditTransfer>(),
            PaymentCardInformation = PaymentCardInformation?.ToMutable(),
            DirectDebit = DirectDebit?.ToMutable(),
        };
    }
}

public readonly record struct CreditTransfer : IIRDeserializable<CreditTransfer>, IToMutable<Mut.CreditTransfer>
{
    // BT-84
    public required Identifier PaymentAccountIdentifier { get; init; }

    // BT-85
    public required Text? PaymentAccountName { get; init; }

    // BT-86
    public required Identifier? PaymentServiceProviderIdentifier { get; init; }

    public static CreditTransfer Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("credit-transfer", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("payment-account-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier paymentAccountIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? paymentAccountName = null;

        if (reader.IsStartElement("payment-account-name", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paymentAccountName = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? paymentServiceProviderIdentifier = null;

        if (reader.IsStartElement("payment-service-provider-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paymentServiceProviderIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new CreditTransfer
        {
            PaymentAccountIdentifier = paymentAccountIdentifier,
            PaymentAccountName = paymentAccountName,
            PaymentServiceProviderIdentifier = paymentServiceProviderIdentifier,
        };
    }

    public Mut.CreditTransfer ToMutable()
    {
        return new Mut.CreditTransfer
        {
            PaymentAccountIdentifier = PaymentAccountIdentifier.ToMutable(),
            PaymentAccountName = PaymentAccountName?.ToMutable(),
            PaymentServiceProviderIdentifier = PaymentServiceProviderIdentifier?.ToMutable(),
        };
    }
}

public readonly record struct PaymentCardInformation : IIRDeserializable<PaymentCardInformation>, IToMutable<Mut.PaymentCardInformation>
{
    // BT-87
    public required Text PaymentCardPrimaryAccountNumber { get; init; }

    // BT-88
    public required Text? PaymentCardHolderName { get; init; }

    public static PaymentCardInformation Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("payment-card-information", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("payment-card-primary-account-number", IRConfig.NS);
        reader.MoveToContent();

        Text paymentCardPrimaryAccountNumber = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? paymentCardHolderName = null;

        if (reader.IsStartElement("payment-card-holder-name", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paymentCardHolderName = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new PaymentCardInformation
        {
            PaymentCardPrimaryAccountNumber = paymentCardPrimaryAccountNumber,
            PaymentCardHolderName = paymentCardHolderName,
        };
    }

    public Mut.PaymentCardInformation ToMutable()
    {
        return new Mut.PaymentCardInformation
        {
            PaymentCardPrimaryAccountNumber = PaymentCardPrimaryAccountNumber.ToMutable(),
            PaymentCardHolderName = PaymentCardHolderName?.ToMutable(),
        };
    }
}

public readonly record struct DirectDebit : IIRDeserializable<DirectDebit>, IToMutable<Mut.DirectDebit>
{
    // BT-89
    public required Identifier MandateReferenceIdentifier { get; init; }

    // BT-90
    public required Identifier BankAssignedCreditorIdentifier { get; init; }

    // BT-91
    public required Identifier DebitedAccountIdentifier { get; init; }

    public static DirectDebit Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("direct-debit", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("mandate-reference-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier mandateReferenceIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("bank-assigned-creditor-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier bankAssignedCreditorIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("debited-account-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier debitedAccountIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DirectDebit
        {
            MandateReferenceIdentifier = mandateReferenceIdentifier,
            BankAssignedCreditorIdentifier = bankAssignedCreditorIdentifier,
            DebitedAccountIdentifier = debitedAccountIdentifier,
        };
    }

    public Mut.DirectDebit ToMutable()
    {
        return new Mut.DirectDebit
        {
            MandateReferenceIdentifier = MandateReferenceIdentifier.ToMutable(),
            BankAssignedCreditorIdentifier = BankAssignedCreditorIdentifier.ToMutable(),
            DebitedAccountIdentifier = DebitedAccountIdentifier.ToMutable(),
        };
    }
}

public readonly record struct DocumentLevelAllowance : IIRDeserializable<DocumentLevelAllowance>, IToMutable<Mut.DocumentLevelAllowance>
{
    // BT-92
    public required Amount DocumentLevelAllowanceAmount { get; init; }

    // BT-93
    public required Amount? DocumentLevelAllowanceBaseAmount { get; init; }

    // BT-94
    public required Percentage? DocumentLevelAllowancePercentage { get; init; }

    // BT-95
    public required Code DocumentLevelAllowanceVatCategoryCode { get; init; }

    // BT-96
    public required Percentage? DocumentLevelAllowanceVatRate { get; init; }

    // BT-97
    public required Text? DocumentLevelAllowanceReason { get; init; }

    // BT-98
    public required Code? DocumentLevelAllowanceReasonCode { get; init; }

    public static DocumentLevelAllowance Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("document-level-allowance", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("document-level-allowance-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount documentLevelAllowanceAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? documentLevelAllowanceBaseAmount = null;

        if (reader.IsStartElement("document-level-allowance-base-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelAllowanceBaseAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Percentage? documentLevelAllowancePercentage = null;

        if (reader.IsStartElement("document-level-allowance-percentage", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelAllowancePercentage = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("document-level-allowance-vat-category-code", IRConfig.NS);
        reader.MoveToContent();

        Code documentLevelAllowanceVatCategoryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Percentage? documentLevelAllowanceVatRate = null;

        if (reader.IsStartElement("document-level-allowance-vat-rate", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelAllowanceVatRate = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? documentLevelAllowanceReason = null;

        if (reader.IsStartElement("document-level-allowance-reason", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelAllowanceReason = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? documentLevelAllowanceReasonCode = null;

        if (reader.IsStartElement("document-level-allowance-reason-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelAllowanceReasonCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DocumentLevelAllowance
        {
            DocumentLevelAllowanceAmount = documentLevelAllowanceAmount,
            DocumentLevelAllowanceBaseAmount = documentLevelAllowanceBaseAmount,
            DocumentLevelAllowancePercentage = documentLevelAllowancePercentage,
            DocumentLevelAllowanceVatCategoryCode = documentLevelAllowanceVatCategoryCode,
            DocumentLevelAllowanceVatRate = documentLevelAllowanceVatRate,
            DocumentLevelAllowanceReason = documentLevelAllowanceReason,
            DocumentLevelAllowanceReasonCode = documentLevelAllowanceReasonCode,
        };
    }

    public Mut.DocumentLevelAllowance ToMutable()
    {
        return new Mut.DocumentLevelAllowance
        {
            DocumentLevelAllowanceAmount = DocumentLevelAllowanceAmount.ToMutable(),
            DocumentLevelAllowanceBaseAmount = DocumentLevelAllowanceBaseAmount?.ToMutable(),
            DocumentLevelAllowancePercentage = DocumentLevelAllowancePercentage?.ToMutable(),
            DocumentLevelAllowanceVatCategoryCode = DocumentLevelAllowanceVatCategoryCode.ToMutable(),
            DocumentLevelAllowanceVatRate = DocumentLevelAllowanceVatRate?.ToMutable(),
            DocumentLevelAllowanceReason = DocumentLevelAllowanceReason?.ToMutable(),
            DocumentLevelAllowanceReasonCode = DocumentLevelAllowanceReasonCode?.ToMutable(),
        };
    }
}

public readonly record struct DocumentLevelCharge : IIRDeserializable<DocumentLevelCharge>, IToMutable<Mut.DocumentLevelCharge>
{
    // BT-99
    public required Amount DocumentLevelChargeAmount { get; init; }

    // BT-100
    public required Amount? DocumentLevelChargeBaseAmount { get; init; }

    // BT-101
    public required Percentage? DocumentLevelChargePercentage { get; init; }

    // BT-102
    public required Code DocumentLevelChargeVatCategoryCode { get; init; }

    // BT-103
    public required Percentage? DocumentLevelChargeVatRate { get; init; }

    // BT-104
    public required Text? DocumentLevelChargeReason { get; init; }

    // BT-105
    public required Code? DocumentLevelChargeReasonCode { get; init; }

    public static DocumentLevelCharge Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("document-level-charge", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("document-level-charge-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount documentLevelChargeAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? documentLevelChargeBaseAmount = null;

        if (reader.IsStartElement("document-level-charge-base-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelChargeBaseAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Percentage? documentLevelChargePercentage = null;

        if (reader.IsStartElement("document-level-charge-percentage", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelChargePercentage = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("document-level-charge-vat-category-code", IRConfig.NS);
        reader.MoveToContent();

        Code documentLevelChargeVatCategoryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Percentage? documentLevelChargeVatRate = null;

        if (reader.IsStartElement("document-level-charge-vat-rate", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelChargeVatRate = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? documentLevelChargeReason = null;

        if (reader.IsStartElement("document-level-charge-reason", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelChargeReason = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? documentLevelChargeReasonCode = null;

        if (reader.IsStartElement("document-level-charge-reason-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelChargeReasonCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DocumentLevelCharge
        {
            DocumentLevelChargeAmount = documentLevelChargeAmount,
            DocumentLevelChargeBaseAmount = documentLevelChargeBaseAmount,
            DocumentLevelChargePercentage = documentLevelChargePercentage,
            DocumentLevelChargeVatCategoryCode = documentLevelChargeVatCategoryCode,
            DocumentLevelChargeVatRate = documentLevelChargeVatRate,
            DocumentLevelChargeReason = documentLevelChargeReason,
            DocumentLevelChargeReasonCode = documentLevelChargeReasonCode,
        };
    }

    public Mut.DocumentLevelCharge ToMutable()
    {
        return new Mut.DocumentLevelCharge
        {
            DocumentLevelChargeAmount = DocumentLevelChargeAmount.ToMutable(),
            DocumentLevelChargeBaseAmount = DocumentLevelChargeBaseAmount?.ToMutable(),
            DocumentLevelChargePercentage = DocumentLevelChargePercentage?.ToMutable(),
            DocumentLevelChargeVatCategoryCode = DocumentLevelChargeVatCategoryCode.ToMutable(),
            DocumentLevelChargeVatRate = DocumentLevelChargeVatRate?.ToMutable(),
            DocumentLevelChargeReason = DocumentLevelChargeReason?.ToMutable(),
            DocumentLevelChargeReasonCode = DocumentLevelChargeReasonCode?.ToMutable(),
        };
    }
}

public readonly record struct DocumentTotals : IIRDeserializable<DocumentTotals>, IToMutable<Mut.DocumentTotals>
{
    // BT-106
    public required Amount SumOfInvoiceLineNetAmount { get; init; }

    // BT-107
    public required Amount? SumOfAllowancesOnDocumentLevel { get; init; }

    // BT-108
    public required Amount? SumOfChargesOnDocumentLevel { get; init; }

    // BT-109
    public required Amount InvoiceTotalAmountWithoutVat { get; init; }

    // BT-110
    public required Amount? InvoiceTotalVatAmount { get; init; }

    // BT-111
    public required Amount? InvoiceTotalVatAmountInAccountingCurrency { get; init; }

    // BT-112
    public required Amount InvoiceTotalAmountWithVat { get; init; }

    // BT-113
    public required Amount? PaidAmount { get; init; }

    // BT-114
    public required Amount? RoundingAmount { get; init; }

    // BT-115
    public required Amount AmountDueForPayment { get; init; }

    public static DocumentTotals Deserialize(XmlReader reader)
    {

        reader.ReadStartElement("document-totals", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("sum-of-invoice-line-net-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount sumOfInvoiceLineNetAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? sumOfAllowancesOnDocumentLevel = null;

        if (reader.IsStartElement("sum-of-allowances-on-document-level", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sumOfAllowancesOnDocumentLevel = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Amount? sumOfChargesOnDocumentLevel = null;

        if (reader.IsStartElement("sum-of-charges-on-document-level", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sumOfChargesOnDocumentLevel = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("invoice-total-amount-without-vat", IRConfig.NS);
        reader.MoveToContent();

        Amount invoiceTotalAmountWithoutVat = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? invoiceTotalVatAmount = null;

        if (reader.IsStartElement("invoice-total-vat-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceTotalVatAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Amount? invoiceTotalVatAmountInAccountingCurrency = null;

        if (reader.IsStartElement("invoice-total-vat-amount-in-accounting-currency", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceTotalVatAmountInAccountingCurrency = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("invoice-total-amount-with-vat", IRConfig.NS);
        reader.MoveToContent();

        Amount invoiceTotalAmountWithVat = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? paidAmount = null;

        if (reader.IsStartElement("paid-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paidAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Amount? roundingAmount = null;

        if (reader.IsStartElement("rounding-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            roundingAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("amount-due-for-payment", IRConfig.NS);
        reader.MoveToContent();

        Amount amountDueForPayment = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DocumentTotals
        {
            SumOfInvoiceLineNetAmount = sumOfInvoiceLineNetAmount,
            SumOfAllowancesOnDocumentLevel = sumOfAllowancesOnDocumentLevel,
            SumOfChargesOnDocumentLevel = sumOfChargesOnDocumentLevel,
            InvoiceTotalAmountWithoutVat = invoiceTotalAmountWithoutVat,
            InvoiceTotalVatAmount = invoiceTotalVatAmount,
            InvoiceTotalVatAmountInAccountingCurrency = invoiceTotalVatAmountInAccountingCurrency,
            InvoiceTotalAmountWithVat = invoiceTotalAmountWithVat,
            PaidAmount = paidAmount,
            RoundingAmount = roundingAmount,
            AmountDueForPayment = amountDueForPayment,
        };
    }

    public Mut.DocumentTotals ToMutable()
    {
        return new Mut.DocumentTotals
        {
            SumOfInvoiceLineNetAmount = SumOfInvoiceLineNetAmount.ToMutable(),
            SumOfAllowancesOnDocumentLevel = SumOfAllowancesOnDocumentLevel?.ToMutable(),
            SumOfChargesOnDocumentLevel = SumOfChargesOnDocumentLevel?.ToMutable(),
            InvoiceTotalAmountWithoutVat = InvoiceTotalAmountWithoutVat.ToMutable(),
            InvoiceTotalVatAmount = InvoiceTotalVatAmount?.ToMutable(),
            InvoiceTotalVatAmountInAccountingCurrency = InvoiceTotalVatAmountInAccountingCurrency?.ToMutable(),
            InvoiceTotalAmountWithVat = InvoiceTotalAmountWithVat.ToMutable(),
            PaidAmount = PaidAmount?.ToMutable(),
            RoundingAmount = RoundingAmount?.ToMutable(),
            AmountDueForPayment = AmountDueForPayment.ToMutable(),
        };
    }
}

public readonly record struct VatBreakdown : IIRDeserializable<VatBreakdown>, IToMutable<Mut.VatBreakdown>
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
    public required Text? VatExemptionReasonText { get; init; }

    // BT-121
    // VATEX Vat exemption reason code list
    public required Code? VatExemptionReasonCode { get; init; }

    public static VatBreakdown Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("vat-breakdown", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("vat-category-taxable-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount vatCategoryTaxableAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("vat-category-tax-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount vatCategoryTaxAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("vat-category-code", IRConfig.NS);
        reader.MoveToContent();

        Code vatCategoryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("vat-category-rate", IRConfig.NS);
        reader.MoveToContent();

        Percentage vatCategoryRate = Percentage.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? vatExemptionReasonText = null;

        if (reader.IsStartElement("vat-exemption-reason-text", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            vatExemptionReasonText = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? vatExemptionReasonCode = null;

        if (reader.IsStartElement("vat-exemption-reason-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            vatExemptionReasonCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new VatBreakdown
        {
            VatCategoryTaxableAmount = vatCategoryTaxableAmount,
            VatCategoryTaxAmount = vatCategoryTaxAmount,
            VatCategoryCode = vatCategoryCode,
            VatCategoryRate = vatCategoryRate,
            VatExemptionReasonText = vatExemptionReasonText,
            VatExemptionReasonCode = vatExemptionReasonCode,
        };
    }

    public Mut.VatBreakdown ToMutable()
    {
        return new Mut.VatBreakdown
        {
            VatCategoryTaxableAmount = VatCategoryTaxableAmount.ToMutable(),
            VatCategoryTaxAmount = VatCategoryTaxAmount.ToMutable(),
            VatCategoryCode = VatCategoryCode.ToMutable(),
            VatCategoryRate = VatCategoryRate.ToMutable(),
            VatExemptionReasonText = VatExemptionReasonText?.ToMutable(),
            VatExemptionReasonCode = VatExemptionReasonCode?.ToMutable(),
        };
    }
}

public readonly record struct AdditionalSupportingDocument : IIRDeserializable<AdditionalSupportingDocument>, IToMutable<Mut.AdditionalSupportingDocument>
{
    // BT-122
    public required DocumentReference SupportingDocumentReference { get; init; }

    // BT-123
    public required Text? SupportingDocumentDescription { get; init; }

    // BT-124
    public required Text? ExternalDocumentLocation { get; init; }

    // BT-125
    public required BinaryObject? AttachedDocument { get; init; }

    public static AdditionalSupportingDocument Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("additional-supporting-document", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("supporting-document-reference", IRConfig.NS);
        reader.MoveToContent();

        DocumentReference supportingDocumentReference = DocumentReference.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? supportingDocumentDescription = null;

        if (reader.IsStartElement("supporting-document-description", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            supportingDocumentDescription = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? externalDocumentLocation = null;

        if (reader.IsStartElement("external-document-location", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            externalDocumentLocation = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        BinaryObject? attachedDocument = null;

        if (reader.IsStartElement("attached-document", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            attachedDocument = BinaryObject.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new AdditionalSupportingDocument
        {
            SupportingDocumentReference = supportingDocumentReference,
            SupportingDocumentDescription = supportingDocumentDescription,
            ExternalDocumentLocation = externalDocumentLocation,
            AttachedDocument = attachedDocument,
        };
    }

    public Mut.AdditionalSupportingDocument ToMutable()
    {
        return new Mut.AdditionalSupportingDocument
        {
            SupportingDocumentReference = SupportingDocumentReference.ToMutable(),
            SupportingDocumentDescription = SupportingDocumentDescription?.ToMutable(),
            ExternalDocumentLocation = ExternalDocumentLocation?.ToMutable(),
            AttachedDocument = AttachedDocument?.ToMutable(),
        };
    }
}

public readonly record struct InvoiceLine : IIRDeserializable<InvoiceLine>, IToMutable<Mut.InvoiceLine>
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
            invoiceLinePeriod = Immutable.InvoiceLinePeriod.Deserialize(reader);
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
        };
    }

    public Mut.InvoiceLine ToMutable()
    {
        return new Mut.InvoiceLine
        {
            InvoiceLineIdentifier = InvoiceLineIdentifier.ToMutable(),
            InvoiceLineNote = InvoiceLineNote?.ToMutable(),
            InvoiceLineObjectIdentifier = InvoiceLineObjectIdentifier?.ToMutable(),
            InvoicedQuantity = InvoicedQuantity.ToMutable(),
            InvoicedQuantityUnitOfMeasureCode = InvoicedQuantityUnitOfMeasureCode.ToMutable(),
            InvoiceLineNetAmount = InvoiceLineNetAmount.ToMutable(),
            ReferencedPurchaseOrderLineReference = ReferencedPurchaseOrderLineReference?.ToMutable(),
            InvoiceLineBuyerAccountingReference = InvoiceLineBuyerAccountingReference?.ToMutable(),
            InvoiceLinePeriod = InvoiceLinePeriod?.ToMutable(),
            InvoiceLineAllowances = InvoiceLineAllowances.ToMutable<InvoiceLineAllowance, Mut.InvoiceLineAllowance>(),
            InvoiceLineCharges = InvoiceLineCharges.ToMutable<InvoiceLineCharge, Mut.InvoiceLineCharge>(),
            PriceDetails = PriceDetails.ToMutable(),
            LineVatInformation = LineVatInformation.ToMutable(),
            ItemInformation = ItemInformation.ToMutable(),
        };
    }
}

public readonly record struct InvoiceLinePeriod : IIRDeserializable<InvoiceLinePeriod>, IToMutable<Mut.InvoiceLinePeriod>
{
    // BT-134
    public required Date? InvoiceLinePeriodStartDate { get; init; }

    // BT-135
    public required Date? InvoiceLinePeriodEndDate { get; init; }

    public static InvoiceLinePeriod Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("invoice-line-period", IRConfig.NS);
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

        return new InvoiceLinePeriod
        {
            InvoiceLinePeriodStartDate = invoiceLinePeriodStartDate,
            InvoiceLinePeriodEndDate = invoiceLinePeriodEndDate,
        };
    }

    public Mut.InvoiceLinePeriod ToMutable()
    {
        return new Mut.InvoiceLinePeriod
        {
            InvoiceLinePeriodStartDate = InvoiceLinePeriodStartDate?.ToMutable(),
            InvoiceLinePeriodEndDate = InvoiceLinePeriodEndDate?.ToMutable(),
        };
    }
}

public readonly record struct InvoiceLineAllowance : IIRDeserializable<InvoiceLineAllowance>, IToMutable<Mut.InvoiceLineAllowance>
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

    public static InvoiceLineAllowance Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("invoice-line-allowance", IRConfig.NS);
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

        return new InvoiceLineAllowance
        {
            InvoiceLineAllowanceAmount = invoiceLineAllowanceAmount,
            InvoiceLineAllowanceBaseAmount = invoiceLineAllowanceBaseAmount,
            InvoiceLineAllowancePercentage = invoiceLineAllowancePercentage,
            InvoiceLineAllowanceReason = invoiceLineAllowanceReason,
            InvoiceLineAllowanceReasonCode = invoiceLineAllowanceReasonCode,
        };
    }

    public Mut.InvoiceLineAllowance ToMutable()
    {
        return new Mut.InvoiceLineAllowance
        {
            InvoiceLineAllowanceAmount = InvoiceLineAllowanceAmount.ToMutable(),
            InvoiceLineAllowanceBaseAmount = InvoiceLineAllowanceBaseAmount?.ToMutable(),
            InvoiceLineAllowancePercentage = InvoiceLineAllowancePercentage?.ToMutable(),
            InvoiceLineAllowanceReason = InvoiceLineAllowanceReason?.ToMutable(),
            InvoiceLineAllowanceReasonCode = InvoiceLineAllowanceReasonCode?.ToMutable(),
        };
    }
}

public readonly record struct InvoiceLineCharge : IIRDeserializable<InvoiceLineCharge>, IToMutable<Mut.InvoiceLineCharge>
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

    public static InvoiceLineCharge Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("invoice-line-charge", IRConfig.NS);
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

        return new InvoiceLineCharge
        {
            InvoiceLineChargeAmount = invoiceLineChargeAmount,
            InvoiceLineChargeBaseAmount = invoiceLineChargeBaseAmount,
            InvoiceLineChargePercentage = invoiceLineChargePercentage,
            InvoiceLineChargeReason = invoiceLineChargeReason,
            InvoiceLineChargeReasonCode = invoiceLineChargeReasonCode,
        };
    }

    public Mut.InvoiceLineCharge ToMutable()
    {
        return new Mut.InvoiceLineCharge
        {
            InvoiceLineChargeAmount = InvoiceLineChargeAmount.ToMutable(),
            InvoiceLineChargeBaseAmount = InvoiceLineChargeBaseAmount?.ToMutable(),
            InvoiceLineChargePercentage = InvoiceLineChargePercentage?.ToMutable(),
            InvoiceLineChargeReason = InvoiceLineChargeReason?.ToMutable(),
            InvoiceLineChargeReasonCode = InvoiceLineChargeReasonCode?.ToMutable(),
        };
    }
}

public readonly record struct PriceDetails : IIRDeserializable<PriceDetails>, IToMutable<Mut.PriceDetails>
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

    public static PriceDetails Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("price-details", IRConfig.NS);
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

        return new PriceDetails
        {
            ItemNetPrice = itemNetPrice,
            ItemPriceDiscount = itemPriceDiscount,
            ItemGrossPrice = itemGrossPrice,
            ItemPriceBaseQuantity = itemPriceBaseQuantity,
            ItemPriceBaseQuantityUnitOfMeasureCode = itemPriceBaseQuantityUnitOfMeasureCode,
        };
    }

    public Mut.PriceDetails ToMutable()
    {
        return new Mut.PriceDetails
        {
            ItemNetPrice = ItemNetPrice.ToMutable(),
            ItemPriceDiscount = ItemPriceDiscount?.ToMutable(),
            ItemGrossPrice = ItemGrossPrice?.ToMutable(),
            ItemPriceBaseQuantity = ItemPriceBaseQuantity?.ToMutable(),
            ItemPriceBaseQuantityUnitOfMeasureCode = ItemPriceBaseQuantityUnitOfMeasureCode?.ToMutable(),
        };
    }
}

public readonly record struct LineVatInformation : IIRDeserializable<LineVatInformation>, IToMutable<Mut.LineVatInformation>
{
    // BT-151
    // UNTDID 5305
    public required Code InvoicedItemVatCategoryCode { get; init; }

    // BT-152
    public required Percentage? InvoicedItemVatRate { get; init; }

    public static LineVatInformation Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("line-vat-information", IRConfig.NS);
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

        return new LineVatInformation
        {
            InvoicedItemVatCategoryCode = invoicedItemVatCategoryCode,
            InvoicedItemVatRate = invoicedItemVatRate,
        };
    }

    public Mut.LineVatInformation ToMutable()
    {
        return new Mut.LineVatInformation
        {
            InvoicedItemVatCategoryCode = InvoicedItemVatCategoryCode.ToMutable(),
            InvoicedItemVatRate = InvoicedItemVatRate?.ToMutable(),
        };
    }
}

public readonly record struct ItemInformation : IIRDeserializable<ItemInformation>, IToMutable<Mut.ItemInformation>
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

    // BG-32
    public required Array<ItemAttribute> ItemAttributes { get; init; }

    public static ItemInformation Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("item-information", IRConfig.NS);
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

        Array<ItemAttribute> itemAttributes = Array<ItemAttribute>.Empty;

        if (reader.IsStartElement("item-attributes", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<ItemAttribute> builder = [];
            while (reader.IsStartElement("item-attribute", IRConfig.NS))
            {
                builder.Add(ItemAttribute.Deserialize(reader));
            }

            itemAttributes = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new ItemInformation
        {
            ItemName = itemName,
            ItemDescription = itemDescription,
            ItemSellersIdentifier = itemSellersIdentifier,
            ItemBuyersIdentifier = itemBuyersIdentifier,
            ItemStandardIdentifier = itemStandardIdentifier,
            ItemClassificationIdentifiers = itemClassificationIdentifiers,
            ItemCountryOfOrigin = itemCountryOfOrigin,
            ItemAttributes = itemAttributes,
        };
    }

    public Mut.ItemInformation ToMutable()
    {
        return new Mut.ItemInformation
        {
            ItemName = ItemName.ToMutable(),
            ItemDescription = ItemDescription?.ToMutable(),
            ItemSellersIdentifier = ItemSellersIdentifier?.ToMutable(),
            ItemBuyersIdentifier = ItemBuyersIdentifier?.ToMutable(),
            ItemStandardIdentifier = ItemStandardIdentifier?.ToMutable(),
            ItemClassificationIdentifiers = ItemClassificationIdentifiers.ToMutable<Identifier, Mut.Primitives.Identifier>(),
            ItemCountryOfOrigin = ItemCountryOfOrigin?.ToMutable(),
            ItemAttributes = ItemAttributes.ToMutable<ItemAttribute, Mut.ItemAttribute>(),
        };
    }
}

public readonly record struct ItemAttribute : IIRDeserializable<ItemAttribute>, IToMutable<Mut.ItemAttribute>
{
    // BT-160
    public required Text ItemAttributeName { get; init; }

    // BT-161
    public required Text ItemAttributeValue { get; init; }

    public static ItemAttribute Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("item-attribute", IRConfig.NS);
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

        return new ItemAttribute
        {
            ItemAttributeName = itemAttributeName,
            ItemAttributeValue = itemAttributeValue,
        };
    }

    public Mut.ItemAttribute ToMutable()
    {
        return new Mut.ItemAttribute
        {
            ItemAttributeName = ItemAttributeName.ToMutable(),
            ItemAttributeValue = ItemAttributeValue.ToMutable(),
        };
    }
}
