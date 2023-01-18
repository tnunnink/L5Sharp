using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    public class ArrayElementSerializer : ILogixSerializer<Member>
    {
        private readonly StructureSerializer _structureSerializer = new();
        
        /// <inheritdoc />
        public XElement Serialize(Member obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Element);
            element.Add(new XAttribute(L5XName.Index, obj.Name));
            
            switch (obj.DataType)
            {
                case AtomicType atomicType:
                    element.Add(new XAttribute(L5XName.Value, atomicType.ToString()));
                    break;
                case StructureType structureType:
                    element.Add(_structureSerializer.Serialize(structureType));
                    break;
            }
            
            return element;
        }

        /// <inheritdoc />
        public Member Deserialize(XElement element)
        {
            Check.NotNull(element);
            
            var index = element.Attribute(L5XName.Index)?.Value
                        ?? throw new ArgumentException($"Element must have {L5XName.Index} attribute.");;
            
            if (element.Attribute(L5XName.Value) is not null)
            {
                var dataType = element.Parent!.Attribute(L5XName.DataType)!.Value;
                var radix = element.Parent!.Attribute(L5XName.Radix)?.Value.Parse<Radix>() ?? Radix.Decimal;
                var value = element.Attribute(L5XName.Value)!.Value;
                var atomic = Atomic.Parse(dataType, value, radix);

                return new Member(index, atomic);
            }

            var structure = _structureSerializer.Deserialize(element.Element(L5XName.Structure)!);
            return new Member(index, structure);
        }
    }
}