using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class TaskEventTrigger : SmartEnum<TaskEventTrigger>
    {
        private TaskEventTrigger(string name, int value) : base(name, value)
        {
        }

        public static readonly TaskEventTrigger AxisHome = new("Axis Home", 0);
        public static readonly TaskEventTrigger AxisWatch = new("Axis Watch", 1);
        public static readonly TaskEventTrigger AxisRegistration1 = new("Axis Registration 1", 2);
        public static readonly TaskEventTrigger AxisRegistration2 = new("Axis Registration 2", 3);
        public static readonly TaskEventTrigger MotionGroupExecution = new("Motion Group Execution", 4);
        public static readonly TaskEventTrigger EventInstructionOnly = new("EVENT Instruction Only", 5);
        public static readonly TaskEventTrigger ModuleInputDataStateChange = new("Module Input Data State Change", 6);
        public static readonly TaskEventTrigger ConsumedTag = new("Consumed Tag", 7);
        public static readonly TaskEventTrigger WindowsEvent = new("Windows Event", 8);
    }
}