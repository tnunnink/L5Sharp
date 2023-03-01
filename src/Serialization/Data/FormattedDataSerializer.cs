using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class FormattedDataSerializer : ILogixSerializer<ILogixType>
    {
        /// <inheritdoc />
        public XElement Serialize(ILogixType obj)
        {
            Check.NotNull(obj);

            var format = DataFormat.FromDataType(obj);

            if (format == DataFormat.Decorated)
                return TagDataSerializer.DecoratedData.Serialize(obj);
            
            if (format == DataFormat.Alarm)
                return TagDataSerializer.AlarmData.Serialize(obj);
            
            if (format == DataFormat.String)
                return TagDataSerializer.StringData.Serialize(obj);

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
                return TagDataSerializer.DecoratedData.Deserialize(element);
            
            if (format == DataFormat.Alarm)
                return TagDataSerializer.AlarmData.Deserialize(element);
            
            if (format == DataFormat.String)
                return TagDataSerializer.StringData.Deserialize(element);
            
            if (format == DataFormat.Message)
                throw new NotImplementedException();

            return Logix.Null;
        }
    }
}