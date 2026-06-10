using System;
using System.IO;
using System.Text;
using System.Xml;
using En16931.Model.Immutable.Primitives;
using Xunit;

namespace Tests.IR;

public class Deserialize
{
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
