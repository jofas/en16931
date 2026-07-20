using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using En16931.Model.Primitives;
using En16931.Utils;

namespace En16931;

public enum Schema
{
    UblInvoice,
    UblCreditNote,
    CiiCrossIndustryInvoice,
}

static class Xsd
{
    public static XmlSchemaSet SchemaSet;

    static Xsd()
    {
        SchemaSet = new XmlSchemaSet();
        SchemaSet.XmlResolver = new XmlUrlResolver();

        SchemaSet.Add(null, "Resources/Ubl/maindoc/UBL-Invoice-2.1.xsd");
        SchemaSet.Add(null, "Resources/Ubl/maindoc/UBL-CreditNote-2.1.xsd");
        SchemaSet.Add(null, "Resources/Cii/CrossIndustryInvoice_100pD16B.xsd");

        // Schema is DTD annotated, which is why we have to add it like this,
        // instead of adding the file directly with `SchemaSet.Add`
        FileStream w3XmlSigSchemaFile = File.OpenRead("Resources/W3/xmldsig-core-schema.xsd");
        XmlSchema w3XmlSigSchema = XmlSchema.Read(w3XmlSigSchemaFile, null)!;
        SchemaSet.Add(w3XmlSigSchema);

        SchemaSet.Compile();
    }

}

public readonly ref struct Document
{
    private static XNamespace _ublInvoice = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
    private static XNamespace _ublCreditNote = "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2";
    private static XNamespace _ublCbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";

    private static XNamespace _cii = "urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100";
    private static XNamespace _ciiRam = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100";

    private readonly bool _initialized;

    public XDocument Doc { get { Assert.IsTrue(_initialized, "Document can't be uninitialized"); return field; } }
    public Schema Schema { get { Assert.IsTrue(_initialized, "Document can't be uninitialized"); return field; } }
    public Identifier Specification { get { Assert.IsTrue(_initialized, "Document can't be uninitialized"); return field; } }

    public Document(XDocument doc)
    {
        doc.Validate(Xsd.SchemaSet, null);

        XElement root = doc.Root ?? throw new System.Exception("Could not find root node.");

        Schema schema = root.Name switch
        {
            XName n when n == _ublInvoice + "Invoice" => Schema.UblInvoice,
            XName n when n == _ublCreditNote + "CreditNote" => Schema.UblCreditNote,
            XName n when n == _cii + "CrossIndustryInvoice" => Schema.CiiCrossIndustryInvoice,
            _ => throw new System.Exception($"Unknown root node: {root.Name}."),
        };

        string rawSpecification = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => root
                .Element(_ublCbc + "CustomizationID")!
                .Value,
            Schema.CiiCrossIndustryInvoice => root
                .Element(_cii + "ExchangedDocumentContext")!
                .Element(_ciiRam + "GuidelineSpecifiedDocumentContextParameter")!
                .Element(_ciiRam + "ID")!
                .Value,
            _ => throw new UnreachableException(),
        };

        Doc = doc;
        Schema = schema;
        Specification = new(rawSpecification);
        _initialized = true;
    }

    public Document(XmlReader reader) : this(XDocument.Load(reader)) { }

    public void WriteTo(XmlWriter writer)
    {
        Doc.WriteTo(writer);
    }
}

