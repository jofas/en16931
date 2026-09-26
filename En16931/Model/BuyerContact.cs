using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct BuyerContact : IIRDeserializable<BuyerContact>, IIRSerializable
{
    // BT-56
    public required Text? BuyerContactPoint { get; init; }

    // BT-57
    public required Text? BuyerContactTelephoneNumber { get; init; }

    // BT-58
    public required Text? BuyerContactEmailAddress { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("buyer-contact", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-9");

        if (BuyerContactPoint is not null)
        {
            writer.WriteStartElement("buyer-contact-point", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-56");
            BuyerContactPoint.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerContactTelephoneNumber is not null)
        {
            writer.WriteStartElement("buyer-contact-telephone-number", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-57");
            BuyerContactTelephoneNumber.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerContactEmailAddress is not null)
        {
            writer.WriteStartElement("buyer-contact-email-address", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-58");
            BuyerContactEmailAddress.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static BuyerContact Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("buyer-contact", IRConfig.NS);
        reader.MoveToContent();

        Text? buyerContactPoint = null;

        if (reader.IsStartElement("buyer-contact-point", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerContactPoint = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerContactTelephoneNumber = null;

        if (reader.IsStartElement("buyer-contact-telephone-number", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerContactTelephoneNumber = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerContactEmailAddress = null;

        if (reader.IsStartElement("buyer-contact-email-address", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerContactEmailAddress = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new BuyerContact
        {
            BuyerContactPoint = buyerContactPoint,
            BuyerContactTelephoneNumber = buyerContactTelephoneNumber,
            BuyerContactEmailAddress = buyerContactEmailAddress,
        };
    }
}
