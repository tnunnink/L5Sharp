using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ITask : ILogixComponent
    {
        TaskType Type { get; }
        byte Priority { get; }
        float Rate { get; }
        float Watchdog { get; }
        bool InhibitTask { get; set; }
        bool DisableUpdateOutputs { get; set; }
        IEnumerable<string> ScheduledPrograms { get; }
        void SetType(TaskType type);
        void SetPriority(byte priority);
        void SetRate(float rate);
        void SetWatchdog(float watchdog);
        void ScheduleProgram(string name);
        void RemoveProgram(string name);
    }
}