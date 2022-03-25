using System;
using System.Xml.Linq;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class DecoratedDataSerializer : L5XSerializer<IDataType>
    {
        private readonly DataValueSerializer _dataValueSerializer;
        private readonly StructureSerializer _structureSerializer;
        private readonly ArraySerializer _arraySerializer;

        public DecoratedDataSerializer()
        {
            _dataValueSerializer = new DataValueSerializer();
            _structureSerializer = new StructureSerializer();
            _arraySerializer = new ArraySerializer(_structureSerializer);
        }
        
        public override XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            return component switch
            {
                IAtomicType atomicType => _dataValueSerializer.Serialize(atomicType),
                IArrayType<IDataType> arrayType => _arraySerializer.Serialize(arrayType),
                IComplexType complexType => _structureSerializer.Serialize(complexType),
                _ => throw new ArgumentException(
                    $"Data type {component.GetType()} is valid for the serializer {GetType()}")
            };
        }

        public override IDataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var name = Enum.Parse<L5XElement>(element.Name.ToString());
            
            // Only following root element names are valid for decorated data serializer 
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return name switch
            {
                L5XElement.DataValue => _dataValueSerializer.Deserialize(element),
                L5XElement.Array => _arraySerializer.Deserialize(element),
                L5XElement.Structure => _structureSerializer.Deserialize(element),
                _ => throw new ArgumentException($"Element '{name}' not valid for the serializer {GetType()}.")
            };
        }
    }
}