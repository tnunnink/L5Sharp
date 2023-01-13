using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public class Task : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets the type of the task component (Continuous, Periodic, Event).
        /// </summary>
        /// <value>A <see cref="Enums.TaskType"/> enum representing the type of the task.</value>
        public TaskType Type { get; set; } = TaskType.Periodic;

        /// <summary>
        /// The scan priority of the task component. Default of 10.
        /// </summary>
        /// <value>>A <see cref="TaskPriority"/> value type representing the <see cref="int"/> priority of the task.</value>
        public TaskPriority Priority { get; set; } = new(10);

        /// <summary>
        /// The scan rate (ms) of the task component. Default of 10.
        /// </summary>
        /// <value>>A <see cref="ScanRate"/> value type representing the <see cref="float"/> rate of the task.</value>
        public ScanRate Rate { get; set; } = new(10);

        /// <summary>
        /// The watchdog rate (ms) of the task component. Default of 500.
        /// </summary>
        /// <value>>A <see cref="Watchdog"/> value type representing the <see cref="float"/> watchdog of the task.</value>
        public Watchdog Watchdog { get; set; } = new(500);

        /// <summary>
        /// The value indicating whether the task is inhibited.
        /// </summary>
        /// <value>A <see cref="bool"/>; <c>true</c> if the task is inhibited; otherwise <c>false</c>.</value>
        public bool InhibitTask { get; set; }

        /// <summary>
        /// The value indicating whether the task is set to disable updating output values.
        /// </summary>
        /// <value>A <see cref="bool"/>; <c>true</c> if the task has disabled update outputs; otherwise <c>false</c>.</value>
        public bool DisableUpdateOutputs { get; set; }

        /// <summary>
        /// The collection of program names that are scheduled to the task.
        /// </summary>
        /// <value>A <see cref="List{T}"/> containing the string program names.</value>
        public List<string> ScheduledPrograms { get; set; } = new();

        /// <summary>
        /// The <see cref="TaskEventInfo"/> properties that specify the configuration for a event type task.
        /// </summary>
        /// <value>A <see cref="TaskEventInfo"/>object instance.</value>
        public TaskEventInfo? EventInfo { get; set; } = null;

        /// <inheritdoc />
        public XElement Serialize()
        {
            var element = new XElement(L5XName.Task);

            element.AddComponentName(Name);
            element.AddComponentDescription(Description);
            element.Add(new XAttribute(L5XName.Type, Type.Value));
            if (Type != TaskType.Continuous)
                element.Add(new XAttribute(L5XName.Rate, Rate));
            element.Add(new XAttribute(L5XName.Priority, Priority));
            element.Add(new XAttribute(L5XName.Watchdog, Watchdog));
            element.Add(new XAttribute(L5XName.DisableUpdateOutputs, DisableUpdateOutputs));
            element.Add(new XAttribute(L5XName.InhibitTask, InhibitTask));

            if (!ScheduledPrograms.Any())
                return element;

            var scheduled = new XElement(L5XName.ScheduledPrograms);

            scheduled.Add(ScheduledPrograms
                .Select(p => new XElement(L5XName.ScheduledProgram,
                    new XAttribute(L5XName.Name, p))));

            element.Add(scheduled);

            return element;
        }

        /// <inheritdoc />
        public void Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != L5XName.Task)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            Name = element.ComponentName();
            Description = element.ComponentDescription();
            Type = element.Attribute(L5XName.Type)?.Value.Parse<TaskType>() ?? throw new XmlException();
            Rate = element.Attribute(L5XName.Rate)?.Value?.Parse<ScanRate>() ?? default;
            Priority = element.Attribute(L5XName.Priority)?.Value?.Parse<TaskPriority>() ?? default;
            Watchdog = element.Attribute(L5XName.Watchdog)?.Value?.Parse<Watchdog>() ?? default;
            DisableUpdateOutputs = element.Attribute(L5XName.DisableUpdateOutputs)?.Value?.Parse<bool>() ?? default;
            InhibitTask = element.Attribute(L5XName.InhibitTask)?.Value?.Parse<bool>() ?? default;
            ScheduledPrograms = element.Descendants(L5XName.ScheduledProgram).Select(e => e.ComponentName()).ToList();

            var infoElement = element.Element(L5XName.EventInfo);

            if (infoElement is not null)
            {
                EventInfo = new TaskEventInfo
                {
                    EventTrigger = infoElement.Attribute(L5XName.EventTrigger)?.Value.Parse<TaskEventTrigger>(),
                    EnableTimeout = infoElement.Attribute(L5XName.EnableTimeout)?.Value?.Parse<bool>() ?? default,
                    EventTag = infoElement.Attribute(L5XName.EventTag)?.Value
                };
            }
        }
    }
}