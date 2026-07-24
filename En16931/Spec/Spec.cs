using System;
using System.Linq;
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
    public Identifier SpecificationIdentifier { get; }

    // Invariant: bt-24 in doc must be equal to `SpecificationIdentifier`
    // ... does it though? We can't map it in the parser and if users want to call the validator directly, we don't really care do we?
    // XRechnung: BR-DE-21
    // Core: no rule; must be implemented
    public void Validate(ref readonly Document doc);
}

public interface ISpecificationParser : ISpecificationValidator
{
    public IInvoice Parse(ref readonly Document doc);

    public Document Serialize(IInvoice invoice, Schema schema);
}

public interface ISpecificationParser<TInvoice, TSpec> where TInvoice : IInvoice<TSpec> where TSpec : ISpecification
{
    public TInvoice Parse(ref readonly Document doc);

    public Document Serialize(scoped ref readonly TInvoice invoice, Schema schema);
}
