using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Serialization
{
    public class MemberSerializer : ILogixSerializer<Member>
    {
        private readonly DataValueMemberSerializer _dataValueMemberSerializer = new();
        private readonly StructureMemberSerializer _structureMemberSerializer = new();
        private readonly StringMemberSerializer _stringMemberSerializer = new();
        private readonly ArrayMemberSerializer _arrayMemberSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(Member obj)
        {
            return obj.DataType switch
            {
                AtomicType => _dataValueMemberSerializer.Serialize(obj),
                ArrayType<ILogixType> => _arrayMemberSerializer.Serialize(obj),
                StringType => _stringMemberSerializer.Serialize(obj),
                StructureType => _structureMemberSerializer.Serialize(obj),
                _ => throw new InvalidOperationException()
            };
        }

        /// <inheritdoc />
        public Member Deserialize(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}