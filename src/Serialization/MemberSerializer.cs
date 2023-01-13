using System;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class MemberSerializer : L5XSerializer<DataTypeMember>
    {
        private static readonly XName ElementName = L5XName.Member;

        public override XElement Serialize(DataTypeMember component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XName.DataType, component.DataType));
            element.Add(new XAttribute(L5XName.Dimension, component.Dimensions));
            element.Add(new XAttribute(L5XName.Radix, component.Radix));
            element.Add(new XAttribute(L5XName.Hidden, false));
            element.Add(new XAttribute(L5XName.ExternalAccess, component.ExternalAccess));

            return element;
        }

        public override DataTypeMember Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var dataType = element.DataTypeName();
            var dimensions = element.Attribute(L5XName.Dimension)?.Value.Parse<Dimensions>();
            var radix = element.Attribute(L5XName.Radix)?.Value.Parse<Radix>();
            var access = element.Attribute(L5XName.ExternalAccess)?.Value.Parse<ExternalAccess>();
            
            return new DataTypeMember
            {
                Name = name,
                Description = description,
                DataType = dataType,
                Dimensions = dimensions ?? Dimensions.Empty,
                Radix = radix ?? Radix.Null,
                ExternalAccess = access ?? ExternalAccess.ReadWrite
            };
        }
    }
}