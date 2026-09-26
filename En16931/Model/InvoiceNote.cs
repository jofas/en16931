using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct InvoiceNote : IIRDeserializable<InvoiceNote>, IIRSerializable
{
    // BT-21
    public required Code? InvoiceNoteSubjectCode { get; init; }

    // BT-22
    public required Text Note { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("invoice-note", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-1");

        if (InvoiceNoteSubjectCode is not null)
        {
            writer.WriteStartElement("invoice-note-subject-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-21");
            InvoiceNoteSubjectCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("invoice-note", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-22");
        Note.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static InvoiceNote Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("invoice-note", IRConfig.NS);
        reader.MoveToContent();

        Code? invoiceNoteSubjectCode = null;

        if (reader.IsStartElement("invoice-note-subject-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceNoteSubjectCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("invoice-note", IRConfig.NS);
        reader.MoveToContent();

        Text note = Text.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new InvoiceNote
        {
            InvoiceNoteSubjectCode = invoiceNoteSubjectCode,
            Note = note,
        };
    }
}
