using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    public interface ITaskBuilder : IComponentBuilder<ITask>
    {
        ITaskBuilder HasDescription(string description);
        ITaskBuilder IsType(TaskType type);
        ITaskBuilder WithPriority(byte priority);
        ITaskBuilder WithRate(float rate);
        ITaskBuilder WithWatchdog(float watchdog);
        ITaskBuilder IsInhibited();
        ITaskBuilder DisablesUpdateOutputs();
        ITaskBuilder ScheduleProgram(string name);
    }
}