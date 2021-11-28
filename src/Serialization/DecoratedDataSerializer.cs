using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class DecoratedDataSerializer : IXSerializer<IDataType>
    {
        private const string ElementName = LogixNames.Data;
        
        public XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(component.ToAttribute(c => c.Format));
            
            

            return element;
        }

        public IDataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException(
                    $"Element name '{element.Name}' invalid. Expecting '{ElementName}'.");

            var root = element.Elements().FirstOrDefault();
            return root?.Deserialize<IDataType>();
        }
    }
}