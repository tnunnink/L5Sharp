using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class UserDefinedSerializer : IXSerializer<IUserDefined>
    {
        private readonly LogixContext _context;

        public UserDefinedSerializer(LogixContext context)
        {
            _context = context;
        }

        public XElement Serialize(IUserDefined component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            var element = new XElement(LogixNames.DataType);
            element.Add(component.ToAttribute(c => c.Name));
            element.Add(component.ToAttribute(c => c.Family));
            element.Add(component.ToAttribute(c => c.Class));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToElement(c => c.Description));

            var members = new XElement(nameof(component.Members));
            members.Add(component.Members.Select(m => _context.Serializer.Serialize(m)));
            element.Add(members);

            return element;
        }

        public IUserDefined Deserialize(XElement element)
        {
            if (element == null) return null;

            var name = element.GetAttribute<IUserDefined>(x => x.Name);
            var description = element.GetDescription();
            
            var members = element.Descendants(LogixNames.Member).Select(e =>
                _context.Serializer.Deserialize<IMember<IDataType>>(e));

            return new DataType(name, description, members);
        }
    }
}