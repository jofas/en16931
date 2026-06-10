using System.Xml;

namespace En16931.IR;

public static class IRConfig
{
    public static string NS = "urn:todo";
}

public interface IIRDeserializable<TSelf>
{
    public abstract static TSelf Deserialize(XmlReader reader);
}
