using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class DataValueMemberSerializer : IXSerializer<IMember<IAtomic>>
    {
        public XElement Serialize(IMember<IAtomic> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            var element = new XElement(LogixNames.DataValueMember);
            
            element.Add(component.ToAttribute(m => m.Name));
            element.Add(component.ToAttribute(m => m.DataType));
            element.Add(component.ToAttribute(m => m.Radix));
            element.Add(component.ToAttribute(m => m.DataType.Value), "Value");

            return element;
        }

        public IMember<IAtomic> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != LogixNames.DataValueMember)
                throw new ArgumentException(
                    $"Expecting element with name {LogixNames.DataValueMember} but got {element.Name}");

            var name = element.GetName();
            var dataType = (IAtomic) Logix.InstantiateType(element.GetDataTypeName());
            var radix = element.GetValue<IMember<IAtomic>, Radix>(e => e.Radix);
            var value = element.Attribute("Value")?.Value;
            dataType.SetValue(value);

            return Member.Create(name, dataType, Dimensions.Empty, radix);
        }
    }
}