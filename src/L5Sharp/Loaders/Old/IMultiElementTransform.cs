using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Loaders
{
    public interface IMultiElementTransform
    {
        IEnumerable<XElement> TransformMany(IEnumerable<XElement> elements);
        
        IEnumerable<XElement> TransformMany(XElement element);
    }
}