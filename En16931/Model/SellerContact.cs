using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct SellerContact : IIRDeserializable<SellerContact>, IIRSerializable
{
    // BT-41
    public required Text? SellerContactPoint { get; init; }

    // BT-42
    public required Text? SellerContactTelephoneNumber { get; init; }

    // BT-43
    public required Text? SellerContactEmailAddress { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("seller-contact", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-6");

        if (SellerContactPoint is not null)
        {
            writer.WriteStartElement("seller-contact-point", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-41");
            SellerContactPoint.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SellerContactTelephoneNumber is not null)
        {
            writer.WriteStartElement("seller-contact-telephone-number", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-42");
            SellerContactTelephoneNumber.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SellerContactEmailAddress is not null)
        {
            writer.WriteStartElement("seller-contact-email-address", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-43");
            SellerContactEmailAddress.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static SellerContact Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("seller-contact", IRConfig.NS);
        reader.MoveToContent();

        Text? sellerContactPoint = null;

        if (reader.IsStartElement("seller-contact-point", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerContactPoint = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? sellerContactTelephoneNumber = null;

        if (reader.IsStartElement("seller-contact-telephone-number", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerContactTelephoneNumber = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? sellerContactEmailAddress = null;

        if (reader.IsStartElement("seller-contact-email-address", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerContactEmailAddress = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SellerContact
        {
            SellerContactPoint = sellerContactPoint,
            SellerContactTelephoneNumber = sellerContactTelephoneNumber,
            SellerContactEmailAddress = sellerContactEmailAddress,
        };
    }
}
