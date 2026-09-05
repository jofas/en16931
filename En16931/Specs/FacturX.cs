using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using En16931.Collections.Immutable;
using En16931.Model;
using En16931.Model.Primitives;
using En16931.Spec;
using En16931.Spec.Utils;

namespace En16931.Specs;

public class FacturXBasic : ISpecification, ISpecificationValidator, ISpecificationParser, ISpecificationParser<Invoice<FacturXBasic>>
{
    private enum TransformerId
    {
        FacturXBasicCii,
        CiiToIr,
        IrToCii,
    }

    public static FacturXBasic Instance = new();

    public static Identifier SpecificationIdentifier { get; } = new("urn:cen.eu:en16931:2017#compliant#urn:factur-x.eu:1p0:basic");

    private FacturXBasic() { }

    private readonly TransformerSet<TransformerId> _transformers = new(new Dictionary<TransformerId, string>() {
        { TransformerId.FacturXBasicCii, $"{AppContext.BaseDirectory}/En16931.Resources.Extern/FacturX/FACTUR-X_BASIC.xslt" },
        { TransformerId.CiiToIr, $"{AppContext.BaseDirectory}/IR/cii2ir.xslt" },
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
            Schema.CiiD16b or Schema.CiiD22b => TransformerId.FacturXBasicCii,
            _ => throw new SchemaNotSupportedException(doc.Schema, "FacturXBasic.Validate"),
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
        Invoice<FacturXBasic> unboxed = (Invoice<FacturXBasic>)invoice;
        return Serialize(ref unboxed, schema);
    }

    public Invoice<FacturXBasic> Parse(ref readonly Document doc)
    {
        TransformerId transformerId = doc.Schema switch
        {
            Schema.CiiD16b or Schema.CiiD22b => TransformerId.CiiToIr,
            _ => throw new SchemaNotSupportedException(doc.Schema, "FacturXBasic.Parse"),
        };

        XDocument ir = _transformers[transformerId].Transform(doc.Doc);

        return Invoice<FacturXBasic>.Deserialize(ir.CreateReader());
    }

    public Document Serialize(scoped ref readonly Invoice<FacturXBasic> invoice, Schema schema)
    {
        XDocument ir = new();

        using (XmlWriter irWriter = ir.CreateWriter())
        {
            invoice.Serialize(irWriter);
        }

        TransformerId transformerId = schema switch
        {
            Schema.CiiD16b or Schema.CiiD22b => TransformerId.IrToCii,
            _ => throw new SchemaNotSupportedException(schema, "FacturXBasic.Serialize"),
        };

        string? initialMode = schema switch
        {
            Schema.CiiD16b => "d16b",
            Schema.CiiD22b => "d22b",
            _ => throw new UnreachableException(),
        };

        XDocument result = _transformers[transformerId].Transform(ir, initialMode);

        return new Document(result);
    }
}
