using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Factories;
using L5Sharp.L5X;
using L5Sharp.Types;

namespace L5Sharp.Serialization.Data
{
    internal class DataValueMemberSerializer : IL5XSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.DataValueMember.ToXName();

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (component.DataType is not IAtomicType atomic)
                throw new ArgumentException($"{ElementName} must have an atomic data type.");

            var element = new XElement(ElementName);

            element.AddAttribute(component, m => m.Name);
            element.AddAttribute(component, m => m.DataType);
            
            element.AddAttribute(component, m => m.Radix,
                m => !string.Equals(m.DataType.Name, nameof(Bool), StringComparison.OrdinalIgnoreCase));

            var value = component.DataType is Bool ? atomic.Format() : atomic.Format(component.Radix);
            element.Add(new XAttribute(L5XAttribute.Value.ToXName(), value));

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.GetComponentName();
            var atomic = (IAtomicType)DataType.Create(element.GetDataTypeName());
            var radix = element.GetAttribute<IMember<IDataType>, Radix>(m => m.Radix) ?? Radix.Default(atomic);
            var value = element.Attribute(L5XAttribute.Value.ToXName())?.Value!;
            atomic.SetValue(value);

            return Member.Create(name, atomic, radix);
        }
    }
}