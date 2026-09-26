using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct Payee : IIRDeserializable<Payee>, IIRSerializable
{
    // BT-59
    public required Text PayeeName { get; init; }

    // BT-60
    public required Identifier? PayeeIdentifier { get; init; }

    // BT-61
    public required Identifier? PayeeLegalRegistrationIdentifier { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("payee", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-10");

        writer.WriteStartElement("payee-name", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-59");
        PayeeName.Serialize(writer);
        writer.WriteEndElement();

        if (PayeeIdentifier is not null)
        {
            writer.WriteStartElement("payee-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-60");
            PayeeIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (PayeeLegalRegistrationIdentifier is not null)
        {
            writer.WriteStartElement("payee-legal-registration-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-61");
            PayeeLegalRegistrationIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static Payee Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("payee", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("payee-name", IRConfig.NS);
        reader.MoveToContent();

        Text payeeName = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Identifier? payeeIdentifier = null;

        if (reader.IsStartElement("payee-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            payeeIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? payeeLegalRegistrationIdentifier = null;

        if (reader.IsStartElement("payee-legal-registration-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            payeeLegalRegistrationIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new Payee
        {
            PayeeName = payeeName,
            PayeeIdentifier = payeeIdentifier,
            PayeeLegalRegistrationIdentifier = payeeLegalRegistrationIdentifier,
        };
    }
}
