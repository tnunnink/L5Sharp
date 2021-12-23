using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization
{
    internal class DataValueMemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private const string ElementName = LogixNames.DataValueMember;

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (component.DataType is not IAtomicType atomic)
                throw new InvalidOperationException("DataValueMembers must have an atomic data type.");

            var element = new XElement(ElementName);

            element.Add(component.ToAttribute(m => m.Name));
            element.Add(component.ToAttribute(m => m.DataType));
            element.Add(component.ToAttribute(m => m.Radix));

            var value = component.Radix.Convert(atomic);
            element.Add(new XAttribute(LogixNames.Value, value));

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
            var radix = element.GetValue<IMember<IAtomicType>, Radix>(e => e.Radix);
            var value = element.GetValue<IAtomicType, object>(a => a.Value);
            
            atomic.SetValue(value!);

            return Member.Create(name!, atomic, radix: radix);
        }
    }
}