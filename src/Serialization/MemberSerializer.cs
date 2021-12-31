using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class UserDefinedMemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = LogixNames.Member;

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddValue(component, c => c.Name);
            element.AddValue(component, c => c.DataType.ToString());
            element.AddValue(component, c => c.Dimension);
            element.AddValue(component, c => c.Radix);
            element.AddValue(component, c => c.ExternalAccess);

            if (!component.Description.IsEmpty())
                element.AddValue(component, x => x.Description, true);

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var dataType = element.GetDataType();
            var dimensions = element.GetValue<Member<IDataType>, Dimensions>(m => m.Dimension);
            var radix = element.GetValue<Member<IDataType>, Radix>(m => m.Radix);
            var access = element.GetValue<Member<IDataType>, ExternalAccess>(m => m.ExternalAccess);
            var description = element.GetValue<Member<IDataType>, string>(m => m.Description);

            return Member.Create(name, dataType, dimensions, radix, access, description);
        }
    }
}