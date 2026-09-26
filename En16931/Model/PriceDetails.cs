using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct PriceDetails : IIRDeserializable<PriceDetails>, IIRSerializable
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

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("price-details", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-29");

        writer.WriteStartElement("item-net-price", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-146");
        ItemNetPrice.Serialize(writer);
        writer.WriteEndElement();

        if (ItemPriceDiscount is not null)
        {
            writer.WriteStartElement("item-price-discount", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-147");
            ItemPriceDiscount.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemGrossPrice is not null)
        {
            writer.WriteStartElement("item-gross-price", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-148");
            ItemGrossPrice.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemPriceBaseQuantity is not null)
        {
            writer.WriteStartElement("item-price-base-quantity", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-149");
            ItemPriceBaseQuantity.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemPriceBaseQuantityUnitOfMeasureCode is not null)
        {
            writer.WriteStartElement("item-price-base-quantity-unit-of-measure-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-150");
            ItemPriceBaseQuantityUnitOfMeasureCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

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
}
