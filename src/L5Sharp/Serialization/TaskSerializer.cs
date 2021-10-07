using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    public class TaskSerializer : IComponentSerializer<Task>
    {
        public XElement Serialize(Task component)
        {
            var element = new XElement(nameof(Task));
            element.Add(new XAttribute(nameof(component.Name), component.Name));
            element.Add(new XAttribute(nameof(component.Type), component.Type));
            element.Add(new XAttribute(nameof(component.Rate), component.Rate));
            element.Add(new XAttribute(nameof(component.Priority), component.Priority));
            element.Add(new XAttribute(nameof(component.Watchdog), component.Watchdog));
            element.Add(new XAttribute(nameof(component.InhibitTask), component.InhibitTask));
            element.Add(new XAttribute(nameof(component.DisableUpdateOutputs), component.DisableUpdateOutputs));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(new XElement(nameof(component.Description)), component.Description);

            var programs = component.ScheduledPrograms.ToList();
            if (programs.Count <= 0) return element;

            var scheduled = new XElement(nameof(component.ScheduledPrograms));
            foreach (var program in programs)
                scheduled.Add(new XElement("ScheduledProgram",
                    new XAttribute(L5XNames.Attributes.Name, program)));

            element.Add(scheduled);

            return element;
        }

        public Task Deserialize(XElement element)
        {
            //todo add helpers for the rest of the methods
            var task = new Task(element.GetName(), element.GetTaskType(), element.GetDescription());

            var programs = element.Descendants("ScheduledProgram").ToList();
            if (programs.Count <= 0) return task;

            var serializer = new ProgramSerializer();
            foreach (var program in programs)
                task.AddProgram(serializer.Deserialize(program));

            return task;
        }
    }
}