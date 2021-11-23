using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a priority number for the <c>Task</c>.
    /// </summary>
    /// <remarks>
    /// <c>TaskPriority</c> is a property of a <see cref="ITask"/>. It represents the ...
    /// </remarks>
    public readonly struct TaskPriority : IEquatable<TaskPriority>
    {
        private readonly byte _priority;
        
        /// <summary>
        /// Creates a new instance of <see cref="TaskPriority"/> with the specified value.
        /// </summary>
        /// <param name="priority">The value of the priority. Must be a value between 1 and 15</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when the provided priority is not within the
        /// specified range</exception>
        public TaskPriority(byte priority)
        {
            if (priority < 1 || priority > 15)
                throw new ArgumentOutOfRangeException(nameof(priority), "Priority must be value between 1 and 15");

            _priority = priority;
        }

        /// <inheritdoc />
        public bool Equals(TaskPriority other)
        {
            return _priority == other._priority;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is TaskPriority other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _priority.GetHashCode();
        }

        /// <summary>
        /// Determines if two <see cref="TaskPriority"/> objects are equal.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns>True if they are equal</returns>
        public static bool operator ==(TaskPriority left, TaskPriority right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines if two <see cref="TaskPriority"/> objects are not equal.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns>True if they are not equal</returns>
        public static bool operator !=(TaskPriority left, TaskPriority right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Implicitly converts from a <see cref="TaskPriority"/> to a <see cref="byte"/>
        /// </summary>
        /// <param name="priority">The value of the <c>TaskPriority</c></param>
        /// <returns></returns>
        public static implicit operator byte(TaskPriority priority)
        {
            return priority._priority;
        }

        /// <summary>
        /// Implicitly converts from a <see cref="byte"/> to a <see cref="TaskPriority"/>
        /// </summary>
        /// <param name="priority">The value of the priority.</param>
        /// <returns></returns>
        public static implicit operator TaskPriority(byte priority)
        {
            return new TaskPriority(priority);
        }

        /// <summary>
        /// Parses a string value into a <c>TaskPriority</c>
        /// </summary>
        /// <param name="str">The string value to parse</param>
        /// <returns>A TaskPriority value if the parse was successful, default if not</returns>
        public static TaskPriority Parse(string str)
        {
            byte.TryParse(str, out var result);
            return new TaskPriority(result);
        }
        
        /// <inheritdoc />
        public override string ToString()
        {
            return _priority.ToString();
        }
    }
}