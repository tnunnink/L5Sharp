using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class DataTypeSerializer : L5XSerializer<IComplexType>
    {
        private readonly L5XContent? _document;
        private static readonly XName ElementName = L5XName.DataType;

        private MemberSerializer MemberSerializer => _document is not null
            ? _document.Serializers.Get<MemberSerializer>()
            : new MemberSerializer(_document);
        
        public DataTypeSerializer(L5XContent? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IComplexType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XName.Family, component.Family.Value));
            element.Add(new XAttribute(L5XName.Class, component.Class));

            var members = new XElement(nameof(component.Members));
            members.Add(component.Members.Select(m => MemberSerializer.Serialize(m)));
            element.Add(members);

            return element;
        }

        public override IComplexType Deserialize(XElement element)
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
                .Select(e => MemberSerializer.Deserialize(e))
                .ToList();

            return family == DataTypeFamily.String
                ? new StringDefined(name, members.First(m => m.Name == "DATA").Dimensions.X, description)
                : new UserDefined(name, description, members);
        }
    }
}