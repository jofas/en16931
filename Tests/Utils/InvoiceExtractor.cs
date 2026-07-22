using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using En16931.Collections.Immutable;
using En16931.Model;
using En16931.Utils;

namespace Tests.Utils;

public static class InvoiceExtractor
{
    public static Array<T> Invoices<T>(Type invoiceCollection) where T : struct, IInvoice
    {
        IEnumerable<T> invoices = invoiceCollection
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(f => f.FieldType.IsAssignableTo(typeof(T)))
            .Select(f => (T)f.GetValue(null)!)
            .ToArray();

        return new(invoices);
    }

    public static T Invoice<T>(Type invoiceCollection, string name) where T : struct, IInvoice
    {
        FieldInfo? field = invoiceCollection.GetField($"Invoice{name}", BindingFlags.Static | BindingFlags.Public);

        Assert.ArgIsNotNull(field);

        Assert.IsTrue(field!.FieldType.IsAssignableTo(typeof(T)), $"Field {invoiceCollection.Name}.Invoice{name} must be castable to type: {typeof(T).Name}");

        return (T)field.GetValue(null)!;
    }
}
