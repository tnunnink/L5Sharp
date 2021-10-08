using System;
using L5Sharp.Base;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class EventTask : TaskBase
    {
        private float _rate;

        public EventTask(string name, string description = null, TaskTrigger eventTrigger = null, string eventTag = null,
            bool enableTimeout = false, float rate = 10, byte priority = 10, float watchdog = 500,
            bool inhibitTask = false, bool disableUpdateOutputs = false) : base(name, description, priority, watchdog,
            inhibitTask, disableUpdateOutputs)
        {
            EventTrigger = eventTrigger == null ? TaskTrigger.EventInstructionOnly : eventTrigger;
            EventTag = eventTag;
            EnableTimeout = enableTimeout;
            Rate = rate;
        }

        public override TaskType Type => TaskType.Event;

        public float Rate
        {
            get => _rate;
            set
            {
                if (value < 0.1f || value > 2000000.0f)
                    throw new ArgumentOutOfRangeException(nameof(Priority),
                        "Rate must be value between 0.1 and 2,000,000.0 ms");

                _rate = value;
            }
        }

        public TaskTrigger EventTrigger { get; set; }
        public string EventTag { get; set; }
        public bool EnableTimeout { get; set; }
    }
}