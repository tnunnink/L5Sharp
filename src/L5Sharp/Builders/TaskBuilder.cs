using System;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

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

        public void WithProgram(string name, Action<IProgramBuilder> builder)
        {
            var programBuilder = new ProgramBuilder(name);
            builder.Invoke(programBuilder);
            var program = programBuilder.Build();
            _task.AddProgram(program);
        }
    }
}