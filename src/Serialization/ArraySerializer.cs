using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="ArrayType{TLogixType}"/> or array data elements.
    /// </summary>
    public class ArraySerializer : ILogixSerializer<ArrayType<ILogixType>>
    {
        private readonly StructureSerializer _structureSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(ArrayType<ILogixType> obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Array);
            element.AddValue(obj.Name, L5XName.DataType);
            element.AddValue(obj.Dimensions, L5XName.Dimensions);

            if (obj.First() is AtomicType atomic)
                element.AddValue(atomic.Radix, L5XName.Radix);

            element.Add(obj.Elements.Select(e =>
            {
                var index = new XElement(L5XName.Element);
                index.AddValue(e.Name, L5XName.Index);

                switch (e.DataType)
                {
                    case AtomicType atomicType:
                        index.AddValue(atomicType, L5XName.Value);
                        break;
                    case StructureType structureType:
                        index.Add(_structureSerializer.Serialize(structureType));
                        break;
                }

                return index;
            }));

            return element;
        }

        /// <inheritdoc />
        public ArrayType<ILogixType> Deserialize(XElement element)
        {
            Check.NotNull(element);

            var dataType = element.GetValue<string>(L5XName.DataType);
            var dimensions = element.GetValue<Dimensions>(L5XName.Dimensions);
            var radix = element.ValueOrDefault<Radix>(L5XName.Radix);

            var elements = element.Elements().Select(e =>
            {
                if (e.Attribute(L5XName.Value) is not null)
                    return Atomic.Parse(dataType, e.GetValue<string>(L5XName.Value), radix);

                return e.Element(L5XName.Structure) is not null
                    ? _structureSerializer.Deserialize(e.Element(L5XName.Structure)!)
                    : LogixType.Null;
            });

            return new ArrayType<ILogixType>(dimensions, elements.ToList());
        }
    }
}