using System;
using System.Xml.Linq;
using L5Sharp.L5X;
using L5Sharp.Types;

namespace L5Sharp.Serialization.Data
{
    internal class AlarmAnalogParametersSerializer : L5XSerializer<ALARM_ANALOG>
    {
        private static readonly XName ElementName = L5XElement.AlarmAnalogParameters.ToString();
        
        public override XElement Serialize(ALARM_ANALOG component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.Add(new XAttribute(nameof(component.EnableIn), component.EnableIn.DataType.Value));
            element.Add(new XAttribute(nameof(component.InFault), component.InFault.DataType.Value));
            element.Add(new XAttribute(nameof(component.HHEnabled), component.HHEnabled.DataType.Value));
            element.Add(new XAttribute(nameof(component.HEnabled), component.HEnabled.DataType.Value));
            element.Add(new XAttribute(nameof(component.LEnabled), component.LEnabled.DataType.Value));
            element.Add(new XAttribute(nameof(component.LLEnabled), component.LLEnabled.DataType.Value));
            element.Add(new XAttribute(nameof(component.AckRequired), component.AckRequired.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgAckAll), component.ProgAckAll.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperAckAll), component.OperAckAll.DataType.Value));
            element.Add(new XAttribute(nameof(component.HHProgAck), component.HHProgAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.HHOperAck), component.HHOperAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.HProgAck), component.HProgAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.HOperAck), component.HOperAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.LProgAck), component.LProgAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.LOperAck), component.LOperAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.LLProgAck), component.LLProgAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.LLOperAck), component.LLOperAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCPosProgAck), component.ROCPosProgAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCPosOperAck), component.ROCPosOperAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCNegProgAck), component.ROCNegProgAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCNegOperAck), component.ROCNegOperAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgSuppress), component.ProgSuppress.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperSuppress), component.OperSuppress.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgUnsuppress), component.ProgUnsuppress.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperUnsuppress), component.OperUnsuppress.DataType.Value));
            element.Add(new XAttribute(nameof(component.HHOperShelve), component.HHOperShelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.HOperShelve), component.HOperShelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.LOperShelve), component.LOperShelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.LLOperShelve), component.LLOperShelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCPosOperShelve), component.ROCPosOperShelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCNegOperShelve), component.ROCNegOperShelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgUnshelveAll), component.ProgUnshelveAll.DataType.Value));
            element.Add(new XAttribute(nameof(component.HHOperUnshelve), component.HHOperUnshelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.HOperUnshelve), component.HOperUnshelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.LOperUnshelve), component.LOperUnshelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.LLOperUnshelve), component.LLOperUnshelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCPosOperUnshelve), component.ROCPosOperUnshelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCNegOperUnshelve), component.ROCNegOperUnshelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgDisable), component.ProgDisable.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperDisable), component.OperDisable.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgEnable), component.ProgEnable.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperEnable), component.OperEnable.DataType.Value));
            element.Add(new XAttribute(nameof(component.AlarmCountReset), component.AlarmCountReset.DataType.Value));
            element.Add(new XAttribute(nameof(component.HHMinDurationEnable), component.HHMinDurationEnable.DataType.Value));
            element.Add(new XAttribute(nameof(component.HMinDurationEnable), component.HMinDurationEnable.DataType.Value));
            element.Add(new XAttribute(nameof(component.LMinDurationEnable), component.LMinDurationEnable.DataType.Value));
            element.Add(new XAttribute(nameof(component.LLMinDurationEnable), component.LLMinDurationEnable.DataType.Value));
            element.Add(new XAttribute(nameof(component.In), component.In.DataType.Value));
            element.Add(new XAttribute(nameof(component.HHLimit), component.HHLimit.DataType.Value));
            element.Add(new XAttribute(nameof(component.HHSeverity), component.HHSeverity.DataType.Value));
            element.Add(new XAttribute(nameof(component.HLimit), component.HLimit.DataType.Value));
            element.Add(new XAttribute(nameof(component.HSeverity), component.HSeverity.DataType.Value));
            element.Add(new XAttribute(nameof(component.LLimit), component.LLimit.DataType.Value));
            element.Add(new XAttribute(nameof(component.LSeverity), component.LSeverity.DataType.Value));
            element.Add(new XAttribute(nameof(component.LLLimit), component.LLLimit.DataType.Value));
            element.Add(new XAttribute(nameof(component.LLSeverity), component.LLSeverity.DataType.Value));
            element.Add(new XAttribute(nameof(component.MinDurationPRE), component.MinDurationPRE.DataType.Value));
            element.Add(new XAttribute(nameof(component.ShelveDuration), component.ShelveDuration.DataType.Value));
            element.Add(new XAttribute(nameof(component.MaxShelveDuration), component.MaxShelveDuration.DataType.Value));
            element.Add(new XAttribute(nameof(component.Deadband), component.Deadband.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCPosLimit), component.ROCPosLimit.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCPosSeverity), component.ROCPosSeverity.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCNegLimit), component.ROCNegLimit.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCNegSeverity), component.ROCNegSeverity.DataType.Value));
            element.Add(new XAttribute(nameof(component.ROCPeriod), component.ROCPeriod.DataType.Value));

            return element;
        }

        public override ALARM_ANALOG Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var type = new ALARM_ANALOG();

            type.EnableIn.DataType.SetValue(element.Attribute(nameof(type.EnableIn))?.Value!);
            type.InFault.DataType.SetValue(element.Attribute(nameof(type.InFault))?.Value!);
            type.HHEnabled.DataType.SetValue(element.Attribute(nameof(type.HHEnabled))?.Value!);
            type.HEnabled.DataType.SetValue(element.Attribute(nameof(type.HEnabled))?.Value!);
            type.LEnabled.DataType.SetValue(element.Attribute(nameof(type.LEnabled))?.Value!);
            type.LLEnabled.DataType.SetValue(element.Attribute(nameof(type.LLEnabled))?.Value!);
            type.AckRequired.DataType.SetValue(element.Attribute(nameof(type.AckRequired))?.Value!);
            type.ProgAckAll.DataType.SetValue(element.Attribute(nameof(type.ProgAckAll))?.Value!);
            type.OperAckAll.DataType.SetValue(element.Attribute(nameof(type.OperAckAll))?.Value!);
            type.HHProgAck.DataType.SetValue(element.Attribute(nameof(type.HHProgAck))?.Value!);
            type.HHOperAck.DataType.SetValue(element.Attribute(nameof(type.HHOperAck))?.Value!);
            type.HProgAck.DataType.SetValue(element.Attribute(nameof(type.HProgAck))?.Value!);
            type.HOperAck.DataType.SetValue(element.Attribute(nameof(type.HOperAck))?.Value!);
            type.LProgAck.DataType.SetValue(element.Attribute(nameof(type.LProgAck))?.Value!);
            type.LOperAck.DataType.SetValue(element.Attribute(nameof(type.LOperAck))?.Value!);
            type.LLProgAck.DataType.SetValue(element.Attribute(nameof(type.LLProgAck))?.Value!);
            type.LLOperAck.DataType.SetValue(element.Attribute(nameof(type.LLOperAck))?.Value!);
            type.ROCPosProgAck.DataType.SetValue(element.Attribute(nameof(type.ROCPosProgAck))?.Value!);
            type.ROCPosOperAck.DataType.SetValue(element.Attribute(nameof(type.ROCPosOperAck))?.Value!);
            type.ROCNegProgAck.DataType.SetValue(element.Attribute(nameof(type.ROCNegProgAck))?.Value!);
            type.ROCNegOperAck.DataType.SetValue(element.Attribute(nameof(type.ROCNegOperAck))?.Value!);
            type.ProgSuppress.DataType.SetValue(element.Attribute(nameof(type.ProgSuppress))?.Value!);
            type.OperSuppress.DataType.SetValue(element.Attribute(nameof(type.OperSuppress))?.Value!);
            type.ProgUnsuppress.DataType.SetValue(element.Attribute(nameof(type.ProgUnsuppress))?.Value!);
            type.OperUnsuppress.DataType.SetValue(element.Attribute(nameof(type.OperUnsuppress))?.Value!);
            type.HHOperShelve.DataType.SetValue(element.Attribute(nameof(type.HHOperShelve))?.Value!);
            type.HOperShelve.DataType.SetValue(element.Attribute(nameof(type.HOperShelve))?.Value!);
            type.LOperShelve.DataType.SetValue(element.Attribute(nameof(type.LOperShelve))?.Value!);
            type.LLOperShelve.DataType.SetValue(element.Attribute(nameof(type.LLOperShelve))?.Value!);
            type.ROCPosOperShelve.DataType.SetValue(element.Attribute(nameof(type.ROCPosOperShelve))?.Value!);
            type.ROCNegOperShelve.DataType.SetValue(element.Attribute(nameof(type.ROCNegOperShelve))?.Value!);
            type.ProgUnshelveAll.DataType.SetValue(element.Attribute(nameof(type.ProgUnshelveAll))?.Value!);
            type.HHOperUnshelve.DataType.SetValue(element.Attribute(nameof(type.HHOperUnshelve))?.Value!);
            type.HOperUnshelve.DataType.SetValue(element.Attribute(nameof(type.HOperUnshelve))?.Value!);
            type.LOperUnshelve.DataType.SetValue(element.Attribute(nameof(type.LOperUnshelve))?.Value!);
            type.LLOperUnshelve.DataType.SetValue(element.Attribute(nameof(type.LLOperUnshelve))?.Value!);
            type.ROCPosOperUnshelve.DataType.SetValue(element.Attribute(nameof(type.ROCPosOperUnshelve))?.Value!);
            type.ROCNegOperUnshelve.DataType.SetValue(element.Attribute(nameof(type.ROCNegOperUnshelve))?.Value!);
            type.ProgDisable.DataType.SetValue(element.Attribute(nameof(type.ProgDisable))?.Value!);
            type.OperDisable.DataType.SetValue(element.Attribute(nameof(type.OperDisable))?.Value!);
            type.ProgEnable.DataType.SetValue(element.Attribute(nameof(type.ProgEnable))?.Value!);
            type.OperEnable.DataType.SetValue(element.Attribute(nameof(type.OperEnable))?.Value!);
            type.AlarmCountReset.DataType.SetValue(element.Attribute(nameof(type.AlarmCountReset))?.Value!);
            type.HHMinDurationEnable.DataType.SetValue(element.Attribute(nameof(type.HHMinDurationEnable))?.Value!);
            type.HMinDurationEnable.DataType.SetValue(element.Attribute(nameof(type.HMinDurationEnable))?.Value!);
            type.LMinDurationEnable.DataType.SetValue(element.Attribute(nameof(type.LMinDurationEnable))?.Value!);
            type.LLMinDurationEnable.DataType.SetValue(element.Attribute(nameof(type.LLMinDurationEnable))?.Value!);
            type.In.DataType.SetValue(element.Attribute(nameof(type.In))?.Value!);
            type.HHLimit.DataType.SetValue(element.Attribute(nameof(type.HHLimit))?.Value!);
            type.HHSeverity.DataType.SetValue(element.Attribute(nameof(type.HHSeverity))?.Value!);
            type.HLimit.DataType.SetValue(element.Attribute(nameof(type.HLimit))?.Value!);
            type.HSeverity.DataType.SetValue(element.Attribute(nameof(type.HSeverity))?.Value!);
            type.LLimit.DataType.SetValue(element.Attribute(nameof(type.LLimit))?.Value!);
            type.LSeverity.DataType.SetValue(element.Attribute(nameof(type.LSeverity))?.Value!);
            type.LLLimit.DataType.SetValue(element.Attribute(nameof(type.LLLimit))?.Value!);
            type.LLSeverity.DataType.SetValue(element.Attribute(nameof(type.LLSeverity))?.Value!);
            type.MinDurationPRE.DataType.SetValue(element.Attribute(nameof(type.MinDurationPRE))?.Value!);
            type.ShelveDuration.DataType.SetValue(element.Attribute(nameof(type.ShelveDuration))?.Value!);
            type.MaxShelveDuration.DataType.SetValue(element.Attribute(nameof(type.MaxShelveDuration))?.Value!);
            type.Deadband.DataType.SetValue(element.Attribute(nameof(type.Deadband))?.Value!);
            type.ROCPosLimit.DataType.SetValue(element.Attribute(nameof(type.ROCPosLimit))?.Value!);
            type.ROCPosSeverity.DataType.SetValue(element.Attribute(nameof(type.ROCPosSeverity))?.Value!);
            type.ROCNegLimit.DataType.SetValue(element.Attribute(nameof(type.ROCNegLimit))?.Value!);
            type.ROCNegSeverity.DataType.SetValue(element.Attribute(nameof(type.ROCNegSeverity))?.Value!);
            type.ROCPeriod.DataType.SetValue(element.Attribute(nameof(type.ROCPeriod))?.Value!);

            return type;
        }
    }
}