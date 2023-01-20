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

            var name = element.Attribute(L5XName.Name)?.Value ?? string.Empty;
            var dataType = element.Attribute(L5XName.Dimension)?.Value ?? string.Empty;
            var dimensions = element.Attribute(L5XName.Dimension)?.Value.Parse<Dimensions>() ?? Dimensions.Empty;
            var radix = element.Attribute(L5XName.Radix)?.Value.Parse<Radix>() ?? Radix.Null;
            var access = element.Attribute(L5XName.ExternalAccess)?.Value.Parse<ExternalAccess>() ??
                         ExternalAccess.ReadWrite;
            var description = element.Element(L5XName.Description)?.Value ?? string.Empty;

            return new DataTypeMember
            {
                Name = name,
                DataType = dataType,
                Dimensions = dimensions,
                Radix = radix,
                ExternalAccess = access,
                Description = description
            };
        }
    }
}