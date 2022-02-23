using System;
using System.Xml.Linq;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class DecoratedDataSerializer : IL5XSerializer<IDataType>
    {
        public XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            switch (component)
            {
                case IAtomicType atomicType:
                    var dataValueSerializer = new DataValueSerializer();
                    return dataValueSerializer.Serialize(atomicType);
                case IArrayType<IDataType> arrayType:
                    var arraySerializer = new ArraySerializer();
                    return arraySerializer.Serialize(arrayType);
                case IComplexType complexType:
                    var structureSerializer = new StructureSerializer();
                    return structureSerializer.Serialize(complexType);
                default:
                    throw new ArgumentException(
                        $"Data type {component.GetType()} is valid for the serializer {GetType()}");
            }
        }

        public IDataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var name = Enum.Parse<L5XElement>(element.Name.ToString());

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            // Only following element names are valid for 
            switch (name)
            {
                case L5XElement.DataValue:
                    var dataValueSerializer = new DataValueSerializer();
                    return dataValueSerializer.Deserialize(element);
                case L5XElement.Array:
                    var arraySerializer = new ArraySerializer();
                    return arraySerializer.Deserialize(element);
                case L5XElement.Structure:
                    var structureSerializer = new StructureSerializer();
                    return structureSerializer.Deserialize(element);
                default:
                    throw new ArgumentException(
                        $"Element '{name}' not valid for the serializer {GetType()}.");
            }
        }
    }
}