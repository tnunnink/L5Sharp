using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class DataTypeSerializer : L5XSerializer<DataType>
    {
        private static readonly XName ElementName = L5XName.DataType;
        private readonly MemberSerializer _memberSerializer;

        public DataTypeSerializer()
        {
            _memberSerializer = new MemberSerializer();
        }

        public override XElement Serialize(DataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XName.Family, component.Family.Value));
            element.Add(new XAttribute(L5XName.Class, component.Class));

            
            var members = new XElement(nameof(component.Members));
            members.Add(component.Members.Select(m => _memberSerializer.Serialize(m)));
            element.Add(members);

            return element;
        }

        public override DataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var family = element.Attribute(L5XName.Family)?.Value.Parse<DataTypeFamily>();
            var members = element.Descendants(L5XName.Member)
                .Where(e => !bool.Parse(e.Attribute(L5XName.Hidden)?.Value!))
                .Select(e => _memberSerializer.Deserialize(e))
                .ToList();

            return new DataType
            {
                Name = name,
                Description = description,
                Family = family ?? DataTypeFamily.None,
                Members = members
            };
        }
    }
}