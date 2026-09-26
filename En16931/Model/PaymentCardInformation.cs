using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct PaymentCardInformation : IIRDeserializable<PaymentCardInformation>, IIRSerializable
{
    // BT-87
    public required Text PaymentCardPrimaryAccountNumber { get; init; }

    // BT-88
    public required Text? PaymentCardHolderName { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("payment-card-information", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-18");

        writer.WriteStartElement("payment-card-primary-account-number", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-87");
        PaymentCardPrimaryAccountNumber.Serialize(writer);
        writer.WriteEndElement();

        if (PaymentCardHolderName is not null)
        {
            writer.WriteStartElement("payment-card-holder-name", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-88");
            PaymentCardHolderName.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static PaymentCardInformation Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("payment-card-information", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("payment-card-primary-account-number", IRConfig.NS);
        reader.MoveToContent();

        Text paymentCardPrimaryAccountNumber = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? paymentCardHolderName = null;

        if (reader.IsStartElement("payment-card-holder-name", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paymentCardHolderName = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new PaymentCardInformation
        {
            PaymentCardPrimaryAccountNumber = paymentCardPrimaryAccountNumber,
            PaymentCardHolderName = paymentCardHolderName,
        };
    }
}
