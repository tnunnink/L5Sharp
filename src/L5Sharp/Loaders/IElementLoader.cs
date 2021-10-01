using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Loaders
{
    public interface IElementLoader
    {
        void Load(XElement element);
        void Load(IEnumerable<XElement> elements);
    }
}