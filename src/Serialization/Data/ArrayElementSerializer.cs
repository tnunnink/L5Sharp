using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization.Data
{
    internal class ArrayElementSerializer : IL5XSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.Element.ToXName();
        
        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name, nameOverride: L5XAttribute.Index.ToString());

            switch (component.DataType)
            {
                case IAtomicType atomic:
                    element.Add(new XAttribute(L5XAttribute.Value.ToXName(), atomic.Format(component.Radix)));
                    break;
                case IComplexType complexType:
                {
                    var serializer = new StructureSerializer();
                    var structure = serializer.Serialize(complexType);
                    element.Add(structure);
                    break;
                }
            }

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var index = element.Attribute(L5XAttribute.Index.ToXName())?.Value!;
            var dataType = GetElementType(element);

            return new Member<IDataType>(index, dataType);
        }
        
        /// <summary>
        /// Gets the data type for the current element of the array.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> that represents the current array element.</param>
        /// <returns>A <see cref="IDataType"/> instance based on the current XML structure.</returns>
        /// <remarks>
        /// An array can either be a value type array, in which it has a value attribute and a parent Array element to
        /// determine an atomic type from, or it can be a complex structure array, in which it has a child structure element
        /// that can be parsed into a complex type using a structure serializer.
        /// </remarks>
        private static IDataType GetElementType(XElement element)
        {
            var structure = element.Element(L5XElement.Structure.ToXName());

            if (structure is not null)
            {
                var serializer = new StructureSerializer();
                return serializer.Deserialize(structure);
            }

            var dataType = (IAtomicType)DataType.Create(element.Parent?.GetDataTypeName()!);
            var value = element.Attribute(L5XAttribute.Value.ToXName())?.Value!;
            dataType.SetValue(value);
            return dataType;
        }
    }
}