using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class TaskTrigger : SmartEnum<TaskTrigger>
    {
        private TaskTrigger(string name, int value) : base(name, value)
        {
        }

        public static readonly TaskTrigger AxisHome = new TaskTrigger("Axis Home", 0);
        public static readonly TaskTrigger AxisWatch = new TaskTrigger("Axis Watch", 1);
        public static readonly TaskTrigger AxisRegistration1 = new TaskTrigger("Axis Registration 1", 2);
        public static readonly TaskTrigger AxisRegistration2 = new TaskTrigger("Axis Registration 2", 3);
        public static readonly TaskTrigger MotionGroupExecution = new TaskTrigger("Motion Group Execution", 4);
        public static readonly TaskTrigger EventInstructionOnly = new TaskTrigger("EVENT Instruction Only", 5);
        public static readonly TaskTrigger ModuleInputDataStateChange =
            new TaskTrigger("Module Input Data State Change", 6);
        public static readonly TaskTrigger ConsumedTag = new TaskTrigger("Consumed Tag", 7);
        public static readonly TaskTrigger WindowsEvent = new TaskTrigger("Windows Event", 8);
    }
}