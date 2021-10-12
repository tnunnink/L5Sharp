using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class MemberSerializer : IComponentSerializer<IMember>
    {
        public XElement Serialize(IMember component)
        {
            var element = new XElement(L5XNames.Components.Member);
            element.Add(component.ToXAttribute(c => c.Name));
            element.Add(component.ToXAttribute(c => c.DataType));
            element.Add(component.ToXAttribute(c => c.Dimension));
            element.Add(component.ToXAttribute(c => c.Radix));
            element.Add(component.ToXAttribute(c => c.ExternalAccess));
            
            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToXCDataElement(x => x.Description));

            return element;
        }

        public IMember Deserialize(XElement element)
        {
            var dataType = element.GetDataType();
            
            return new Member(element.GetName(), dataType, element.GetDimension(), element.GetRadix(),
                element.GetExternalAccess(), element.GetDescription());
        }
    }
}