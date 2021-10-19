using System;
using System.Globalization;
using System.Reflection;
using Ardalis.SmartEnum;
using L5Sharp.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.Enums
{
    public abstract class TaskType : SmartEnum<TaskType, string>
    {
        private TaskType(string name, string value) : base(name, value)
        {
        }

        public abstract ITask Create(string name, string description = null,
            byte priority = 10, float watchdog = 500, bool inhibit = false, bool disableUpdateOutputs = false);

        public static T Create<T>(string name) where T : ITask
        {
            return (T)Activator.CreateInstance(typeof(T),
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding, null, new[] { name, Type.Missing }, CultureInfo.CurrentCulture);
        }

        public static readonly TaskType Continuous = new ContinuousType();
        public static readonly TaskType Periodic = new PeriodicType();
        public static readonly TaskType Event = new EventType();


        private class ContinuousType : TaskType
        {
            public ContinuousType() : base(nameof(Continuous).ToUpper(), nameof(Continuous).ToUpper())
            {
            }

            public override ITask Create(string name, string description, byte priority, float watchdog, bool inhibit,
                bool disableUpdateOutputs)
            {
                return new ContinuousTask(name, description, priority, watchdog, inhibit, disableUpdateOutputs);
            }
        }

        private class PeriodicType : TaskType
        {
            public PeriodicType() : base(nameof(Periodic).ToUpper(), nameof(Periodic).ToUpper())
            {
            }

            public override ITask Create(string name, string description, byte priority, float watchdog, bool inhibit,
                bool disableUpdateOutputs)
            {
                return new PeriodicTask(name, description,
                    priority: priority,
                    watchdog: watchdog,
                    inhibitTask: inhibit,
                    disableUpdateOutputs: disableUpdateOutputs);
            }
        }

        private class EventType : TaskType
        {
            public EventType() : base(nameof(Event).ToUpper(), nameof(Event).ToUpper())
            {
            }

            public override ITask Create(string name, string description, byte priority, float watchdog, bool inhibit,
                bool disableUpdateOutputs)
            {
                return new EventTask(name, description,
                    priority: priority,
                    watchdog: watchdog,
                    inhibitTask: inhibit,
                    disableUpdateOutputs: disableUpdateOutputs);
            }
        }
    }
}