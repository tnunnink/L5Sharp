using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Builders;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.ITask" />
    public class Task : ITask, IEquatable<Task>
    {
        private readonly HashSet<string> _programs = new HashSet<string>();

        internal Task(ComponentName name, TaskType type,
            TaskPriority priority, ScanRate rate, Watchdog watchdog,
            bool inhibitTask, bool disableUpdateOutputs, string description)
        {
            Name = name;
            Description = description;
            Type = type ?? TaskType.Periodic;
            Priority = priority;
            Rate = rate;
            Watchdog = watchdog;
            InhibitTask = inhibitTask;
            DisableUpdateOutputs = disableUpdateOutputs;
        }

        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public TaskType Type { get; }

        /// <inheritdoc />
        public TaskPriority Priority { get; }

        /// <inheritdoc />
        public ScanRate Rate { get; }

        /// <inheritdoc />
        public Watchdog Watchdog { get; }

        /// <inheritdoc />
        public bool InhibitTask { get; }

        /// <inheritdoc />
        public bool DisableUpdateOutputs { get; }

        /// <inheritdoc />
        public IEnumerable<string> ScheduledPrograms => _programs.AsEnumerable();

        /// <summary>
        /// Creates a new instance of a <see cref="ITask"/> with default properties.
        /// </summary>
        /// <param name="name">The name of the task to create.</param>
        /// <returns>A new instance of <see cref="ITask"/></returns>
        public static ITask Create(string name)
        {
            return new Task(name, TaskType.Periodic, new TaskPriority(10), new ScanRate(10), new Watchdog(500),
                false, false, null);
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ITask"/> with default properties.
        /// </summary>
        /// <param name="name">The name of the task to create.</param>
        /// <param name="type">The type of the task to create.</param>
        /// <returns>A new instance of <see cref="ITask"/></returns>
        public static ITask Create(string name, TaskType type)
        {
            return new Task(name, type, new TaskPriority(10), new ScanRate(10), new Watchdog(500),
                false, false, null);
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ITask"/> with default properties.
        /// </summary>
        /// <param name="name">The name of the task to create.</param>
        /// <param name="type">The type of the task to create.</param>
        /// <param name="priority">The priority for the new task.</param>
        /// <param name="rate">The scan rate of the new task.</param>
        /// <param name="watchdog">The watchdog of the new task.</param>
        /// <returns>A new instance of <see cref="ITask"/></returns>
        public static ITask Create(string name, TaskType type, TaskPriority priority, ScanRate rate, Watchdog watchdog)
        {
            return new Task(name, type, priority, rate, watchdog, false, false, null);
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
            return _programs.SequenceEqual(other._programs) && Equals(Name, other.Name) &&
                   Description == other.Description &&
                   Equals(Type, other.Type) && Equals(Priority, other.Priority) && Equals(Rate, other.Rate) &&
                   Equals(Watchdog, other.Watchdog) && InhibitTask == other.InhibitTask &&
                   DisableUpdateOutputs == other.DisableUpdateOutputs;
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
            var hashCode = new HashCode();
            hashCode.Add(_programs);
            hashCode.Add(Name);
            hashCode.Add(Description);
            hashCode.Add(Type);
            hashCode.Add(Priority);
            hashCode.Add(Rate);
            hashCode.Add(Watchdog);
            hashCode.Add(InhibitTask);
            hashCode.Add(DisableUpdateOutputs);
            return hashCode.ToHashCode();
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