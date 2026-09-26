using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct PrecedingInvoiceReference : IIRDeserializable<PrecedingInvoiceReference>, IIRSerializable
{
    // BT-25
    public required DocumentReference Reference { get; init; }

    // BT-26
    public required Date? PrecedingInvoiceIssueDate { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("preceding-invoice-reference", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-3");

        writer.WriteStartElement("preceding-invoice-reference", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-25");
        Reference.Serialize(writer);
        writer.WriteEndElement();

        if (PrecedingInvoiceIssueDate is not null)
        {
            writer.WriteStartElement("preceding-invoice-issue-date", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-26");
            PrecedingInvoiceIssueDate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static PrecedingInvoiceReference Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("preceding-invoice-reference", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("preceding-invoice-reference", IRConfig.NS);
        reader.MoveToContent();

        DocumentReference reference = DocumentReference.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Date? precedingInvoiceIssueDate = null;

        if (reader.IsStartElement("preceding-invoice-issue-date", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            precedingInvoiceIssueDate = Date.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new PrecedingInvoiceReference
        {
            Reference = reference,
            PrecedingInvoiceIssueDate = precedingInvoiceIssueDate,
        };
    }
}

