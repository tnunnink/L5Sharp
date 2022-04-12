using System;
using System.Xml.Linq;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class DecoratedDataSerializer : L5XSerializer<IDataType>
    {
        private readonly L5XContent? _document;

        private DataValueSerializer DataValueSerializer => _document is not null
            ? _document.Serializers.Get<DataValueSerializer>()
            : new DataValueSerializer();

        private ArraySerializer ArraySerializer => _document is not null
            ? _document.Serializers.Get<ArraySerializer>()
            : new ArraySerializer(_document);

        private StructureSerializer StructureSerializer => _document is not null
            ? _document.Serializers.Get<StructureSerializer>()
            : new StructureSerializer(_document);

        public DecoratedDataSerializer(L5XContent? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            return component switch
            {
                IAtomicType atomicType => DataValueSerializer.Serialize(atomicType),
                IArrayType<IDataType> arrayType => ArraySerializer.Serialize(arrayType),
                IComplexType complexType => StructureSerializer.Serialize(complexType),
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
                L5XElement.DataValue => DataValueSerializer.Deserialize(element),
                L5XElement.Array => ArraySerializer.Deserialize(element),
                L5XElement.Structure => StructureSerializer.Deserialize(element),
                _ => throw new ArgumentException($"Element '{name}' not valid for the serializer {GetType()}.")
            };
        }
    }
}