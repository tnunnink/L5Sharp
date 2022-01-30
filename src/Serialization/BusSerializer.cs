using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class BusSerializer : IXSerializer<Bus>
    {
        private static readonly XName ElementName = LogixNames.Bus;

        public XElement Serialize(Bus component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            var element = new XElement(ElementName);
            
            element.AddAttribute(component, b => b.Size);
            element.AddAttribute(component, b => b.Baud);

            return element;
        }

        public Bus Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var size = element.GetAttribute<Bus, int?>(b => b.Size);
            var baud = element.GetAttribute<Bus, float?>(b => b.Baud);

            return new Bus(size, baud);
        }
    }
}