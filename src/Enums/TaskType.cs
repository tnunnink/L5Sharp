using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public abstract class TaskType : SmartEnum<TaskType, string>
    {
        private TaskType(string name, string value) : base(name, value)
        {
        }

        public static readonly TaskType Continuous = new ContinuousType();
        public static readonly TaskType Periodic = new PeriodicType();
        public static readonly TaskType Event = new EventType();


        private class ContinuousType : TaskType
        {
            public ContinuousType() : base(nameof(Continuous).ToUpper(), nameof(Continuous).ToUpper())
            {
            }
        }

        private class PeriodicType : TaskType
        {
            public PeriodicType() : base(nameof(Periodic).ToUpper(), nameof(Periodic).ToUpper())
            {
            }
        }

        private class EventType : TaskType
        {
            public EventType() : base(nameof(Event).ToUpper(), nameof(Event).ToUpper())
            {
            }
        }
    }
}