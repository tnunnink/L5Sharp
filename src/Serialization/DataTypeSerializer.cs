using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="DataType"/> logix components.
    /// </summary>
    public class DataTypeSerializer : ILogixSerializer<DataType>
    {
        private readonly DataTypeMemberSerializer _dataTypeMemberSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(DataType obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.DataType);
            element.Add(new XAttribute(L5XName.Name, obj.Name));
            element.Add(new XAttribute(L5XName.Family, obj.Family.Value));
            element.Add(new XAttribute(L5XName.Class, obj.Class.Value));

            if (!obj.Description.IsEmpty())
                element.Add(new XElement(L5XName.Description, new XCData(obj.Description)));

            var members = new XElement(nameof(L5XName.Members));
            members.Add(obj.Members.Select(m => _dataTypeMemberSerializer.Serialize(m)));
            element.Add(members);

            return element;
        }

        /// <inheritdoc />
        public DataType Deserialize(XElement element)
        {
            Check.NotNull(element);

            var name = element.Attribute(L5XName.Name)?.Value ?? string.Empty;
            var family = element.Attribute(L5XName.Family)?.Value.Parse<DataTypeFamily>() ?? DataTypeFamily.None;
            var description = element.Element(L5XName.Description)?.Value ?? string.Empty;
            var members = element.Descendants(L5XName.Member).Select(m => _dataTypeMemberSerializer.Deserialize(m));

            return new DataType
            {
                Name = name,
                Family = family,
                Members = members.ToList(),
                Description = description
            };
        }
    }
}