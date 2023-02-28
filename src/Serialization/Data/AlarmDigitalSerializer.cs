using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="ALARM_DIGITAL"/> objects.
    /// </summary>
    public class AlarmDigitalSerializer : ILogixSerializer<ALARM_DIGITAL>
    {
        /// <inheritdoc />
        public XElement Serialize(ALARM_DIGITAL obj)
        {
            Check.NotNull(obj);
            
            var element = new XElement(L5XName.AlarmDigitalParameters);
            
            element.AddValue((int)obj.Severity, L5XName.Severity);
            element.AddValue((int)obj.MinDurationPRE, L5XName.MinDurationPRE);
            element.AddValue((int)obj.ShelveDuration, L5XName.ShelveDuration);
            element.AddValue((int)obj.MaxShelveDuration, L5XName.MaxShelveDuration);
            element.AddValue(obj.ProgTime, L5XName.ProgTime);
            element.AddValue((bool)obj.EnableIn, L5XName.EnableIn);
            element.AddValue((bool)obj.In, L5XName.In);
            element.AddValue((bool)obj.InFault, L5XName.InFault);
            element.AddValue((bool)obj.Condition, L5XName.Condition);
            element.AddValue((bool)obj.AckRequired, L5XName.AckRequired);
            element.AddValue((bool)obj.Latched, L5XName.Latched);
            element.AddValue((bool)obj.ProgAck, L5XName.ProgAck);
            element.AddValue((bool)obj.OperAck, L5XName.OperAck);
            element.AddValue((bool)obj.ProgReset, L5XName.ProgReset);
            element.AddValue((bool)obj.OperReset, L5XName.OperReset);
            element.AddValue((bool)obj.ProgSuppress, L5XName.ProgSuppress);
            element.AddValue((bool)obj.OperSuppress, L5XName.OperSuppress);
            element.AddValue((bool)obj.ProgUnsuppress, L5XName.ProgUnsuppress);
            element.AddValue((bool)obj.OperUnsuppress, L5XName.OperUnsuppress);
            element.AddValue((bool)obj.OperShelve, L5XName.OperShelve);
            element.AddValue((bool)obj.ProgUnshelve, L5XName.ProgUnshelve);
            element.AddValue((bool)obj.OperUnshelve, L5XName.OperUnshelve);
            element.AddValue((bool)obj.ProgDisable, L5XName.ProgDisable);
            element.AddValue((bool)obj.OperDisable, L5XName.OperDisable);
            element.AddValue((bool)obj.ProgEnable, L5XName.ProgEnable);
            element.AddValue((bool)obj.OperEnable, L5XName.OperEnable);
            element.AddValue((bool)obj.AlarmCountReset, L5XName.AlarmCountReset);
            element.AddValue((bool)obj.UseProgTime, L5XName.UseProgTime);

            return element;
        }

        /// <inheritdoc />
        public ALARM_DIGITAL Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new ALARM_DIGITAL
            {
                Severity = element.GetValue<int>(L5XName.Severity),
                MinDurationPRE = element.GetValue<int>(L5XName.MinDurationPRE),
                ShelveDuration = element.GetValue<int>(L5XName.ShelveDuration),
                MaxShelveDuration = element.GetValue<int>(L5XName.MaxShelveDuration),
                ProgTime = element.GetValue<string>(L5XName.ProgTime),
                EnableIn = element.GetValue<bool>(L5XName.EnableIn),
                In = element.GetValue<bool>(L5XName.In),
                InFault = element.GetValue<bool>(L5XName.InFault),
                Condition = element.GetValue<bool>(L5XName.Condition),
                AckRequired = element.GetValue<bool>(L5XName.AckRequired),
                Latched = element.GetValue<bool>(L5XName.Latched),
                ProgAck = element.GetValue<bool>(L5XName.ProgAck),
                OperAck = element.GetValue<bool>(L5XName.OperAck),
                ProgReset = element.GetValue<bool>(L5XName.ProgReset),
                OperReset = element.GetValue<bool>(L5XName.OperReset),
                ProgSuppress = element.GetValue<bool>(L5XName.ProgSuppress),
                OperSuppress = element.GetValue<bool>(L5XName.OperSuppress),
                ProgUnsuppress = element.GetValue<bool>(L5XName.ProgUnsuppress),
                OperUnsuppress = element.GetValue<bool>(L5XName.OperUnsuppress),
                OperShelve = element.GetValue<bool>(L5XName.OperShelve),
                ProgUnshelve = element.GetValue<bool>(L5XName.ProgUnshelve),
                OperUnshelve = element.GetValue<bool>(L5XName.OperUnshelve),
                ProgDisable = element.GetValue<bool>(L5XName.ProgDisable),
                OperDisable = element.GetValue<bool>(L5XName.OperDisable),
                ProgEnable = element.GetValue<bool>(L5XName.ProgEnable),
                OperEnable = element.GetValue<bool>(L5XName.OperEnable),
                AlarmCountReset = element.GetValue<bool>(L5XName.AlarmCountReset),
                UseProgTime = element.GetValue<bool>(L5XName.UseProgTime)
            };
        }
    }
}