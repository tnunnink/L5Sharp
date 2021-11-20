using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Builders;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.ITask" />
    public class Task : LogixComponent, ITask, IEquatable<Task>
    {
        private readonly HashSet<string> _programs = new HashSet<string>();
        
        internal Task(string name, TaskType type = null, 
            TaskPriority priority = null, ScanRate rate = null, Watchdog watchdog = null,
            bool inhibitTask = false, bool disableUpdateOutputs = false, string description = null)
            : base(name, description)
        {
            Type = type ?? TaskType.Periodic;
            Priority = priority ?? new TaskPriority(10);
            Rate = rate ?? new ScanRate(10);
            Watchdog = watchdog ?? new Watchdog(500);
            InhibitTask = inhibitTask;
            DisableUpdateOutputs = disableUpdateOutputs;
        }

        /// <inheritdoc />
        public TaskType Type { get; private set; }

        /// <inheritdoc />
        public TaskPriority Priority { get; private set; }

        /// <inheritdoc />
        public ScanRate Rate { get; private set; }

        /// <inheritdoc />
        public Watchdog Watchdog { get; private set; }

        /// <inheritdoc />
        public bool InhibitTask { get; set; }

        /// <inheritdoc />
        public bool DisableUpdateOutputs { get; set; }

        /// <inheritdoc />
        public IEnumerable<string> ScheduledPrograms => _programs.AsEnumerable();

        /// <summary>
        /// Creates a new instance of a <see cref="ITask"/> with default properties.
        /// </summary>
        /// <param name="name">The name of the task to create.</param>
        /// <returns>A new instance of <see cref="ITask"/></returns>
        public static ITask Create(string name)
        {
            return new Task(name);
        }
        
        /// <summary>
        /// Builds a new instance of <see cref="ITask"/> using the fluent builder API.
        /// </summary>
        /// <param name="name">The name of the task to create.</param>
        /// <returns>A new instance of <see cref="ITaskBuilder"/></returns>
        public static ITaskBuilder Build(string name)
        {
            return new TaskBuilder(name);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">Thrown when the scan rate is null.</exception>
        public void SetRate(ScanRate rate)
        {
            if (rate == null)
                throw new ArgumentNullException(nameof(rate));

            Rate = rate;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">Thrown when the name is null.</exception>
        public void ScheduleProgram(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Can not be null or empty", nameof(name));
            if (_programs.Contains(name)) return;
            _programs.Add(name);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">Thrown when the name is null.</exception>
        public void RemoveProgram(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Can not be null or empty", nameof(name));
            if (!_programs.Contains(name)) return;
            _programs.Remove(name);
        }

        /// <inheritdoc />
        public bool Equals(Task other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && ScheduledPrograms.SequenceEqual(other.ScheduledPrograms)
                   && Type.Equals(other.Type)
                   && Priority.Equals(other.Priority)
                   && Rate.Equals(other.Rate) 
                   && Watchdog.Equals(other.Watchdog) 
                   && InhibitTask == other.InhibitTask 
                   && DisableUpdateOutputs == other.DisableUpdateOutputs;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Task)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), _programs, Type, Priority, Rate, Watchdog, InhibitTask,
                DisableUpdateOutputs);
        }

        /// <summary>
        /// Indicates whether one object is equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public static bool operator ==(Task left, Task right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Indicates whether one object is not equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are not equal, otherwise false.</returns>
        public static bool operator !=(Task left, Task right)
        {
            return !Equals(left, right);
        }
    }
}