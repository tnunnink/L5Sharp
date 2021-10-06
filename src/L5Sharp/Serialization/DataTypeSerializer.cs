using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class DataTypeSerializer : IComponentSerializer<IDataType>
    {
        public XElement Serialize(IDataType component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            var element = new XElement(nameof(DataType));
            element.Add(new XAttribute(nameof(component.Name), component.Name));
            element.Add(new XAttribute(nameof(component.Family), component.Family));
            element.Add(new XAttribute(nameof(component.Class), component.Class));

            if (component is DataType type)
            {
                if (!string.IsNullOrEmpty(type.Description))
                    element.Add(new XElement(nameof(type.Description), new XCData(type.Description)));
            }

            var serializer = new MemberSerializer();
            element.Add(new XElement(nameof(component.Members),
                component.Members.Select(m => serializer.Serialize((Member)m))));

            return element;
        }

        public IDataType Deserialize(XElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            var members = element.GetAll<Member>().Select(x => x.Deserialize<Member>());

            return new DataType(element.GetName(), element.GetDescription(), members);
        }
    }
}