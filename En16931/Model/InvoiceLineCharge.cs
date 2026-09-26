using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct InvoiceLineCharge : IIRDeserializable<InvoiceLineCharge>, IIRSerializable
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
        writer.WriteStartElement("invoice-line-charge", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-28");

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
}
