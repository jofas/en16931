using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct CreditTransfer : IIRDeserializable<CreditTransfer>, IIRSerializable
{
    // BT-84
    public required Identifier PaymentAccountIdentifier { get; init; }

    // BT-85
    public required Text? PaymentAccountName { get; init; }

    // BT-86
    public required Identifier? PaymentServiceProviderIdentifier { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("credit-transfer", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-17");

        writer.WriteStartElement("payment-account-identifier", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-84");
        PaymentAccountIdentifier.Serialize(writer);
        writer.WriteEndElement();

        if (PaymentAccountName is not null)
        {
            writer.WriteStartElement("payment-account-name", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-85");
            PaymentAccountName.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (PaymentServiceProviderIdentifier is not null)
        {
            writer.WriteStartElement("payment-service-provider-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-86");
            PaymentServiceProviderIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static CreditTransfer Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("credit-transfer", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("payment-account-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier paymentAccountIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? paymentAccountName = null;

        if (reader.IsStartElement("payment-account-name", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paymentAccountName = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? paymentServiceProviderIdentifier = null;

        if (reader.IsStartElement("payment-service-provider-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paymentServiceProviderIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new CreditTransfer
        {
            PaymentAccountIdentifier = paymentAccountIdentifier,
            PaymentAccountName = paymentAccountName,
            PaymentServiceProviderIdentifier = paymentServiceProviderIdentifier,
        };
    }
}
