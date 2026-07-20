using System;
using System.Linq;
using System.Xml;
using En16931.Collections.Immutable;
using En16931.Model;
using En16931.Model.Primitives;
using En16931.Utils;

namespace En16931.Spec;

public interface ISpecification
{
    public abstract static Identifier SpecificationIdentifier { get; }
}

public interface ISpecificationValidator
{
    public NonEmptyArray<Schema> SupportedSchemas { get; }

    public Identifier SpecificationIdentifier { get; }

    // Invariant: bt-24 in doc must be equal to `SpecificationIdentifier`
    public void Validate(ref readonly Document doc);
}

public interface ISpecificationParser : ISpecificationValidator
{
    public InvoiceType InvoiceType { get; }

    public IInvoice Parse(ref readonly Document doc);

    public void Serialize(IInvoice invoice, Schema schema, XmlWriter writer);
}

public interface ISpecificationParser<TInvoice, TSpec> where TInvoice : IInvoice<TSpec> where TSpec : ISpecification
{
    public TInvoice Parse(ref readonly Document doc);

    public void Serialize(ref readonly TInvoice invoice, Schema schema, XmlWriter writer);
}

public readonly record struct InvoiceType
{
    public Type Type
    {
        get
        {
            Assert.IsNotNull(field);
            return field;
        }
    }

    public InvoiceType(Type invoiceType, Type specType)
    {
        bool isInvoiceType(Type t)
        {
            return t.IsGenericType
                && t.GetGenericTypeDefinition() == typeof(IInvoice<>)
                && t.GetGenericArguments()[0] == specType;
        }

        Assert.ArgIsNotNull(invoiceType);
        Assert.ArgIsNotNull(specType);

        if (!invoiceType.GetInterfaces().Any(isInvoiceType))
        {
            ThrowHelper.ThrowArgumentException($"{invoiceType} is not an invoice type (it doesn't implement `IInvoice`correctly).");
        }

        Type = invoiceType;
    }
}
