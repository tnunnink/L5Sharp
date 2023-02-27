using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public class FormattedDataSerializer : ILogixSerializer<ILogixType>
    {
        private readonly DecoratedDataSerializer _decoratedDataSerializer = new();
        private readonly AlarmDataSerializer _alarmDataSerializer = new();
        private readonly StringDataSerializer _stringDataSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(ILogixType obj)
        {
            Check.NotNull(obj);

            var format = DataFormat.FromDataType(obj);

            if (format == DataFormat.Decorated)
                return _decoratedDataSerializer.Serialize(obj);
            
            if (format == DataFormat.Alarm)
                return _alarmDataSerializer.Serialize(obj);
            
            if (format == DataFormat.String)
                return _stringDataSerializer.Serialize(obj);

            if (format == DataFormat.Message)
                throw new NotImplementedException();

            return new XElement(L5XName.Data);
        }

        /// <inheritdoc />
        public ILogixType Deserialize(XElement element)
        {
            Check.NotNull(element);

            DataFormat.TryFromName(element.Attribute(L5XName.Format)?.Value, out var format);

            if (format == DataFormat.Decorated)
                return _decoratedDataSerializer.Deserialize(element);
            
            if (format == DataFormat.Alarm)
                return _alarmDataSerializer.Deserialize(element);
            
            if (format == DataFormat.String)
                return _stringDataSerializer.Deserialize(element);
            
            if (format == DataFormat.Message)
                throw new NotImplementedException();

            return LogixType.Null;
        }
    }
}