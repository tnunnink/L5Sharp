using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="ILogixType"/> to/from decorated data elements.
    /// </summary>
    public class DecoratedDataSerializer : ILogixSerializer<ILogixType>
    {
        /// <inheritdoc />
        public XElement Serialize(ILogixType obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Data);
            element.AddValue(DataFormat.Decorated, L5XName.Format);
            
            var data = obj switch
            {
                AtomicType atomicType => TagDataSerializer.DataValue.Serialize(atomicType),
                ArrayType<ILogixType> arrayType => TagDataSerializer.Array.Serialize(arrayType),
                StructureType structureType => TagDataSerializer.Structure.Serialize(structureType),
                _ => throw new ArgumentException(
                    $"Logix data type {obj.GetType()} is not valid for the serializer {GetType()}")
            };
            
            element.Add(data);
            return element;
        }

        /// <inheritdoc />
        public ILogixType Deserialize(XElement element)
        {
            Check.NotNull(element);

            var data = element.Elements().First();
            var name = data.Name.ToString();
            
            return name switch
            {
                L5XName.DataValue => TagDataSerializer.DataValue.Deserialize(data),
                L5XName.Array => TagDataSerializer.Array.Deserialize(data),
                L5XName.Structure => TagDataSerializer.Structure.Deserialize(data),
                _ => throw new ArgumentException($"Element '{name}' not valid for the serializer {GetType()}.")
            };
        }
    }
}