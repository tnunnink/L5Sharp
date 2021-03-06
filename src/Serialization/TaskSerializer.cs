using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class TaskSerializer : L5XSerializer<ITask>
    {
        private static readonly XName ElementName = L5XName.Task;

        public override XElement Serialize(ITask component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XName.Type, component.Type.Value));
            if (component.Type != TaskType.Continuous)
                element.Add(new XAttribute(L5XName.Rate, component.Rate));
            element.Add(new XAttribute(L5XName.Priority, component.Priority));
            element.Add(new XAttribute(L5XName.Watchdog, component.Watchdog));
            element.Add(new XAttribute(L5XName.DisableUpdateOutputs, component.DisableUpdateOutputs));
            element.Add(new XAttribute(L5XName.InhibitTask, component.InhibitTask));

            if (!component.ScheduledPrograms.Any()) return element;

            var scheduled = new XElement(L5XName.ScheduledPrograms);

            scheduled.Add(component.ScheduledPrograms
                .Select(p => new XElement(L5XName.ScheduledProgram,
                    new XAttribute(L5XName.Name, p))));

            element.Add(scheduled);

            return element;
        }

        public override ITask Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var type = element.Attribute(L5XName.Type)?.Value.Parse<TaskType>();
            var rate = element.Attribute(L5XName.Rate)?.Value?.Parse<ScanRate>() ?? default;
            var priority = element.Attribute(L5XName.Priority)?.Value?.Parse<TaskPriority>() ?? default;
            var watchdog = element.Attribute(L5XName.Watchdog)?.Value?.Parse<Watchdog>() ?? default;
            var disableUpdateOutputs =
                element.Attribute(L5XName.DisableUpdateOutputs)?.Value?.Parse<bool>() ?? default;
            var inhibitTask = element.Attribute(L5XName.InhibitTask)?.Value?.Parse<bool>() ?? default;

            var programs = element.Descendants(L5XName.ScheduledProgram).Select(e => e.ComponentName());

            if (type == TaskType.Continuous)
                return new ContinuousTask(name, rate, priority, watchdog, disableUpdateOutputs,
                    inhibitTask, programs, description);

            if (type == TaskType.Periodic)
                return new PeriodicTask(name, rate, priority, watchdog, disableUpdateOutputs,
                    inhibitTask, programs, description);

            var eventInfo = element.Element(L5XName.EventInfo);
            var eventTrigger = eventInfo?.Attribute(L5XName.EventTrigger)
                ?.Value.Parse<TaskEventTrigger>();
            var enableTimeout = eventInfo?.Attribute(L5XName.EnableTimeout)
                ?.Value?.Parse<bool>() ?? default;
            var eventTag = eventInfo?.Attribute(L5XName.EventTag)
                ?.Value;

            return new EventTask(name, rate, priority, watchdog, disableUpdateOutputs,
                inhibitTask, programs, eventTrigger, enableTimeout, eventTag, description);
        }
    }
}