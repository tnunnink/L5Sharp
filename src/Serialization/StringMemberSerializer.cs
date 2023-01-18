using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    public class StringMemberSerializer : ILogixSerializer<Member>
    {
        /// <inheritdoc />
        public XElement Serialize(Member obj)
        {
            var stringType = (StringType)obj.DataType;
            
            var structureMember = new XElement(L5XName.StructureMember);
            structureMember.Add(new XAttribute(L5XName.Name, obj.Name));
            structureMember.Add(new XAttribute(L5XName.DataType, stringType.Name));

            var len = new XElement(L5XName.DataValueMember);
            len.Add(new XAttribute(L5XName.Name, nameof(stringType.LEN)));
            len.Add(new XAttribute(L5XName.DataType, stringType.LEN.Name));
            len.Add(new XAttribute(L5XName.Radix, stringType.LEN.Radix));
            len.Add(new XAttribute(L5XName.Value, stringType.LEN.ToString()));
            structureMember.Add(len);

            var data = new XElement(L5XName.DataValueMember);
            data.Add(new XAttribute(L5XName.Name, nameof(stringType.DATA)));
            data.Add(new XAttribute(L5XName.DataType, stringType.Name));
            data.Add(new XAttribute(L5XName.Radix, Radix.Ascii.Value));
            data.Add(new XCData(stringType.DATA.AsString()));
            structureMember.Add(data);

            return structureMember;
        }

        /// <inheritdoc />
        public Member Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}