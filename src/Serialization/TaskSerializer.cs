using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class TaskSerializer : IXSerializer<ITask>
    {
        private static readonly string ScheduledProgram = nameof(ScheduledProgram);
        private static readonly string Name = nameof(Name);
        
        public XElement Serialize(ITask component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));
            
            var element = new XElement(LogixNames.Task);
            element.Add(component.ToAttribute(c => c.Name));
            element.Add(component.ToAttribute(c => c.Type));
            element.Add(component.ToAttribute(c => c.Priority));
            element.Add(component.ToAttribute(c => c.Rate));
            element.Add(component.ToAttribute(c => c.Watchdog));
            element.Add(component.ToAttribute(c => c.InhibitTask));
            element.Add(component.ToAttribute(c => c.DisableUpdateOutputs));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToElement(c => c.Description));

            var programs = component.ScheduledPrograms.ToList();
            if (programs.Count <= 0) return element;

            var scheduled = new XElement(nameof(component.ScheduledPrograms));

            foreach (var program in programs)
                scheduled.Add(new XElement(ScheduledProgram, new XAttribute(Name, program)));

            element.Add(scheduled);

            return element;
        }

        public ITask Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var name = element.GetName();
            var description = element.GetValue<ITask, string>(m => m.Description);
            var type = element.GetValue<ITask, TaskType>(t => t.Type);
            var priority = element.GetValue<ITask, TaskPriority>(t => t.Priority);
            var rate = element.GetValue<ITask, ScanRate>(t => t.Rate);
            var watchdog = element.GetValue<ITask, Watchdog>(t => t.Watchdog);
            var inhibitTask = element.GetValue<ITask, bool>(t => t.InhibitTask);
            var disableUpdateOutputs = element.GetValue<ITask, bool>(t => t.DisableUpdateOutputs);

            var task = new Task(name, type, priority, rate, watchdog, inhibitTask, disableUpdateOutputs, description);

            var programs = element.Descendants(ScheduledProgram).Select(e => e.GetName());

            foreach (var program in programs)
                task.ScheduleProgram(program);

            return task;
        }
    }
}