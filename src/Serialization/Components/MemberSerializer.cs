using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class MemberSerializer : IL5XSerializer<IMember<IDataType>>
    {
        //Override the default property name.
        private const string Dimension = "Dimension";
        private static readonly XName ElementName = L5XElement.Member.ToXName();

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.DataType.Name, nameOverride: nameof(component.DataType));
            element.AddAttribute(component, c => c.Dimensions, nameOverride: Dimension);
            element.AddAttribute(component, c => c.Radix);
            element.Add(new XAttribute("Hidden", false));
            element.AddAttribute(component, c => c.ExternalAccess);

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var dataType = element.DataType();
            var dimensions = element.GetAttribute<Member<IDataType>, Dimensions>(m => m.Dimensions, Dimension);
            var radix = element.GetAttribute<Member<IDataType>, Radix>(m => m.Radix);
            var access = element.GetAttribute<Member<IDataType>, ExternalAccess>(m => m.ExternalAccess);

            if (dimensions is null || dimensions.AreEmpty)
                return new Member<IDataType>(name, dataType, radix, access, description);

            var arrayType = new ArrayType<IDataType>(dimensions, dataType, radix, access, description);
            return new Member<IDataType>(name, arrayType, radix, access, description);
        }
    }
}