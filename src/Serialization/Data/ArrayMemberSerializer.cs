using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="ArrayType{TLogixType}"/> or array member elements.
    /// </summary>
    public class ArrayMemberSerializer : ILogixSerializer<Member>
    {
        /// <inheritdoc />
        public XElement Serialize(Member obj)
        {
            Check.NotNull(obj);
            
            var arrayType = (ArrayType<ILogixType>)obj.DataType;
            
            var element = new XElement(L5XName.ArrayMember);
            element.AddValue(obj.Name, L5XName.Name);
            element.AddValue(arrayType.First().Name, L5XName.DataType);
            element.AddValue(arrayType.Dimensions, L5XName.Dimensions);

            if (arrayType.First() is AtomicType atomic && arrayType.First() is not BOOL)
                element.AddValue(atomic.Radix, L5XName.Radix);

            element.Add(arrayType.Elements.Select(e =>
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
        public Member Deserialize(XElement element)
        {
            Check.NotNull(element);

            var name = element.GetValue<string>(L5XName.Name);
            var dataType = element.GetValue<string>(L5XName.DataType);
            var dimensions = element.GetValue<Dimensions>(L5XName.Dimensions);
            var radix = element.TryGetValue<Radix>(L5XName.Radix);

            var elements = element.Elements().Select(e =>
            {
                if (e.Attribute(L5XName.Value) is not null)
                    return Atomic.Parse(dataType, e.GetValue<string>(L5XName.Value), radix);

                return e.Element(L5XName.Structure) is not null
                    ? TagDataSerializer.Structure.Deserialize(e.Element(L5XName.Structure)!)
                    : Logix.Null;
            });

            var array = new ArrayType<ILogixType>(dimensions, elements.ToList());
            return new Member(name, array);
        }
    }
}