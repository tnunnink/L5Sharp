using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class TaskSerializer : IL5XSerializer<ITask>
    {
        private static readonly XName ElementName = L5XElement.Task.ToXName();
        
        public XElement Serialize(ITask component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.Type.Value, nameOverride: nameof(component.Type));
            element.AddAttribute(component, c => c.Rate, t => t.Type != TaskType.Continuous);
            element.AddAttribute(component, c => c.Priority);
            element.AddAttribute(component, c => c.Watchdog);
            element.AddAttribute(component, c => c.DisableUpdateOutputs);
            element.AddAttribute(component, c => c.InhibitTask);

            if (!component.ScheduledPrograms.Any()) return element;

            var scheduled = new XElement(nameof(component.ScheduledPrograms));
            
            scheduled.Add(component.ScheduledPrograms
                .Select(p => new XElement(L5XElement.ScheduledProgram.ToXName(),
                    new XAttribute(L5XAttribute.Name.ToString(), p))));
            
            element.Add(scheduled);

            return element;
        }

        public ITask Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            
            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var type = element.GetAttribute<ITask, TaskType>(t => t.Type);
            var rate = element.GetAttribute<ITask, ScanRate>(t => t.Rate);
            var priority = element.GetAttribute<ITask, TaskPriority>(t => t.Priority);
            var watchdog = element.GetAttribute<ITask, Watchdog>(t => t.Watchdog);
            var disableUpdateOutputs = element.GetAttribute<ITask, bool>(t => t.DisableUpdateOutputs);
            var inhibitTask = element.GetAttribute<ITask, bool>(t => t.InhibitTask);
            var programs = element.Descendants(L5XElement.ScheduledProgram.ToXName()).Select(e => e.ComponentName());

            if (type is null)
                throw new ArgumentException("Provided element must have a task type attribute");

            if (type == TaskType.Continuous)
                return new ContinuousTask(name, rate, priority, watchdog, disableUpdateOutputs,
                    inhibitTask, programs, description);

            if (type == TaskType.Periodic)
                return new PeriodicTask(name, rate, priority, watchdog, disableUpdateOutputs,
                    inhibitTask, programs, description);

            var eventInfo = element.Element(L5XElement.EventInfo.ToXName());
            var eventTrigger = eventInfo?.GetAttribute<EventTask, TaskEventTrigger>(t => t.EventTrigger);
            var enableTimeout = eventInfo?.GetAttribute<EventTask, bool>(t => t.EnableTimeout) ?? false;
            var eventTag = eventInfo?.GetAttribute<EventTask, string?>(t => t.EventTag);

            return new EventTask(name, rate, priority, watchdog, disableUpdateOutputs,
                inhibitTask, programs, eventTrigger, enableTimeout, eventTag, description);
        }
    }
}