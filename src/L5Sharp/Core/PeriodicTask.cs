using System;
using L5Sharp.Base;
using L5Sharp.Enumerations;

namespace L5Sharp.Core
{
    public class PeriodicTask : TaskBase
    {
        private float _rate;

        public PeriodicTask(string name, string description = null,
            float rate = 10, byte priority = 10, float watchdog = 500,
            bool inhibitTask = false, bool disableUpdateOutputs = false)
            : base(name, description, priority, watchdog, inhibitTask, disableUpdateOutputs)
        {
            Rate = rate;
            Priority = priority;
        }

        public override TaskType Type => TaskType.Periodic;

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
    }
}