using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Saxon.Api;
using Im = En16931.Model.Immutable;
using Mut = En16931.Model;

namespace En16931;

public class Parser
{
    XmlNamespaceManager _namespaces;

    XmlSchemaSet _schemaSet;

    XmlSerializer _invoiceSerializer;

    DocumentBuilder _docBuilder;

    Validator _en16931UblValidator;
    Validator _en16931CiiValidator;

    Validator _xRechnungUblValidator;
    Validator _xRechnungCiiValidator;

    Transformer _irTransformer;

    public Parser()
    {
        _namespaces = new XmlNamespaceManager(new NameTable());

        // UBL namespaces
        _namespaces.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
        _namespaces.AddNamespace("invoice", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
        _namespaces.AddNamespace("credit-note", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");

        // CII namespaces
        _namespaces.AddNamespace("rsm", "urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100");
        _namespaces.AddNamespace("ram", "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100");

        // Schema is DTD annotated, which is why we have to add it like this,
        // instead of adding the file directly with `_schemaSet.Add`
        FileStream w3XmlSigSchemaFile = File.OpenRead("Resources/W3/xmldsig-core-schema.xsd");
        XmlSchema w3XmlSigSchema = XmlSchema.Read(w3XmlSigSchemaFile, null)!;

        _schemaSet = new XmlSchemaSet();
        _schemaSet.XmlResolver = new XmlUrlResolver();
        _schemaSet.Add(null, "Resources/Ubl/maindoc/UBL-Invoice-2.1.xsd");
        _schemaSet.Add(null, "Resources/Ubl/maindoc/UBL-CreditNote-2.1.xsd");
        _schemaSet.Add(null, "Resources/Cii/CrossIndustryInvoice_100pD16B.xsd");
        _schemaSet.Add(w3XmlSigSchema);
        _schemaSet.Compile();

        _invoiceSerializer = new XmlSerializer(typeof(Mut.Invoice));

        Processor processor = new Processor(false);
        processor.ErrorWriter = TextWriter.Null;

        _docBuilder = processor.NewDocumentBuilder();

        XsltCompiler xsltCompiler = processor.NewXsltCompiler();

        Uri en16931UblUri = new Uri(new FileInfo("Resources/En16931/EN16931-UBL-validation.xslt").FullName);
        _en16931UblValidator = new Validator(xsltCompiler.Compile(en16931UblUri));

        Uri en16931CiiUri = new Uri(new FileInfo("Resources/En16931/EN16931-CII-validation.xslt").FullName);
        _en16931CiiValidator = new Validator(xsltCompiler.Compile(en16931CiiUri));

        Uri xRechnungUblUri = new Uri(new FileInfo("Resources/XRechnung/XRechnung-UBL-validation.xsl").FullName);
        _xRechnungUblValidator = new Validator(xsltCompiler.Compile(xRechnungUblUri));

        Uri xRechnungCiiUri = new Uri(new FileInfo("Resources/XRechnung/XRechnung-CII-validation.xsl").FullName);
        _xRechnungCiiValidator = new Validator(xsltCompiler.Compile(xRechnungCiiUri));

        Uri irUri = new Uri(new FileInfo("Resources/IR/ir.xslt").FullName);
        _irTransformer = new Transformer(xsltCompiler.Compile(irUri));
    }

    public Result<Im.Invoice> ParseFileToImmutable(string filepath)
    {
        RefResult<Mut.Invoice> mut = ParseFile(filepath);

        if (mut.HasErrors())
        {
            return mut.As<Im.Invoice>();
        }

        return mut.Map<Im.Invoice>(v => v!.ToImmutable());
    }

    public RefResult<Mut.Invoice> ParseFile(string filepath)
    {
        RefResult<XmlDocument> ir = ParseFileToIR(filepath);

        if (ir.HasErrors())
        {
            return ir.AsRef<Mut.Invoice>();
        }

        XmlNodeReader reader = new(ir.Value!);

        Mut.Invoice invoice = (Mut.Invoice)_invoiceSerializer.Deserialize(reader)!;

        return ir.WithRef(invoice);
    }

    private RefResult<XmlDocument> ParseFileToIR(string filepath)
    {
        using StreamReader reader = new(filepath);

        RefResult<XmlDocument> validatedDocResult = GetSchemaValidatedDocument(reader);

        if (validatedDocResult.HasErrors())
        {
            return validatedDocResult;
        }

        XmlDocument validatedDoc = validatedDocResult.Unwrap();

        Result<DocumentType> docTypeResult = GetDocumentType(validatedDoc);

        if (docTypeResult.HasErrors())
        {
            return docTypeResult.AsRef<XmlDocument>();
        }

        DocumentType docType = docTypeResult.Unwrap();

        XdmNode doc = _docBuilder.Build(validatedDoc);

        Validator en16931Validator = docType.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => _en16931UblValidator,
            Schema.Cii => _en16931CiiValidator,
            _ => throw new UnreachableException(),
        };

        Validator xRechnungValidator = docType.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => _xRechnungUblValidator,
            Schema.Cii => _xRechnungCiiValidator,
            _ => throw new UnreachableException(),
        };

        Report en16931Report = en16931Validator.Validate(doc);
        Report xRechnungReport = xRechnungValidator.Validate(doc);

        if (docType.Standard == Standard.XRechnungExtension)
        {
            // Extensions can extend code listings and otherwise add elements
            // or override rules of the EN16931 specification.
            // These overridden rules of the EN16931 Schematron can fail early,
            // even when the invoice is valid according to the extension.
            // Here we remove these failed asserts from the query and continue
            // executing the rules that override the code listings.
            //
            // The XRechnung Extension overrides the following rules:
            //
            // * BR-CL-10 => BR-DEX-04
            // * BR-CL-11 => BR-DEX-05
            // * BR-CL-21 => BR-DEX-06
            // * BR-CL-25 => BR-DEX-07
            // * BR-CL-26 => BR-DEX-08
            // * BR-CO-16 => BR-DEX-09
            //
            en16931Report.Errors.RemoveAll(e =>
            {
                return ((string[])[
                    "BR-CL-10",
                    "BR-CL-11",
                    "BR-CL-21",
                    "BR-CL-25",
                    "BR-CL-26",
                    "BR-CO-16",
                ]).Contains(e);
            });
        }

        Report finalReport = en16931Report.Merge(xRechnungReport);

        if (finalReport.HasErrors())
        {
            return finalReport.AsRefResult<XmlDocument>();
        }

        return finalReport.AsRefResult<XmlDocument>(_irTransformer.Transform(doc));
    }

    private RefResult<XmlDocument> GetSchemaValidatedDocument(TextReader reader)
    {
        XmlDocument doc = new XmlDocument()
        {
            Schemas = _schemaSet,
        };

        doc.Load(reader);

        try
        {
            doc.Validate(null);
        }
        catch (XmlSchemaValidationException e)
        {
            return Report.FromSchemaViolation(e).AsRefResult<XmlDocument>();
        }

        return Report.Ok.AsRefResult<XmlDocument>(doc);
    }

    private Result<DocumentType> GetDocumentType(XmlDocument doc)
    {
        XmlNode root = doc.DocumentElement ?? throw new UnreachableException();

        Schema schema = (root.NamespaceURI, root.LocalName) switch
        {
            (string namespaceUri, "Invoice") when namespaceUri == _namespaces.LookupNamespace("invoice") => Schema.UblInvoice,
            (string namespaceUri, "CreditNote") when namespaceUri == _namespaces.LookupNamespace("credit-note") => Schema.UblCreditNote,
            (string namespaceUri, "CrossIndustryInvoice") when namespaceUri == _namespaces.LookupNamespace("rsm") => Schema.Cii,
            (_, _) => throw new UnreachableException(),
        };

        string specificationIdentifier = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => root.SelectSingleNode("cbc:CustomizationID", _namespaces)?.InnerText ?? throw new Exception("Could not find specification identifier node."),
            Schema.Cii => root.SelectSingleNode("rsm:ExchangedDocumentContext/ram:GuidelineSpecifiedDocumentContextParameter/ram:ID", _namespaces)?.InnerText ?? throw new Exception("Could not find specification identifier node."),
            _ => throw new UnreachableException(),
        };

        Result<Standard> standardResult = specificationIdentifier switch
        {
            "urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0" => Report.Ok.AsResult<Standard>(Standard.XRechnungCius),
            "urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#conformant#urn:xeinkauf.de:kosit:extension:xrechnung_3.0" => Report.Ok.AsResult<Standard>(Standard.XRechnungExtension),
            string unknown => Report.FromUnknownSpecificationIdentifier(unknown).AsResult<Standard>(),
        };

        // TODO: XRechnungExtension is incompatible with CII

        if (standardResult.HasErrors())
        {
            return standardResult.As<DocumentType>();
        }

        return standardResult.Map<DocumentType>(s => new DocumentType(schema, s!.Value));
    }
}

public readonly record struct Result<T> where T : struct
{
    public required Report Report { get; init; }
    public required T? Value { get => HasErrors() ? null : field; init; }

    [SetsRequiredMembers]
    public Result(Report report, T? value)
    {
        Report = report;
        Value = value;
    }

    public bool IsOk() => Report.IsOk();

    public bool HasErrors() => Report.HasErrors();

    public T Unwrap()
    {
        return Value!.Value;
    }

    public Result<U> Map<U>(Func<T?, U?> map) where U : struct
    {
        return new Result<U>(Report, map(Value));
    }

    public RefResult<U> MapRef<U>(Func<T?, U?> map) where U : class
    {
        return new RefResult<U>(Report, map(Value));
    }

    public Result<U> With<U>(U? value) where U : struct
    {
        return new Result<U>(Report, value);
    }

    public RefResult<U> WithRef<U>(U? value) where U : class
    {
        return new RefResult<U>(Report, value);
    }

    public Result<U> As<U>() where U : struct
    {
        return new Result<U>(Report, Value as U?);
    }

    public RefResult<U> AsRef<U>() where U : class
    {
        return new RefResult<U>(Report, Value as U);
    }
}

public readonly record struct RefResult<T> where T : class
{
    public required Report Report { get; init; }
    public required T? Value { get => HasErrors() ? null : field; init; }

    [SetsRequiredMembers]
    public RefResult(Report report, T? value)
    {
        Report = report;
        Value = value;
    }

    public bool IsOk() => Report.IsOk();

    public bool HasErrors() => Report.HasErrors();

    public T Unwrap()
    {
        if (Value is null)
        {
            throw new InvalidOperationException("Nullable object must have a value.");
        }

        return Value;
    }

    public Result<U> Map<U>(Func<T?, U?> map) where U : struct
    {
        return new Result<U>(Report, map(Value));
    }

    public RefResult<U> MapRef<U>(Func<T?, U?> map) where U : class
    {
        return new RefResult<U>(Report, map(Value));
    }

    public Result<U> With<U>(U? value) where U : struct
    {
        return new Result<U>(Report, value);
    }

    public RefResult<U> WithRef<U>(U? value) where U : class
    {
        return new RefResult<U>(Report, value);
    }

    public Result<U> As<U>() where U : struct
    {
        return new Result<U>(Report, Value as U?);
    }

    public RefResult<U> AsRef<U>() where U : class
    {
        return new RefResult<U>(Report, Value as U);
    }
}

// TODO: proper value type semantics
public readonly record struct Report
{
    public static Report Ok = new Report
    {
        SchemaViolation = null,
        UnknownSpecificationIdentifier = null,
        Infos = [],
        Warnings = [],
        Errors = [],
    };

    public required XmlSchemaValidationException? SchemaViolation { get; init; }

    public required string? UnknownSpecificationIdentifier { get; init; }

    // TODO: infos and warnings are only relevant on the success path
    // TODO: Result<Report<T>>(Report<T>, Error) throw Exception if Error is not null
    // TODO: Report<T>(Infos, Warnings, T)
    //
    public required List<string> Infos { get; init; }
    public required List<string> Warnings { get; init; }
    public required List<string> Errors { get; init; }

    public bool IsOk()
    {
        return SchemaViolation is null
            && UnknownSpecificationIdentifier is null
            && Errors.Count == 0;
    }

    public bool HasErrors() => !IsOk();

    public Report Merge(Report other)
    {
        return new Report
        {
            SchemaViolation = SchemaViolation ?? other.SchemaViolation,
            UnknownSpecificationIdentifier = UnknownSpecificationIdentifier ?? other.UnknownSpecificationIdentifier,
            Infos = Infos.Concat(other.Infos).ToList(),
            Warnings = Warnings.Concat(other.Warnings).ToList(),
            Errors = Errors.Concat(other.Errors).ToList(),
        };
    }

    public Result<T> AsResult<T>(T? value = null) where T : struct
    {
        return new Result<T>(this, value);
    }

    public RefResult<T> AsRefResult<T>(T? value = null) where T : class
    {
        return new RefResult<T>(this, value);
    }

    public static Report FromSchemaViolation(XmlSchemaValidationException schemaViolation)
    {
        return new Report
        {
            SchemaViolation = schemaViolation,
            UnknownSpecificationIdentifier = null,
            Infos = [],
            Warnings = [],
            Errors = [],
        };
    }

    public static Report FromUnknownSpecificationIdentifier(string unknownSpecificationIdentifier)
    {
        return new Report
        {
            SchemaViolation = null,
            UnknownSpecificationIdentifier = unknownSpecificationIdentifier,
            Infos = [],
            Warnings = [],
            Errors = [],
        };
    }

    public static Report FromSchematronOutput(List<string> infos, List<string> warnings, List<string> errors)
    {
        return new Report
        {
            SchemaViolation = null,
            UnknownSpecificationIdentifier = null,
            Infos = infos,
            Warnings = warnings,
            Errors = errors,
        };
    }
}

readonly record struct Validator(XsltExecutable Executable)
{
    public Report Validate(XdmNode doc)
    {
        Xslt30Transformer transformer = Executable.Load30();
        transformer.GlobalContextItem = doc;
        XdmNode result = (XdmNode)transformer.ApplyTemplates(doc);

        List<string> infos = [];
        List<string> warnings = [];
        List<string> errors = [];

        foreach (XdmNode node in result.Children("failed-assert"))
        {
            List<string> list = node.GetAttributeValue("flag") switch
            {
                "information" => infos,
                "warning" => warnings,
                "fatal" => errors,
                _ => throw new UnreachableException(),
            };

            list.Add(node.GetAttributeValue("id"));
        }

        return Report.FromSchematronOutput(infos, warnings, errors);
    }
}

readonly record struct Transformer(XsltExecutable Executable)
{
    public XmlDocument Transform(XdmNode doc)
    {
        DomDestination destination = new();

        Xslt30Transformer transformer = Executable.Load30();
        transformer.GlobalContextItem = doc;
        transformer.ApplyTemplates(doc, destination);

        return destination.XmlDocument;
    }
}

enum Schema
{
    UblInvoice,
    UblCreditNote,
    Cii,
}

enum Standard
{
    XRechnungCius,
    XRechnungExtension,
}

readonly record struct DocumentType(Schema Schema, Standard Standard);
