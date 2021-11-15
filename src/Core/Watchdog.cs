using System;
using System.Globalization;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a watch dog in milliseconds.
    /// <see cref="Watchdog"/> is a property of a <see cref="ITask"/>
    /// </summary>
    public class Watchdog : IEquatable<Watchdog>
    {
        private readonly float _watchdog;


        /// <summary>
        /// Creates a new instance of <see cref="Watchdog"/> with the specified value.
        /// </summary>
        /// <param name="watchdog">The value of the watchdog in milliseconds.
        /// Must be a value between 0.1 and 2M.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when the provided value is not within the
        /// specified range.</exception>
        public Watchdog(float watchdog)
        {
            if (watchdog < 0.1f || watchdog > 2000000.0f)
                throw new ArgumentOutOfRangeException(nameof(watchdog),
                    "Watchdog must be value between 0.1 and 2,000,000.0 ms");

            _watchdog = watchdog;
        }

        /// <summary>
        /// Determines if the current <see cref="Watchdog"/> instance equals another.
        /// </summary>
        /// <param name="other">The other instance to compare</param>
        /// <returns><c>True</c> if both instances refer to the same object or if the watchdog values are
        /// equivalent. </returns>
        public bool Equals(Watchdog other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || _watchdog.Equals(other._watchdog);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Watchdog)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _watchdog.GetHashCode();
        }

        /// <summary>
        /// Determines if two <see cref="Watchdog"/> objects are equal.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns>True if they are equal</returns>
        public static bool operator ==(Watchdog left, Watchdog right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines if two <see cref="Watchdog"/> objects are not equal.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns>True if they are not equal</returns>
        public static bool operator !=(Watchdog left, Watchdog right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Implicitly converts from a <see cref="Watchdog"/> to a <see cref="float"/>
        /// </summary>
        /// <param name="watchdog">The instance of the <see cref="Watchdog"/></param>
        /// <returns>A <see cref="float"/> representing the value of the <see cref="Watchdog"/></returns>
        public static implicit operator float(Watchdog watchdog)
        {
            return watchdog._watchdog;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _watchdog.ToString(CultureInfo.CurrentCulture);
        }
    }
}