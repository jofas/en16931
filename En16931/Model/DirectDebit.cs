using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct DirectDebit : IIRDeserializable<DirectDebit>, IIRSerializable
{
    // BT-89
    public required Identifier MandateReferenceIdentifier { get; init; }

    // BT-90
    public required Identifier BankAssignedCreditorIdentifier { get; init; }

    // BT-91
    public required Identifier DebitedAccountIdentifier { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("direct-debit", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-19");

        writer.WriteStartElement("mandate-reference-identifier", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-89");
        MandateReferenceIdentifier.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("bank-assigned-creditor-identifier", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-90");
        BankAssignedCreditorIdentifier.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("debited-account-identifier", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-91");
        DebitedAccountIdentifier.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static DirectDebit Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("direct-debit", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("mandate-reference-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier mandateReferenceIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("bank-assigned-creditor-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier bankAssignedCreditorIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("debited-account-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier debitedAccountIdentifier = Identifier.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DirectDebit
        {
            MandateReferenceIdentifier = mandateReferenceIdentifier,
            BankAssignedCreditorIdentifier = bankAssignedCreditorIdentifier,
            DebitedAccountIdentifier = debitedAccountIdentifier,
        };
    }
}
