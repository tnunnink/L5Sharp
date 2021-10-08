using System.Xml.Linq;
using Ardalis.SmartEnum;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.Enumerations
{
    public abstract class TaskType : SmartEnum<TaskType, string>
    {
        private TaskType(string name, string value) : base(name, value)
        {
        }

        public abstract ITask Create(string name);

        internal abstract ITask Create(XElement element);

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

            internal override ITask Create(XElement element)
            {
                return new ContinuousTask(element.GetName(), element.GetDescription(), element.GetPriority(),
                    element.GetWatchdog(), element.GetInhibitTask(), element.GetDisableUpdateOutputs());
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

            internal override ITask Create(XElement element)
            {
                return new PeriodicTask(element.GetName(), element.GetDescription(), element.GetRate(),
                    element.GetPriority(), element.GetWatchdog(), element.GetInhibitTask(),
                    element.GetDisableUpdateOutputs());
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

            internal override ITask Create(XElement element)
            {
                return new EventTask(element.GetName(), element.GetDescription(), element.GetEventTrigger(),
                    element.GetEventTag(), element.GetEnableTimeout(), element.GetRate(),
                    element.GetPriority(), element.GetWatchdog(), element.GetInhibitTask(),
                    element.GetDisableUpdateOutputs());
            }
        }
    }
}