using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct SellerTaxRepresentativePostalAddress : IIRDeserializable<SellerTaxRepresentativePostalAddress>, IIRSerializable
{
    // BT-64
    public required Text? TaxRepresentativeAddressLine1 { get; init; }

    // BT-65
    public required Text? TaxRepresentativeAddressLine2 { get; init; }

    // BT-164
    public required Text? TaxRepresentativeAddressLine3 { get; init; }

    // BT-66
    public required Text? TaxRepresentativeCity { get; init; }

    // BT-67
    public required Text? TaxRepresentativePostCode { get; init; }

    // BT-68
    public required Text? TaxRepresentativeCountrySubdivision { get; init; }

    // BT-69
    // ISO 3166-1 - Codes for the representation of names of countries and their subdivisions - Alpha-2
    public required Code TaxRepresentativeCountryCode { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("seller-tax-representative-postal-address", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-12");

        if (TaxRepresentativeAddressLine1 is not null)
        {
            writer.WriteStartElement("tax-representative-address-line-1", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-64");
            TaxRepresentativeAddressLine1.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (TaxRepresentativeAddressLine2 is not null)
        {
            writer.WriteStartElement("tax-representative-address-line-2", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-65");
            TaxRepresentativeAddressLine2.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (TaxRepresentativeAddressLine3 is not null)
        {
            writer.WriteStartElement("tax-representative-address-line-3", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-164");
            TaxRepresentativeAddressLine3.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (TaxRepresentativeCity is not null)
        {
            writer.WriteStartElement("tax-representative-city", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-66");
            TaxRepresentativeCity.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (TaxRepresentativePostCode is not null)
        {
            writer.WriteStartElement("tax-representative-post-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-67");
            TaxRepresentativePostCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (TaxRepresentativeCountrySubdivision is not null)
        {
            writer.WriteStartElement("tax-representative-country-subdivision", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-68");
            TaxRepresentativeCountrySubdivision.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("tax-representative-country-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-69");
        TaxRepresentativeCountryCode.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static SellerTaxRepresentativePostalAddress Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("seller-tax-representative-postal-address", IRConfig.NS);
        reader.MoveToContent();

        Text? taxRepresentativeAddressLine1 = null;

        if (reader.IsStartElement("tax-representative-address-line-1", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativeAddressLine1 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? taxRepresentativeAddressLine2 = null;

        if (reader.IsStartElement("tax-representative-address-line-2", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativeAddressLine2 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? taxRepresentativeAddressLine3 = null;

        if (reader.IsStartElement("tax-representative-address-line-3", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativeAddressLine3 = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? taxRepresentativeCity = null;

        if (reader.IsStartElement("tax-representative-city", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativeCity = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? taxRepresentativePostCode = null;

        if (reader.IsStartElement("tax-representative-post-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativePostCode = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? taxRepresentativeCountrySubdivision = null;

        if (reader.IsStartElement("tax-representative-country-subdivision", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            taxRepresentativeCountrySubdivision = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("tax-representative-country-code", IRConfig.NS);
        reader.MoveToContent();

        Code taxRepresentativeCountryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SellerTaxRepresentativePostalAddress
        {
            TaxRepresentativeAddressLine1 = taxRepresentativeAddressLine1,
            TaxRepresentativeAddressLine2 = taxRepresentativeAddressLine2,
            TaxRepresentativeAddressLine3 = taxRepresentativeAddressLine3,
            TaxRepresentativeCity = taxRepresentativeCity,
            TaxRepresentativePostCode = taxRepresentativePostCode,
            TaxRepresentativeCountrySubdivision = taxRepresentativeCountrySubdivision,
            TaxRepresentativeCountryCode = taxRepresentativeCountryCode,
        };
    }
}
