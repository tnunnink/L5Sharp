using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="ILogixType"/> to/from decorated data elements.
    /// </summary>
    public class DecoratedDataSerializer : ILogixSerializer<ILogixType>
    {
        private readonly DataValueSerializer _dataValueSerializer = new();
        private readonly ArraySerializer _arraySerializer = new();
        private readonly StructureSerializer _structureSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(ILogixType obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Data);
            element.AddValue(DataFormat.Decorated, L5XName.Format);
            
            var data = obj switch
            {
                AtomicType atomicType => _dataValueSerializer.Serialize(atomicType),
                ArrayType<ILogixType> arrayType => _arraySerializer.Serialize(arrayType),
                StructureType structureType => _structureSerializer.Serialize(structureType),
                _ => throw new ArgumentException(
                    $"Logix dat type {obj.GetType()} is not valid for the serializer {GetType()}")
            };
            
            element.Add(data);
            return element;
        }

        /// <inheritdoc />
        public ILogixType Deserialize(XElement element)
        {
            Check.NotNull(element);

            var name = element.Elements().First().Name.ToString();
            
            return name switch
            {
                L5XName.DataValue => _dataValueSerializer.Deserialize(element),
                L5XName.Array => _arraySerializer.Deserialize(element),
                L5XName.Structure => _structureSerializer.Deserialize(element),
                _ => throw new ArgumentException($"Element '{name}' not valid for the serializer {GetType()}.")
            };
        }
    }
}