using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct DocumentTotals : IIRDeserializable<DocumentTotals>, IIRSerializable
{
    // BT-106
    public required Amount SumOfInvoiceLineNetAmount { get; init; }

    // BT-107
    public required Amount? SumOfAllowancesOnDocumentLevel { get; init; }

    // BT-108
    public required Amount? SumOfChargesOnDocumentLevel { get; init; }

    // BT-109
    public required Amount InvoiceTotalAmountWithoutVat { get; init; }

    // BT-110
    public required Amount? InvoiceTotalVatAmount { get; init; }

    // BT-111
    public required Amount? InvoiceTotalVatAmountInAccountingCurrency { get; init; }

    // BT-112
    public required Amount InvoiceTotalAmountWithVat { get; init; }

    // BT-113
    public required Amount? PaidAmount { get; init; }

    // BT-114
    public required Amount? RoundingAmount { get; init; }

    // BT-115
    public required Amount AmountDueForPayment { get; init; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("document-totals", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-22");

        writer.WriteStartElement("sum-of-invoice-line-net-amount", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-106");
        SumOfInvoiceLineNetAmount.Serialize(writer);
        writer.WriteEndElement();

        if (SumOfAllowancesOnDocumentLevel is not null)
        {
            writer.WriteStartElement("sum-of-allowances-on-document-level", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-107");
            SumOfAllowancesOnDocumentLevel.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (SumOfChargesOnDocumentLevel is not null)
        {
            writer.WriteStartElement("sum-of-charges-on-document-level", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-108");
            SumOfChargesOnDocumentLevel.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("invoice-total-amount-without-vat", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-109");
        InvoiceTotalAmountWithoutVat.Serialize(writer);
        writer.WriteEndElement();

        if (InvoiceTotalVatAmount is not null)
        {
            writer.WriteStartElement("invoice-total-vat-amount", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-110");
            InvoiceTotalVatAmount.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (InvoiceTotalVatAmountInAccountingCurrency is not null)
        {
            writer.WriteStartElement("invoice-total-vat-amount-in-accounting-currency", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-111");
            InvoiceTotalVatAmountInAccountingCurrency.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("invoice-total-amount-with-vat", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-112");
        InvoiceTotalAmountWithVat.Serialize(writer);
        writer.WriteEndElement();

        if (PaidAmount is not null)
        {
            writer.WriteStartElement("paid-amount", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-113");
            PaidAmount.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (RoundingAmount is not null)
        {
            writer.WriteStartElement("rounding-amount", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-114");
            RoundingAmount.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("amount-due-for-payment", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-115");
        AmountDueForPayment.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static DocumentTotals Deserialize(XmlReader reader)
    {

        reader.ReadStartElement("document-totals", IRConfig.NS);
        reader.MoveToContent();

        reader.ReadStartElement("sum-of-invoice-line-net-amount", IRConfig.NS);
        reader.MoveToContent();

        Amount sumOfInvoiceLineNetAmount = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? sumOfAllowancesOnDocumentLevel = null;

        if (reader.IsStartElement("sum-of-allowances-on-document-level", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sumOfAllowancesOnDocumentLevel = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Amount? sumOfChargesOnDocumentLevel = null;

        if (reader.IsStartElement("sum-of-charges-on-document-level", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            sumOfChargesOnDocumentLevel = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("invoice-total-amount-without-vat", IRConfig.NS);
        reader.MoveToContent();

        Amount invoiceTotalAmountWithoutVat = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? invoiceTotalVatAmount = null;

        if (reader.IsStartElement("invoice-total-vat-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceTotalVatAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Amount? invoiceTotalVatAmountInAccountingCurrency = null;

        if (reader.IsStartElement("invoice-total-vat-amount-in-accounting-currency", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            invoiceTotalVatAmountInAccountingCurrency = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("invoice-total-amount-with-vat", IRConfig.NS);
        reader.MoveToContent();

        Amount invoiceTotalAmountWithVat = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        Amount? paidAmount = null;

        if (reader.IsStartElement("paid-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            paidAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Amount? roundingAmount = null;

        if (reader.IsStartElement("rounding-amount", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            roundingAmount = Amount.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("amount-due-for-payment", IRConfig.NS);
        reader.MoveToContent();

        Amount amountDueForPayment = Amount.Deserialize(reader);

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DocumentTotals
        {
            SumOfInvoiceLineNetAmount = sumOfInvoiceLineNetAmount,
            SumOfAllowancesOnDocumentLevel = sumOfAllowancesOnDocumentLevel,
            SumOfChargesOnDocumentLevel = sumOfChargesOnDocumentLevel,
            InvoiceTotalAmountWithoutVat = invoiceTotalAmountWithoutVat,
            InvoiceTotalVatAmount = invoiceTotalVatAmount,
            InvoiceTotalVatAmountInAccountingCurrency = invoiceTotalVatAmountInAccountingCurrency,
            InvoiceTotalAmountWithVat = invoiceTotalAmountWithVat,
            PaidAmount = paidAmount,
            RoundingAmount = roundingAmount,
            AmountDueForPayment = amountDueForPayment,
        };
    }
}
