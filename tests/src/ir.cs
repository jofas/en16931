using System;
using System.Collections.Immutable;
using System.IO;
using System.Xml.Schema;
using System.Xml.Serialization;
using Dev.Fassbender.En16931;
using Dev.Fassbender.En16931.Model;
using net.liberty_development.SaxonHE12s9apiExtensions;
using net.sf.saxon.s9api;
using Xunit;

namespace Tests;

public class IR
{
    [Theory]
    [InlineData("resources/schematrons/xrechnung/cius/ubl/invoice/success/1.xml")]
    public void UblInvoice1(string invoiceLocation)
    {
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

        Invoice invoice = (Invoice)invoiceSerializer.Deserialize(
            new StringReader(irDestination.getXdmNode().ToString())
        )!;

        Invoice expected = new Invoice
        {
            InvoiceNumber = new Identifier("1234567"),
            InvoiceIssueDate = new Date(new DateTime(2018, 4, 13)),
            InvoiceTypeCode = new Code("380"),
            InvoiceCurrencyCode = new Code("EUR"),
            BuyerReference = new Text("90000000-03083-72"),
            InvoiceNotes = [
                new InvoiceNote {
                    InvoiceNoteSubjectCode = new Code("AAC"),
                    Note = new Text("Invoice Note Description"),
                },
                new InvoiceNote {
                    InvoiceNoteSubjectCode = new Code("AAC"),
                    Note = new Text("Invoice Note Description 2"),
                },
            ],
            ProcessControl = new ProcessControl
            {
                BusinessProcessType = new Text("urn:fdc:peppol.eu:2017:poacc:billing:01:1.0"),
                SpecificationIdentifier = new Identifier("urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0"),
            },
            PrecedingInvoiceReferences = [
                new PrecedingInvoiceReference {
                    Reference = new DocumentReference("PIR1234567890"),
                    PrecedingInvoiceIssueDate = new Date(new DateTime(2018, 2, 4)),
                },
                new PrecedingInvoiceReference {
                    Reference = new DocumentReference("PIR0987654321"),
                    PrecedingInvoiceIssueDate = new Date(new DateTime(2018, 3, 5)),
                }
            ],
            Seller = new Seller
            {
                SellerName = new Text("[Seller name]"),
                SellerTradingName = new Text("[Seller trading name]"),
                SellerIdentifiers = new Array<Identifier>([
                    new Identifier("987654321", "0088")
                ]),
                SellerLegalRegistrationIdentifier = new Identifier("123456789", "0088"),
                SellerVatIdentifier = new Identifier("ATU123456789"),
                SellerTaxRegistrationIdentifier = new Identifier("123/456/789"),
                SellerAdditionalLegalInformation = new Text("Amtsgericht […], Geschäftsführer: […], Sitz der Gesellschaft […], Aufsichtsratvorsitzender: […]"),
                SellerElectronicAddress = new Identifier("rechnungsausgang@test.com", "EM"),
                SellerPostalAddress = new SellerPostalAddress
                {
                    SellerAddressLine1 = new Text("[Seller address line 1]"),
                    SellerAddressLine2 = new Text("[Seller address line 2]"),
                    SellerAddressLine3 = new Text("[Seller address line 3]"),
                    SellerCity = new Text("[Seller city]"),
                    SellerPostCode = new Text("12345"),
                    SellerCountrySubdivision = new Text("Bayern"),
                    SellerCountryCode = new Code("DE"),
                },
                SellerContact = new SellerContact
                {
                    SellerContactPoint = new Text("Tim Tester"),
                    SellerContactTelephoneNumber = new Text("012 3456789"),
                    SellerContactEmailAddress = new Text("tim.tester@test.com"),
                },
            },
            Buyer = new Buyer
            {
                BuyerName = new Text("[Buyer Name]"),
                BuyerPostalAddress = new BuyerPostalAddress
                {
                    BuyerCity = new Text("[Buyer city]"),
                    BuyerPostCode = new Text("98765"),
                    BuyerCountryCode = new Code("DE"),
                },
            },
            PaymentInstructions = new PaymentInstructions
            {
                PaymentMeansTypeCode = new Code("58"),
                CreditTransfers = [],
            },
            DocumentLevelAllowances = [],
            DocumentLevelCharges = [],
            DocumentTotals = new DocumentTotals
            {
                SumOfInvoiceLineNetAmount = new Amount(10781.25m),
                InvoiceTotalAmountWithoutVat = new Amount(10781.25m),
                InvoiceTotalAmountWithVat = new Amount(12829.69m),
                AmountDueForPayment = new Amount(12829.69m),
            },
            VatBreakdown = [],
            AdditionalSupportingDocuments = [],
            InvoiceLines = [],
        };

        Assert.Equal(expected.InvoiceNotes, invoice.InvoiceNotes);

        Assert.Equal(expected.ProcessControl, invoice.ProcessControl);

        Assert.Equal(expected.PrecedingInvoiceReferences, invoice.PrecedingInvoiceReferences);

        Assert.Equal(expected.Seller, invoice.Seller);

        //Console.WriteLine(irDestination.getXdmNode());

        //Assert.Equal(expected, invoice);
    }
}
