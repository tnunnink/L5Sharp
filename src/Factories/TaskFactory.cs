using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Factories.Tests")]

namespace L5Sharp.Factories
{
    internal class TaskFactory : IComponentFactory<ITask>
    {
        private readonly IComponentCache<ITask> _cache;

        public TaskFactory(LogixContext context)
        {
            _cache = context.GetCache<ITask>();
        }
        
        public ITask Create(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var name = element.GetName();

            if (_cache.HasComponent(name))
                return _cache.Get(name);
            
            var type = element.GetValue<ITask>(t => t.Type);
            var description = element.GetDescription();
            var priority = element.GetValue<ITask>(t => t.Priority);
            var watchdog = element.GetValue<ITask>(t => t.Watchdog);
            var inhibitTask = element.GetValue<ITask>(t => t.InhibitTask);
            var disableUpdateOutputs = element.GetValue<ITask>(t => t.DisableUpdateOutputs);
            
            var task = type.Create(name, description, priority, watchdog, inhibitTask, disableUpdateOutputs);

            if (task is PeriodicTask periodicTask)
            {
                periodicTask.Rate = element.GetValue<PeriodicTask>(t => t.Rate);
            }
            
            if (task is EventTask eventTask)
            {
                eventTask.Rate = element.GetValue<PeriodicTask>(t => t.Rate);
            }

            var programs = element.Descendants("ScheduledProgram").Select(e => e.GetName());
            
            foreach (var program in programs)
                task.AddProgram(program);

            return task;
        }
    }
}