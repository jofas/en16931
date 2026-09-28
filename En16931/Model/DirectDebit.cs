using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct DirectDebit : IIRDeserializable<DirectDebit>, IIRSerializable, ICanBeEmpty
{
    // BT-89
    public required Identifier? MandateReferenceIdentifier { get; init; }

    // BT-90
    public required Identifier? BankAssignedCreditorIdentifier { get; init; }

    // BT-91
    public required Identifier? DebitedAccountIdentifier { get; init; }

    public bool IsEmpty
    {
        get => MandateReferenceIdentifier is null
            && BankAssignedCreditorIdentifier is null
            && DebitedAccountIdentifier is null;
    }

    public void Serialize(XmlWriter writer)
    {
        if (IsEmpty) return;

        writer.WriteStartElement("direct-debit", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-19");

        if (MandateReferenceIdentifier is not null)
        {
            writer.WriteStartElement("mandate-reference-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-89");
            MandateReferenceIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (BankAssignedCreditorIdentifier is not null)
        {
            writer.WriteStartElement("bank-assigned-creditor-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-90");
            BankAssignedCreditorIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DebitedAccountIdentifier is not null)
        {
            writer.WriteStartElement("debited-account-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-91");
            DebitedAccountIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static DirectDebit Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("direct-debit", IRConfig.NS);
        reader.MoveToContent();

        Identifier? mandateReferenceIdentifier = null;

        if (reader.IsStartElement("mandate-reference-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            mandateReferenceIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? bankAssignedCreditorIdentifier = null;

        if (reader.IsStartElement("bank-assigned-creditor-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            bankAssignedCreditorIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? debitedAccountIdentifier = null;

        if (reader.IsStartElement("debited-account-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            debitedAccountIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

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
