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
        bool InhibitTask { get; }
        bool DisableUpdateOutputs { get; }
        TaskTrigger EventTrigger { get; }
        string EventTag { get; }
        bool EnableTimeout { get; }
        IEnumerable<string> ScheduledPrograms { get; }
        void SetType(TaskType type);
        void SetPriority(byte priority);
        void SetRate(float rate);
        void SetWatchdog(float watchdog);
        void Inhibit();
        void UnInhibit();
        void SetDisableUpdateOutputs(bool disableUpdateOutputs);
        void SetEventTrigger(TaskTrigger trigger);
        void SetEventTag(string tagName);
        void SetEnableTimeout(bool enableTimeout);
        void AddProgram(string name);
        void RemoveProgram(string name);
        IProgram NewProgram(string name);
    }
}