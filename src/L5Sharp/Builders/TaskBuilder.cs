using System;
using L5Sharp.Builders.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enumerations;

namespace L5Sharp.Builders
{
    public class TaskBuilder : ITaskBuilder
    {
        private readonly Task _task;

        public TaskBuilder(string name, TaskType type)
        {
            _task = new Task(name, type);
        }

        public Task Build()
        {
            return _task;
        }

        public ITaskBuilder HasType(TaskType type)
        {
            _task.Type = type;
            return this;
        }

        public ITaskBuilder WithRate(float rate)
        {
            _task.Rate = rate;
            return this;
        }

        public ITaskBuilder HasPriority(short priority)
        {
            _task.Priority = priority;
            return this;
        }

        public ITaskBuilder HasWatchdog(float watchdog)
        {
            _task.Watchdog = watchdog;
            return this;
        }

        public ITaskBuilder WithProgram(string name, Action<IProgramBuilder> builder)
        {
            if (builder == null)
            {
                _task.AddProgram(name);
                return this;
            }
            
            var programBuilder = new ProgramBuilder(name);
            builder.Invoke(programBuilder);

            var program = programBuilder.Build();
            
            _task.AddProgram(program);
            return this;
        }
    }
}