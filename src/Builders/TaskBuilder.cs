using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    internal class TaskBuilder : ITaskBuilder
    {
        private readonly string _name;
        private string _description;
        private TaskType _type;
        private TaskPriority _priority;
        private ScanRate _rate;
        private Watchdog _watchdog;
        private bool _inhibitTask;
        private bool _disableUpdateOutputs;
        private readonly List<string> _programs = new List<string>();
        
        public TaskBuilder(string name)
        {
            _name = name;
        }
        
        public ITask Create()
        {
            var task = new Task(_name, _type, _priority, _rate, _watchdog, _inhibitTask, _disableUpdateOutputs,
                _description);
            foreach (var program in _programs)
                task.ScheduleProgram(program);
            return task;
        }
        
        public ITaskBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }
        
        public ITaskBuilder OfType(TaskType type)
        {
            _type = type;
            return this;
        }

        public ITaskBuilder WithPriority(TaskPriority priority)
        {
            _priority = priority;
            return this;
        }

        public ITaskBuilder WithRate(ScanRate rate)
        {
            _rate = rate;
            return this;
        }

        public ITaskBuilder WithWatchdog(Watchdog watchdog)
        {
            _watchdog = watchdog;
            return this;
        }

        public ITaskBuilder IsInhibited()
        {
            _inhibitTask = true;
            return this;
        }

        public ITaskBuilder DisablesUpdateOutputs()
        {
            _disableUpdateOutputs = true;
            return this;
        }

        public ITaskBuilder ScheduleProgram(string programName)
        {
            _programs.Add(programName);
            return this;
        }
    }
}