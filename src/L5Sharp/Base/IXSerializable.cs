using System.Xml.Linq;

namespace L5Sharp.Base
{
    public interface IXSerializable
    {
        XElement Serialize();
    }
}