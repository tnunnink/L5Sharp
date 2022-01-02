using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
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

            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.Type);
            element.AddAttribute(component, c => c.Rate);
            element.AddAttribute(component, c => c.Priority);
            element.AddAttribute(component, c => c.Watchdog);
            element.AddAttribute(component, c => c.DisableUpdateOutputs);
            element.AddAttribute(component, c => c.InhibitTask);

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
            
            var type = element.GetAttribute<ITask, TaskType>(t => t.Type);

            if (type is null)
                throw new ArgumentException("The provided XElement must have an attribute 'Type' to create a ITask.");

            return type.FromElement(element);
        }
    }
}