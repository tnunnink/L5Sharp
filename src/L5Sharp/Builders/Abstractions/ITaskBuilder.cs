using System;
using L5Sharp.Base;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

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