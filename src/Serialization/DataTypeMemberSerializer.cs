using System;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="DataTypeMember"/> or member elements of the data type component.
    /// </summary>
    public class DataTypeMemberSerializer : ILogixSerializer<DataTypeMember>
    {
        /// <inheritdoc />
        public XElement Serialize(DataTypeMember obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));

            var element = new XElement(L5XName.Member);

            element.Add(new XAttribute(L5XName.Name, obj.Name));
            element.Add(new XAttribute(L5XName.DataType, obj.DataType));
            element.Add(new XAttribute(L5XName.Dimension, obj.Dimensions));
            element.Add(new XAttribute(L5XName.Radix, obj.Radix));
            element.Add(new XAttribute(L5XName.Hidden, false));
            element.Add(new XAttribute(L5XName.ExternalAccess, obj.ExternalAccess));

            if (!obj.Description.IsEmpty())
                element.Add(new XElement(L5XName.Description, new XCData(obj.Description)));

            return element;
        }

        /// <inheritdoc />
        public DataTypeMember Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new DataTypeMember
            {
                Name = element.ComponentName(),
                DataType = element.PropertyOrDefault<string>(L5XName.DataType) ?? string.Empty,
                Dimensions = element.PropertyOrDefault<Dimensions>(L5XName.Dimension) ?? Dimensions.Empty,
                Radix = element.PropertyOrDefault<Radix>(L5XName.Radix) ?? Radix.Null,
                ExternalAccess = element.PropertyOrDefault<ExternalAccess>(L5XName.ExternalAccess) ?? ExternalAccess.ReadWrite,
                Description = element.Element(L5XName.Description)?.Value ?? string.Empty
            };
        }
    }
}