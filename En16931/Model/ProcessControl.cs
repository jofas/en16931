using System.Xml;
using En16931.IR;
using En16931.Model.Primitives;
using En16931.Spec;
using En16931.Utils;

namespace En16931.Model;

public readonly record struct ProcessControl<T> : IProcessControl, IIRDeserializable<ProcessControl<T>>, IIRSerializable where T : ISpecification
{
    // BT-23
    public required Text? BusinessProcessType { get; init; }

    // BT-24
    public Identifier SpecificationIdentifier { get => T.SpecificationIdentifier; }

    public void Serialize(XmlWriter writer)
    {
        writer.WriteStartElement("process-control", IRConfig.NS);
        writer.WriteAttributeString("id", "bg-2");

        if (BusinessProcessType is not null)
        {
            writer.WriteStartElement("business-process-type", IRConfig.NS);
            writer.WriteAttributeString("id", "bt-23");
            BusinessProcessType.Value.Serialize(writer);
            writer.WriteEndElement();
        }

        writer.WriteStartElement("specification-identifier", IRConfig.NS);
        writer.WriteAttributeString("id", "bt-24");
        SpecificationIdentifier.Serialize(writer);
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public static ProcessControl<T> Deserialize(XmlReader reader)
    {
        reader.ReadStartElement("process-control", IRConfig.NS);
        reader.MoveToContent();

        Text? businessProcessType = null;

        if (reader.IsStartElement("business-process-type", IRConfig.NS))
        {
            reader.ReadStartElement();
            reader.MoveToContent();

            businessProcessType = Text.Deserialize(reader);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadStartElement("specification-identifier", IRConfig.NS);
        reader.MoveToContent();

        Identifier specificationIdentifier = Identifier.Deserialize(reader);

        if (specificationIdentifier != T.SpecificationIdentifier)
        {
            ThrowHelper.ThrowInvalidOperationException($"`specification-identifier` field value `{specificationIdentifier.Content}` from the xml document does not match the identifier from the specification {typeof(T).Name}, which is: {T.SpecificationIdentifier.Content}");
        }

        reader.ReadEndElement();
        reader.MoveToContent();

        reader.ReadEndElement();
        reader.MoveToContent();

        return new ProcessControl<T>
        {
            BusinessProcessType = businessProcessType,
        };
    }
}
