using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class DataValueSerializer : IXSerializer<IAtomic>
    {
        private const string ElementName = LogixNames.DataValue;
        
        public XElement Serialize(IAtomic component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(component.ToAttribute(c => c.Name, LogixNames.DataType));
            element.Add(component.ToAttribute(c => c.Radix));
            element.Add(component.ToAttribute(c => c.FormattedValue));

            return element;
        }

        public IAtomic Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var type = element.GetDataTypeName();
            var radix = element.GetValue<IAtomic, Radix>(x => x.Radix);
            var value = element.GetValue<IAtomic, object>(x => x.Value);

            var atomic = (IAtomic) Logix.DataType.Instantiate(type);
            atomic.SetRadix(radix);
            atomic.SetValue(value);

            return atomic;
        }
    }
}