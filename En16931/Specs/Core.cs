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

namespace En16931.Specs;

public class Core : ISpecification, ISpecificationValidator, ISpecificationParser, ISpecificationParser<Invoice<Core>>
{
    private enum TransformerId
    {
        En16931Ubl,
        En16931Cii,
        UblToIr,
        CiiToIr,
        IrToUbl,
        IrToCii,
    }

    public static Core Instance = new();

    public static Identifier SpecificationIdentifier { get; } = new("urn:cen.eu:en16931:2017");

    private Core() { }

    private readonly TransformerSet<TransformerId> _transformers = new(new Dictionary<TransformerId, string>() {
        { TransformerId.En16931Ubl, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/En16931/EN16931-UBL-validation.xslt" },
        { TransformerId.En16931Cii, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/En16931/EN16931-CII-validation.xslt" },
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
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.En16931Ubl,
            Schema.CiiD16b => TransformerId.En16931Cii,
            _ => throw new SchemaNotSupportedException(doc.Schema, "Core.Validate"),
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

    public Document Serialize(IInvoice invoice, Schema schema)
    {
        Invoice<Core> unboxed = (Invoice<Core>)invoice;
        return Serialize(ref unboxed, schema);
    }

    public Invoice<Core> Parse(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.UblInvoice or Schema.UblCreditNote => TransformerId.UblToIr,
            Schema.CiiD16b => TransformerId.CiiToIr,
            _ => throw new SchemaNotSupportedException(doc.Schema, "Core.Parse"),
        };

        XDocument ir = _transformers[transformerId].Transform(doc.Doc);

        return Invoice<Core>.Deserialize(ir.CreateReader());
    }

    public Document Serialize(scoped ref readonly Invoice<Core> invoice, Schema schema)
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
            _ => throw new SchemaNotSupportedException(schema, "Core.Serialize"),
        };

        string? initialMode = schema switch
        {
            Schema.UblInvoice => "invoice",
            Schema.UblCreditNote => "credit-note",
            Schema.CiiD16b => "d16b",
            _ => throw new UnreachableException(),
        };

        XDocument result = _transformers[transformerId].Transform(ir, initialMode);

        return new Document(result);
    }
}
