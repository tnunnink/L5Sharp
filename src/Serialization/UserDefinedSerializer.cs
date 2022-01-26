using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class UserDefinedSerializer : IXSerializer<IUserDefined>
    {
        private readonly LogixContext _context;
        private static readonly XName ElementName = LogixNames.DataType;

        public UserDefinedSerializer(LogixContext context)
        {
            _context = context;
        }

        public XElement Serialize(IUserDefined component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.Family);
            element.AddAttribute(component, c => c.Class);

            var members = new XElement(nameof(component.Members));
            members.Add(component.Members.Select(m => _context.Serializer.Serialize(m)));
            element.Add(members);

            return element;
        }

        public IUserDefined Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var description = element.GetComponentDescription();
            var members = element.Descendants(LogixNames.Member).Where(e => !bool.Parse(e.Attribute("Hidden")?.Value))
                .Select(e => _context.Serializer.Deserialize<IMember<IDataType>>(e));

            return new UserDefined(name, description, members);
        }
    }
}