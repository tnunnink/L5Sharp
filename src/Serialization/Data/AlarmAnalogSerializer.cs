using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
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

            element.AddValue((bool)obj.EnableIn, L5XName.EnableIn);
            element.AddValue((bool)obj.InFault, L5XName.InFault);
            element.AddValue((bool)obj.HHEnabled, L5XName.HHEnabled);
            element.AddValue((bool)obj.HEnabled, L5XName.HEnabled);
            element.AddValue((bool)obj.LEnabled, L5XName.LEnabled);
            element.AddValue((bool)obj.LLEnabled, L5XName.LLEnabled);
            element.AddValue((bool)obj.AckRequired, L5XName.AckRequired);
            element.AddValue((bool)obj.ProgAckAll, L5XName.ProgAckAll);
            element.AddValue((bool)obj.OperAckAll, L5XName.OperAckAll);
            element.AddValue((bool)obj.HHProgAck, L5XName.HHProgAck);
            element.AddValue((bool)obj.HHOperAck, L5XName.HHOperAck);
            element.AddValue((bool)obj.HProgAck, L5XName.HProgAck);
            element.AddValue((bool)obj.HOperAck, L5XName.HOperAck);
            element.AddValue((bool)obj.LProgAck, L5XName.LProgAck);
            element.AddValue((bool)obj.LOperAck, L5XName.LOperAck);
            element.AddValue((bool)obj.LLProgAck, L5XName.LLProgAck);
            element.AddValue((bool)obj.LLOperAck, L5XName.LLOperAck);
            element.AddValue((bool)obj.ROCPosProgAck, L5XName.ROCPosProgAck);
            element.AddValue((bool)obj.ROCPosOperAck, L5XName.ROCPosOperAck);
            element.AddValue((bool)obj.ROCNegProgAck, L5XName.ROCNegProgAck);
            element.AddValue((bool)obj.ROCNegOperAck, L5XName.ROCNegOperAck);
            element.AddValue((bool)obj.ProgSuppress, L5XName.ProgSuppress);
            element.AddValue((bool)obj.OperSuppress, L5XName.OperSuppress);
            element.AddValue((bool)obj.ProgUnsuppress, L5XName.ProgUnsuppress);
            element.AddValue((bool)obj.OperUnsuppress, L5XName.OperUnsuppress);
            element.AddValue((bool)obj.HHOperShelve, L5XName.HHOperShelve);
            element.AddValue((bool)obj.HOperShelve, L5XName.HOperShelve);
            element.AddValue((bool)obj.LOperShelve, L5XName.LOperShelve);
            element.AddValue((bool)obj.LLOperShelve, L5XName.LLOperShelve);
            element.AddValue((bool)obj.ROCPosOperShelve, L5XName.ROCPosOperShelve);
            element.AddValue((bool)obj.ROCNegOperShelve, L5XName.ROCNegOperShelve);
            element.AddValue((bool)obj.ProgUnshelveAll, L5XName.ProgUnshelveAll);
            element.AddValue((bool)obj.HHOperUnshelve, L5XName.HHOperUnshelve);
            element.AddValue((bool)obj.HOperUnshelve, L5XName.HOperUnshelve);
            element.AddValue((bool)obj.LOperUnshelve, L5XName.LOperUnshelve);
            element.AddValue((bool)obj.LLOperUnshelve, L5XName.LLOperUnshelve);
            element.AddValue((bool)obj.ROCPosOperUnshelve, L5XName.ROCPosOperUnshelve);
            element.AddValue((bool)obj.ROCNegOperUnshelve, L5XName.ROCNegOperUnshelve);
            element.AddValue((bool)obj.ProgDisable, L5XName.ProgDisable);
            element.AddValue((bool)obj.OperDisable, L5XName.OperDisable);
            element.AddValue((bool)obj.ProgEnable, L5XName.ProgEnable);
            element.AddValue((bool)obj.OperEnable, L5XName.OperEnable);
            element.AddValue((bool)obj.AlarmCountReset, L5XName.AlarmCountReset);
            element.AddValue((bool)obj.HHMinDurationEnable, L5XName.HHMinDurationEnable);
            element.AddValue((bool)obj.HMinDurationEnable, L5XName.HMinDurationEnable);
            element.AddValue((bool)obj.LMinDurationEnable, L5XName.LMinDurationEnable);
            element.AddValue((bool)obj.LLMinDurationEnable, L5XName.LLMinDurationEnable);
            element.AddValue((float)obj.In, L5XName.In);
            element.AddValue((float)obj.HHLimit, L5XName.HHLimit);
            element.AddValue((int)obj.HHSeverity, L5XName.HHSeverity);
            element.AddValue((float)obj.HLimit, L5XName.HLimit);
            element.AddValue((int)obj.HSeverity, L5XName.HSeverity);
            element.AddValue((float)obj.LLimit, L5XName.LLimit);
            element.AddValue((int)obj.LSeverity, L5XName.LSeverity);
            element.AddValue((float)obj.LLLimit, L5XName.LLLimit);
            element.AddValue((int)obj.LLSeverity, L5XName.LLSeverity);
            element.AddValue((int)obj.MinDurationPRE, L5XName.MinDurationPRE);
            element.AddValue((int)obj.ShelveDuration, L5XName.ShelveDuration);
            element.AddValue((int)obj.MaxShelveDuration, L5XName.MaxShelveDuration);
            element.AddValue((float)obj.Deadband, L5XName.Deadband);
            element.AddValue((float)obj.ROCPosLimit, L5XName.ROCPosLimit);
            element.AddValue((int)obj.ROCPosSeverity, L5XName.ROCPosSeverity);
            element.AddValue((float)obj.ROCNegLimit, L5XName.ROCNegLimit);
            element.AddValue((int)obj.ROCNegSeverity, L5XName.ROCNegSeverity);
            element.AddValue((float)obj.ROCPeriod, L5XName.ROCPeriod);

            return element;
        }

        /// <inheritdoc />
        public ALARM_ANALOG Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new ALARM_ANALOG
            {
                EnableIn = element.GetValue<bool>(L5XName.EnableIn),
                InFault = element.GetValue<bool>(L5XName.InFault),
                HHEnabled = element.GetValue<bool>(L5XName.HHEnabled),
                HEnabled = element.GetValue<bool>(L5XName.HEnabled),
                LEnabled = element.GetValue<bool>(L5XName.LEnabled),
                LLEnabled = element.GetValue<bool>(L5XName.LLEnabled),
                AckRequired = element.GetValue<bool>(L5XName.AckRequired),
                ProgAckAll = element.GetValue<bool>(L5XName.ProgAckAll),
                OperAckAll = element.GetValue<bool>(L5XName.OperAckAll),
                HHProgAck = element.GetValue<bool>(L5XName.HHProgAck),
                HHOperAck = element.GetValue<bool>(L5XName.HHOperAck),
                HProgAck = element.GetValue<bool>(L5XName.HProgAck),
                HOperAck = element.GetValue<bool>(L5XName.HOperAck),
                LProgAck = element.GetValue<bool>(L5XName.LProgAck),
                LOperAck = element.GetValue<bool>(L5XName.LOperAck),
                LLProgAck = element.GetValue<bool>(L5XName.LLProgAck),
                LLOperAck = element.GetValue<bool>(L5XName.LLOperAck),
                ROCPosProgAck = element.GetValue<bool>(L5XName.ROCPosProgAck),
                ROCPosOperAck = element.GetValue<bool>(L5XName.ROCPosOperAck),
                ROCNegProgAck = element.GetValue<bool>(L5XName.ROCNegProgAck),
                ROCNegOperAck = element.GetValue<bool>(L5XName.ROCNegOperAck),
                ProgSuppress = element.GetValue<bool>(L5XName.ProgSuppress),
                OperSuppress = element.GetValue<bool>(L5XName.OperSuppress),
                ProgUnsuppress = element.GetValue<bool>(L5XName.ProgUnsuppress),
                OperUnsuppress = element.GetValue<bool>(L5XName.OperUnsuppress),
                HHOperShelve = element.GetValue<bool>(L5XName.HHOperShelve),
                HOperShelve = element.GetValue<bool>(L5XName.HOperShelve),
                LOperShelve = element.GetValue<bool>(L5XName.LOperShelve),
                LLOperShelve = element.GetValue<bool>(L5XName.LLOperShelve),
                ROCPosOperShelve = element.GetValue<bool>(L5XName.ROCPosOperShelve),
                ROCNegOperShelve = element.GetValue<bool>(L5XName.ROCNegOperShelve),
                ProgUnshelveAll = element.GetValue<bool>(L5XName.ProgUnshelveAll),
                HHOperUnshelve = element.GetValue<bool>(L5XName.HHOperUnshelve),
                HOperUnshelve = element.GetValue<bool>(L5XName.HOperUnshelve),
                LOperUnshelve = element.GetValue<bool>(L5XName.LOperUnshelve),
                LLOperUnshelve = element.GetValue<bool>(L5XName.LLOperUnshelve),
                ROCPosOperUnshelve = element.GetValue<bool>(L5XName.ROCPosOperUnshelve),
                ROCNegOperUnshelve = element.GetValue<bool>(L5XName.ROCNegOperUnshelve),
                ProgDisable = element.GetValue<bool>(L5XName.ProgDisable),
                OperDisable = element.GetValue<bool>(L5XName.OperDisable),
                ProgEnable = element.GetValue<bool>(L5XName.ProgEnable),
                OperEnable = element.GetValue<bool>(L5XName.OperEnable),
                AlarmCountReset = element.GetValue<bool>(L5XName.AlarmCountReset),
                HHMinDurationEnable = element.GetValue<bool>(L5XName.HHMinDurationEnable),
                HMinDurationEnable = element.GetValue<bool>(L5XName.HMinDurationEnable),
                LMinDurationEnable = element.GetValue<bool>(L5XName.LMinDurationEnable),
                LLMinDurationEnable = element.GetValue<bool>(L5XName.LLMinDurationEnable),
                In = element.GetValue<float>(L5XName.In),
                HHLimit = element.GetValue<float>(L5XName.HHLimit),
                HHSeverity = element.GetValue<int>(L5XName.HHSeverity),
                HLimit = element.GetValue<float>(L5XName.HLimit),
                HSeverity = element.GetValue<int>(L5XName.HSeverity),
                LLimit = element.GetValue<float>(L5XName.LLimit),
                LSeverity = element.GetValue<int>(L5XName.LSeverity),
                LLLimit = element.GetValue<float>(L5XName.LLLimit),
                LLSeverity = element.GetValue<int>(L5XName.LLSeverity),
                MinDurationPRE = element.GetValue<int>(L5XName.MinDurationPRE),
                ShelveDuration = element.GetValue<int>(L5XName.ShelveDuration),
                MaxShelveDuration = element.GetValue<int>(L5XName.MaxShelveDuration),
                Deadband = element.GetValue<float>(L5XName.Deadband),
                ROCPosLimit = element.GetValue<float>(L5XName.ROCPosLimit),
                ROCPosSeverity = element.GetValue<int>(L5XName.ROCPosSeverity),
                ROCNegLimit = element.GetValue<float>(L5XName.ROCNegLimit),
                ROCNegSeverity = element.GetValue<int>(L5XName.ROCNegSeverity),
                ROCPeriod = element.GetValue<float>(L5XName.ROCPeriod)
            };
        }
    }
}