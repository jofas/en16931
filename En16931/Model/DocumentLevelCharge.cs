using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct DocumentLevelCharge : IIRDeserializable<DocumentLevelCharge>, IIRSerializable
{
    // BT-99
    public required Amount DocumentLevelChargeAmount { get; init; }

    // BT-100
    public required Amount? DocumentLevelChargeBaseAmount { get; init; }

    // BT-101
    public required Percentage? DocumentLevelChargePercentage { get; init; }

    // BT-102
    public required Code DocumentLevelChargeVatCategoryCode { get; init; }

    // BT-103
    public required Percentage? DocumentLevelChargeVatRate { get; init; }

    // BT-104
    public required Text? DocumentLevelChargeReason { get; init; }

    // BT-105
    public required Code? DocumentLevelChargeReasonCode { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("document-level-charge", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-21");

        writer.WriteStartElement("document-level-charge-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-99");
        DocumentLevelChargeAmount.Serialize(writer);
        writer.WriteEndElement();

        if (DocumentLevelChargeBaseAmount is not null)
        {
            writer.WriteStartElement("document-level-charge-base-amount", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-100");
            DocumentLevelChargeBaseAmount.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DocumentLevelChargePercentage is not null)
        {
            writer.WriteStartElement("document-level-charge-percentage", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-101");
            DocumentLevelChargePercentage.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("document-level-charge-vat-category-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-102");
        DocumentLevelChargeVatCategoryCode.Serialize(writer);
        writer.WriteEndElement();

        if (DocumentLevelChargeVatRate is not null)
        {
            writer.WriteStartElement("document-level-charge-vat-rate", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-103");
            DocumentLevelChargeVatRate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DocumentLevelChargeReason is not null)
        {
            writer.WriteStartElement("document-level-charge-reason", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-104");
            DocumentLevelChargeReason.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DocumentLevelChargeReasonCode is not null)
        {
            writer.WriteStartElement("document-level-charge-reason-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-105");
            DocumentLevelChargeReasonCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static DocumentLevelCharge Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("document-level-charge", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("document-level-charge-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount documentLevelChargeAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? documentLevelChargeBaseAmount = null;

        if (reader.IsStartElement("document-level-charge-base-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelChargeBaseAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Percentage? documentLevelChargePercentage = null;

        if (reader.IsStartElement("document-level-charge-percentage", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelChargePercentage = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("document-level-charge-vat-category-code", IRConfig.NS);
        reader.MoveToContent();

        Code documentLevelChargeVatCategoryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Percentage? documentLevelChargeVatRate = null;

        if (reader.IsStartElement("document-level-charge-vat-rate", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelChargeVatRate = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? documentLevelChargeReason = null;

        if (reader.IsStartElement("document-level-charge-reason", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelChargeReason = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? documentLevelChargeReasonCode = null;

        if (reader.IsStartElement("document-level-charge-reason-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelChargeReasonCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DocumentLevelCharge
        {
            DocumentLevelChargeAmount = documentLevelChargeAmount,
            DocumentLevelChargeBaseAmount = documentLevelChargeBaseAmount,
            DocumentLevelChargePercentage = documentLevelChargePercentage,
            DocumentLevelChargeVatCategoryCode = documentLevelChargeVatCategoryCode,
            DocumentLevelChargeVatRate = documentLevelChargeVatRate,
            DocumentLevelChargeReason = documentLevelChargeReason,
            DocumentLevelChargeReasonCode = documentLevelChargeReasonCode,
        };
    }
}
