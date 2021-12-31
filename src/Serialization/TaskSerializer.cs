using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

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

            element.AddValue(component, c => c.Name);
            element.AddValue(component, c => c.Description, true);
            element.AddValue(component, c => c.Type);
            element.AddValue(component, c => c.Rate);
            element.AddValue(component, c => c.Priority);
            element.AddValue(component, c => c.Watchdog);
            element.AddValue(component, c => c.InhibitTask);
            element.AddValue(component, c => c.DisableUpdateOutputs);

            var scheduled = new XElement(nameof(component.ScheduledPrograms));

            scheduled.Add(component.ScheduledPrograms
                .Select(p => new XElement(ScheduledProgram, new XAttribute(Name, p))));

            element.Add(scheduled);

            return element;
        }

        public ITask Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var name = element.GetComponentName();
            var description = element.GetValue<ITask, string>(m => m.Description);
            var type = element.GetValue<ITask, TaskType>(t => t.Type);
            var priority = element.GetValue<ITask, TaskPriority>(t => t.Priority);
            var rate = element.GetValue<ITask, ScanRate>(t => t.Rate);
            var watchdog = element.GetValue<ITask, Watchdog>(t => t.Watchdog);
            var inhibitTask = element.GetValue<ITask, bool>(t => t.InhibitTask);
            var disableUpdateOutputs = element.GetValue<ITask, bool>(t => t.DisableUpdateOutputs);
            
            //var eventInfoElement = element.Element(LogixNames.)
            
            var programs = element.Descendants(ScheduledProgram).Select(e => e.GetComponentName());

            var task = new Task(name, type, priority, rate, watchdog, 
                inhibitTask, disableUpdateOutputs, null, programs, description);

            return task;
        }
    }
}