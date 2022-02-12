using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class ArraySerializer : IXSerializer<IArrayType<IDataType>>
    {
        private static readonly XName ElementName = LogixNames.Array;

        public XElement Serialize(IArrayType<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name, nameOverride: LogixNames.DataType);
            element.AddAttribute(component, c => c.Dimensions);
            //todo radix?

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
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var dimensions = element.GetAttribute<IArrayType<IDataType>, Dimensions>(t => t.Dimensions);
            
            var serializer = new ArrayElementSerializer();
            var members = element.Elements().Select(e => serializer.Deserialize(e));

            var radix = element.GetAttribute<IMember<IDataType>, Radix>(e => e.Radix);

            return new ArrayType<IDataType>(dimensions!, members.Select(m => m.DataType).ToList(), radix);
        }
    }
}