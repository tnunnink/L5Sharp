using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.ITask" />
    public class Task : ITask, IEquatable<Task>
    {
        private readonly HashSet<string> _programs = new();

        internal Task(ComponentName name, TaskType? type = null,
            TaskPriority priority = default, ScanRate rate = default, Watchdog watchdog = default,
            bool inhibitTask = false, bool disableUpdateOutputs = false,
            TaskEventInfo? eventInfo = null, IEnumerable<string>? scheduledPrograms = null, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            Type = type ?? TaskType.Periodic;
            Priority = priority;
            Rate = rate;
            Watchdog = watchdog;
            InhibitTask = inhibitTask;
            DisableUpdateOutputs = disableUpdateOutputs;
            EventInfo = eventInfo;

        }

        /// <summary>
        /// Creates a new <see cref="Task"/> with the provided name an optional arguments.
        /// </summary>
        /// <param name="name">The name of the <see cref="Task"/>.</param>
        /// <param name="type">The <see cref="Enums.TaskType"/> of the <see cref="Task"/> instance.</param>
        /// <param name="description">The description of the <see cref="Task"/>.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        public Task(ComponentName name, TaskType? type = null, string? description = null) :
            this(name, type, new TaskPriority(10), new ScanRate(10), new Watchdog(500), description: description)
        {
        }

        /// <inheritdoc />
        public string Name { get; }

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

        public TaskEventInfo? EventInfo { get; }

        /// <inheritdoc />
        public IEnumerable<string> ScheduledPrograms => _programs.AsEnumerable();
        

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
        public bool Equals(Task? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Name, other.Name) && 
                   Description == other.Description &&
                   Equals(Type, other.Type) &&
                   Equals(Priority, other.Priority) &&
                   Equals(Rate, other.Rate) &&
                   Equals(Watchdog, other.Watchdog) &&
                   InhibitTask == other.InhibitTask &&
                   DisableUpdateOutputs == other.DisableUpdateOutputs &&
                   _programs.SequenceEqual(other._programs);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Task)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Name);
            hashCode.Add(Description);
            hashCode.Add(Type);
            hashCode.Add(Priority);
            hashCode.Add(Rate);
            hashCode.Add(Watchdog);
            hashCode.Add(InhibitTask);
            hashCode.Add(DisableUpdateOutputs);
            hashCode.Add(_programs);
            return hashCode.ToHashCode();
        }

        /// <summary>
        /// Indicates whether one object is equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public static bool operator ==(Task left, Task right) => Equals(left, right);

        /// <summary>
        /// Indicates whether one object is not equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are not equal, otherwise false.</returns>
        public static bool operator !=(Task left, Task right) => !Equals(left, right);
    }
}