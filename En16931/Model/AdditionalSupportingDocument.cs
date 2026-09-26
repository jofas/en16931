using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct AdditionalSupportingDocument : IIRDeserializable<AdditionalSupportingDocument>, IIRSerializable
{
    // BT-122
    public required DocumentReference SupportingDocumentReference { get; init; }

    // BT-123
    public required Text? SupportingDocumentDescription { get; init; }

    // BT-124
    public required Text? ExternalDocumentLocation { get; init; }

    // BT-125
    public required BinaryObject? AttachedDocument { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("additional-supporting-document", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-24");

        writer.WriteStartElement("supporting-document-reference", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-122");
        SupportingDocumentReference.Serialize(writer);
        writer.WriteEndElement();

        if (SupportingDocumentDescription is not null)
        {
            writer.WriteStartElement("supporting-document-description", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-123");
            SupportingDocumentDescription.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ExternalDocumentLocation is not null)
        {
            writer.WriteStartElement("external-document-location", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-124");
            ExternalDocumentLocation.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (AttachedDocument is not null)
        {
            writer.WriteStartElement("attached-document", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-125");
            AttachedDocument.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static AdditionalSupportingDocument Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("additional-supporting-document", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("supporting-document-reference", IRConfig.NS);
        reader.MoveToContent();

        DocumentReference supportingDocumentReference = DocumentReference.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? supportingDocumentDescription = null;

        if (reader.IsStartElement("supporting-document-description", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            supportingDocumentDescription = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? externalDocumentLocation = null;

        if (reader.IsStartElement("external-document-location", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            externalDocumentLocation = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        BinaryObject? attachedDocument = null;

        if (reader.IsStartElement("attached-document", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            attachedDocument = BinaryObject.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new AdditionalSupportingDocument
        {
            SupportingDocumentReference = supportingDocumentReference,
            SupportingDocumentDescription = supportingDocumentDescription,
            ExternalDocumentLocation = externalDocumentLocation,
            AttachedDocument = attachedDocument,
        };
    }
}
