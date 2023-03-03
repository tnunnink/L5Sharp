using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
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
                return TagDataSerializer.DecoratedData.Deserialize(element.Elements().First());
            
            if (format == DataFormat.Alarm)
                return TagDataSerializer.AlarmData.Deserialize(element.Elements().First());
            
            if (format == DataFormat.String)
                return TagDataSerializer.StringData.Deserialize(element);

            //For some reason Rockwell doesn't deserialize the actual data type but a set of parameters that appear configurable from the MSG instruction.
            //Not sure how to handle this. Either I don't support it or I create a new "Message" type that has the properties that are deserialized.
            //Most properties are strings though...
            if (format == DataFormat.Message)
                return Logix.Null;

            return Logix.Null;
        }
    }
}