using En16931.Model.Primitives;
using En16931.Spec;

namespace En16931.Model;

public interface IInvoice
{
    // BG-2
    public IProcessControl ProcessControl { get; }
}

public interface IProcessControl
{
    // BT-24
    public Identifier SpecificationIdentifier { get; }
}

// Interface for cius/core data model, as it allows multiple implementations.
// Extension invoices do not need to implement this and can instead store BT-24 as a constant inline.
public interface IInvoice<TSpec> where TSpec : ISpecification { }
