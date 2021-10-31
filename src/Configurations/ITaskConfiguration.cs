using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public interface ITaskConfiguration : IComponentConfiguration<ITask>
    {
        ITaskConfiguration HasDescription(string description);
        ITaskConfiguration IsType(TaskType type);
        ITaskConfiguration WithPriority(byte priority);
        ITaskConfiguration WithRate(float rate);
        ITaskConfiguration WithWatchdog(float watchdog);
        ITaskConfiguration IsInhibited();
        ITaskConfiguration DisablesUpdateOutputs();
        ITaskConfiguration ScheduleProgram(string name);
    }
}