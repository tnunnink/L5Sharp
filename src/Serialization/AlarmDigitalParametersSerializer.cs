using System;
using System.Xml.Linq;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class AlarmDigitalParametersSerializer : L5XSerializer<ALARM_DIGITAL>
    {
        private static readonly XName ElementName = L5XName.AlarmDigitalParameters;
        
        public override XElement Serialize(ALARM_DIGITAL component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.Add(new XAttribute(nameof(component.Severity), component.Severity.DataType.Value));
            element.Add(new XAttribute(nameof(component.MinDurationPRE), component.MinDurationPRE.DataType.Value));
            element.Add(new XAttribute(nameof(component.ShelveDuration), component.ShelveDuration.DataType.Value));
            element.Add(new XAttribute(nameof(component.MaxShelveDuration), component.MaxShelveDuration.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgTime), component.ProgTime.DataType.Value));
            element.Add(new XAttribute(nameof(component.EnableIn), component.EnableIn.DataType.Value));
            element.Add(new XAttribute(nameof(component.In), component.In.DataType.Value));
            element.Add(new XAttribute(nameof(component.InFault), component.InFault.DataType.Value));
            element.Add(new XAttribute(nameof(component.Condition), component.Condition.DataType.Value));
            element.Add(new XAttribute(nameof(component.AckRequired), component.AckRequired.DataType.Value));
            element.Add(new XAttribute(nameof(component.Latched), component.Latched.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgAck), component.ProgAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperAck), component.OperAck.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgReset), component.ProgReset.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperReset), component.OperReset.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgSuppress), component.ProgSuppress.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperSuppress), component.OperSuppress.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgUnsuppress), component.ProgUnsuppress.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperUnsuppress), component.OperUnsuppress.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperShelve), component.OperShelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgUnshelve), component.ProgUnshelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperUnshelve), component.OperUnshelve.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgDisable), component.ProgDisable.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperDisable), component.OperDisable.DataType.Value));
            element.Add(new XAttribute(nameof(component.ProgEnable), component.ProgEnable.DataType.Value));
            element.Add(new XAttribute(nameof(component.OperEnable), component.OperEnable.DataType.Value));
            element.Add(new XAttribute(nameof(component.AlarmCountReset), component.AlarmCountReset.DataType.Value));
            element.Add(new XAttribute(nameof(component.UseProgTime), component.UseProgTime.DataType.Value));

            return element;
        }

        public override ALARM_DIGITAL Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var type = new ALARM_DIGITAL();
            
            type.Severity.DataType.SetValue(element.Attribute(nameof(type.Severity))?.Value!);
            type.MinDurationPRE.DataType.SetValue(element.Attribute(nameof(type.MinDurationPRE))?.Value!);
            type.ShelveDuration.DataType.SetValue(element.Attribute(nameof(type.ShelveDuration))?.Value!);
            type.MaxShelveDuration.DataType.SetValue(element.Attribute(nameof(type.MaxShelveDuration))?.Value!);
            type.ProgTime.DataType.SetValue(element.Attribute(nameof(type.ProgTime))?.Value!);
            type.EnableIn.DataType.SetValue(element.Attribute(nameof(type.EnableIn))?.Value!);
            type.In.DataType.SetValue(element.Attribute(nameof(type.In))?.Value!);
            type.InFault.DataType.SetValue(element.Attribute(nameof(type.InFault))?.Value!);
            type.Condition.DataType.SetValue(element.Attribute(nameof(type.Condition))?.Value!);
            type.AckRequired.DataType.SetValue(element.Attribute(nameof(type.AckRequired))?.Value!);
            type.Latched.DataType.SetValue(element.Attribute(nameof(type.Latched))?.Value!);
            type.ProgAck.DataType.SetValue(element.Attribute(nameof(type.ProgAck))?.Value!);
            type.OperAck.DataType.SetValue(element.Attribute(nameof(type.OperAck))?.Value!);
            type.ProgReset.DataType.SetValue(element.Attribute(nameof(type.ProgReset))?.Value!);
            type.OperReset.DataType.SetValue(element.Attribute(nameof(type.OperReset))?.Value!);
            type.ProgSuppress.DataType.SetValue(element.Attribute(nameof(type.ProgSuppress))?.Value!);
            type.OperSuppress.DataType.SetValue(element.Attribute(nameof(type.OperSuppress))?.Value!);
            type.ProgUnsuppress.DataType.SetValue(element.Attribute(nameof(type.ProgUnsuppress))?.Value!);
            type.OperUnsuppress.DataType.SetValue(element.Attribute(nameof(type.OperUnsuppress))?.Value!);
            type.OperShelve.DataType.SetValue(element.Attribute(nameof(type.OperShelve))?.Value!);
            type.ProgUnshelve.DataType.SetValue(element.Attribute(nameof(type.ProgUnshelve))?.Value!);
            type.OperUnshelve.DataType.SetValue(element.Attribute(nameof(type.OperUnshelve))?.Value!);
            type.ProgDisable.DataType.SetValue(element.Attribute(nameof(type.ProgDisable))?.Value!);
            type.OperDisable.DataType.SetValue(element.Attribute(nameof(type.OperDisable))?.Value!);
            type.ProgEnable.DataType.SetValue(element.Attribute(nameof(type.ProgEnable))?.Value!);
            type.OperEnable.DataType.SetValue(element.Attribute(nameof(type.OperEnable))?.Value!);
            type.AlarmCountReset.DataType.SetValue(element.Attribute(nameof(type.AlarmCountReset))?.Value!);
            type.UseProgTime.DataType.SetValue(element.Attribute(nameof(type.UseProgTime))?.Value!);

            return type;
        }
    }
}