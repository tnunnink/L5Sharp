using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class TaskSerializer : L5XSerializer<ITask>
    {
        private static readonly XName ElementName = L5XElement.Task.ToString();

        public override XElement Serialize(ITask component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XAttribute.Type.ToString(), component.Type.Value));
            if (component.Type != TaskType.Continuous)
                element.Add(new XAttribute(L5XAttribute.Rate.ToString(), component.Rate));
            element.Add(new XAttribute(L5XAttribute.Priority.ToString(), component.Priority));
            element.Add(new XAttribute(L5XAttribute.Watchdog.ToString(), component.Watchdog));
            element.Add(new XAttribute(L5XAttribute.DisableUpdateOutputs.ToString(), component.DisableUpdateOutputs));
            element.Add(new XAttribute(L5XAttribute.InhibitTask.ToString(), component.InhibitTask));

            if (!component.ScheduledPrograms.Any()) return element;

            var scheduled = new XElement(L5XElement.ScheduledPrograms.ToString());

            scheduled.Add(component.ScheduledPrograms
                .Select(p => new XElement(L5XElement.ScheduledProgram.ToString(),
                    new XAttribute(L5XAttribute.Name.ToString(), p))));

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
            var type = element.Attribute(L5XAttribute.Type.ToString())?.Value.Parse<TaskType>();
            var rate = element.Attribute(L5XAttribute.Rate.ToString())?.Value?.Parse<ScanRate>() ?? default;
            var priority = element.Attribute(L5XAttribute.Priority.ToString())?.Value?.Parse<TaskPriority>() ?? default;
            var watchdog = element.Attribute(L5XAttribute.Watchdog.ToString())?.Value?.Parse<Watchdog>() ?? default;
            var disableUpdateOutputs =
                element.Attribute(L5XAttribute.DisableUpdateOutputs.ToString())?.Value?.Parse<bool>() ?? default;
            var inhibitTask = element.Attribute(L5XAttribute.InhibitTask.ToString())?.Value?.Parse<bool>() ?? default;

            var programs = element.Descendants(L5XElement.ScheduledProgram.ToString()).Select(e => e.ComponentName());

            if (type == TaskType.Continuous)
                return new ContinuousTask(name, rate, priority, watchdog, disableUpdateOutputs,
                    inhibitTask, programs, description);

            if (type == TaskType.Periodic)
                return new PeriodicTask(name, rate, priority, watchdog, disableUpdateOutputs,
                    inhibitTask, programs, description);

            var eventInfo = element.Element(L5XElement.EventInfo.ToString());
            var eventTrigger = eventInfo?.Attribute(L5XAttribute.EventTrigger.ToString())
                ?.Value.Parse<TaskEventTrigger>();
            var enableTimeout = eventInfo?.Attribute(L5XAttribute.EnableTimeout.ToString())
                ?.Value?.Parse<bool>() ?? default;
            var eventTag = eventInfo?.Attribute(L5XAttribute.EventTag.ToString())
                ?.Value;

            return new EventTask(name, rate, priority, watchdog, disableUpdateOutputs,
                inhibitTask, programs, eventTrigger, enableTimeout, eventTag, description);
        }
    }
}