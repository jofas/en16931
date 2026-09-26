using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct InvoiceLinePeriod : IIRDeserializable<InvoiceLinePeriod>, IIRSerializable, ICanBeEmpty
{
    // BT-134
    public required Date? InvoiceLinePeriodStartDate { get; init; }

    // BT-135
    public required Date? InvoiceLinePeriodEndDate { get; init; }

    public bool IsEmpty
    {
        get => InvoiceLinePeriodStartDate is null
            && InvoiceLinePeriodEndDate is null;
    }

    public void Serialize(XmlWriter writer)
    {
        if (IsEmpty) return;

        writer.WriteStartElement("invoice-line-period", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-26");

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
}
