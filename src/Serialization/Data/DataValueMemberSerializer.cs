using System;
using System.Xml.Linq;
using L5Sharp.Atomics;
using L5Sharp.Core;
using L5Sharp.Creators;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class DataValueMemberSerializer : IL5XSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.DataValueMember.ToString();

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
            element.Add(new XAttribute(L5XAttribute.Value.ToString(), value));

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var dataType = DataType.Create(element.DataTypeName());
            var radix = element.Attribute("Radix")?.Value is not null
                ? Radix.FromValue(element.Attribute("Radix")?.Value!)
                : Radix.Default(dataType);
            var value = element.Attribute(L5XAttribute.Value.ToString())?.Value ?? element.Value;

            switch (dataType)
            {
                case IAtomicType atomicType:
                    atomicType.TrySetValue(value);
                    return new Member<IDataType>(name, atomicType, radix);
                case IStringType stringType:
                    stringType.SetValue(value);
                    return new Member<IDataType>(name, stringType.DATA.DataType, radix);
                default:
                    return Member.Create(name, dataType, radix);
            }
        }
    }
}