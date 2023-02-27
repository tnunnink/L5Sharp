using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="Member"/> whose type is a <see cref="StringType"/>
    /// </summary>
    public class StringMemberSerializer : ILogixSerializer<Member>
    {
        /// <inheritdoc />
        public XElement Serialize(Member obj)
        {
            Check.NotNull(obj);

            var stringType = (StringType)obj.DataType;

            var element = new XElement(L5XName.StructureMember);
            element.Add(new XAttribute(L5XName.Name, obj.Name));
            element.Add(new XAttribute(L5XName.DataType, stringType.Name));

            var len = new XElement(L5XName.DataValueMember);
            len.Add(new XAttribute(L5XName.Name, nameof(stringType.LEN)));
            len.Add(new XAttribute(L5XName.DataType, stringType.LEN.Name));
            len.Add(new XAttribute(L5XName.Radix, Radix.Decimal.Value));
            len.Add(new XAttribute(L5XName.Value, stringType.LEN.ToString()));
            element.Add(len);

            var data = new XElement(L5XName.DataValueMember);
            data.Add(new XAttribute(L5XName.Name, nameof(stringType.DATA)));
            data.Add(new XAttribute(L5XName.DataType, stringType.Name));
            data.Add(new XAttribute(L5XName.Radix, Radix.Ascii.Value));
            data.Add(new XCData(stringType.DATA.AsString()));
            element.Add(data);

            return element;
        }

        /// <inheritdoc />
        public Member Deserialize(XElement element)
        {
            var name = element.GetValue<string>(L5XName.Name);
            var dataType = element.GetValue<string>(L5XName.DataType);
            var value = element.Elements(L5XName.DataValueMember).FirstOrDefault(e =>
                    e.Attribute(L5XName.DataType)?.Value == dataType && e.Attribute(L5XName.Name)?.Value == "DATA")
                ?.Value ?? string.Empty;

            var stringType = new StringType(dataType, value);
            return new Member(name, stringType);
        }
    }
}