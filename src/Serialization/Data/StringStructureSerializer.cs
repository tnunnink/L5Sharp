using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="StringType"/> structures. This serializer is for
    /// string types that are part of arrays specifically. 
    /// </summary>
    public class StringStructureSerializer : ILogixSerializer<StringType>
    {
        /// <inheritdoc />
        public XElement Serialize(StringType obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Structure);
            element.Add(new XAttribute(L5XName.DataType, obj.Name));

            var len = new XElement(L5XName.DataValueMember);
            len.AddValue(nameof(obj.LEN), L5XName.Name);
            len.AddValue(obj.LEN.Name, L5XName.DataType);
            len.AddValue(obj.LEN.Radix, L5XName.Radix);
            len.AddValue(obj.LEN, L5XName.Value);
            element.Add(len);

            var data = new XElement(L5XName.DataValueMember);
            data.AddValue(nameof(obj.DATA), L5XName.Name);
            data.AddValue(obj.Name, L5XName.DataType);
            data.AddValue(Radix.Ascii.Value, L5XName.Radix);
            data.Add(new XCData(obj.DATA.AsString()));
            element.Add(data);

            return element;
        }

        /// <inheritdoc />
        public StringType Deserialize(XElement element)
        {
            Check.NotNull(element);
            
            var name = element.GetValue<string>(L5XName.DataType);
            var value = element.Elements(L5XName.DataValueMember).FirstOrDefault(e =>
                    e.Attribute(L5XName.DataType)?.Value == name && e.Attribute(L5XName.Name)?.Value == "DATA")
                ?.Value ?? string.Empty;

            return new StringType(name, value);
        }
    }
}