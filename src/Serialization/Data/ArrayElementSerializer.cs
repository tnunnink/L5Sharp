using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization.Data
{
    internal class ArrayElementSerializer : IXSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = LogixNames.Element;
        
        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name, nameOverride: LogixNames.Index);

            switch (component.DataType)
            {
                case IAtomicType atomic:
                    element.Add(new XAttribute(LogixNames.Value, atomic.Format(component.Radix)));
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
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var index = element.Attribute(LogixNames.Index)?.Value!;
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
            var structure = element.Element(LogixNames.Structure);

            if (structure is not null)
            {
                var serializer = new StructureSerializer();
                return serializer.Deserialize(structure);
            }

            var typeName = element.Parent?.Attribute(LogixNames.DataType)?.Value!;
            var value = element.Attribute(LogixNames.Value)?.Value!;
            
            var atomicType = (IAtomicType)DataType.New(typeName);
            atomicType.SetValue(value);
            return atomicType;
        }
    }
}