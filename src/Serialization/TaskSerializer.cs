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
    }
}