using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Extensions;

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
            
            element.AddAttribute(component,c => c.Name);
            element.AddElement(component,c => c.Description);
            element.AddAttribute(component,c => c.Family);
            element.AddAttribute(component,c => c.Class);

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
            var description = element.GetAttribute<IDataType, string>(x => x.Description);
            var members = element.Descendants(LogixNames.Member).Select(e => e.Deserialize<IMember<IDataType>>());

            return new UserDefined(name, description, members);
        }
    }
}