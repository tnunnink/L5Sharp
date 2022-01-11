using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;

namespace L5Sharp.Serialization.Data
{
    internal class DataValueMemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = LogixNames.DataValueMember;

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (component.DataType is not IAtomicType atomic)
                throw new InvalidOperationException($"{ElementName} must have an atomic data type.");

            var element = new XElement(ElementName);

            element.AddAttribute(component, m => m.Name);
            element.AddAttribute(component, m => m.DataType);
            
            //Looks like radix is added only when the type is not a Bool...
            element.AddAttribute(component, m => m.Radix,
                m => !string.Equals(m.DataType.Name, nameof(Bool), StringComparison.OrdinalIgnoreCase));
            
            element.Add(new XAttribute(LogixNames.Value, atomic.Format(component.Radix)));

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException(
                    $"Expecting element with name {LogixNames.DataValueMember} but got {element.Name}");

            var name = element.GetComponentName();
            var atomic = (IAtomicType)element.GetDataType();
            var radix = element.GetAttribute<IMember<IDataType>, Radix>(m => m.Radix) ?? Radix.Default(atomic);
            var value = element.Attribute(LogixNames.Value)?.Value!;
            atomic.SetValue(value);

            return Member.Create(name, atomic, radix);
        }
    }
}