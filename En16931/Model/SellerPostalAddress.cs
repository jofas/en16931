using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct SellerPostalAddress : IIRDeserializable<SellerPostalAddress>, IIRSerializable
{
    // BT-35
    public required Text? SellerAddressLine1 { get; init; }

    // BT-36
    public required Text? SellerAddressLine2 { get; init; }

    // BT-162
    public required Text? SellerAddressLine3 { get; init; }

    // BT-37
    public required Text SellerCity { get; init; }

    // BT-38
    public required Text SellerPostCode { get; init; }

    // BT-39
    public required Text? SellerCountrySubdivision { get; init; }

    // BT-40
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code SellerCountryCode { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("seller-postal-address", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-5");

        if (SellerAddressLine1 is not null)
        {
            writer.WriteStartElement("seller-address-line-1", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-35");
            SellerAddressLine1.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SellerAddressLine2 is not null)
        {
            writer.WriteStartElement("seller-address-line-2", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-36");
            SellerAddressLine2.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SellerAddressLine3 is not null)
        {
            writer.WriteStartElement("seller-address-line-3", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-162");
            SellerAddressLine3.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("seller-city", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-37");
        SellerCity.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("seller-post-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-38");
        SellerPostCode.Serialize(writer);
        writer.WriteEndElement();

        if (SellerCountrySubdivision is not null)
        {
            writer.WriteStartElement("seller-country-subdivision", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-39");
            SellerCountrySubdivision.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("seller-country-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-40");
        SellerCountryCode.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static SellerPostalAddress Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("seller-postal-address", IRConfig.NS);
        reader.MoveToContent();

        Text? sellerAddressLine1 = null;

        if (reader.IsStartElement("seller-address-line-1", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerAddressLine1 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? sellerAddressLine2 = null;

        if (reader.IsStartElement("seller-address-line-2", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerAddressLine2 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? sellerAddressLine3 = null;

        if (reader.IsStartElement("seller-address-line-3", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerAddressLine3 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("seller-city", IRConfig.NS);
        reader.MoveToContent();

        Text sellerCity = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("seller-post-code", IRConfig.NS);
        reader.MoveToContent();

        Text sellerPostCode = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? sellerCountrySubdivision = null;

        if (reader.IsStartElement("seller-country-subdivision", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sellerCountrySubdivision = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("seller-country-code", IRConfig.NS);
        reader.MoveToContent();

        Code sellerCountryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SellerPostalAddress
        {
            SellerAddressLine1 = sellerAddressLine1,
            SellerAddressLine2 = sellerAddressLine2,
            SellerAddressLine3 = sellerAddressLine3,
            SellerCity = sellerCity,
            SellerPostCode = sellerPostCode,
            SellerCountrySubdivision = sellerCountrySubdivision,
            SellerCountryCode = sellerCountryCode,
        };
    }
}
