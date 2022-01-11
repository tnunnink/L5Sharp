using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization.Core
{
    internal class UserDefinedMemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = LogixNames.Member;

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.DataType.ToString());
            element.AddAttribute(component, c => c.Dimensions);
            element.AddAttribute(component, c => c.Radix);
            element.AddAttribute(component, c => c.ExternalAccess);

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var description = element.GetComponentDescription();
            var dataType = element.GetDataType();
            var dimensions = element.GetAttribute<Member<IDataType>, Dimensions>(m => m.Dimensions);
            var radix = element.GetAttribute<Member<IDataType>, Radix>(m => m.Radix);
            var access = element.GetAttribute<Member<IDataType>, ExternalAccess>(m => m.ExternalAccess);
            
            
            
            return Member.Create(name, dataType, dimensions, radix, access, description);
        }
    }
}