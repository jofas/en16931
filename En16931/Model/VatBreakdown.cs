using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct VatBreakdown : IIRDeserializable<VatBreakdown>, IIRSerializable
{
    // BT-116
    public required Amount VatCategoryTaxableAmount { get; init; }

    // BT-117
    public required Amount VatCategoryTaxAmount { get; init; }

    // BT-118
    // UNTDID 5305
    public required Code VatCategoryCode { get; init; }

    // BT-119
    public required Percentage? VatCategoryRate { get; init; }

    // BT-120
    public required Text? VatExemptionReasonText { get; init; }

    // BT-121
    // VATEX Vat exemption reason code list
    public required Code? VatExemptionReasonCode { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("vat-breakdown", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-23");

        writer.WriteStartElement("vat-category-taxable-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-116");
        VatCategoryTaxableAmount.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("vat-category-tax-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-117");
        VatCategoryTaxAmount.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("vat-category-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-118");
        VatCategoryCode.Serialize(writer);
        writer.WriteEndElement();

        if (VatCategoryRate is not null)
        {
            writer.WriteStartElement("vat-category-rate", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-119");
            VatCategoryRate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (VatExemptionReasonText is not null)
        {
            writer.WriteStartElement("vat-exemption-reason-text", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-120");
            VatExemptionReasonText.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (VatExemptionReasonCode is not null)
        {
            writer.WriteStartElement("vat-exemption-reason-code", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-121");
            VatExemptionReasonCode.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    public static VatBreakdown Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("vat-breakdown", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("vat-category-taxable-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount vatCategoryTaxableAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("vat-category-tax-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount vatCategoryTaxAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadStartElement("vat-category-code", IRConfig.NS);
        reader.MoveToContent();

        Code vatCategoryCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Percentage? vatCategoryRate = null;

        if (reader.IsStartElement("vat-category-rate", IRConfig.NS))
        {
            reader.ReadStartElement("vat-category-rate", IRConfig.NS);
            reader.MoveToContent();

            vatCategoryRate = Percentage.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? vatExemptionReasonText = null;

        if (reader.IsStartElement("vat-exemption-reason-text", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            vatExemptionReasonText = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Code? vatExemptionReasonCode = null;

        if (reader.IsStartElement("vat-exemption-reason-code", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            vatExemptionReasonCode = Code.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new VatBreakdown
        {
            VatCategoryTaxableAmount = vatCategoryTaxableAmount,
            VatCategoryTaxAmount = vatCategoryTaxAmount,
            VatCategoryCode = vatCategoryCode,
            VatCategoryRate = vatCategoryRate,
            VatExemptionReasonText = vatExemptionReasonText,
            VatExemptionReasonCode = vatExemptionReasonCode,
        };
    }
}
