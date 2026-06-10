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
    public void DeserializeItemInformation()
    {
        string itemInformationXml = $"""
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
