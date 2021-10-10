using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    public class TaskSerializer : IComponentSerializer<ITask>
    {
        public XElement Serialize(ITask component)
        {
            var element = new XElement(L5XNames.Components.Task);
            element.Add(component.ToXAttribute(c => c.Name));
            element.Add(component.ToXAttribute(c => c.Type));
            element.Add(component.ToXAttribute(c => c.Priority));

            if (component is PeriodicTask periodicTask)
                element.Add(periodicTask.ToXAttribute(c => c.Rate));

            element.Add(component.ToXAttribute(c => c.Watchdog));
            element.Add(component.ToXAttribute(c => c.InhibitTask));
            element.Add(component.ToXAttribute(c => c.DisableUpdateOutputs));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToXElement(c => c.Description));

            var programs = component.ScheduledPrograms.ToList();
            if (programs.Count <= 0) return element;

            var scheduled = new XElement(nameof(component.ScheduledPrograms));
            
            foreach (var program in programs)
                scheduled.Add(new XElement("ScheduledProgram",
                    new XAttribute(L5XNames.Attributes.Name, program)));

            element.Add(scheduled);

            return element;
        }

        public ITask Deserialize(XElement element)
        {
            var type = element.GetTaskType();
            if (type == null)
                throw new InvalidOperationException();

            var task = type.Create(element);

            var programs = element.Descendants("ScheduledProgram").Select(e => e.GetName());
            foreach (var program in programs)
                task.AddProgram(program);

            return task;
        }
    }
}