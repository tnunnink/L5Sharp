using System.Xml.Linq;

namespace L5Sharp.Loaders
{
    public interface IElementTransform
    {
        XElement Normalize(XElement element);
        XElement Revert(XElement element);
    }
}