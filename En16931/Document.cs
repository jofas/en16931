using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using En16931.Model.Primitives;
using En16931.Utils;

namespace En16931;

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
        XElement root = doc.Root ?? throw new System.Exception("Could not find root node.");
        XNamespace ns = root.Name.Namespace;

        Schema schema;

        if (ns == _ublInvoice)
        {
            doc.Validate(Xsd.UblInvoice, null);
            schema = Schema.UblInvoice;
        }
        else if (ns == _ublCreditNote)
        {
            doc.Validate(Xsd.UblCreditNote, null);
            schema = Schema.UblCreditNote;
        }
        else if (ns == _cii)
        {
            try
            {
                doc.Validate(Xsd.CiiD16b, null);
                schema = Schema.CiiD16b;
            }
            catch
            {
                doc.Validate(Xsd.CiiD22b, null);
                schema = Schema.CiiD22b;
            }
        }
        else
        {
            throw new System.Exception($"Unknown root node: {root.Name}.");
        }

        string rawSpecification = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => root
                .Element(_ublCbc + "CustomizationID")!
                .Value,
            Schema.CiiD16b or Schema.CiiD22b => root
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

    public Document(TextReader reader) : this(new XmlTextReader(reader)) { }

    public void WriteTo(XmlWriter writer)
    {
        Doc.WriteTo(writer);
    }
}

static class Xsd
{
    public static XmlSchemaSet UblInvoice;
    public static XmlSchemaSet UblCreditNote;
    public static XmlSchemaSet CiiD16b;
    public static XmlSchemaSet CiiD22b;

    static Xsd()
    {
        using XmlReader ublXmlSigSchemaFile = XmlReader.Create(
            $"{AppContext.BaseDirectory}/En16931.Resources.Extern/Ubl/common/UBL-xmldsig-core-schema-2.1.xsd",
            new() { DtdProcessing = DtdProcessing.Ignore }
        );
        XmlSchema ublXmlSigSchema = XmlSchema.Read(ublXmlSigSchemaFile, null)!;

        XmlSchemaSet ublInvoice = new();
        ublInvoice.XmlResolver = new XmlUrlResolver();
        ublInvoice.Add(ublXmlSigSchema);
        ublInvoice.Add(null, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/Ubl/maindoc/UBL-Invoice-2.1.xsd");
        ublInvoice.Compile();
        UblInvoice = ublInvoice;

        XmlSchemaSet ublCreditNote = new();
        ublCreditNote.XmlResolver = new XmlUrlResolver();
        ublCreditNote.Add(ublXmlSigSchema);
        ublCreditNote.Add(null, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/Ubl/maindoc/UBL-CreditNote-2.1.xsd");
        ublCreditNote.Compile();
        UblCreditNote = ublCreditNote;

        XmlSchemaSet ciiD16b = new();
        ciiD16b.XmlResolver = new XmlUrlResolver();
        ciiD16b.Add(null, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/Cii/D16b/CrossIndustryInvoice_100pD16B.xsd");
        ciiD16b.Compile();
        CiiD16b = ciiD16b;

        XmlSchemaSet ciiD22b = new();
        ciiD22b.XmlResolver = new XmlUrlResolver();
        ciiD22b.Add(null, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/Cii/D22b/CrossIndustryInvoice_100pD22B.xsd");
        ciiD22b.Compile();
        CiiD22b = ciiD22b;
    }
}
