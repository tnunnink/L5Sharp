using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public class TaskConfiguration : ITaskConfiguration
    {
        private readonly string _name;

        public TaskConfiguration(string name)
        {
            _name = name;
        }

        public ITask Compile()
        {
            throw new System.NotImplementedException();
        }

        public ITaskConfiguration HasDescription(string description)
        {
            throw new System.NotImplementedException();
        }

        public ITaskConfiguration IsType(TaskType type)
        {
            throw new System.NotImplementedException();
        }

        public ITaskConfiguration WithPriority(byte priority)
        {
            throw new System.NotImplementedException();
        }

        public ITaskConfiguration WithRate(float rate)
        {
            throw new System.NotImplementedException();
        }

        public ITaskConfiguration WithWatchdog(float watchdog)
        {
            throw new System.NotImplementedException();
        }

        public ITaskConfiguration IsInhibited()
        {
            throw new System.NotImplementedException();
        }

        public ITaskConfiguration DisablesUpdateOutputs()
        {
            throw new System.NotImplementedException();
        }

        public ITaskConfiguration ScheduleProgram(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}