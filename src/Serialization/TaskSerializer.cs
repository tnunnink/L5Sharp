using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization
{
    public class TaskSerializer : IXSerializer<ITask>
    {
        private static readonly string ScheduledProgram = nameof(ScheduledProgram);
        private static readonly string Name = nameof(Name);
        
        public XElement Serialize(ITask component)
        {
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
            var description = element.GetDescription();
            var type = element.GetAttribute<ITask>(t => t.Type);
            var priority = element.GetAttribute<ITask>(t => t.Priority);
            var rate = element.GetAttribute<ITask>(t => t.Rate);
            var watchdog = element.GetAttribute<ITask>(t => t.Watchdog);
            var inhibitTask = element.GetAttribute<ITask>(t => t.InhibitTask);
            var disableUpdateOutputs = element.GetAttribute<ITask>(t => t.DisableUpdateOutputs);

            var task = Task.Create(name);

            var programs = element.Descendants("ScheduledProgram").Select(e => e.GetName());
            
            foreach (var program in programs)
                task.ScheduleProgram(program);

            return task;
        }
    }
}