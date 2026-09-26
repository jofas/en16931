using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct DeliverToAddress : IIRDeserializable<DeliverToAddress>, IIRSerializable
{
    // BT-75
    public required Text? DeliverToAddressLine1 { get; init; }

    // BT-76
    public required Text? DeliverToAddressLine2 { get; init; }

    // BT-165
    public required Text? DeliverToAddressLine3 { get; init; }

    // BT-77
    public required Text DeliverToCity { get; init; }

    // BT-78
    public required Text DeliverToPostCode { get; init; }

    // BT-79
    public required Text? DeliverToCountrySubdivision { get; init; }

    // BT-80
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code DeliverToCountryCode { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("deliver-to-address", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-15");

        if (DeliverToAddressLine1 is not null)
        {
            writer.WriteStartElement("deliver-to-address-line-1", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-75");
            DeliverToAddressLine1.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DeliverToAddressLine2 is not null)
        {
            writer.WriteStartElement("deliver-to-address-line-2", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-76");
            DeliverToAddressLine2.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DeliverToAddressLine3 is not null)
        {
            writer.WriteStartElement("deliver-to-address-line-3", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-165");
            DeliverToAddressLine3.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("deliver-to-city", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-77");
        DeliverToCity.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("deliver-to-post-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-78");
        DeliverToPostCode.Serialize(writer);
        writer.WriteEndElement();

        if (DeliverToCountrySubdivision is not null)
        {
            writer.WriteStartElement("deliver-to-country-subdivision", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-79");
            DeliverToCountrySubdivision.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("deliver-to-country-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-80");
        DeliverToCountryCode.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static DeliverToAddress Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("deliver-to-address", IRConfig.NS);
        reader.MoveToContent();

        Text? deliverToAddressLine1 = null;

        if (reader.IsStartElement("deliver-to-address-line-1", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToAddressLine1 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? deliverToAddressLine2 = null;

        if (reader.IsStartElement("deliver-to-address-line-2", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToAddressLine2 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? deliverToAddressLine3 = null;

        if (reader.IsStartElement("deliver-to-address-line-3", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToAddressLine3 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("deliver-to-city", IRConfig.NS);
        reader.MoveToContent();

        Text deliverToCity = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("deliver-to-post-code", IRConfig.NS);
        reader.MoveToContent();

        Text deliverToPostCode = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? deliverToCountrySubdivision = null;

        if (reader.IsStartElement("deliver-to-country-subdivision", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToCountrySubdivision = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("deliver-to-country-code", IRConfig.NS);
        reader.MoveToContent();

        Code deliverToCountryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DeliverToAddress
        {
            DeliverToAddressLine1 = deliverToAddressLine1,
            DeliverToAddressLine2 = deliverToAddressLine2,
            DeliverToAddressLine3 = deliverToAddressLine3,
            DeliverToCity = deliverToCity,
            DeliverToPostCode = deliverToPostCode,
            DeliverToCountrySubdivision = deliverToCountrySubdivision,
            DeliverToCountryCode = deliverToCountryCode,
        };
    }
}
