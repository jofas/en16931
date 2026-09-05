using System;
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
using XRE = En16931.Model.XRechnungExtension;

namespace En16931.Specs;

public class XRechnung : ISpecification, ISpecificationValidator, ISpecificationParser, ISpecificationParser<Invoice<XRechnung>>
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
        { TransformerId.En16931Ubl, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/En16931/EN16931-UBL-validation.xslt" },
        { TransformerId.En16931Cii, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/En16931/EN16931-CII-validation.xslt" },
        { TransformerId.XRechnungUbl, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/XRechnung/XRechnung-UBL-validation.xsl" },
        { TransformerId.XRechnungCii, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/XRechnung/XRechnung-CII-validation.xsl" },
        { TransformerId.UblToIr, $"{AppContext.BaseDirectory}/IR/ubl2ir.xslt" },
        { TransformerId.CiiToIr, $"{AppContext.BaseDirectory}/IR/cii2ir.xslt" },
        { TransformerId.IrToUbl, $"{AppContext.BaseDirectory}/IR/ir2ubl.xslt" },
        { TransformerId.IrToCii, $"{AppContext.BaseDirectory}/IR/ir2cii.xslt" },
    });

    Identifier ISpecificationValidator.SpecificationIdentifier { get => SpecificationIdentifier; }

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
            Schema.CiiD16b => TransformerId.CiiToIr,
            _ => throw new SchemaNotSupportedException(doc.Schema, "XRechnung.Parse"),
        };

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
            Schema.CiiD16b => TransformerId.IrToCii,
            Schema.CiiD22b => throw new SchemaNotSupportedException(schema, "XRechnung.Serialize"),
            _ => throw new UnreachableException(),
        };

        string? initialMode = schema switch
        {
            Schema.UblInvoice => "invoice",
            Schema.UblCreditNote => "credit-note",
            Schema.CiiD16b => "d16b",
            Schema.CiiD22b => "d22b",
            _ => throw new UnreachableException(),
        };

        XDocument result = _transformers[transformerId].Transform(ir, initialMode);

        return new Document(result);
    }

    private void ValidateEn16931(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.En16931Ubl,
            Schema.CiiD16b => TransformerId.En16931Cii,
            _ => throw new SchemaNotSupportedException(doc.Schema, "XRechnung.Validate"),
        };

        SchematronResult result = Svrl.Validate(doc.Doc, _transformers[transformerId]);

        if (result.Errors.Count > 0)
        {
            throw new ValidationException
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
            Schema.CiiD16b => TransformerId.XRechnungCii,
            _ => throw new SchemaNotSupportedException(doc.Schema, "XRechnung.Validate"),
        };

        SchematronResult result = Svrl.Validate(doc.Doc, _transformers[transformerId]);

        if (result.Errors.Count > 0)
        {
            throw new ValidationException
            {
                Errors = new RefArray<string>(result.Errors),
            };
        }
    }
}

public class XRechnungExtension : ISpecification, ISpecificationValidator, ISpecificationParser, ISpecificationParser<XRE.Invoice>
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
        { TransformerId.En16931Ubl, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/En16931/EN16931-UBL-validation.xslt" },
        { TransformerId.En16931Cii, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/En16931/EN16931-CII-validation.xslt" },
        { TransformerId.XRechnungUbl, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/XRechnung/XRechnung-UBL-validation.xsl" },
        { TransformerId.XRechnungCii, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/XRechnung/XRechnung-CII-validation.xsl" },
        { TransformerId.UblToIr, $"{AppContext.BaseDirectory}/IR/XRechnungExtension/ubl2ir.xslt" },
        { TransformerId.CiiToIr, $"{AppContext.BaseDirectory}/IR/cii2ir.xslt" },
        { TransformerId.IrToUbl, $"{AppContext.BaseDirectory}/IR/XRechnungExtension/ir2ubl.xslt" },
        { TransformerId.IrToCii, $"{AppContext.BaseDirectory}/IR/ir2cii.xslt" },
    });

    Identifier ISpecificationValidator.SpecificationIdentifier { get => SpecificationIdentifier; }

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
        catch (ValidationException e)
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
        XRE.Invoice unboxed = (XRE.Invoice)invoice;
        return Serialize(in unboxed, schema);
    }

    public XRE.Invoice Parse(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.UblToIr,
            Schema.CiiD16b => TransformerId.CiiToIr,
            _ => throw new SchemaNotSupportedException(doc.Schema, "XRechnungExtension.Parse"),
        };

        XDocument ir = _transformers[transformerId].Transform(doc.Doc);

        return XRE.Invoice.Deserialize(ir.CreateReader());
    }

    public Document Serialize(scoped ref readonly XRE.Invoice invoice, Schema schema)
    {
        XDocument ir = new();

        using (XmlWriter irWriter = ir.CreateWriter())
        {
            invoice.Serialize(irWriter);
        }

        TransformerId transformerId = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.IrToUbl,
            Schema.CiiD16b => TransformerId.IrToCii,
            Schema.CiiD22b => throw new SchemaNotSupportedException(schema, "XRechnungExtension.Serialize"),
            _ => throw new UnreachableException(),
        };

        string? initialMode = schema switch
        {
            Schema.UblInvoice => "invoice",
            Schema.UblCreditNote => "credit-note",
            Schema.CiiD16b => "d16b",
            Schema.CiiD22b => "d22b",
            _ => throw new UnreachableException(),
        };

        XDocument result = _transformers[transformerId].Transform(ir, initialMode);

        return new Document(result);
    }

    private void ValidateEn16931(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.En16931Ubl,
            Schema.CiiD16b => TransformerId.En16931Cii,
            _ => throw new SchemaNotSupportedException(doc.Schema, "XRechnungExtension.Validate"),
        };

        SchematronResult result = Svrl.Validate(doc.Doc, _transformers[transformerId]);

        if (result.Errors.Count > 0)
        {
            throw new ValidationException
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
            Schema.CiiD16b => TransformerId.XRechnungCii,
            _ => throw new SchemaNotSupportedException(doc.Schema, "XRechnungExtension.Validate"),
        };

        SchematronResult result = Svrl.Validate(doc.Doc, _transformers[transformerId]);

        if (result.Errors.Count > 0)
        {
            throw new ValidationException
            {
                Errors = new RefArray<string>(result.Errors),
            };
        }
    }
}

public class XRechnungCvd : ISpecification, ISpecificationValidator, ISpecificationParser, ISpecificationParser<Invoice<XRechnungCvd>>
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

    public static XRechnungCvd Instance = new();

    public static Identifier SpecificationIdentifier { get; } = new("urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0#compliant#urn:xeinkauf.de:kosit:xrechnung:cvd_0.9");

    private XRechnungCvd() { }

    private readonly TransformerSet<TransformerId> _transformers = new(new Dictionary<TransformerId, string>() {
        { TransformerId.En16931Ubl, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/En16931/EN16931-UBL-validation.xslt" },
        { TransformerId.En16931Cii, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/En16931/EN16931-CII-validation.xslt" },
        { TransformerId.XRechnungUbl, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/XRechnung/XRechnung-UBL-validation.xsl" },
        { TransformerId.XRechnungCii, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/XRechnung/XRechnung-CII-validation.xsl" },
        { TransformerId.UblToIr, $"{AppContext.BaseDirectory}/IR/ubl2ir.xslt" },
        { TransformerId.CiiToIr, $"{AppContext.BaseDirectory}/IR/cii2ir.xslt" },
        { TransformerId.IrToUbl, $"{AppContext.BaseDirectory}/IR/ir2ubl.xslt" },
        { TransformerId.IrToCii, $"{AppContext.BaseDirectory}/IR/ir2cii.xslt" },
    });

    Identifier ISpecificationValidator.SpecificationIdentifier { get => SpecificationIdentifier; }

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
        catch (ValidationException e)
        {
            // * BR-CL-13 => BR-TMP-CVD-01
            //
            if (!e.Errors.All(e =>
            {
                return ((string[])[
                    "BR-CL-13",
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
        Invoice<XRechnungCvd> unboxed = (Invoice<XRechnungCvd>)invoice;
        return Serialize(ref unboxed, schema);
    }

    public Invoice<XRechnungCvd> Parse(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.UblToIr,
            Schema.CiiD16b => TransformerId.CiiToIr,
            _ => throw new SchemaNotSupportedException(doc.Schema, "XRechnungCvd.Parse"),
        };

        XDocument ir = _transformers[transformerId].Transform(doc.Doc);

        return Invoice<XRechnungCvd>.Deserialize(ir.CreateReader());
    }

    public Document Serialize(scoped ref readonly Invoice<XRechnungCvd> invoice, Schema schema)
    {
        XDocument ir = new();

        using (XmlWriter irWriter = ir.CreateWriter())
        {
            invoice.Serialize(irWriter);
        }

        TransformerId transformerId = schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.IrToUbl,
            Schema.CiiD16b => TransformerId.IrToCii,
            Schema.CiiD22b => throw new SchemaNotSupportedException(schema, "XRechnungCvd.Serialize"),
            _ => throw new UnreachableException(),
        };

        string? initialMode = schema switch
        {
            Schema.UblInvoice => "invoice",
            Schema.UblCreditNote => "credit-note",
            Schema.CiiD16b => "d16b",
            Schema.CiiD22b => "d22b",
            _ => throw new UnreachableException(),
        };

        XDocument result = _transformers[transformerId].Transform(ir, initialMode);

        return new Document(result);
    }

    private void ValidateEn16931(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.En16931Ubl,
            Schema.CiiD16b => TransformerId.En16931Cii,
            _ => throw new SchemaNotSupportedException(doc.Schema, "XRechnungCvd.Validate"),
        };

        SchematronResult result = Svrl.Validate(doc.Doc, _transformers[transformerId]);

        if (result.Errors.Count > 0)
        {
            throw new ValidationException
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
            Schema.CiiD16b => TransformerId.XRechnungCii,
            _ => throw new SchemaNotSupportedException(doc.Schema, "XRechnungCvd.Validate"),
        };

        SchematronResult result = Svrl.Validate(doc.Doc, _transformers[transformerId]);

        if (result.Errors.Count > 0)
        {
            throw new ValidationException
            {
                Errors = new RefArray<string>(result.Errors),
            };
        }
    }
}
