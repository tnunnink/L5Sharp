using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    public class Task : LogixComponent, ITask
    {
        private readonly HashSet<string> _programs = new HashSet<string>();

        public Task(string name, TaskType type = null, byte priority = 10, float rate = 10, float watchdog = 500,
            bool inhibitTask = false, bool disableUpdateOutputs = false, string description = null)
            : base(name, description)
        {
            Type = type ?? TaskType.Periodic;
            SetPriority(priority);
            SetRate(rate);
            SetWatchdog(watchdog);
            InhibitTask = inhibitTask;
            DisableUpdateOutputs = disableUpdateOutputs;
        }

        public TaskType Type { get; private set; }
        public byte Priority { get; private set; }
        public float Rate { get; private set; }
        public float Watchdog { get; private set; }
        public bool InhibitTask { get; private set; }
        public bool DisableUpdateOutputs { get; private set; }
        public TaskTrigger EventTrigger { get; private set; }
        public string EventTag { get; private set; }
        public bool EnableTimeout { get; private set; }
        public IEnumerable<string> ScheduledPrograms => _programs.AsEnumerable();

        public void SetType(TaskType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), "Task Type can not be null");

            Type = type;
        }

        public void SetPriority(byte priority)
        {
            if (priority < 1 || priority > 15)
                throw new ArgumentOutOfRangeException(nameof(Priority), "Priority must be value between 1 and 15");

            Priority = priority;
        }

        public void SetRate(float rate)
        {
            if (rate < 0.1f || rate > 2000000.0f)
                throw new ArgumentOutOfRangeException(nameof(Priority),
                    "Rate must be value between 0.1 and 2,000,000.0 ms");

            Rate = rate;
        }

        public void SetWatchdog(float watchdog)
        {
            if (watchdog < 0.1f || watchdog > 2000000.0f)
                throw new ArgumentOutOfRangeException(nameof(Watchdog),
                    "Watchdog must be value between 0.1 and 2,000,000.0 ms");

            Watchdog = watchdog;
        }

        public void Inhibit()
        {
            InhibitTask = true;
        }

        public void UnInhibit()
        {
            InhibitTask = false;
        }

        public void SetDisableUpdateOutputs(bool disableUpdateOutputs)
        {
            DisableUpdateOutputs = disableUpdateOutputs;
        }

        public void SetEventTrigger(TaskTrigger trigger)
        {
            EventTrigger = trigger;
        }

        public void SetEventTag(string tagName)
        {
            EventTag = tagName;
        }

        public void SetEnableTimeout(bool enableTimeout)
        {
            EnableTimeout = enableTimeout;
        }

        public void AddProgram(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Can not be null or empty", nameof(name));
            if (_programs.Contains(name)) return;
            _programs.Add(name);
        }

        public void RemoveProgram(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Can not be null or empty", nameof(name));
            if (!_programs.Contains(name)) return;
            _programs.Remove(name);
        }

        public IProgram NewProgram(string name)
        {
            if (_programs.Contains(name))
                throw new ComponentNameCollisionException(name, typeof(Program));
            
            var program = new Program(name);
            
            AddProgram(program.Name);
            
            return program;
        }
    }
}