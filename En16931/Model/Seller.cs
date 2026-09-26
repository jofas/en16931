using System.Collections.Generic;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct Seller : IIRDeserializable<Seller>, IIRSerializable
{
    // BT-27
    public required Text SellerName { get; init; }

    // BT-28
    public required Text? SellerTradingName { get; init; }

    // BT-29
    public required Array<Identifier> SellerIdentifiers { get; init; }

    // BT-30
    public required Identifier? SellerLegalRegistrationIdentifier { get; init; }

    // BT-31
    public required Identifier? SellerVatIdentifier { get; init; }

    // BT-32
    public required Identifier? SellerTaxRegistrationIdentifier { get; init; }

    // BT-33
    public required Text? SellerAdditionalLegalInformation { get; init; }

    // BT-34
    public required Identifier? SellerElectronicAddress { get; init; }

    // BG-5
    public required SellerPostalAddress SellerPostalAddress { get; init; }

    // BG-6
    public required SellerContact? SellerContact { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("seller", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-4");

        writer.WriteStartElement("seller-name", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-27");
        SellerName.Serialize(writer);
        writer.WriteEndElement();

        if (SellerTradingName is not null)
        {
            writer.WriteStartElement("seller-trading-name", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-28");
            SellerTradingName.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SellerIdentifiers.Length > 0)
        {
            writer.WriteStartElement("seller-identifiers", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-29");

            foreach (Identifier i in SellerIdentifiers)
            {
                writer.WriteStartElement("seller-identifier", IRConfig.NS);
                writer.WriteAttributeString("id", "bt-29");
                i.Serialize(writer);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        if (SellerLegalRegistrationIdentifier is not null)
        {
            writer.WriteStartElement("seller-legal-registration-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-30");
            SellerLegalRegistrationIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SellerVatIdentifier is not null)
        {
            writer.WriteStartElement("seller-vat-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-31");
            SellerVatIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SellerTaxRegistrationIdentifier is not null)
        {
            writer.WriteStartElement("seller-tax-registration-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-32");
            SellerTaxRegistrationIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SellerAdditionalLegalInformation is not null)
        {
            writer.WriteStartElement("seller-additional-legal-information", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-33");
            SellerAdditionalLegalInformation.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SellerElectronicAddress is not null)
        {
            writer.WriteStartElement("seller-electronic-address", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-34");
            SellerElectronicAddress.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        SellerPostalAddress.Serialize(writer);

        SellerContact?.Serialize(writer);

        writer.WriteEndElement();
    }

    public static Seller Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("seller", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("seller-name", IRConfig.NS);
        reader.MoveToContent();

        Text sellerName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? sellerTradingName = null;

        if (reader.IsStartElement("seller-trading-name", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerTradingName = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Array<Identifier> sellerIdentifiers = Array<Identifier>.Empty;

        if (reader.IsStartElement("seller-identifiers", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<Identifier> builder = [];
            while (reader.IsStartElement("seller-identifier", IRConfig.NS))
            {
                reader.ReadStartElement();
                reader.MoveToContent();

                builder.Add(Identifier.Deserialize(reader));

                reader.ReadEndElement();
                reader.MoveToContent();
            }

            sellerIdentifiers = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? sellerLegalRegistrationIdentifier = null;

        if (reader.IsStartElement("seller-legal-registration-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerLegalRegistrationIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? sellerVatIdentifier = null;

        if (reader.IsStartElement("seller-vat-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerVatIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? sellerTaxRegistrationIdentifier = null;

        if (reader.IsStartElement("seller-tax-registration-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerTaxRegistrationIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? sellerAdditionalLegalInformation = null;

        if (reader.IsStartElement("seller-additional-legal-information", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerAdditionalLegalInformation = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? sellerElectronicAddress = null;

        if (reader.IsStartElement("seller-electronic-address", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerElectronicAddress = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        SellerPostalAddress sellerPostalAddress = SellerPostalAddress.Deserialize(reader);

        SellerContact? sellerContact = null;

        if (reader.IsStartElement("seller-contact", IRConfig.NS))
        {
            sellerContact = Model.SellerContact.Deserialize(reader);
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new Seller
        {
            SellerName = sellerName,
            SellerTradingName = sellerTradingName,
            SellerIdentifiers = sellerIdentifiers,
            SellerLegalRegistrationIdentifier = sellerLegalRegistrationIdentifier,
            SellerVatIdentifier = sellerVatIdentifier,
            SellerTaxRegistrationIdentifier = sellerTaxRegistrationIdentifier,
            SellerAdditionalLegalInformation = sellerAdditionalLegalInformation,
            SellerElectronicAddress = sellerElectronicAddress,
            SellerPostalAddress = sellerPostalAddress,
            SellerContact = sellerContact,
        };
    }
}
