using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct InvoicingPeriod : IIRDeserializable<InvoicingPeriod>, IIRSerializable, ICanBeEmpty
{
    // BT-73
    public required Date? InvoicingPeriodStartDate { get; init; }

    // BT-74
    public required Date? InvoicingPeriodEndDate { get; init; }

    public bool IsEmpty
    {
        get => InvoicingPeriodStartDate is null
            && InvoicingPeriodEndDate is null;
    }

    public void Serialize(XmlWriter writer)
    {
        if (IsEmpty) return;

        writer.WriteStartElement("invoicing-period", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-14");

        if (InvoicingPeriodStartDate is not null)
        {
            writer.WriteStartElement("invoicing-period-start-date", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-73");
            InvoicingPeriodStartDate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoicingPeriodEndDate is not null)
        {
            writer.WriteStartElement("invoicing-period-end-date", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-74");
            InvoicingPeriodEndDate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

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
}
