using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.L5X;
using L5Sharp.Types;

namespace L5Sharp.Serialization
{
    internal class FormattedDataSerializer : L5XSerializer<IDataType>
    {
        private readonly L5XDocument? _document;
        private readonly L5XElement _element;

        private DecoratedDataSerializer DecoratedDataSerializer => _document is not null
            ? _document.Serializers.Get<DecoratedDataSerializer>()
            : new DecoratedDataSerializer(_document);
        
        private AlarmDataSerializer AlarmDataSerializer => _document is not null
            ? _document.Serializers.Get<AlarmDataSerializer>()
            : new AlarmDataSerializer(_document);

        public FormattedDataSerializer(L5XDocument? document = null, L5XElement? element = null)
        {
            _element = element ?? L5XElement.Data;
            _document = document;
        }

        public override XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(_element.ToString());

            var format = TagDataFormat.FromDataType(component);
            
            element.Add(new XAttribute(L5XAttribute.Format.ToString(), format));

            format
                .When(TagDataFormat.Decorated).Then(() => element.Add(DecoratedDataSerializer.Serialize(component)))
                .When(TagDataFormat.Alarm).Then(() => element.Add(AlarmDataSerializer.Serialize(component)))
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

            if (element.Name != _element.ToString())
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            TagDataFormat.TryFromName(element.Attribute(L5XAttribute.Format.ToString())?.Value, out var format);

            if (format is null) return new UNDEFINED();
            
            IDataType dataType = new UNDEFINED();
            
            format
                .When(TagDataFormat.Decorated).Then(() =>
                {
                    dataType = DecoratedDataSerializer.Deserialize(element.Elements().First());
                })
                .When(TagDataFormat.Alarm).Then(() =>
                {
                    dataType = AlarmDataSerializer.Deserialize(element.Elements().First());
                })
                .When(TagDataFormat.String).Then(() =>
                {
                    dataType = new STRING(element.Value);
                });

            return dataType;
        }
    }
}