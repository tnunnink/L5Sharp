using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    public class TaskSerializer : LogixSerializer<Task>
    {
        /// <inheritdoc />
        public override XElement Serialize(Task obj)
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

            if (!obj.ScheduledPrograms.Any())
                return element;

            var scheduled = new XElement(L5XName.ScheduledPrograms);
            scheduled.Add(obj.ScheduledPrograms.Select(p =>
                new XElement(L5XName.ScheduledProgram, new XAttribute(L5XName.Name, p))));
            element.Add(scheduled);

            return element;
        }

        /// <inheritdoc />
        public override Task Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new Task
            {
                Name = element.Attribute(L5XName.Name)!.Value,
                Type = GetRequiredAttribute(element, t => t.Type).Parse<TaskType>(),
                Rate = GetOptionalAttribute(element, t => t.Rate)?.Parse<ScanRate>() ?? default,
                Priority = GetOptionalAttribute(element, t => t.Rate)?.Parse<TaskPriority>() ?? default,
                Watchdog = GetOptionalAttribute(element, t => t.Rate)?.Parse<Watchdog>() ?? default,
                DisableUpdateOutputs = GetRequiredAttribute(element, t => t.DisableUpdateOutputs).Parse<bool>(),
                InhibitTask = GetRequiredAttribute(element, t => t.DisableUpdateOutputs).Parse<bool>(),
                ScheduledPrograms = element.Descendants(L5XName.ScheduledProgram)
                    .Select(p => GetRequiredAttribute(p, L5XName.Name)).ToList(),
            };

            /*
            var infoElement = element.Element(L5XName.EventInfo);

            if (infoElement is not null)
            {
                var eventInfo = new TaskEventInfo
                {
                    EventTrigger = infoElement.Attribute(L5XName.EventTrigger)?.Value.Parse<TaskEventTrigger>(),
                    EnableTimeout = infoElement.Attribute(L5XName.EnableTimeout)?.Value?.Parse<bool>() ?? default,
                    EventTag = infoElement.Attribute(L5XName.EventTag)?.Value
                };
            }*/
        }

        /// <inheritdoc />
        protected override void Configure(ISerializationConfiguration<Task> configuration)
        {
            configuration.Register(t => t.Name).AsAttribute();
            
            configuration.Register(t => t.Watchdog)
                .As(e => e.Descendants(L5XName.EventInfo).First())
                .HasConvertion(Watchdog.Parse, w => w.ToString());

            configuration
                .Register(t => t.Type, e => e.Attribute(L5XName.Type), TaskType.FromValue, t => t.ToString())
                .Register(t => t.Description, e => e.Element(L5XName.Description));
            //Register(t => t.Rate, e => e.Attribute(L5XName.Rate), ScanRate.Parse, r => r.ToString());
        }
    }
}