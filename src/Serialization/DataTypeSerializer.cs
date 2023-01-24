using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
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
            members.Add(obj.Members.Select(m =>
            {
                var me = new XElement(L5XName.Member);

                me.Add(new XAttribute(L5XName.Name, m.Name));
                me.Add(new XAttribute(L5XName.DataType, m.DataType));
                me.Add(new XAttribute(L5XName.Dimension, m.Dimensions));
                me.Add(new XAttribute(L5XName.Radix, m.Radix));
                me.Add(new XAttribute(L5XName.Hidden, false));
                me.Add(new XAttribute(L5XName.ExternalAccess, m.ExternalAccess));

                if (!m.Description.IsEmpty())
                    me.Add(new XElement(L5XName.Description, new XCData(m.Description)));
                
                return me;
            }));
            element.Add(members);

            return element;
        }

        /// <inheritdoc />
        public DataType Deserialize(XElement element)
        {
            Check.NotNull(element);

            var name = element.ComponentName();
            var family = element.PropertyOrDefault<DataTypeFamily>(L5XName.Family) ?? DataTypeFamily.None;
            var description = element.Element(L5XName.Description)?.Value ?? string.Empty;
            var members = element.Descendants(L5XName.Member).Select(m => new DataTypeMember
            {
                Name = m.ComponentName(),
                DataType = m.PropertyOrDefault<string>(L5XName.DataType) ?? string.Empty,
                Dimensions = m.PropertyOrDefault<Dimensions>(L5XName.Dimension) ?? Dimensions.Empty,
                Radix = m.PropertyOrDefault<Radix>(L5XName.Radix) ?? Radix.Null,
                ExternalAccess = m.PropertyOrDefault<ExternalAccess>(L5XName.ExternalAccess) ?? ExternalAccess.ReadWrite,
                Description = m.Element(L5XName.Description)?.Value ?? string.Empty
            });

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