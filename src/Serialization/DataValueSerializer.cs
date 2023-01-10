using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class DataValueSerializer : L5XSerializer<IAtomicType>
    {
        private static readonly XName ElementName = L5XName.DataValue;
        
        public override XElement Serialize(IAtomicType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(new XAttribute(L5XName.DataType, component.Name));
            element.Add(new XAttribute(L5XName.Value, component.Format()));

            return element;
        }

        public override IAtomicType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");
            
            var name = element.DataTypeName();
            var value = element.Attribute(L5XName.Value)?.Value.Parse<IAtomicType>();

            return LogixType.Atomic(name, value);
        }
    }
}