using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct DocumentLevelAllowance : IIRDeserializable<DocumentLevelAllowance>, IIRSerializable
{
    // BT-92
    public required Amount DocumentLevelAllowanceAmount { get; init; }

    // BT-93
    public required Amount? DocumentLevelAllowanceBaseAmount { get; init; }

    // BT-94
    public required Percentage? DocumentLevelAllowancePercentage { get; init; }

    // BT-95
    public required Code DocumentLevelAllowanceVatCategoryCode { get; init; }

    // BT-96
    public required Percentage? DocumentLevelAllowanceVatRate { get; init; }

    // BT-97
    public required Text? DocumentLevelAllowanceReason { get; init; }

    // BT-98
    public required Code? DocumentLevelAllowanceReasonCode { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("document-level-allowance", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-20");

        writer.WriteStartElement("document-level-allowance-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-92");
        DocumentLevelAllowanceAmount.Serialize(writer);
        writer.WriteEndElement();

        if (DocumentLevelAllowanceBaseAmount is not null)
        {
            writer.WriteStartElement("document-level-allowance-base-amount", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-93");
            DocumentLevelAllowanceBaseAmount.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DocumentLevelAllowancePercentage is not null)
        {
            writer.WriteStartElement("document-level-allowance-percentage", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-94");
            DocumentLevelAllowancePercentage.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("document-level-allowance-vat-category-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-95");
        DocumentLevelAllowanceVatCategoryCode.Serialize(writer);
        writer.WriteEndElement();

        if (DocumentLevelAllowanceVatRate is not null)
        {
            writer.WriteStartElement("document-level-allowance-vat-rate", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-96");
            DocumentLevelAllowanceVatRate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DocumentLevelAllowanceReason is not null)
        {
            writer.WriteStartElement("document-level-allowance-reason", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-97");
            DocumentLevelAllowanceReason.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DocumentLevelAllowanceReasonCode is not null)
        {
            writer.WriteStartElement("document-level-allowance-reason-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-98");
            DocumentLevelAllowanceReasonCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static DocumentLevelAllowance Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("document-level-allowance", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("document-level-allowance-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount documentLevelAllowanceAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? documentLevelAllowanceBaseAmount = null;

        if (reader.IsStartElement("document-level-allowance-base-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelAllowanceBaseAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Percentage? documentLevelAllowancePercentage = null;

        if (reader.IsStartElement("document-level-allowance-percentage", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelAllowancePercentage = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("document-level-allowance-vat-category-code", IRConfig.NS);
        reader.MoveToContent();

        Code documentLevelAllowanceVatCategoryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Percentage? documentLevelAllowanceVatRate = null;

        if (reader.IsStartElement("document-level-allowance-vat-rate", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelAllowanceVatRate = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? documentLevelAllowanceReason = null;

        if (reader.IsStartElement("document-level-allowance-reason", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelAllowanceReason = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? documentLevelAllowanceReasonCode = null;

        if (reader.IsStartElement("document-level-allowance-reason-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            documentLevelAllowanceReasonCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DocumentLevelAllowance
        {
            DocumentLevelAllowanceAmount = documentLevelAllowanceAmount,
            DocumentLevelAllowanceBaseAmount = documentLevelAllowanceBaseAmount,
            DocumentLevelAllowancePercentage = documentLevelAllowancePercentage,
            DocumentLevelAllowanceVatCategoryCode = documentLevelAllowanceVatCategoryCode,
            DocumentLevelAllowanceVatRate = documentLevelAllowanceVatRate,
            DocumentLevelAllowanceReason = documentLevelAllowanceReason,
            DocumentLevelAllowanceReasonCode = documentLevelAllowanceReasonCode,
        };
    }
}
