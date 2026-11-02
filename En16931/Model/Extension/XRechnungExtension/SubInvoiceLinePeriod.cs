using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;

namespace En16931.Model.Extension.XRechnungExtension;

public readonly record struct SubInvoiceLinePeriod
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
