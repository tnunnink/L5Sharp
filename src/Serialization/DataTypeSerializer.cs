using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class DataTypeSerializer : IXSerializer<IUserDefined>
    {
        private static readonly XName ElementName = LogixNames.DataType;

        public XElement Serialize(IUserDefined component)
        {
            if (component == null) 
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddValue(component,c => c.Name);
            element.AddValue(component,c => c.Family);
            element.AddValue(component,c => c.Class);

            if (!component.Description.IsEmpty())
                element.AddValue(component, x => x.Description, true);

            var members = new XElement(nameof(component.Members));
            members.Add(component.Members.Select(m => m.Serialize()));
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
            var members = element.Descendants(LogixNames.Member).Select(e => e.Deserialize<IMember<IDataType>>());

            return UserDefined.Create(name, description, members);
        }
    }
}