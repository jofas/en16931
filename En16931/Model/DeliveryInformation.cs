using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public readonly record struct DeliveryInformation : IIRDeserializable<DeliveryInformation>, IIRSerializable, ICanBeEmpty
{
    // BT-70
    public required Text? DeliverToPartyName { get; init; }

    // BT-71
    public required Identifier? DeliverToLocationIdentifier { get; init; }

    // BT-72
    public required Date? ActualDeliveryDate { get; init; }

    // BG-14
    public required InvoicingPeriod? InvoicingPeriod { get; init; }

    // BG-15
    public required DeliverToAddress? DeliverToAddress { get; init; }

    public bool IsEmpty
    {
        get => DeliverToPartyName is null
            && DeliverToLocationIdentifier is null
            && ActualDeliveryDate is null
            && (InvoicingPeriod?.IsEmpty ?? true)
            && DeliverToAddress is null;
    }

    public void Serialize(XmlWriter writer)
    {
        if (IsEmpty) return;

        writer.WriteStartElement("delivery-information", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-13");

        if (DeliverToPartyName is not null)
        {
            writer.WriteStartElement("deliver-to-party-name", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-70");
            DeliverToPartyName.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (DeliverToLocationIdentifier is not null)
        {
            writer.WriteStartElement("deliver-to-location-identifier", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-71");
            DeliverToLocationIdentifier.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        if (ActualDeliveryDate is not null)
        {
            writer.WriteStartElement("actual-delivery-date", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-72");
            ActualDeliveryDate.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        InvoicingPeriod?.Serialize(writer);

        DeliverToAddress?.Serialize(writer);

        writer.WriteEndElement();
    }

    public static DeliveryInformation Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("delivery-information", IRConfig.NS);
        reader.MoveToContent();

        Text? deliverToPartyName = null;

        if (reader.IsStartElement("deliver-to-party-name", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToPartyName = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Identifier? deliverToLocationIdentifier = null;

        if (reader.IsStartElement("deliver-to-location-identifier", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            deliverToLocationIdentifier = Identifier.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        Date? actualDeliverDate = null;

        if (reader.IsStartElement("actual-delivery-date", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            actualDeliverDate = Date.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        InvoicingPeriod? invoicingPeriod = null;

        if (reader.IsStartElement("invoicing-period", IRConfig.NS))
        {
            invoicingPeriod = Model.InvoicingPeriod.Deserialize(reader);
        }

        DeliverToAddress? deliverToAddress = null;

        if (reader.IsStartElement("deliver-to-address", IRConfig.NS))
        {
            deliverToAddress = Model.DeliverToAddress.Deserialize(reader);
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        return new DeliveryInformation
        {
            DeliverToPartyName = deliverToPartyName,
            DeliverToLocationIdentifier = deliverToLocationIdentifier,
            ActualDeliveryDate = actualDeliverDate,
            InvoicingPeriod = invoicingPeriod,
            DeliverToAddress = deliverToAddress,
        };
    }
}
