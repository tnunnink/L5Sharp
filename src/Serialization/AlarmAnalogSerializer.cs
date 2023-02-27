using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="ALARM_ANALOG"/> objects.
    /// </summary>
    public class AlarmAnalogSerializer : ILogixSerializer<ALARM_ANALOG>
    {
        /// <inheritdoc />
        public XElement Serialize(ALARM_ANALOG obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.AlarmAnalogParameters);

            element.AddValue(obj.EnableIn, L5XName.EnableIn);
            element.AddValue(obj.InFault, L5XName.InFault);
            element.AddValue(obj.HHEnabled, L5XName.HHEnabled);
            element.AddValue(obj.HEnabled, L5XName.HEnabled);
            element.AddValue(obj.LEnabled, L5XName.LEnabled);
            element.AddValue(obj.LLEnabled, L5XName.LLEnabled);
            element.AddValue(obj.AckRequired, L5XName.AckRequired);
            element.AddValue(obj.ProgAckAll, L5XName.ProgAckAll);
            element.AddValue(obj.OperAckAll, L5XName.OperAckAll);
            element.AddValue(obj.HHProgAck, L5XName.HHProgAck);
            element.AddValue(obj.HHOperAck, L5XName.HHOperAck);
            element.AddValue(obj.HProgAck, L5XName.HProgAck);
            element.AddValue(obj.HOperAck, L5XName.HOperAck);
            element.AddValue(obj.LProgAck, L5XName.LProgAck);
            element.AddValue(obj.LOperAck, L5XName.LOperAck);
            element.AddValue(obj.LLProgAck, L5XName.LLProgAck);
            element.AddValue(obj.LLOperAck, L5XName.LLOperAck);
            element.AddValue(obj.ROCPosProgAck, L5XName.ROCPosProgAck);
            element.AddValue(obj.ROCPosOperAck, L5XName.ROCPosOperAck);
            element.AddValue(obj.ROCNegProgAck, L5XName.ROCNegProgAck);
            element.AddValue(obj.ROCNegOperAck, L5XName.ROCNegOperAck);
            element.AddValue(obj.ProgSuppress, L5XName.ProgSuppress);
            element.AddValue(obj.OperSuppress, L5XName.OperSuppress);
            element.AddValue(obj.ProgUnsuppress, L5XName.ProgUnsuppress);
            element.AddValue(obj.OperUnsuppress, L5XName.OperUnsuppress);
            element.AddValue(obj.HHOperShelve, L5XName.HHOperShelve);
            element.AddValue(obj.HOperShelve, L5XName.HOperShelve);
            element.AddValue(obj.LOperShelve, L5XName.LOperShelve);
            element.AddValue(obj.LLOperShelve, L5XName.LLOperShelve);
            element.AddValue(obj.ROCPosOperShelve, L5XName.ROCPosOperShelve);
            element.AddValue(obj.ROCNegOperShelve, L5XName.ROCNegOperShelve);
            element.AddValue(obj.ProgUnshelveAll, L5XName.ProgUnshelveAll);
            element.AddValue(obj.HHOperUnshelve, L5XName.HHOperUnshelve);
            element.AddValue(obj.HOperUnshelve, L5XName.HOperUnshelve);
            element.AddValue(obj.LOperUnshelve, L5XName.LOperUnshelve);
            element.AddValue(obj.LLOperUnshelve, L5XName.LLOperUnshelve);
            element.AddValue(obj.ROCPosOperUnshelve, L5XName.ROCPosOperUnshelve);
            element.AddValue(obj.ROCNegOperUnshelve, L5XName.ROCNegOperUnshelve);
            element.AddValue(obj.ProgDisable, L5XName.ProgDisable);
            element.AddValue(obj.OperDisable, L5XName.OperDisable);
            element.AddValue(obj.ProgEnable, L5XName.ProgEnable);
            element.AddValue(obj.OperEnable, L5XName.OperEnable);
            element.AddValue(obj.AlarmCountReset, L5XName.AlarmCountReset);
            element.AddValue(obj.HHMinDurationEnable, L5XName.HHMinDurationEnable);
            element.AddValue(obj.HMinDurationEnable, L5XName.HMinDurationEnable);
            element.AddValue(obj.LMinDurationEnable, L5XName.LMinDurationEnable);
            element.AddValue(obj.LLMinDurationEnable, L5XName.LLMinDurationEnable);
            element.AddValue(obj.In, L5XName.In);
            element.AddValue(obj.HHLimit, L5XName.HHLimit);
            element.AddValue(obj.HHSeverity, L5XName.HHSeverity);
            element.AddValue(obj.HLimit, L5XName.HLimit);
            element.AddValue(obj.HSeverity, L5XName.HSeverity);
            element.AddValue(obj.LLimit, L5XName.LLimit);
            element.AddValue(obj.LSeverity, L5XName.LSeverity);
            element.AddValue(obj.LLLimit, L5XName.LLLimit);
            element.AddValue(obj.LLSeverity, L5XName.LLSeverity);
            element.AddValue(obj.MinDurationPRE, L5XName.MinDurationPRE);
            element.AddValue(obj.ShelveDuration, L5XName.ShelveDuration);
            element.AddValue(obj.MaxShelveDuration, L5XName.MaxShelveDuration);
            element.AddValue(obj.Deadband, L5XName.Deadband);
            element.AddValue(obj.ROCPosLimit, L5XName.ROCPosLimit);
            element.AddValue(obj.ROCPosSeverity, L5XName.ROCPosSeverity);
            element.AddValue(obj.ROCNegLimit, L5XName.ROCNegLimit);
            element.AddValue(obj.ROCNegSeverity, L5XName.ROCNegSeverity);
            element.AddValue(obj.ROCPeriod, L5XName.ROCPeriod);

            return element;
        }

        /// <inheritdoc />
        public ALARM_ANALOG Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new ALARM_ANALOG
            {
                EnableIn = element.GetValue<string>(L5XName.EnableIn),
                InFault = element.GetValue<string>(L5XName.InFault),
                HHEnabled = element.GetValue<string>(L5XName.HHEnabled),
                HEnabled = element.GetValue<string>(L5XName.HEnabled),
                LEnabled = element.GetValue<string>(L5XName.LEnabled),
                LLEnabled = element.GetValue<string>(L5XName.LLEnabled),
                AckRequired = element.GetValue<string>(L5XName.AckRequired),
                ProgAckAll = element.GetValue<string>(L5XName.ProgAckAll),
                OperAckAll = element.GetValue<string>(L5XName.OperAckAll),
                HHProgAck = element.GetValue<string>(L5XName.HHProgAck),
                HHOperAck = element.GetValue<string>(L5XName.HHOperAck),
                HProgAck = element.GetValue<string>(L5XName.HProgAck),
                HOperAck = element.GetValue<string>(L5XName.HOperAck),
                LProgAck = element.GetValue<string>(L5XName.LProgAck),
                LOperAck = element.GetValue<string>(L5XName.LOperAck),
                LLProgAck = element.GetValue<string>(L5XName.LLProgAck),
                LLOperAck = element.GetValue<string>(L5XName.LLOperAck),
                ROCPosProgAck = element.GetValue<string>(L5XName.ROCPosProgAck),
                ROCPosOperAck = element.GetValue<string>(L5XName.ROCPosOperAck),
                ROCNegProgAck = element.GetValue<string>(L5XName.ROCNegProgAck),
                ROCNegOperAck = element.GetValue<string>(L5XName.ROCNegOperAck),
                ProgSuppress = element.GetValue<string>(L5XName.ProgSuppress),
                OperSuppress = element.GetValue<string>(L5XName.OperSuppress),
                ProgUnsuppress = element.GetValue<string>(L5XName.ProgUnsuppress),
                OperUnsuppress = element.GetValue<string>(L5XName.OperUnsuppress),
                HHOperShelve = element.GetValue<string>(L5XName.HHOperShelve),
                HOperShelve = element.GetValue<string>(L5XName.HOperShelve),
                LOperShelve = element.GetValue<string>(L5XName.LOperShelve),
                LLOperShelve = element.GetValue<string>(L5XName.LLOperShelve),
                ROCPosOperShelve = element.GetValue<string>(L5XName.ROCPosOperShelve),
                ROCNegOperShelve = element.GetValue<string>(L5XName.ROCNegOperShelve),
                ProgUnshelveAll = element.GetValue<string>(L5XName.ProgUnshelveAll),
                HHOperUnshelve = element.GetValue<string>(L5XName.HHOperUnshelve),
                HOperUnshelve = element.GetValue<string>(L5XName.HOperUnshelve),
                LOperUnshelve = element.GetValue<string>(L5XName.LOperUnshelve),
                LLOperUnshelve = element.GetValue<string>(L5XName.LLOperUnshelve),
                ROCPosOperUnshelve = element.GetValue<string>(L5XName.ROCPosOperUnshelve),
                ROCNegOperUnshelve = element.GetValue<string>(L5XName.ROCNegOperUnshelve),
                ProgDisable = element.GetValue<string>(L5XName.ProgDisable),
                OperDisable = element.GetValue<string>(L5XName.OperDisable),
                ProgEnable = element.GetValue<string>(L5XName.ProgEnable),
                OperEnable = element.GetValue<string>(L5XName.OperEnable),
                AlarmCountReset = element.GetValue<string>(L5XName.AlarmCountReset),
                HHMinDurationEnable = element.GetValue<string>(L5XName.HHMinDurationEnable),
                HMinDurationEnable = element.GetValue<string>(L5XName.HMinDurationEnable),
                LMinDurationEnable = element.GetValue<string>(L5XName.LMinDurationEnable),
                LLMinDurationEnable = element.GetValue<string>(L5XName.LLMinDurationEnable),
                In = element.GetValue<string>(L5XName.In),
                HHLimit = element.GetValue<string>(L5XName.HHLimit),
                HHSeverity = element.GetValue<string>(L5XName.HHSeverity),
                HLimit = element.GetValue<string>(L5XName.HLimit),
                HSeverity = element.GetValue<string>(L5XName.HSeverity),
                LLimit = element.GetValue<string>(L5XName.LLimit),
                LSeverity = element.GetValue<string>(L5XName.LSeverity),
                LLLimit = element.GetValue<string>(L5XName.LLLimit),
                LLSeverity = element.GetValue<string>(L5XName.LLSeverity),
                MinDurationPRE = element.GetValue<string>(L5XName.MinDurationPRE),
                ShelveDuration = element.GetValue<string>(L5XName.ShelveDuration),
                MaxShelveDuration = element.GetValue<string>(L5XName.MaxShelveDuration),
                Deadband = element.GetValue<string>(L5XName.Deadband),
                ROCPosLimit = element.GetValue<string>(L5XName.ROCPosLimit),
                ROCPosSeverity = element.GetValue<string>(L5XName.ROCPosSeverity),
                ROCNegLimit = element.GetValue<string>(L5XName.ROCNegLimit),
                ROCNegSeverity = element.GetValue<string>(L5XName.ROCNegSeverity),
                ROCPeriod = element.GetValue<string>(L5XName.ROCPeriod)
            };
        }
    }
}