using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
{
    public class MemberSerializer : ILogixSerializer<Member>
    {
        /// <inheritdoc />
        public XElement Serialize(Member obj)
        {
            Check.NotNull(obj);
            
            return obj.DataType switch
            {
                AtomicType => TagDataSerializer.DataValueMember.Serialize(obj),
                ArrayType<ILogixType> => TagDataSerializer.ArrayMember.Serialize(obj),
                StringType => TagDataSerializer.StringMember.Serialize(obj),
                StructureType => TagDataSerializer.StructureMember.Serialize(obj),
                _ => throw new InvalidOperationException()
            };
        }

        /// <inheritdoc />
        public Member Deserialize(XElement element)
        {
            Check.NotNull(element);

            if (element.HasLogixStringStructure())
                return TagDataSerializer.StringMember.Deserialize(element);
            
            var name = element.Name.ToString();
            
            return name switch
            {
                L5XName.DataValueMember => TagDataSerializer.DataValueMember.Deserialize(element),
                L5XName.ArrayMember => TagDataSerializer.ArrayMember.Deserialize(element),
                L5XName.StructureMember => TagDataSerializer.StructureMember.Deserialize(element),
                _ => throw new InvalidOperationException()
            };
        }
    }
}