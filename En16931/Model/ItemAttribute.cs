using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct ItemAttribute : IIRDeserializable<ItemAttribute>, IIRSerializable
{
    // BT-160
    public required Text ItemAttributeName { get; init; }

    // BT-161
    public required Text ItemAttributeValue { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("item-attribute", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-32");

        writer.WriteStartElement("item-attribute-name", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-160");
        ItemAttributeName.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("item-attribute-value", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-161");
        ItemAttributeValue.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static ItemAttribute Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("item-attribute", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("item-attribute-name", IRConfig.NS);
        reader.MoveToContent();

        Text itemAttributeName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("item-attribute-value", IRConfig.NS);
        reader.MoveToContent();

        Text itemAttributeValue = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new ItemAttribute
        {
            ItemAttributeName = itemAttributeName,
            ItemAttributeValue = itemAttributeValue,
        };
    }
}
