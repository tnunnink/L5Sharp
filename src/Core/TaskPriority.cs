using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a task priority which is a number between 1 and 15.
    /// <see cref="TaskPriority"/> is a property of <see cref="ITask"/> 
    /// </summary>
    public class TaskPriority : IEquatable<TaskPriority>
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

        /// <summary>
        /// Determines if the current <see cref="TaskPriority"/> instance equals another.
        /// </summary>
        /// <param name="other">The other instance to compare</param>
        /// <returns><c>True</c> if both instances refer to the same object or if the priority values are
        /// equivalent. </returns>
        public bool Equals(TaskPriority other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _priority == other._priority;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TaskPriority)obj);
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
        /// Implicitly converts from a <see cref="TaskPriority"/> to a float
        /// </summary>
        /// <param name="priority">The instance of the <see cref="TaskPriority"/></param>
        /// <returns></returns>
        public static implicit operator byte(TaskPriority priority)
        {
            return priority._priority;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _priority.ToString();
        }
    }
}