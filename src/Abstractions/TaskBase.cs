using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class TaskBase : ITask
    {
        private string _name;
        private byte _priority;
        private float _watchdog;
        private readonly HashSet<string> _programs = new HashSet<string>();

        protected TaskBase(string name, string description = null, byte priority = 10,
            float watchdog = 500, bool inhibitTask = false, bool disableUpdateOutputs = false)
        {
            Name = name;
            Description = description ?? string.Empty;
            Priority = priority;
            Watchdog = watchdog;
            InhibitTask = inhibitTask;
            DisableUpdateOutputs = disableUpdateOutputs;
        }


        public string Name
        {
            get => _name;
            set
            {
                Validate.Name(value);
                _name = value;
            }
        }

        public string Description { get; set; }
        public abstract TaskType Type { get; }

        public byte Priority
        {
            get => _priority;
            set
            {
                if (value < 1 || value > 15)
                    throw new ArgumentOutOfRangeException(nameof(Priority), "Priority must be value between 1 and 15");

                _priority = value;
            }
        }

        public float Watchdog
        {
            get => _watchdog;
            set
            {
                if (value < 0.1f || value > 2000000.0f)
                    throw new ArgumentOutOfRangeException(nameof(Watchdog),
                        "Watchdog must be value between 0.1 and 2,000,000.0 ms");

                _watchdog = value;
            }
        }

        public bool InhibitTask { get; set; }
        public bool DisableUpdateOutputs { get; set; }
        public IEnumerable<string> ScheduledPrograms => _programs.AsEnumerable();

        public virtual void AddProgram(string name) => AddProgramComponent(name);

        public virtual void RemoveProgram(string name) => RemoveProgramComponent(name);

        public virtual IProgram NewProgram(string name)
        {
            var program = new Program(name);
            AddProgramComponent(program.Name);
            return program;
        }

        public virtual ITask ChangeType(TaskType type)
        {
            return type.Create(Name, Description, Priority, Watchdog, InhibitTask, DisableUpdateOutputs);
        }

        private void AddProgramComponent(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (_programs.Contains(name))
                throw new ComponentNameCollisionException(name, typeof(Program));

            _programs.Add(name);
        }

        private void RemoveProgramComponent(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            if (!_programs.Contains(name)) return;

            _programs.Remove(name);
        }
    }
}