using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using En16931.Model;
using En16931.Utils;

namespace Tests.Utils;

internal static class InvoiceExtractor<C, T> where T : IInvoice
{
    public static IEnumerable<T> Invoices => _invoices.Values;

    private static readonly ImmutableDictionary<string, T> _invoices;

    static InvoiceExtractor()
    {
        _invoices = typeof(C)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Select(f => KeyValuePair.Create(f.Name, (T)f.GetValue(null)!))
            .ToImmutableDictionary();
    }

    public static T Invoice(string name)
    {
        return _invoices[$"Invoice{name}"];
    }
}
