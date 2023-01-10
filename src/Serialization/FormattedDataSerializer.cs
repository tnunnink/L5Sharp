using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class FormattedDataSerializer : L5XSerializer<IDataType>
    {
        private readonly LogixInfo? _document;
        private readonly string _element;

        private DecoratedDataSerializer DecoratedDataSerializer => _document is not null
            ? _document.Serializers.Get<DecoratedDataSerializer>()
            : new DecoratedDataSerializer(_document);
        
        private AlarmDataSerializer AlarmDataSerializer => _document is not null
            ? _document.Serializers.Get<AlarmDataSerializer>()
            : new AlarmDataSerializer(_document);

        public FormattedDataSerializer(LogixInfo? document = null, string? element = null)
        {
            _element = element ?? L5XName.Data;
            _document = document;
        }

        public override XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(_element);

            var format = TagDataFormat.FromDataType(component);
            
            element.Add(new XAttribute(L5XName.Format, format));

            format
                .When(TagDataFormat.Decorated).Then(() => element.Add(DecoratedDataSerializer.Serialize(component)))
                .When(TagDataFormat.Alarm).Then(() => element.Add(AlarmDataSerializer.Serialize(component)))
                .When(TagDataFormat.String).Then(() =>
                {
                    var str = (STRING)component;
                    element.Add(new XAttribute(L5XName.Length, str.LEN.DataType.Value));
                    element.Add(new XCData($"'{str.Value}'"));
                });

            return element;
        }

        public override IDataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != _element)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            TagDataFormat.TryFromName(element.Attribute(L5XName.Format)?.Value, out var format);

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