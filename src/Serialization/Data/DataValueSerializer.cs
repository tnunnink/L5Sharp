using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class DataValueSerializer : L5XSerializer<IAtomicType>
    {
        private static readonly XName ElementName = L5XElement.DataValue.ToString();
        
        public override XElement Serialize(IAtomicType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(new XAttribute(L5XElement.DataType.ToString(), component.Name));
            element.Add(new XAttribute(L5XAttribute.Value.ToString(), component.Format()));

            return element;
        }

        public override IAtomicType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");
            
            var name = element.DataTypeName();
            var value = element.Attribute(L5XAttribute.Value.ToString())?.Value?.TryParse<IAtomicType>();

            return DataType.Atomic(name, value);
        }
    }
}