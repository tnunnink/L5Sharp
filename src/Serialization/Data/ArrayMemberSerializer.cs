using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class ArrayMemberSerializer : IL5XSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.ArrayMember.ToXName();

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (component.DataType is not IArrayType<IDataType> arrayType)
                throw new ArgumentException("Provided component is not an array type.");

            var element = new XElement(ElementName);

            element.AddAttribute(component, m => m.Name);
            element.AddAttribute(component, m => m.DataType.Name, nameOverride: nameof(component.DataType));
            element.AddAttribute(component, m => m.Dimensions);
            element.AddAttribute(component, m => m.Radix, m => m.Radix != Radix.Null);

            var serializer = new ArrayElementSerializer();
            var elements = arrayType.Select(m => serializer.Serialize(m));
            element.Add(elements);

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var dimensions = element.GetAttribute<IMember<IDataType>, Dimensions>(m => m.Dimensions);
            var radix = element.GetAttribute<IMember<IDataType>, Radix>(m => m.Radix);

            var serializer = new ArrayElementSerializer();
            var members = element.Elements().Select(e => serializer.Deserialize(e));

            var arrayType = new ArrayType<IDataType>(dimensions!, members.Select(m => m.DataType).ToList(), radix);
            
            return new Member<IArrayType<IDataType>>(name, arrayType, radix);
        }
    }
}