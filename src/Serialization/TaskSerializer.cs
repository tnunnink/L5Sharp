using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskSerializer : ILogixSerializer<Task>
    {
        /// <inheritdoc />
        public XElement Serialize(Task obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Task);
            element.Add(new XAttribute(L5XName.Name, obj.Name));
            element.Add(new XAttribute(L5XName.Type, obj.Type.Value));
            if (obj.Type != TaskType.Continuous)
                element.Add(new XAttribute(L5XName.Rate, obj.Rate));
            element.Add(new XAttribute(L5XName.Priority, obj.Priority));
            element.Add(new XAttribute(L5XName.Watchdog, obj.Watchdog));
            element.Add(new XAttribute(L5XName.DisableUpdateOutputs, obj.DisableUpdateOutputs));
            element.Add(new XAttribute(L5XName.InhibitTask, obj.InhibitTask));

            if (!obj.Description.IsEmpty())
                element.Add(new XElement(L5XName.Description, new XCData(obj.Description)));

            if (obj.EventInfo is not null)
            {
                var eventInfo = new XElement(L5XName.EventInfo);
                eventInfo.Add(new XAttribute(L5XName.EventTrigger, obj.EventInfo.EventTrigger.Value));
                eventInfo.Add(new XAttribute(L5XName.EnableTimeout, obj.EventInfo.EnableTimeout));

                if (obj.EventInfo.EventTag is not null)
                    eventInfo.Add(new XAttribute(L5XName.EventTag, obj.EventInfo.EventTag));
                
                element.Add(eventInfo);
            }

            if (!obj.ScheduledPrograms.Any()) return element;
            
            var scheduled = new XElement(L5XName.ScheduledPrograms);
            scheduled.Add(obj.ScheduledPrograms.Select(p =>
                new XElement(L5XName.ScheduledProgram, new XAttribute(L5XName.Name, p))));
            element.Add(scheduled);

            return element;
        }

        /// <inheritdoc />
        public Task Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new Task
            {
                Name = element.ComponentName(),
                Type = element.Property<TaskType>(L5XName.Type),
                Rate = element.PropertyOrDefault<ScanRate?>(L5XName.Rate) ?? default,
                Priority = element.PropertyOrDefault<TaskPriority?>(L5XName.Priority) ?? default,
                Watchdog = element.PropertyOrDefault<Watchdog?>(L5XName.Watchdog) ?? default,
                DisableUpdateOutputs = element.Property<bool>(L5XName.DisableUpdateOutputs),
                InhibitTask = element.Property<bool>(L5XName.InhibitTask),
                ScheduledPrograms = element.Descendants(L5XName.ScheduledProgram).Select(p => p.ComponentName())
                    .ToList(),
                EventInfo = GetEventInfo(element)
            };
        }

        private static TaskEventInfo? GetEventInfo(XElement element)
        {
            var eventInfo = element.Element(L5XName.EventInfo);

            if (eventInfo is null)
                return null;
            
            return new TaskEventInfo
            {
                EventTrigger = element.Property<TaskEventTrigger>(L5XName.EventTrigger),
                EnableTimeout = element.Property<bool>(L5XName.EnableTimeout),
                EventTag = element.Property<string?>(L5XName.EventTag)
            };
        }
    }
}