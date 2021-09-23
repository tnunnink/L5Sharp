using System.Xml.Linq;

namespace LogixHelper.Abstractions
{
    public interface IXSerializable
    {
        XElement Serialize();
    }
}