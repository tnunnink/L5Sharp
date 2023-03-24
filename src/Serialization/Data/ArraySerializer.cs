using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="ArrayType{TLogixType}"/> or array data elements.
    /// </summary>
    public class ArraySerializer : ILogixSerializer<ILogixArray<ILogixType>>
    {
        /// <inheritdoc />
        public XElement Serialize(ILogixArray<ILogixType> obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Array);
            element.AddValue(obj.Name, L5XName.DataType);
            element.AddValue(obj.Dimensions, L5XName.Dimensions);

            if (obj.FirstOrDefault() is AtomicType atomic)
                element.AddValue(atomic.Radix, L5XName.Radix);

            element.Add(obj.Members.Select(e =>
            {
                var index = new XElement(L5XName.Element);
                index.AddValue(e.Name, L5XName.Index);

                switch (e.DataType)
                {
                    case AtomicType atomicType:
                        index.AddValue(atomicType, L5XName.Value);
                        break;
                    case StructureType structureType:
                        index.Add(TagDataSerializer.Structure.Serialize(structureType));
                        break;
                }

                return index;
            }));

            return element;
        }

        /// <inheritdoc />
        public ILogixArray<ILogixType> Deserialize(XElement element)
        {
            Check.NotNull(element);

            var dataType = element.GetValue<string>(L5XName.DataType);
            var dimensions = element.GetValue<Dimensions>(L5XName.Dimensions);
            var radix = element.TryGetValue<Radix>(L5XName.Radix);

            var elements = element.Elements().Select(e =>
            {
                if (e.Attribute(L5XName.Value) is not null)
                {
                    var value = e.GetValue<string>(L5XName.Value);
                    var format = Radix.Infer(value);
                    return Logix.Atomic(dataType, value, !format.Equals(radix) ? format : radix);
                }
                    

                return e.Element(L5XName.Structure) is not null
                    ? TagDataSerializer.Structure.Deserialize(e.Element(L5XName.Structure)!)
                    : Logix.Null;
            });

            return new ArrayType<ILogixType>(dimensions, elements.ToList());
        }
    }
}