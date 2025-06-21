using System;
using System.IO;
using System.Xml.Schema;
using System.Xml.Serialization;
using Dev.Fassbender.En16931;
using Dev.Fassbender.En16931.Model;
using Xunit;
using net.liberty_development.SaxonHE12s9apiExtensions;
using net.sf.saxon.s9api;

namespace Tests;

public class IR {
    [Theory]
    [InlineData("resources/schematrons/xrechnung/cius/ubl/invoice/success/1.xml")]
    public void UblInvoice1(string invoiceLocation) {
        Processor processor = new Processor(false);

        XsltTransformer irTransformer = processor.newXsltCompiler().Compile(
           new FileInfo("resources/ir.xslt")
        ).load();

        XdmNode node = processor.newDocumentBuilder().Build(new FileInfo(invoiceLocation));

        XdmDestination irDestination = new XdmDestination();

        irTransformer.setDestination(irDestination);
        irTransformer.setInitialContextNode(node);
        irTransformer.transform();

        XmlSerializer invoiceSerializer = new XmlSerializer(typeof(Invoice));

        // Console.WriteLine(irDestination.getXdmNode());

        Invoice invoice = (Invoice)invoiceSerializer.Deserialize(
            new StringReader(irDestination.getXdmNode().ToString())
        )!;

        Console.WriteLine(invoice);
    }
}
