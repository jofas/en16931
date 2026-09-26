using System.Collections.Generic;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct PaymentInstructions : IIRDeserializable<PaymentInstructions>, IIRSerializable
{
    // BT-81
    // UNTDID-4461
    public required Code PaymentMeansTypeCode { get; init; }

    // BT-82
    public required Text? PaymentMeansText { get; init; }

    // BT-83
    public required Text? RemittanceInformation { get; init; }

    // BG-17
    public required Array<CreditTransfer> CreditTransfers { get; init; }

    // BG-18
    public required PaymentCardInformation? PaymentCardInformation { get; init; }

    // BG-19
    public required DirectDebit? DirectDebit { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("payment-instructions", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-16");

        writer.WriteStartElement("payment-means-type-code", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-81");
        PaymentMeansTypeCode.Serialize(writer);
        writer.WriteEndElement();

        if (PaymentMeansText is not null)
        {
            writer.WriteStartElement("payment-means-text", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-82");
            PaymentMeansText.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (RemittanceInformation is not null)
        {
            writer.WriteStartElement("remittance-information", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-83");
            RemittanceInformation.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (CreditTransfers.Length > 0)
        {
            writer.WriteStartElement("credit-transfers", IRConfig.NS);
            writer.WriteAttributeString("id", "bg-17");

            foreach (CreditTransfer ct in CreditTransfers)
            {
                ct.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        PaymentCardInformation?.Serialize(writer);

        DirectDebit?.Serialize(writer);

        writer.WriteEndElement();
    }

    public static PaymentInstructions Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("payment-instructions", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("payment-means-type-code", IRConfig.NS);
        reader.MoveToContent();

        Code paymentMeansTypeCode = Code.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Text? paymentMeansText = null;

        if (reader.IsStartElement("payment-means-text", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paymentMeansText = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Text? remittanceInformation = null;

        if (reader.IsStartElement("remittance-information", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            remittanceInformation = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Array<CreditTransfer> creditTransfers = Array<CreditTransfer>.Empty;

        if (reader.IsStartElement("credit-transfers", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            List<CreditTransfer> builder = [];
            while (reader.IsStartElement("credit-transfer", IRConfig.NS))
            {
                builder.Add(CreditTransfer.Deserialize(reader));
            }

            creditTransfers = new(builder);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        PaymentCardInformation? paymentCardInformation = null;

        if (reader.IsStartElement("payment-card-information", IRConfig.NS))
        {
            paymentCardInformation = Model.PaymentCardInformation.Deserialize(reader);
        }

        DirectDebit? directDebit = null;

        if (reader.IsStartElement("direct-debit", IRConfig.NS))
        {
            directDebit = Model.DirectDebit.Deserialize(reader);
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new PaymentInstructions
        {
            PaymentMeansTypeCode = paymentMeansTypeCode,
            PaymentMeansText = paymentMeansText,
            RemittanceInformation = remittanceInformation,
            CreditTransfers = creditTransfers,
            PaymentCardInformation = paymentCardInformation,
            DirectDebit = directDebit,
        };
    }
}
