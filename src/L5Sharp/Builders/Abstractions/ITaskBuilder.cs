using System;
using L5Sharp.Core;
using L5Sharp.Enumerations;

namespace L5Sharp.Builders.Abstractions
{
    public interface ITaskBuilder : IBuilder<Task>
    {
        ITaskBuilder HasType(TaskType type);
        ITaskBuilder WithRate(float rate);
        ITaskBuilder HasPriority(short priority);
        ITaskBuilder HasWatchdog(float watchdog);
        ITaskBuilder WithProgram(string name, Action<IProgramBuilder> builder = null);
    }
}