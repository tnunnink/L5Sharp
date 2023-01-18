using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="StringType"/> or string formatted data elements.
    /// </summary>
    public class StringSerializer : ILogixSerializer<StringType>
    {
        /// <inheritdoc />
        public XElement Serialize(StringType obj)
        {
            var data = new XElement(L5XName.Data);
            data.Add(new XAttribute(L5XName.Format, DataFormat.String));
            data.Add(new XAttribute(L5XName.Length, obj.LEN.ToString()));
            data.Add(new XCData(obj.DATA.AsString()));
            return data;
        }

        /// <inheritdoc />
        public StringType Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}