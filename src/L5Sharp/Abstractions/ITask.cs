using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enumerations;

namespace L5Sharp.Abstractions
{
    public interface ITask : IComponent
    {
        string Description { get; }
        TaskType Type { get; }
        public byte Priority { get; }
        float Watchdog { get; }
        bool InhibitTask { get; }
        bool DisableUpdateOutputs { get; }
        IEnumerable<string> ScheduledPrograms { get; }
        void AddProgram(string name);
        void RemoveProgram(string name);
        Program NewProgram(string name);
    }
}