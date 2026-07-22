using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using En16931.Collections.Immutable;
using En16931.Model;
using En16931.Model.Primitives;
using En16931.Spec;
using En16931.Spec.Utils;
using En16931.Utils;

namespace En16931.Specs;

// TODO: restructure Test suite (IRTests.cs aren't IR tests but end-to-end tests of the parsing pipeline)
// TODO: have a dedicated Invoice instance for every test (instead of .. with { ... })
// TODO: use filename to check for violated business rules in failing tests
//
// TODO: test parsing, validating wrong bt-24
// TODO: test InvoiceType
//
// TODO: ExtensionInvoice type for XRechnungExtension (by hand for now)
// TODO: ir xslt files for ExtensionInvoice (by hand for now)
//
// TODO: Parser interface: add support for ExtensionInvoice
// TODO: Tests for XRechnung extension

// TODO: Core Spec

public static class BuiltinSpecs
{
    public static readonly RefArray<ISpecificationParser> All = [XRechnung.Instance, XRechnungExtension.Instance];
}

public class XRechnung : ISpecification, ISpecificationValidator, ISpecificationParser, ISpecificationParser<Invoice<XRechnung>, XRechnung>
{
    private enum TransformerId
    {
        En16931Ubl,
        En16931Cii,
        XRechnungUbl,
        XRechnungCii,
        UblToIr,
        CiiToIr,
        IrToUbl,
        IrToCii,
    }

    public static XRechnung Instance = new();

    public static Identifier SpecificationIdentifier { get; } = new("urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0");

    private XRechnung() { }

    private readonly TransformerSet<TransformerId> _transformers = new(new Dictionary<TransformerId, string>() {
        { TransformerId.En16931Ubl, "Resources/En16931/EN16931-UBL-validation.xslt" },
        { TransformerId.En16931Cii, "Resources/En16931/EN16931-CII-validation.xslt" },
        { TransformerId.XRechnungUbl, "Resources/XRechnung/XRechnung-UBL-validation.xsl" },
        { TransformerId.XRechnungCii, "Resources/XRechnung/XRechnung-CII-validation.xsl" },
        { TransformerId.UblToIr, "Resources/IR/ubl2ir.xslt" },
        { TransformerId.CiiToIr, "Resources/IR/cii2ir.xslt" },
        { TransformerId.IrToUbl, "Resources/IR/ir2ubl.xslt" },
        { TransformerId.IrToCii, "Resources/IR/ir2cii.xslt" },
    });

    Identifier ISpecificationValidator.SpecificationIdentifier { get => SpecificationIdentifier; }

    public NonEmptyArray<Schema> SupportedSchemas { get; } = [Schema.UblInvoice, Schema.UblCreditNote, Schema.CiiCrossIndustryInvoice];

    public InvoiceType InvoiceType { get; } = new(typeof(Invoice<XRechnung>), typeof(XRechnung));

    IInvoice ISpecificationParser.Parse(ref readonly Document doc)
    {
        return (IInvoice)Parse(in doc);
    }

    public void Validate(ref readonly Document doc)
    {
        ValidateEn16931(in doc);
        ValidateXRechnung(in doc);
    }

    public Document Serialize(IInvoice invoice, Schema schema)
    {
        Invoice<XRechnung> unboxed = (Invoice<XRechnung>)invoice;
        return Serialize(ref unboxed, schema);
    }

    public Invoice<XRechnung> Parse(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.UblToIr,
            Schema.CiiCrossIndustryInvoice => TransformerId.CiiToIr,
            _ => throw new UnreachableException(),
        };

        // TODO: have tranformer participate in `Document` API once IR become fully
        //   a first-class syntax... That would require supporting SVRL documents as well
        //
        XDocument ir = _transformers[transformerId].Transform(doc.Doc);

        return Invoice<XRechnung>.Deserialize(ir.CreateReader());
    }

    public Document Serialize(scoped ref readonly Invoice<XRechnung> invoice, Schema schema)
    {
        XDocument ir = new();

        using (XmlWriter irWriter = ir.CreateWriter())
        {
            invoice.Serialize(irWriter);
        }

        TransformerId transformerId = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.IrToUbl,
            Schema.CiiCrossIndustryInvoice => TransformerId.IrToCii,
            _ => throw new UnreachableException(),
        };

        string? initialMode = schema switch
        {
            Schema.UblInvoice => "invoice",
            Schema.UblCreditNote => "credit-note",
            _ => null,
        };

        XDocument result = _transformers[transformerId].Transform(ir, initialMode);

        return new Document(result);
    }

    private void ValidateEn16931(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.En16931Ubl,
            Schema.CiiCrossIndustryInvoice => TransformerId.En16931Cii,
            _ => throw new UnreachableException(),
        };

        SchematronResult result = Svrl.Validate(doc.Doc, _transformers[transformerId]);

        if (result.Errors.Count > 0)
        {
            throw new En16931SchematronException
            {
                Errors = new RefArray<string>(result.Errors),
            };
        }
    }

    private void ValidateXRechnung(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.XRechnungUbl,
            Schema.CiiCrossIndustryInvoice => TransformerId.XRechnungCii,
            _ => throw new UnreachableException(),
        };

        SchematronResult result = Svrl.Validate(doc.Doc, _transformers[transformerId]);

        if (result.Errors.Count > 0)
        {
            throw new XRechnungSchematronException
            {
                Errors = new RefArray<string>(result.Errors),
            };
        }
    }
}

public class XRechnungExtension : ISpecification, ISpecificationValidator, ISpecificationParser, ISpecificationParser<Invoice<XRechnungExtension>, XRechnungExtension>
{
    private enum TransformerId
    {
        En16931Ubl,
        En16931Cii,
        XRechnungUbl,
        XRechnungCii,
        UblToIr,
        CiiToIr,
        IrToUbl,
        IrToCii,
    }

    public static XRechnungExtension Instance = new();

    public static Identifier SpecificationIdentifier { get; } = new("urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#conformant#urn:xeinkauf.de:kosit:extension:xrechnung_3.0");

    private XRechnungExtension() { }

    private readonly TransformerSet<TransformerId> _transformers = new(new Dictionary<TransformerId, string>() {
        { TransformerId.En16931Ubl, "Resources/En16931/EN16931-UBL-validation.xslt" },
        { TransformerId.En16931Cii, "Resources/En16931/EN16931-CII-validation.xslt" },
        { TransformerId.XRechnungUbl, "Resources/XRechnung/XRechnung-UBL-validation.xsl" },
        { TransformerId.XRechnungCii, "Resources/XRechnung/XRechnung-CII-validation.xsl" },
        { TransformerId.UblToIr, "Resources/IR/ubl2ir.xslt" },
        { TransformerId.CiiToIr, "Resources/IR/cii2ir.xslt" },
        { TransformerId.IrToUbl, "Resources/IR/ir2ubl.xslt" },
        { TransformerId.IrToCii, "Resources/IR/ir2cii.xslt" },
    });

    Identifier ISpecificationValidator.SpecificationIdentifier { get => SpecificationIdentifier; }

    public NonEmptyArray<Schema> SupportedSchemas { get; } = [Schema.UblInvoice, Schema.UblCreditNote, Schema.CiiCrossIndustryInvoice];

    public InvoiceType InvoiceType { get; } = new(typeof(Invoice<XRechnung>), typeof(XRechnung));

    IInvoice ISpecificationParser.Parse(ref readonly Document doc)
    {
        return (IInvoice)Parse(in doc);
    }

    public void Validate(ref readonly Document doc)
    {
        try
        {
            ValidateEn16931(in doc);
        }
        catch (En16931SchematronException e)
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
            if (!e.Errors.All(e =>
            {
                return ((string[])[
                    "BR-CL-10",
                    "BR-CL-11",
                    "BR-CL-21",
                    "BR-CL-25",
                    "BR-CL-26",
                    "BR-CO-16",
                ]).Contains(e);
            }))
            {
                throw;
            }
        }

        ValidateXRechnung(in doc);
    }

    public Document Serialize(IInvoice invoice, Schema schema)
    {
        Invoice<XRechnungExtension> unboxed = (Invoice<XRechnungExtension>)invoice;
        return Serialize(in unboxed, schema);
    }

    public Invoice<XRechnungExtension> Parse(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.UblToIr,
            Schema.CiiCrossIndustryInvoice => TransformerId.CiiToIr,
            _ => throw new UnreachableException(),
        };

        XDocument ir = _transformers[transformerId].Transform(doc.Doc);

        return Invoice<XRechnungExtension>.Deserialize(ir.CreateReader());
    }

    public Document Serialize(scoped ref readonly Invoice<XRechnungExtension> invoice, Schema schema)
    {
        XDocument ir = new();

        using (XmlWriter irWriter = ir.CreateWriter())
        {
            invoice.Serialize(irWriter);
        }

        TransformerId transformerId = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.IrToUbl,
            Schema.CiiCrossIndustryInvoice => TransformerId.IrToCii,
            _ => throw new UnreachableException(),
        };

        string? initialMode = schema switch
        {
            Schema.UblInvoice => "invoice",
            Schema.UblCreditNote => "credit-note",
            _ => null,
        };

        XDocument result = _transformers[transformerId].Transform(ir, initialMode);

        return new Document(result);
    }

    private void ValidateEn16931(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.En16931Ubl,
            Schema.CiiCrossIndustryInvoice => TransformerId.En16931Cii,
            _ => throw new UnreachableException(),
        };

        SchematronResult result = Svrl.Validate(doc.Doc, _transformers[transformerId]);

        if (result.Errors.Count > 0)
        {
            throw new En16931SchematronException
            {
                Errors = new RefArray<string>(result.Errors),
            };
        }
    }

    private void ValidateXRechnung(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.XRechnungUbl,
            Schema.CiiCrossIndustryInvoice => TransformerId.XRechnungCii,
            _ => throw new UnreachableException(),
        };

        SchematronResult result = Svrl.Validate(doc.Doc, _transformers[transformerId]);

        if (result.Errors.Count > 0)
        {
            throw new XRechnungSchematronException
            {
                Errors = new RefArray<string>(result.Errors),
            };
        }
    }
}
