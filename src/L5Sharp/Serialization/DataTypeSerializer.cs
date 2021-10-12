using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class DataTypeSerializer : IComponentSerializer<IDataType>
    {
        public XElement Serialize(IDataType component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            var element = new XElement(L5XNames.Components.DataType);
            element.Add(component.ToXAttribute(c => c.Name));
            element.Add(component.ToXAttribute(c => c.Family));
            element.Add(component.ToXAttribute(c => c.Class));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToXCDataElement(c => c.Description));

            var serializer = new MemberSerializer();
            
            element.Add(new XElement(nameof(component.Members),
                component.Members.Select(m => serializer.Serialize(m))));

            return element;
        }

        public IDataType Deserialize(XElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            var members = element.GetAll<IMember>().Select(x => x.Deserialize<IMember>());

            return new DataType(element.GetName(), members, element.GetDescription());
        }
    }
}