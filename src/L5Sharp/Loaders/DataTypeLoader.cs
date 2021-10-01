using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Loaders
{
    public class DataTypeLoader : IElementLoader
    {
        private readonly IController _controller;

        public DataTypeLoader(IController controller)
        {
            _controller = controller;
        }
        
        public void Load(XElement element)
        {
            var dataType = new DataType(element.GetName(), element.GetDescription());
            _controller.AddDataType(dataType);
        }

        public void Load(IEnumerable<XElement> elements)
        {
            foreach (var element in elements)
                Load(element);
        }
    }
}