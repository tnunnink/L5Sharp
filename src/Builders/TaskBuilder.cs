using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    public class TaskBuilder : ITaskBuilder
    {
        private readonly string _name;

        public TaskBuilder()
        {
        }

        public ITask Create()
        {
            throw new System.NotImplementedException();
        }

        public ITaskBuilder HasDescription(string description)
        {
            throw new System.NotImplementedException();
        }

        public ITaskBuilder IsType(TaskType type)
        {
            throw new System.NotImplementedException();
        }

        public ITaskBuilder WithPriority(byte priority)
        {
            throw new System.NotImplementedException();
        }

        public ITaskBuilder WithRate(float rate)
        {
            throw new System.NotImplementedException();
        }

        public ITaskBuilder WithWatchdog(float watchdog)
        {
            throw new System.NotImplementedException();
        }

        public ITaskBuilder IsInhibited()
        {
            throw new System.NotImplementedException();
        }

        public ITaskBuilder DisablesUpdateOutputs()
        {
            throw new System.NotImplementedException();
        }

        public ITaskBuilder ScheduleProgram(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}