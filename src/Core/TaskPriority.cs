using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// A configurable property of a <see cref="ITask"/> that controls the order in which the Logix Controller
    /// will scan the given Task.
    /// </summary>
    /// <remarks>
    /// <see cref="TaskPriority"/> is a simple byte value that must be between 1 and 15.
    /// Attempting to set the priority to a value outside that range will result in an <see cref="ArgumentOutOfRangeException"/>.
    /// This parameter will control the scan order of task components as related to other tasks.
    /// </remarks>
    public readonly struct TaskPriority : IEquatable<TaskPriority>
    {
        private readonly byte _priority;

        /// <summary>
        /// Creates a new instance of <see cref="TaskPriority"/> with the provided value.
        /// </summary>
        /// <param name="priority">The value of the priority. Must be a value between 1 and 15.</param>
        /// <exception cref="ArgumentOutOfRangeException">priority is less than 1 -or- greater than 15.</exception>
        public TaskPriority(byte priority)
        {
            if (priority < 1 || priority > 15)
                throw new ArgumentOutOfRangeException(nameof(priority), "Priority must be value between 1 and 15");

            _priority = priority;
        }

        /// <summary>
        /// Converts a <see cref="TaskPriority"/> to a <see cref="byte"/>
        /// </summary>
        /// <param name="priority">The value to convert.</param>
        /// <returns>A <see cref="byte"/> value.</returns>
        public static implicit operator byte(TaskPriority priority) => priority._priority;

        /// <summary>
        /// Converts a <see cref="byte"/> to a <see cref="TaskPriority"/>
        /// </summary>
        /// <param name="priority">The value to convert.</param>
        /// <returns>A <see cref="TaskPriority"/> value.</returns>
        public static implicit operator TaskPriority(byte priority) => new(priority);

        /// <summary>
        /// Parses a string value into a <see cref="TaskPriority"/>.
        /// </summary>
        /// <param name="str">The string to parse.</param>
        /// <returns>A <see cref="TaskPriority"/> value if the parse was successful; otherwise; the default value.</returns>
        public static TaskPriority Parse(string str) =>
            byte.TryParse(str, out var result) ? new TaskPriority(result) : default;

        /// <inheritdoc />
        public bool Equals(TaskPriority other) => _priority == other._priority;

        /// <inheritdoc />
        public override string ToString() => _priority.ToString();

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is TaskPriority other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => _priority.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(TaskPriority left, TaskPriority right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(TaskPriority left, TaskPriority right) => !Equals(left, right);
    }
}