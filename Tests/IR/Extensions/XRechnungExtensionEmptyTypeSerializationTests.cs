using System.IO;
using System.Xml;
using En16931.Model.Extension.XRechnungExtension;
using Xunit;

namespace Tests.IR.Extension;

public class XRechnungExtensionEmptyTypeSerializationTests
{
    [Fact]
    public void SerializeEmptyInvoicingPeriod()
    {
        SubInvoiceLinePeriod subInvoiceLinePeriod = new SubInvoiceLinePeriod
        {
            InvoiceLinePeriodStartDate = null,
            InvoiceLinePeriodEndDate = null,
        };

        using StringWriter writer = new();
        using XmlTextWriter xmlWriter = new(writer);

        subInvoiceLinePeriod.Serialize(xmlWriter);

        string serializedXml = writer.ToString();

        Assert.Equal("", serializedXml);
    }
}
