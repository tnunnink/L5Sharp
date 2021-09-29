using System.Xml.Linq;

namespace L5Sharp.Transforms
{
    public interface IElementTransform
    {
        XElement Perform(XElement element);
    }
}