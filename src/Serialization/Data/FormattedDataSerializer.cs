using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;
using String = L5Sharp.Types.Predefined.String;

namespace L5Sharp.Serialization.Data
{
    internal class FormattedDataSerializer : IL5XSerializer<IDataType>
    {
        private static readonly XName ElementName = L5XElement.Data.ToXName();

        public XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            var format = TagDataFormat.FromDataType(component);
            element.Add(new XAttribute(L5XAttribute.Format.ToXName(), format));

            format
                .When(TagDataFormat.Decorated).Then(() =>
                {
                    var serializer = new DecoratedDataSerializer();
                    element.Add(serializer.Serialize(component));
                })
                .When(TagDataFormat.Alarm).Then(() =>
                {
                    var serializer = new AlarmDataSerializer();
                    element.Add(serializer.Serialize(component));
                })
                .When(TagDataFormat.String).Then(() =>
                {
                    var str = (String)component;
                    element.Add(new XAttribute(L5XAttribute.Length.ToXName(), str.LEN.DataType.Value));
                    element.Add(new XCData($"'{str.Value}'"));
                });

            return element;
        }

        public IDataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            TagDataFormat.TryFromName(element.Attribute(L5XAttribute.Format.ToXName())?.Value, out var format);

            if (format is null)
                throw new ArgumentException(
                    "The provided element does not contains a supported format attribute value");
            
            IDataType dataType = new Undefined();
            
            format
                .When(TagDataFormat.Decorated).Then(() =>
                {
                    var serializer = new DecoratedDataSerializer();
                    dataType = serializer.Deserialize(element.Elements().First());
                })
                .When(TagDataFormat.Alarm).Then(() =>
                {
                    var serializer = new AlarmDataSerializer();
                    dataType = serializer.Deserialize(element.Elements().First());
                })
                .When(TagDataFormat.String).Then(() =>
                {
                    dataType = new String(element.Value);
                });

            return dataType;
        }
    }
}