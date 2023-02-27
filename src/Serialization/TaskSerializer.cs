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
    /// A logix serializer that performs serialization of <see cref="Task"/> components.
    /// </summary>
    public class TaskSerializer : ILogixSerializer<Task>
    {
        /// <inheritdoc />
        public XElement Serialize(Task obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Task);

            element.AddValue(obj, t => t.Name);
            element.AddValue(obj.Type.Value, L5XName.Type);
            if (obj.Type != TaskType.Continuous)
                element.AddValue(obj, t => t.Rate);
            element.AddValue(obj, t => t.Priority);
            element.AddValue(obj, t => t.Watchdog);
            element.AddValue(obj, t => t.DisableUpdateOutputs);
            element.AddValue(obj, t => t.InhibitTask);
            element.AddText(obj, t => t.Description);

            if (!obj.ScheduledPrograms.Any()) return element;
            var programs = new XElement(L5XName.ScheduledPrograms);
            programs.Add(obj.ScheduledPrograms.Select(p =>
                new XElement(L5XName.ScheduledProgram, new XAttribute(L5XName.Name, p))));
            element.Add(programs);

            return element;
        }

        /// <inheritdoc />
        public Task Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new Task
            {
                Name = element.LogixName(),
                Type = element.Value<TaskType>(L5XName.Type),
                Rate = element.ValueOrDefault<ScanRate?>(L5XName.Rate) ?? default,
                Priority = element.ValueOrDefault<TaskPriority?>(L5XName.Priority) ?? default,
                Watchdog = element.ValueOrDefault<Watchdog?>(L5XName.Watchdog) ?? default,
                DisableUpdateOutputs = element.Value<bool>(L5XName.DisableUpdateOutputs),
                InhibitTask = element.Value<bool>(L5XName.InhibitTask),
                ScheduledPrograms = element.Descendants(L5XName.ScheduledProgram).Select(p => p.LogixName()).ToList()
            };
        }
    }
}