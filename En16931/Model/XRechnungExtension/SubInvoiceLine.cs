using System.Collections.Generic;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.IR;
using En16931.Model.Primitives;

namespace En16931.Model.XRechnungExtension;

public record class SubInvoiceLine
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
