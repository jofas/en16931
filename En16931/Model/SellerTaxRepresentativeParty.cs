using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct SellerTaxRepresentativeParty : IIRDeserializable<SellerTaxRepresentativeParty>, IIRSerializable
{
    // BT-62
    public required Text SellerTaxRepresentativeName { get; init; }

    // BT-63
    public required Identifier SellerTaxRepresentativeVatIdentifier { get; init; }

    // BG-12
    public required SellerTaxRepresentativePostalAddress SellerTaxRepresentativePostalAddress { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("seller-tax-representative-party", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-11");

        writer.WriteStartElement("seller-tax-representative-name", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-62");
        SellerTaxRepresentativeName.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("seller-tax-representative-vat-identifier", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-63");
        SellerTaxRepresentativeVatIdentifier.Serialize(writer);
        writer.WriteEndElement();

        SellerTaxRepresentativePostalAddress.Serialize(writer);

        writer.WriteEndElement();
    }

    public static SellerTaxRepresentativeParty Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("seller-tax-representative-party", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("seller-tax-representative-name", IRConfig.NS);
        reader.MoveToContent();

        Text sellerTaxRepresentativeName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("seller-tax-representative-vat-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier sellerTaxRepresentativeVatIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        SellerTaxRepresentativePostalAddress sellerTaxRepresentativePostalAddress = SellerTaxRepresentativePostalAddress.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        return new SellerTaxRepresentativeParty
        {
            SellerTaxRepresentativeName = sellerTaxRepresentativeName,
            SellerTaxRepresentativeVatIdentifier = sellerTaxRepresentativeVatIdentifier,
            SellerTaxRepresentativePostalAddress = sellerTaxRepresentativePostalAddress,
        };
    }
}
