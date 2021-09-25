using System.Xml.Linq;

namespace L5Sharp.Abstractions
{
    public interface IXSerializable
    {
        XElement Serialize();
    }
}