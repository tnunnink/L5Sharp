using Ardalis.SmartEnum;

namespace L5Sharp.Enumerations
{
    public class TaskType : SmartEnum<TaskType>
    {
        public TaskType(string name, int value) : base(name, value)
        {
        }

        public static readonly TaskType Event = new TaskType(nameof(Event).ToUpper(), 0);
        public static readonly TaskType Periodic = new TaskType(nameof(Periodic).ToUpper(), 1);
        public static readonly TaskType Continuous = new TaskType(nameof(Continuous).ToUpper(), 2);
    }
}