using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct Buyer : IIRDeserializable<Buyer>, IIRSerializable
{
    // BT-44
    public required Text BuyerName { get; init; }

    // BT-45
    public required Text? BuyerTradingName { get; init; }

    // BT-46
    public required Identifier? BuyerIdentifier { get; init; }

    // BT-47
    public required Identifier? BuyerLegalRegistrationIdentifier { get; init; }

    // BT-48
    public required Identifier? BuyerVatIdentifier { get; init; }

    // BT-49
    public required Identifier? BuyerElectronicAddress { get; init; }

    // BG-8
    public required BuyerPostalAddress BuyerPostalAddress { get; init; }

    // BG-9
    public required BuyerContact? BuyerContact { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("buyer", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-7");

        writer.WriteStartElement("buyer-name", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-44");
        BuyerName.Serialize(writer);
        writer.WriteEndElement();

        if (BuyerTradingName is not null)
        {
            writer.WriteStartElement("buyer-trading-name", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-45");
            BuyerTradingName.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerIdentifier is not null)
        {
            writer.WriteStartElement("buyer-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-46");
            BuyerIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerLegalRegistrationIdentifier is not null)
        {
            writer.WriteStartElement("buyer-legal-registration-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-47");
            BuyerLegalRegistrationIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerVatIdentifier is not null)
        {
            writer.WriteStartElement("buyer-vat-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-48");
            BuyerVatIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerElectronicAddress is not null)
        {
            writer.WriteStartElement("buyer-electronic-address", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-49");
            BuyerElectronicAddress.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        BuyerPostalAddress.Serialize(writer);

        BuyerContact?.Serialize(writer);

        writer.WriteEndElement();
    }

    public static Buyer Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("buyer", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("buyer-name", IRConfig.NS);
        reader.MoveToContent();

        Text buyerName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? buyerTradingName = null;

        if (reader.IsStartElement("buyer-trading-name", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerTradingName = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? buyerIdentifier = null;

        if (reader.IsStartElement("buyer-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? buyerLegalRegistrationIdentifier = null;

        if (reader.IsStartElement("buyer-legal-registration-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerLegalRegistrationIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? buyerVatIdentifier = null;

        if (reader.IsStartElement("buyer-vat-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerVatIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? buyerElectronicAddress = null;

        if (reader.IsStartElement("buyer-electronic-address", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerElectronicAddress = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        BuyerPostalAddress buyerPostalAddress = BuyerPostalAddress.Deserialize(reader);

        BuyerContact? buyerContact = null;

        if (reader.IsStartElement("buyer-contact", IRConfig.NS))
        {
            buyerContact = Model.BuyerContact.Deserialize(reader);
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new Buyer
        {
            BuyerName = buyerName,
            BuyerTradingName = buyerTradingName,
            BuyerIdentifier = buyerIdentifier,
            BuyerLegalRegistrationIdentifier = buyerLegalRegistrationIdentifier,
            BuyerVatIdentifier = buyerVatIdentifier,
            BuyerElectronicAddress = buyerElectronicAddress,
            BuyerPostalAddress = buyerPostalAddress,
            BuyerContact = buyerContact,
        };
    }
}
