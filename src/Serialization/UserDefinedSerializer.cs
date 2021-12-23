using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class UserDefinedSerializer : IXSerializer<IUserDefined>
    {
        private const string ElementName = LogixNames.DataType;

        public XElement Serialize(IUserDefined component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(component.ToAttribute(c => c.Name));
            element.Add(component.ToAttribute(c => c.Family));
            element.Add(component.ToAttribute(c => c.Class));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToElement(c => c.Description));

            var members = new XElement(nameof(component.Members));
            var memberSerializer = new UserDefinedMemberSerializer();
            members.Add(component.Members.Select(m => memberSerializer.Serialize(m)));
            
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
            var description = element.GetValue<IDataType, string>(x => x.Description);

            var memberSerializer = new UserDefinedMemberSerializer();
            var members = element.Descendants(LogixNames.Member).Select(e => memberSerializer.Deserialize(e));

            return new UserDefined(name, description, members);
        }
    }
}