using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    public class ArrayMemberSerializer : ILogixSerializer<Member>
    {
        /// <inheritdoc />
        public XElement Serialize(Member obj)
        {
            var arrayType = (ArrayType<ILogixType>)obj.DataType;
            
            var arrayMember = new XElement(L5XName.ArrayMember);
            arrayMember.Add(new XAttribute(L5XName.Name, obj.Name));
            arrayMember.Add(new XAttribute(L5XName.DataType, arrayType[0].Name));
            arrayMember.Add(new XAttribute(L5XName.Dimensions, arrayType.Dimensions));

            if (arrayType[0] is BOOL)
                arrayMember.Add(new XAttribute(L5XName.Radix, obj.Radix));
            
            /*var elements = arrayType.Members().Select(m => _elementSerializer.Serialize(m));
            arrayMember.Add(elements);*/

            return arrayMember;
        }

        /// <inheritdoc />
        public Member Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}