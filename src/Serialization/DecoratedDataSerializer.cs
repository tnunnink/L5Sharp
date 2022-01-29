using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// Serializer that will pass through data type to other serializers based on it's type.
    /// </summary>
    internal class DecoratedDataSerializer : IXSerializer<IDataType>
    {
        private static readonly XName ElementName = LogixNames.Data;

        public XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            switch (component)
            {
                case IAtomicType atomicType:
                    var dataValueSerializer = new DataValueSerializer();
                    return (dataValueSerializer.Serialize(atomicType));
                case IArrayType<IDataType> arrayType:
                    var arraySerializer = new ArraySerializer();
                    return arraySerializer.Serialize(arrayType);
                case IComplexType complexType:
                    var structureSerializer = new StructureSerializer();
                    return structureSerializer.Serialize(complexType);
                default:
                    throw new NotSupportedException(
                        $"The provided type is not supported for the serializer of type {GetType()}");
            }
        }

        public IDataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name == LogixNames.DataValue)
            {
                var atomic = (IAtomicType)DataType.Create(element.GetDataTypeName());
                var value = element.Attribute(LogixNames.Value)?.Value;
                atomic.SetValue(value!);
                return atomic;
            }

            if (element.Name == LogixNames.Array)
            {
                var arraySerializer = new ArraySerializer();
                return arraySerializer.Deserialize(element);
            }

            if (element.Name != LogixNames.Structure)
                throw new NotSupportedException(
                    $"The provided element {ElementName} data member with name {element.Name} is not supported for" +
                    $" the serializer of type {GetType()}");

            var structureSerializer = new StructureSerializer();
            return structureSerializer.Deserialize(element);
        }
    }
}