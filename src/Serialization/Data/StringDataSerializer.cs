using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="StringType"/> or string formatted data elements.
    /// </summary>
    public class StringDataSerializer : ILogixSerializer<ILogixType>
    {
        /// <inheritdoc />
        public XElement Serialize(ILogixType obj)
        {
            Check.NotNull(obj);
            
            var stringType = (StringType)obj;
            
            var element = new XElement(L5XName.Data);
            element.Add(new XAttribute(L5XName.Format, DataFormat.String));
            element.Add(new XAttribute(L5XName.Length, stringType.LEN.ToString()));
            element.Add(new XCData(stringType.DATA.AsString()));
            
            return element;
        }

        /// <inheritdoc />
        public ILogixType Deserialize(XElement element)
        {
            Check.NotNull(element);

            var name = element.Ancestors(L5XName.Tag).FirstOrDefault()?.Attribute(L5XName.DataType)?.Value
                ?? string.Empty;

            return new StringType(name, element.Value);
        }
    }
}