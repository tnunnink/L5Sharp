using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class ArraySerializer : IL5XSerializer<IArrayType<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.Array.ToXName();

        public XElement Serialize(IArrayType<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name, nameOverride: L5XElement.DataType.ToString());
            element.AddAttribute(component, c => c.Dimensions);
            element.AddAttribute(component, c => c.First().Radix, t => t.First().Radix != Radix.Null);
            
            var serializer = new ArrayElementSerializer();
            var elements = component.Select(e => serializer.Serialize(e));
            element.Add(elements);

            return element;
        }

        public IArrayType<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var dimensions = element.GetAttribute<IArrayType<IDataType>, Dimensions>(t => t.Dimensions);
            
            var serializer = new ArrayElementSerializer();
            var members = element.Elements().Select(e => serializer.Deserialize(e));
            var radix = element.GetAttribute<IMember<IDataType>, Radix>(e => e.Radix);

            return new ArrayType<IDataType>(dimensions!, members.Select(m => m.DataType).ToList(), radix);
        }
    }
}