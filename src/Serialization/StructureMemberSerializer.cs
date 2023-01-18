using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="Member"/> whose data type is a <see cref="StructureType"/> object.
    /// </summary>
    public class StructureMemberSerializer : ILogixSerializer<Member>
    {
        private readonly MemberSerializer _memberSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(Member obj)
        {
            var structureType = (StructureType)obj.DataType;
            
            var member = new XElement(L5XName.StructureMember);
            member.Add(new XAttribute(L5XName.Name, obj.Name));
            member.Add(new XAttribute(L5XName.DataType, structureType.Name));

            var members = structureType.Members().Select(m => _memberSerializer.Serialize(m));
            member.Add(members);

            return member;
        }

        /// <inheritdoc />
        public Member Deserialize(XElement element)
        {
            Check.NotNull(element);
            
            var name = element.Attribute(L5XName.Name)?.Value
                       ?? throw new ArgumentException($"Element must have {L5XName.Name} attribute.");
            var dataType = element.Attribute(L5XName.DataType)?.Value
                           ?? throw new ArgumentException($"Element must have {L5XName.DataType} attribute.");
            
            
            var members = element.Elements().Select(e => _memberSerializer.Deserialize(e));
            
            
            var structureType = new StructureType(dataType, members);

            return new Member(name, structureType);
        }
    }
}