using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
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
            
            element.AddValue(obj.Severity, L5XName.Severity);
            element.AddValue(obj.MinDurationPRE, L5XName.MinDurationPRE);
            element.AddValue(obj.ShelveDuration, L5XName.ShelveDuration);
            element.AddValue(obj.MaxShelveDuration, L5XName.MaxShelveDuration);
            element.AddValue(obj.ProgTime, L5XName.ProgTime);
            element.AddValue(obj.EnableIn, L5XName.EnableIn);
            element.AddValue(obj.In, L5XName.In);
            element.AddValue(obj.InFault, L5XName.InFault);
            element.AddValue(obj.Condition, L5XName.Condition);
            element.AddValue(obj.AckRequired, L5XName.AckRequired);
            element.AddValue(obj.Latched, L5XName.Latched);
            element.AddValue(obj.ProgAck, L5XName.ProgAck);
            element.AddValue(obj.OperAck, L5XName.OperAck);
            element.AddValue(obj.ProgReset, L5XName.ProgReset);
            element.AddValue(obj.OperReset, L5XName.OperReset);
            element.AddValue(obj.ProgSuppress, L5XName.ProgSuppress);
            element.AddValue(obj.OperSuppress, L5XName.OperSuppress);
            element.AddValue(obj.ProgUnsuppress, L5XName.ProgUnsuppress);
            element.AddValue(obj.OperUnsuppress, L5XName.OperUnsuppress);
            element.AddValue(obj.OperShelve, L5XName.OperShelve);
            element.AddValue(obj.ProgUnshelve, L5XName.ProgUnshelve);
            element.AddValue(obj.OperUnshelve, L5XName.OperUnshelve);
            element.AddValue(obj.ProgDisable, L5XName.ProgDisable);
            element.AddValue(obj.OperDisable, L5XName.OperDisable);
            element.AddValue(obj.ProgEnable, L5XName.ProgEnable);
            element.AddValue(obj.OperEnable, L5XName.OperEnable);
            element.AddValue(obj.AlarmCountReset, L5XName.AlarmCountReset);
            element.AddValue(obj.UseProgTime, L5XName.UseProgTime);

            return element;
        }

        /// <inheritdoc />
        public ALARM_DIGITAL Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new ALARM_DIGITAL
            {
                Severity = element.GetValue<string>(L5XName.Severity),
                MinDurationPRE = element.GetValue<string>(L5XName.MinDurationPRE),
                ShelveDuration = element.GetValue<string>(L5XName.ShelveDuration),
                MaxShelveDuration = element.GetValue<string>(L5XName.MaxShelveDuration),
                ProgTime = element.GetValue<string>(L5XName.ProgTime),
                EnableIn = element.GetValue<string>(L5XName.EnableIn),
                In = element.GetValue<string>(L5XName.In),
                InFault = element.GetValue<string>(L5XName.InFault),
                Condition = element.GetValue<string>(L5XName.Condition),
                AckRequired = element.GetValue<string>(L5XName.AckRequired),
                Latched = element.GetValue<string>(L5XName.Latched),
                ProgAck = element.GetValue<string>(L5XName.ProgAck),
                OperAck = element.GetValue<string>(L5XName.OperAck),
                ProgReset = element.GetValue<string>(L5XName.ProgReset),
                OperReset = element.GetValue<string>(L5XName.OperReset),
                ProgSuppress = element.GetValue<string>(L5XName.ProgSuppress),
                OperSuppress = element.GetValue<string>(L5XName.OperSuppress),
                ProgUnsuppress = element.GetValue<string>(L5XName.ProgUnsuppress),
                OperUnsuppress = element.GetValue<string>(L5XName.OperUnsuppress),
                OperShelve = element.GetValue<string>(L5XName.OperShelve),
                ProgUnshelve = element.GetValue<string>(L5XName.ProgUnshelve),
                OperUnshelve = element.GetValue<string>(L5XName.OperUnshelve),
                ProgDisable = element.GetValue<string>(L5XName.ProgDisable),
                OperDisable = element.GetValue<string>(L5XName.OperDisable),
                ProgEnable = element.GetValue<string>(L5XName.ProgEnable),
                OperEnable = element.GetValue<string>(L5XName.OperEnable),
                AlarmCountReset = element.GetValue<string>(L5XName.AlarmCountReset),
                UseProgTime = element.GetValue<string>(L5XName.UseProgTime)
            };
        }
    }
}