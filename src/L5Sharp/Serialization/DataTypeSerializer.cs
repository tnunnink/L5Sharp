using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class DataTypeSerializer : IL5XSerializer<DataType>
    {
        public XElement Serialize(DataType component)
        {
            var element = new XElement(nameof(DataType));
            element.Add(new XAttribute(nameof(component.Name), component.Name));
            element.Add(new XAttribute(nameof(component.Family), component.Family));
            element.Add(new XAttribute(nameof(component.Class), component.Class));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(new XElement(nameof(component.Description), new XCData(component.Description)));
            
            var serializer = new MemberSerializer();
            element.Add(new XElement(nameof(component.Members),
                component.Members.Select(m => serializer.Serialize((Member)m))));
            
            return element;
        }

        public DataType Deserialize(XElement element)
        {
            var serializer = new MemberSerializer();
            
            var members = element.Descendants(L5XNames.Components.Member)
                .Select(e => serializer.Deserialize(e));

            var dataType = new DataType(element.GetName(), element.GetFamily(), element.GetDescription(), members);

            return dataType;
        }
    }
}