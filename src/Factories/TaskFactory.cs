using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Factories.Tests")]

namespace L5Sharp.Factories
{
    internal class TaskFactory : IComponentFactory<ITask>
    {
        public TaskFactory(LogixContext context)
        {
        }
        
        public ITask Create(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var name = element.GetName();
            var description = element.GetDescription();
            var type = element.GetValue<ITask>(t => t.Type);
            var priority = element.GetValue<ITask>(t => t.Priority);
            var rate = element.GetValue<ITask>(t => t.Rate);
            var watchdog = element.GetValue<ITask>(t => t.Watchdog);
            var inhibitTask = element.GetValue<ITask>(t => t.InhibitTask);
            var disableUpdateOutputs = element.GetValue<ITask>(t => t.DisableUpdateOutputs);

            var task = new Task(name, type, priority, rate, watchdog, inhibitTask, disableUpdateOutputs, description);

            var programs = element.Descendants("ScheduledProgram").Select(e => e.GetName());
            
            foreach (var program in programs)
                task.AddProgram(program);

            return task;
        }
    }
}