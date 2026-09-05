using En16931.Collections.Immutable;
using En16931.Spec;

namespace En16931.Specs;

public static class BuiltinSpecs
{
    public static readonly RefArray<ISpecificationParser> All = [
        XRechnung.Instance,
        XRechnungExtension.Instance,
        XRechnungCvd.Instance,
        FacturXBasic.Instance,
    ];
}

