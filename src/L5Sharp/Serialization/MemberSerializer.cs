using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class MemberSerializer : IComponentSerializer<IMember>
    {
        public XElement Serialize(IMember component)
        {
            var element = new XElement(LogixNames.Components.Member);
            element.Add(component.ToAttribute(c => c.Name));
            element.Add(component.ToAttribute(c => c.DataType));
            element.Add(component.ToAttribute(c => c.Dimension));
            element.Add(component.ToAttribute(c => c.Radix));
            element.Add(component.ToAttribute(c => c.ExternalAccess));
            
            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToCDataElement(x => x.Description));

            return element;
        }
    }
}