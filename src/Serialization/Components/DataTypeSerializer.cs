using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class DataTypeSerializer : IL5XSerializer<IComplexType>
    {
        private static readonly XName ElementName = L5XElement.DataType.ToString();
        private readonly IL5XSerializer<IMember<IDataType>> _memberSerializer;

        public DataTypeSerializer()
        {
            _memberSerializer = new MemberSerializer();
        }

        public DataTypeSerializer(L5XContext context)
        {
            _memberSerializer = new MemberSerializer(context);
        }
        
        public XElement Serialize(IComplexType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XAttribute.Family.ToString(), component.Family));
            element.Add(new XAttribute(L5XAttribute.Class.ToString(), component.Class));

            var members = new XElement(nameof(component.Members));
            members.Add(component.Members.Select(m => _memberSerializer.Serialize(m)));
            element.Add(members);

            return element;
        }

        public IComplexType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var family = element.Attribute(L5XAttribute.Family.ToString())?.Value.Parse<DataTypeFamily>();
            var members = element.Descendants(L5XElement.Member.ToString())
                .Where(e => !bool.Parse(e.Attribute(L5XAttribute.Hidden.ToString())?.Value!))
                .Select(e => _memberSerializer.Deserialize(e))
                .ToList();

            return family == DataTypeFamily.String 
                ? new StringDefined(name, members.First(m => m.Name == "DATA").Dimensions.X, description) 
                : new UserDefined(name, description, members);
        }
    }
}