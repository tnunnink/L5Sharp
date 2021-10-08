using L5Sharp.Base;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class ContinuousTask : TaskBase
    {
        public ContinuousTask(string name, string description = null, byte priority = 10, float watchdog = 500,
            bool inhibitTask = false, bool disableUpdateOutputs = false) : base(name, description, priority, watchdog,
            inhibitTask, disableUpdateOutputs)
        {
        }
        
        public override TaskType Type => TaskType.Continuous;
    }
}