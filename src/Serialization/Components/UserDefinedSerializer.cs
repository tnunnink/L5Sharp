using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class UserDefinedSerializer : IL5XSerializer<IUserDefined>
    {
        private static readonly XName ElementName = L5XElement.DataType.ToString();
        private readonly IL5XSerializer<IMember<IDataType>> _memberSerializer;

        public UserDefinedSerializer(L5XContext? context = null)
        {
            _memberSerializer = context is not null
                ? context.Serializers.GetSerializer<IMember<IDataType>>()
                : new MemberSerializer(context);
        }
        
        public XElement Serialize(IUserDefined component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.Add(new XAttribute(L5XAttribute.Name.ToString(), component.Name));
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.Family);
            element.AddAttribute(component, c => c.Class);

            var members = new XElement(nameof(component.Members));
            members.Add(component.Members.Select(m => _memberSerializer.Serialize(m)));
            element.Add(members);

            return element;
        }

        public IUserDefined Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var members = element.Descendants(L5XElement.Member.ToString())
                .Where(e => !bool.Parse(e.Attribute(L5XAttribute.Hidden.ToString())?.Value!))
                .Select(e => _memberSerializer.Deserialize(e));

            return new UserDefined(name, description, members);
        }
    }
}