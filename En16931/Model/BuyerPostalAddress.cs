using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct BuyerPostalAddress : IIRDeserializable<BuyerPostalAddress>, IIRSerializable
{
    // BT-50
    public required Text? BuyerAddressLine1 { get; init; }

    // BT-51
    public required Text? BuyerAddressLine2 { get; init; }

    // BT-163
    public required Text? BuyerAddressLine3 { get; init; }

    // BT-52
    public required Text? BuyerCity { get; init; }

    // BT-53
    public required Text? BuyerPostCode { get; init; }

    // BT-54
    public required Text? BuyerCountrySubdivision { get; init; }

    // BT-55
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code BuyerCountryCode { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("buyer-postal-address", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-8");

        if (BuyerAddressLine1 is not null)
        {
            writer.WriteStartElement("buyer-address-line-1", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-50");
            BuyerAddressLine1.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerAddressLine2 is not null)
        {
            writer.WriteStartElement("buyer-address-line-2", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-51");
            BuyerAddressLine2.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerAddressLine3 is not null)
        {
            writer.WriteStartElement("buyer-address-line-3", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-163");
            BuyerAddressLine3.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerCity is not null)
        {
            writer.WriteStartElement("buyer-city", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-52");
            BuyerCity.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerPostCode is not null)
        {
            writer.WriteStartElement("buyer-post-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-53");
            BuyerPostCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BuyerCountrySubdivision is not null)
        {
            writer.WriteStartElement("buyer-country-subdivision", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-54");
            BuyerCountrySubdivision.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("buyer-country-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-55");
        BuyerCountryCode.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static BuyerPostalAddress Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("buyer-postal-address", IRConfig.NS);
        reader.MoveToContent();

        Text? buyerAddressLine1 = null;

        if (reader.IsStartElement("buyer-address-line-1", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerAddressLine1 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerAddressLine2 = null;

        if (reader.IsStartElement("buyer-address-line-2", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerAddressLine2 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerAddressLine3 = null;

        if (reader.IsStartElement("buyer-address-line-3", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerAddressLine3 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerCity = null;

        if (reader.IsStartElement("buyer-city", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerCity = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerPostCode = null;

        if (reader.IsStartElement("buyer-post-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerPostCode = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? buyerCountrySubdivision = null;

        if (reader.IsStartElement("buyer-country-subdivision", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            buyerCountrySubdivision = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("buyer-country-code", IRConfig.NS);
        reader.MoveToContent();

        Code buyerCountryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new BuyerPostalAddress
        {
            BuyerAddressLine1 = buyerAddressLine1,
            BuyerAddressLine2 = buyerAddressLine2,
            BuyerAddressLine3 = buyerAddressLine3,
            BuyerCity = buyerCity,
            BuyerPostCode = buyerPostCode,
            BuyerCountrySubdivision = buyerCountrySubdivision,
            BuyerCountryCode = buyerCountryCode,
        };
    }
}
