using System.Collections.Generic;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

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
}
