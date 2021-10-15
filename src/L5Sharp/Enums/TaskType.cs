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

        public abstract ITask Create(string name);

        public static T Create<T>(string name) where T : ITask
        {
            return (T)Activator.CreateInstance(typeof(T),
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding, null, new[] {name, Type.Missing}, CultureInfo.CurrentCulture);
        }

        public static readonly TaskType Continuous = new ContinuousType();
        public static readonly TaskType Periodic = new PeriodicType();
        public static readonly TaskType Event = new EventType();


        private class ContinuousType : TaskType
        {
            public ContinuousType() : base(nameof(Continuous).ToUpper(), nameof(Continuous).ToUpper())
            {
            }

            public override ITask Create(string name)
            {
                return new ContinuousTask(name);
            }
        }

        private class PeriodicType : TaskType
        {
            public PeriodicType() : base(nameof(Periodic).ToUpper(), nameof(Periodic).ToUpper())
            {
            }

            public override ITask Create(string name)
            {
                return new PeriodicTask(name);
            }
        }

        private class EventType : TaskType
        {
            public EventType() : base(nameof(Event).ToUpper(), nameof(Event).ToUpper())
            {
            }

            public override ITask Create(string name)
            {
                return new EventTask(name);
            }
        }
    }
}