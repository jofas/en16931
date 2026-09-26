using System.Collections.Generic;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct ItemInformation : IIRDeserializable<ItemInformation>, IIRSerializable
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

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("item-information", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-31");

        writer.WriteStartElement("item-name", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-153");
        ItemName.Serialize(writer);
        writer.WriteEndElement();

        if (ItemDescription is not null)
        {
            writer.WriteStartElement("item-description", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-154");
            ItemDescription.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemSellersIdentifier is not null)
        {
            writer.WriteStartElement("item-sellers-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-155");
            ItemSellersIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemBuyersIdentifier is not null)
        {
            writer.WriteStartElement("item-buyers-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-156");
            ItemBuyersIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemStandardIdentifier is not null)
        {
            writer.WriteStartElement("item-standard-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-157");
            ItemStandardIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemClassificationIdentifiers.Length > 0)
        {
            writer.WriteStartElement("item-classification-identifiers", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-158");

            foreach (Identifier i in ItemClassificationIdentifiers)
            {
                writer.WriteStartElement("item-classification-identifier", IRConfig.NS);
                writer.WriteAttributeString("id", "bt-158");
                i.Serialize(writer);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        if (ItemCountryOfOrigin is not null)
        {
            writer.WriteStartElement("item-country-of-origin", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-159");
            ItemCountryOfOrigin.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ItemAttributes.Length > 0)
        {
            writer.WriteStartElement("item-attributes", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-32");

            foreach (ItemAttribute ia in ItemAttributes)
            {
                ia.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

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
}
