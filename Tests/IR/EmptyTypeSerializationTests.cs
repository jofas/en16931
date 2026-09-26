using System.IO;
using System.Xml;
using En16931.Model;
using Xunit;

namespace Tests.IR;

public class EmptyTypeSerializationTests
{
    [Fact]
    public void SerializeEmptySellerContact()
    {
        SellerContact sellerContact = new SellerContact
        {
            SellerContactPoint = null,
            SellerContactTelephoneNumber = null,
            SellerContactEmailAddress = null,
        };

        using StringWriter writer = new();
        using XmlTextWriter xmlWriter = new(writer);

        sellerContact.Serialize(xmlWriter);

        string serializedXml = writer.ToString();

        Assert.Equal("", serializedXml);
    }

    [Fact]
    public void SerializeEmptyBuyerContact()
    {
        BuyerContact buyerContact = new BuyerContact
        {
            BuyerContactPoint = null,
            BuyerContactTelephoneNumber = null,
            BuyerContactEmailAddress = null,
        };

        using StringWriter writer = new();
        using XmlTextWriter xmlWriter = new(writer);

        buyerContact.Serialize(xmlWriter);

        string serializedXml = writer.ToString();

        Assert.Equal("", serializedXml);
    }

    [Fact]
    public void SerializeEmptyInvoicingPeriod()
    {
        InvoicingPeriod invoicingPeriod = new InvoicingPeriod
        {
            InvoicingPeriodStartDate = null,
            InvoicingPeriodEndDate = null,
        };

        using StringWriter writer = new();
        using XmlTextWriter xmlWriter = new(writer);

        invoicingPeriod.Serialize(xmlWriter);

        string serializedXml = writer.ToString();

        Assert.Equal("", serializedXml);
    }

    [Fact]
    public void SerializeEmptyDeliveryInformation()
    {

        DeliveryInformation deliverInformation = new DeliveryInformation
        {
            DeliverToPartyName = null,
            DeliverToLocationIdentifier = null,
            ActualDeliveryDate = null,
            InvoicingPeriod = null,
            DeliverToAddress = null,
        };

        using StringWriter writer = new();
        using XmlTextWriter xmlWriter = new(writer);

        deliverInformation.Serialize(xmlWriter);

        string serializedXml = writer.ToString();

        Assert.Equal("", serializedXml);
    }

    [Fact]
    public void SerializeEmptyDeliveryInformationWithEmptyInvoicingPeriod()
    {

        DeliveryInformation deliverInformation = new DeliveryInformation
        {
            DeliverToPartyName = null,
            DeliverToLocationIdentifier = null,
            ActualDeliveryDate = null,
            InvoicingPeriod = new InvoicingPeriod
            {
                InvoicingPeriodStartDate = null,
                InvoicingPeriodEndDate = null,
            },
            DeliverToAddress = null,
        };

        using StringWriter writer = new();
        using XmlTextWriter xmlWriter = new(writer);

        deliverInformation.Serialize(xmlWriter);

        string serializedXml = writer.ToString();

        Assert.Equal("", serializedXml);
    }

    [Fact]
    public void SerializeEmptyInvoiceLinePeriod()
    {
        InvoiceLinePeriod invoiceLinePeriod = new InvoiceLinePeriod
        {
            InvoiceLinePeriodStartDate = null,
            InvoiceLinePeriodEndDate = null,
        };

        using StringWriter writer = new();
        using XmlTextWriter xmlWriter = new(writer);

        invoiceLinePeriod.Serialize(xmlWriter);

        string serializedXml = writer.ToString();

        Assert.Equal("", serializedXml);
    }
}
