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
        private static readonly XName ElementName = L5XElement.Member.ToString();
        private readonly Func<string, IDataType> _dataTypeCreator;

        public MemberSerializer(L5XContext? context = null)
        {
            _dataTypeCreator = context is not null ? context.TypeIndex.GetDataType : DataType.Create;
        }

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.Add(new XAttribute(L5XAttribute.Name.ToString(), component.Name));
            element.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.DataType.Name));
            element.Add(new XAttribute(L5XAttribute.Dimension.ToString(), component.Dimensions));
            element.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.Radix));
            element.Add(new XAttribute(L5XAttribute.Hidden.ToString(), false));
            element.Add(new XAttribute(L5XAttribute.ExternalAccess.ToString(), component.ExternalAccess));
            element.Add(new XElement(L5XAttribute.Description.ToString(), new XCData(component.Description)));

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
            var dataType = _dataTypeCreator.Invoke(element.DataTypeName());
            var dimensions = Dimensions.Parse(element.Attribute(L5XAttribute.Dimension.ToString())?.Value!);
            Radix.TryFromValue(element.Attribute("Radix")?.Value!, out var radix);
            ExternalAccess.TryFromName(element.Attribute("ExternalAccess")?.Value, out var access);

            if (dimensions.AreEmpty)
                return new Member<IDataType>(name, dataType, radix, access, description);

            var arrayType = new ArrayType<IDataType>(dimensions, dataType, radix, access, description);
            return new Member<IDataType>(name, arrayType, radix, access, description);
        }
    }
}