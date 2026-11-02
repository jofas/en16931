using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;

namespace En16931.Model.Extension.XRechnungExtension;

public readonly record struct ThirdPartyPayment
{
    // BT-DEX-001
    public required Text ThirdPartyPaymentType { get; init; }

    // BT-DEX-002
    public required Amount ThirdPartyPaymentAmount { get; init; }

    // BT-DEX-003
    public required Text ThirdPartyPaymentDescription { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("third-party-payment", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-dex-09");

        writer.WriteStartElement("third-party-payment-type", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-dex-001");
        ThirdPartyPaymentType.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("third-party-payment-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-dex-002");
        ThirdPartyPaymentAmount.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("third-party-payment-description", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-dex-003");
        ThirdPartyPaymentDescription.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static ThirdPartyPayment Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("third-party-payment", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("third-party-payment-type", IRConfig.NS);
        reader.MoveToContent();

        Text thirdPartyPaymentType = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("third-party-payment-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount thirdPartyPaymentAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("third-party-payment-description", IRConfig.NS);
        reader.MoveToContent();

        Text thirdPartyPaymentDescription = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new ThirdPartyPayment
        {
            ThirdPartyPaymentType = thirdPartyPaymentType,
            ThirdPartyPaymentAmount = thirdPartyPaymentAmount,
            ThirdPartyPaymentDescription = thirdPartyPaymentDescription,
        };
    }
}
