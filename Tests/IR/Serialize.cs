using System.IO;
using System.Xml;
using En16931.Model;
using En16931.Model.Primitives;
using Xunit;

namespace Tests.IR;

public class Serialize
{

    [Fact]
    public void SerializeBinaryObject()
    {
        Blob content = new("hello world");

        string expected = $"""
            <binary-object xmlns="urn:todo">
              <content>{content.Base64Content}</content>
              <mime-code>text/utf-8</mime-code>
              <filename>hello.txt</filename>
            </binary-object>
            """;

        BinaryObject value = new BinaryObject
        {
            Content = content.RawContent,
            MimeCode = "text/utf-8",
            Filename = "hello.txt",
        };

        using StringWriter writer = new();
        using XmlTextWriter xmlWriter = new(writer)
        {
            Formatting = Formatting.Indented,
        };

        xmlWriter.WriteStartElement("binary-object", "urn:todo");
        value.Serialize(xmlWriter);
        xmlWriter.WriteEndElement();

        string actual = writer.ToString();

        Assert.Equal(expected, actual);

        using StringReader reader = new(actual);
        using XmlTextReader xmlReader = new(reader);

        xmlReader.ReadStartElement("binary-object");
        xmlReader.MoveToContent();

        var roundTrip = BinaryObject.Deserialize(xmlReader);

        Assert.Equal(value, roundTrip);
    }
}
