using System;
using System.IO;
using System.Text;
using System.Xml;
using En16931;
using En16931.Model.Immutable;
using En16931.Model.Immutable.Primitives;
using Xunit;

namespace Tests.IR;

public class Deserialize
{
    // For generating xml data for tests
    [Theory]
    [InlineData("Resources/XRechnung-Cius/Cii/Success/1.xml")]
    public void Cii1IR(string invoiceLocation)
    {
        Parser parser = new Parser();

        XmlDocument doc = parser.ParseFileToIR(invoiceLocation);

        using StringWriter writer = new();
        using XmlTextWriter xmlWriter = new(writer)
        {
            Formatting = Formatting.Indented,
        };

        doc.Save(xmlWriter);

        Console.WriteLine(writer.ToString());
    }

    [Fact]
    public void DeserializeInvoiceLine()
    {
        string invoiceLineXml = """
            <invoice-line id="bg-25" xmlns="urn:todo">
              <invoice-line-identifier id="bt-126">
                <content>1</content>
              </invoice-line-identifier>
              <invoice-line-note id="bt-127">Die letzte Lieferung im Rahmen des abgerechneten Abonnements erfolgt in 12/2016 Lieferung erfolgt / erfolgte direkt vom Verlag</invoice-line-note>
              <invoice-line-object-identifier id="bt-128">
                <content>ANG987654321</content>
                <scheme-identifier>ABZ</scheme-identifier>
              </invoice-line-object-identifier>
              <invoiced-quantity id="bt-129">30.00000000000</invoiced-quantity>
              <invoiced-quantity-unit-of-measure-code id="bt-130">XPP</invoiced-quantity-unit-of-measure-code>
              <invoice-line-net-amount id="bt-131">4743.75</invoice-line-net-amount>
              <referenced-purchase-order-line-reference id="bt-132">6171175.1</referenced-purchase-order-line-reference>
              <invoice-line-buyer-accounting-reference id="bt-133">Konto 1</invoice-line-buyer-accounting-reference>
              <invoice-line-period id="bg-26">
                <invoice-line-period-start-date id="bt-134">2018-04-13</invoice-line-period-start-date>
                <invoice-line-period-end-date id="bt-135">2018-04-13</invoice-line-period-end-date>
              </invoice-line-period>
              <invoice-line-allowances id="bg-27">
                <invoice-line-allowance id="bg-27">
                  <invoice-line-allowance-amount id="bt-136">20</invoice-line-allowance-amount>
                  <invoice-line-allowance-base-amount id="bt-137">200</invoice-line-allowance-base-amount>
                  <invoice-line-allowance-percentage id="bt-138">10</invoice-line-allowance-percentage>
                  <invoice-line-allowance-reason id="bt-139">Fixed long term</invoice-line-allowance-reason>
                  <invoice-line-allowance-reason-code id="bt-140">102</invoice-line-allowance-reason-code>
                </invoice-line-allowance>
                <invoice-line-allowance id="bg-27">
                  <invoice-line-allowance-amount id="bt-136">20</invoice-line-allowance-amount>
                  <invoice-line-allowance-base-amount id="bt-137">200</invoice-line-allowance-base-amount>
                  <invoice-line-allowance-percentage id="bt-138">10</invoice-line-allowance-percentage>
                  <invoice-line-allowance-reason id="bt-139">Fixed long term 2</invoice-line-allowance-reason>
                  <invoice-line-allowance-reason-code id="bt-140">102</invoice-line-allowance-reason-code>
                </invoice-line-allowance>
              </invoice-line-allowances>
              <invoice-line-charges id="bg-28">
                <invoice-line-charge id="bg-28">
                  <invoice-line-charge-amount id="bt-141">20</invoice-line-charge-amount>
                  <invoice-line-charge-base-amount id="bt-142">200</invoice-line-charge-base-amount>
                  <invoice-line-charge-percentage id="bt-143">10</invoice-line-charge-percentage>
                  <invoice-line-charge-reason id="bt-144">Testing</invoice-line-charge-reason>
                  <invoice-line-charge-reason-code id="bt-145">TAC</invoice-line-charge-reason-code>
                </invoice-line-charge>
                <invoice-line-charge id="bg-28">
                  <invoice-line-charge-amount id="bt-141">20</invoice-line-charge-amount>
                  <invoice-line-charge-base-amount id="bt-142">200</invoice-line-charge-base-amount>
                  <invoice-line-charge-percentage id="bt-143">10</invoice-line-charge-percentage>
                  <invoice-line-charge-reason id="bt-144">Testing 2</invoice-line-charge-reason>
                  <invoice-line-charge-reason-code id="bt-145">TAC</invoice-line-charge-reason-code>
                </invoice-line-charge>
              </invoice-line-charges>
              <price-details id="bg-29">
                <item-net-price id="bt-146">158.1250000000</item-net-price>
                <item-price-discount id="bt-147">10.000000000000000000</item-price-discount>
                <item-gross-price id="bt-148">168.1250000000</item-gross-price>
                <item-price-base-quantity id="bt-149">1</item-price-base-quantity>
                <item-price-base-quantity-unit-of-measure-code id="bt-150">XPP</item-price-base-quantity-unit-of-measure-code>
              </price-details>
              <line-vat-information id="bg-30">
                <invoiced-item-vat-category-code id="bt-151">S</invoiced-item-vat-category-code>
                <invoiced-item-vat-rate id="bt-152">19</invoiced-item-vat-rate>
              </line-vat-information>
              <item-information id="bg-31">
                <item-name id="bt-153">Beratung</item-name>
                <item-description id="bt-154">Anforderungsmanagement</item-description>
                <item-sellers-identifier id="bt-155">
                  <content>1034</content>
                </item-sellers-identifier>
                <item-buyers-identifier id="bt-156">
                  <content>5034</content>
                </item-buyers-identifier>
                <item-standard-identifier id="bt-157">
                  <content>123456789</content>
                  <scheme-identifier>0088</scheme-identifier>
                </item-standard-identifier>
                <item-classification-identifiers id="bt-158">
                  <item-classification-identifier id="bt-158">
                    <content>0721-880X</content>
                    <scheme-identifier>IB</scheme-identifier>
                    <scheme-version-identifier>88</scheme-version-identifier>
                  </item-classification-identifier>
                  <item-classification-identifier id="bt-158">
                    <content>0721-880XYZ</content>
                    <scheme-identifier>IB</scheme-identifier>
                    <scheme-version-identifier>88</scheme-version-identifier>
                  </item-classification-identifier>
                </item-classification-identifiers>
                <item-country-of-origin id="bt-159">DE</item-country-of-origin>
                <item-attributes id="bg-32">
                  <item-attribute id="bg-32">
                    <item-attribute-name id="bt-160">[Description]</item-attribute-name>
                    <item-attribute-value id="bt-161">[Value]</item-attribute-value>
                  </item-attribute>
                  <item-attribute id="bg-32">
                    <item-attribute-name id="bt-160">[Description 2]</item-attribute-name>
                    <item-attribute-value id="bt-161">[Value 2]</item-attribute-value>
                  </item-attribute>
                </item-attributes>
              </item-information>
            </invoice-line>
            """;

        InvoiceLine expected = new InvoiceLine
        {
            InvoiceLineIdentifier = new Identifier("1"),
            InvoiceLineNote = new Text("Die letzte Lieferung im Rahmen des abgerechneten Abonnements erfolgt in 12/2016 Lieferung erfolgt / erfolgte direkt vom Verlag"),
            InvoiceLineObjectIdentifier = new Identifier("ANG987654321", "ABZ"),
            InvoicedQuantity = new Quantity(30m),
            InvoicedQuantityUnitOfMeasureCode = new Code("XPP"),
            InvoiceLineNetAmount = new Amount(4743.75m),
            ReferencedPurchaseOrderLineReference = new DocumentReference("6171175.1"),
            InvoiceLineBuyerAccountingReference = new Text("Konto 1"),
            InvoiceLinePeriod = new InvoiceLinePeriod
            {
                InvoiceLinePeriodStartDate = new Date(new DateTime(2018, 04, 13)),
                InvoiceLinePeriodEndDate = new Date(new DateTime(2018, 04, 13)),
            },
            InvoiceLineAllowances = [
                new InvoiceLineAllowance {
                    InvoiceLineAllowanceAmount = new Amount(20m),
                    InvoiceLineAllowanceBaseAmount = new Amount(200m),
                    InvoiceLineAllowancePercentage = new Percentage(10m),
                    InvoiceLineAllowanceReason = new Text("Fixed long term"),
                    InvoiceLineAllowanceReasonCode = new Code("102"),
                },
                new InvoiceLineAllowance {
                    InvoiceLineAllowanceAmount = new Amount(20m),
                    InvoiceLineAllowanceBaseAmount = new Amount(200m),
                    InvoiceLineAllowancePercentage = new Percentage(10m),
                    InvoiceLineAllowanceReason = new Text("Fixed long term 2"),
                    InvoiceLineAllowanceReasonCode = new Code("102"),
                }
            ],
            InvoiceLineCharges = [
                new InvoiceLineCharge {
                    InvoiceLineChargeAmount = new Amount(20m),
                    InvoiceLineChargeBaseAmount = new Amount(200m),
                    InvoiceLineChargePercentage = new Percentage(10m),
                    InvoiceLineChargeReason = new Text("Testing"),
                    InvoiceLineChargeReasonCode = new Code("TAC"),
                },
                new InvoiceLineCharge {
                    InvoiceLineChargeAmount = new Amount(20m),
                    InvoiceLineChargeBaseAmount = new Amount(200m),
                    InvoiceLineChargePercentage = new Percentage(10m),
                    InvoiceLineChargeReason = new Text("Testing 2"),
                    InvoiceLineChargeReasonCode = new Code("TAC"),
                },
            ],
            PriceDetails = new PriceDetails
            {
                ItemNetPrice = new UnitPriceAmount(158.125m),
                ItemPriceDiscount = new UnitPriceAmount(10m),
                ItemGrossPrice = new UnitPriceAmount(168.125m),
                ItemPriceBaseQuantity = new Quantity(1m),
                ItemPriceBaseQuantityUnitOfMeasureCode = new Code("XPP"),
            },
            LineVatInformation = new LineVatInformation
            {
                InvoicedItemVatCategoryCode = new Code("S"),
                InvoicedItemVatRate = new Percentage(19m),
            },
            ItemInformation = new ItemInformation
            {
                ItemName = new Text("Beratung"),
                ItemDescription = new Text("Anforderungsmanagement"),
                ItemSellersIdentifier = new Identifier("1034"),
                ItemBuyersIdentifier = new Identifier("5034"),
                ItemStandardIdentifier = new Identifier("123456789", "0088"),
                ItemClassificationIdentifiers = [
                    new Identifier("0721-880X", "IB", "88"),
                    new Identifier("0721-880XYZ", "IB", "88"),
                ],
                ItemCountryOfOrigin = new Code("DE"),
                ItemAttributes = [
                    new ItemAttribute {
                       ItemAttributeName = new Text("[Description]"),
                       ItemAttributeValue = new Text ("[Value]"),
                    },
                    new ItemAttribute {
                        ItemAttributeName = new Text("[Description 2]"),
                        ItemAttributeValue = new Text("[Value 2]"),
                    },
                ],
            },
        };

        using StringReader reader = new(invoiceLineXml);
        using XmlTextReader xmlReader = new(reader);

        InvoiceLine invoiceLine = InvoiceLine.Deserialize(xmlReader);

        Assert.Equal(expected, invoiceLine);
    }

    [Fact]
    public void DeserializeItemInformation()
    {
        string itemInformationXml = """
            <item-information id="bg-31" xmlns="urn:todo">
              <item-name id="bt-153">Beratung</item-name>
              <item-description id="bt-154">Anforderungsmanagement</item-description>
              <item-sellers-identifier id="bt-155">
                <content>1034</content>
              </item-sellers-identifier>
              <item-buyers-identifier id="bt-156">
                <content>5034</content>
              </item-buyers-identifier>
              <item-standard-identifier id="bt-157">
                <content>123456789</content>
                <scheme-identifier>0088</scheme-identifier>
              </item-standard-identifier>
              <item-classification-identifiers id="bt-158">
                <item-classification-identifier id="bt-158">
                  <content>0721-880X</content>
                  <scheme-identifier>IB</scheme-identifier>
                  <scheme-version-identifier>88</scheme-version-identifier>
                </item-classification-identifier>
                <item-classification-identifier id="bt-158">
                  <content>0721-880XYZ</content>
                  <scheme-identifier>IB</scheme-identifier>
                  <scheme-version-identifier>88</scheme-version-identifier>
                </item-classification-identifier>
              </item-classification-identifiers>
              <item-country-of-origin id="bt-159">DE</item-country-of-origin>
              <item-attributes id="bg-32">
                <item-attribute id="bg-32">
                  <item-attribute-name id="bt-160">[Description]</item-attribute-name>
                  <item-attribute-value id="bt-161">[Value]</item-attribute-value>
                </item-attribute>
                <item-attribute id="bg-32">
                  <item-attribute-name id="bt-160">[Description 2]</item-attribute-name>
                  <item-attribute-value id="bt-161">[Value 2]</item-attribute-value>
                </item-attribute>
              </item-attributes>
            </item-information>
            """;

        ItemInformation expected = new ItemInformation
        {
            ItemName = new Text("Beratung"),
            ItemDescription = new Text("Anforderungsmanagement"),
            ItemSellersIdentifier = new Identifier("1034"),
            ItemBuyersIdentifier = new Identifier("5034"),
            ItemStandardIdentifier = new Identifier("123456789", "0088"),
            ItemClassificationIdentifiers = [
                new Identifier("0721-880X", "IB", "88"),
                new Identifier("0721-880XYZ", "IB", "88"),
            ],
            ItemCountryOfOrigin = new Code("DE"),
            ItemAttributes = [
                new ItemAttribute {
                    ItemAttributeName = new Text("[Description]"),
                    ItemAttributeValue = new Text ("[Value]"),
                 },
                 new ItemAttribute {
                     ItemAttributeName = new Text("[Description 2]"),
                     ItemAttributeValue = new Text("[Value 2]"),
                  },
            ],
        };

        using StringReader reader = new(itemInformationXml);
        using XmlTextReader xmlReader = new(reader);

        ItemInformation itemInformation = ItemInformation.Deserialize(xmlReader);

        Assert.Equal(expected, itemInformation);
    }

    [Fact]
    public void DeserializeBinaryObject()
    {
        byte[] rawContent = Encoding.UTF8.GetBytes("hello world");
        string base64Content = Convert.ToBase64String(rawContent);

        string binaryObjectXml = $"""
            <binary-object xmlns="urn:todo">
                <content>{base64Content}</content>
                <mime-code>text/utf-8</mime-code>
                <filename>hello.txt</filename>
            </binary-object>
            """;

        using StringReader reader = new(binaryObjectXml);
        using XmlTextReader xmlReader = new(reader);

        xmlReader.ReadStartElement("binary-object");
        xmlReader.MoveToContent();

        BinaryObject obj = BinaryObject.Deserialize(xmlReader);

        string content = Encoding.UTF8.GetString([.. obj.Content]);

        Assert.Equal("hello world", content);
    }
}
