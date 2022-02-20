using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class DataValueSerializer : IXSerializer<IAtomicType>
    {
        private static readonly XName ElementName = LogixNames.DataValue;
        
        public XElement Serialize(IAtomicType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, m => m.Name, nameOverride: LogixNames.DataType);
            //todo what about radix...
            element.Add(new XAttribute(LogixNames.Value, component.Format()));

            return element;
        }

        public IAtomicType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");
            
            var atomic = (IAtomicType)DataType.Create(element.GetDataTypeName());
            //todo what about radix...
            var radix = element.GetAttribute<IMember<IDataType>, Radix>(m => m.Radix) ?? Radix.Default(atomic);
            var value = element.Attribute(LogixNames.Value)?.Value!;
            
            atomic.SetValue(value);

            return atomic;
        }
    }
}