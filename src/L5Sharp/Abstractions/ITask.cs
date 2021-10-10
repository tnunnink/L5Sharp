using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface ITask : IComponent
    {
        TaskType Type { get; }
        public byte Priority { get; }
        float Watchdog { get; }
        bool InhibitTask { get; }
        bool DisableUpdateOutputs { get; }
        IEnumerable<string> ScheduledPrograms { get; }
        void AddProgram(string name);
        void RemoveProgram(string name);
        IProgram NewProgram(string name);
        ITask ChangeType(TaskType type);
    }
}