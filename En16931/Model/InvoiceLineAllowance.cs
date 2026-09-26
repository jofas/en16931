using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct InvoiceLineAllowance : IIRDeserializable<InvoiceLineAllowance>, IIRSerializable
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
        writer.WriteStartElement("invoice-line-allowance", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-27");

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
}
