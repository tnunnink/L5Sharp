using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// Serializer used for decorated L5X data element. The format attribute is used to pass through the serialization
    /// to more specific serializers.
    /// </summary>
    internal class FormattedDataSerializer : IXSerializer<IDataType>
    {
        private const string Format = "Format";
        private static readonly XName ElementName = LogixNames.Data;

        public XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            var format = TagDataFormat.FromDataType(component);
            element.Add(new XAttribute(Format, format));

            IXSerializer<IDataType>? serializer = null;

            format
                .When(TagDataFormat.Decorated).Then(() => { serializer = new DecoratedDataSerializer(); });

            if (serializer is null)
                throw new InvalidOperationException(
                    $"Could not determine the correct serializer for the provided type {component.GetType()}");

            return serializer.Serialize(component);
        }

        public IDataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            
            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var formatName = element.Attribute(Format)?.Value;
            if (formatName is null)
                throw new ArgumentException(
                    $"The provided element with name {element.Name} does not have a format attribute");
            
            var format = TagDataFormat.FromName(formatName);

            IXSerializer<IDataType>? serializer = null;

            format
                .When(TagDataFormat.Decorated).Then(() => { serializer = new DecoratedDataSerializer(); });

            if (serializer is null)
                throw new InvalidOperationException(
                    $"Could not determine the correct serializer for the provided element with name '{element.Name}'");

            return serializer.Deserialize(element);
        }
    }
}