using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.L5X;
using L5Sharp.Types;

namespace L5Sharp.Serialization.Data
{
    internal class FormattedDataSerializer : L5XSerializer<IDataType>
    {
        private static readonly XName ElementName = L5XElement.Data.ToString();
        private readonly DecoratedDataSerializer _decoratedDataSerializer;
        private readonly AlarmDataSerializer _alarmDataSerializer;

        public FormattedDataSerializer()
        {
            _decoratedDataSerializer = new DecoratedDataSerializer();
            _alarmDataSerializer = new AlarmDataSerializer();
        }

        public override XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            var format = TagDataFormat.FromDataType(component);
            element.Add(new XAttribute(L5XAttribute.Format.ToString(), format));

            format
                .When(TagDataFormat.Decorated).Then(() => element.Add(_decoratedDataSerializer.Serialize(component)))
                .When(TagDataFormat.Alarm).Then(() => element.Add(_alarmDataSerializer.Serialize(component)))
                .When(TagDataFormat.String).Then(() =>
                {
                    var str = (STRING)component;
                    element.Add(new XAttribute(L5XAttribute.Length.ToString(), str.LEN.DataType.Value));
                    element.Add(new XCData($"'{str.Value}'"));
                });

            return element;
        }

        public override IDataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            TagDataFormat.TryFromName(element.Attribute(L5XAttribute.Format.ToString())?.Value, out var format);

            if (format is null) return new UNDEFINED();
            
            IDataType dataType = new UNDEFINED();
            
            format
                .When(TagDataFormat.Decorated).Then(() =>
                {
                    dataType = _decoratedDataSerializer.Deserialize(element.Elements().First());
                })
                .When(TagDataFormat.Alarm).Then(() =>
                {
                    dataType = _alarmDataSerializer.Deserialize(element.Elements().First());
                })
                .When(TagDataFormat.String).Then(() =>
                {
                    dataType = new STRING(element.Value);
                });

            return dataType;
        }
    }
}