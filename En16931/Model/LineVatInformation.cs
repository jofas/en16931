using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct LineVatInformation : IIRDeserializable<LineVatInformation>, IIRSerializable
{
    // BT-151
    // UNTDID 5305
    public required Code InvoicedItemVatCategoryCode { get; init; }

    // BT-152
    public required Percentage? InvoicedItemVatRate { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("line-vat-information", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-30");

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
}
