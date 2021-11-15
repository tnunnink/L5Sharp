using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class MemberSerializer : IComponentSerializer<IMember<IDataType>>
    {
        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));
            
            var element = new XElement(LogixNames.Member);
            element.Add(component.ToAttribute(c => c.Name));
            element.Add(component.ToAttribute(c => c.DataType));
            element.Add(component.ToAttribute(c => c.Dimensions));
            element.Add(component.ToAttribute(c => c.Radix));
            element.Add(component.ToAttribute(c => c.ExternalAccess));
            
            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToElement(x => x.Description));

            return element;
        }
    }
}