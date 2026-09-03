using System;

namespace En16931;

public class ValidationException : Exception
{
    public required En16931.Collections.Immutable.RefArray<string> Errors { get; init; }
}

public class SchemaNotSupportedException : Exception
{
    public SchemaNotSupportedException(Schema schema) : this(schema, "this operation") { }
    public SchemaNotSupportedException(Schema schema, string method) : base($"Schema {schema} not supported by {method}.") { }
}
