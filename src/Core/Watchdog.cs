using System;
using System.Globalization;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a watchdog in milliseconds for the <c>Task</c>.
    /// </summary>
    /// <remarks>
    /// <c>Watchdog</c> is a property of a <see cref="ITask"/>. It represents the ...
    /// </remarks>
    public readonly struct Watchdog : IEquatable<Watchdog>
    {
        private readonly float _watchdog;

        /// <summary>
        /// Creates a new instance of <c>Watchdog</c> with the specified value.
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
        /// Creates a new instance of a <c>Watchdog</c> with default value of 500ms.
        /// </summary>
        /// <returns>A new <c>Watchdog</c> value.</returns>
        public static Watchdog Default()
        {
            return new Watchdog(500);
        }

        /// <inheritdoc />
        public bool Equals(Watchdog other)
        {
            return _watchdog.Equals(other._watchdog);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Watchdog other && Equals(other);
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
        /// <param name="watchdog">The <c>Watchdog</c> value to convert</param>
        /// <returns>A new float value.</returns>
        public static implicit operator float(Watchdog watchdog)
        {
            return watchdog._watchdog;
        }
        
        /// <summary>
        /// Implicitly converts from a <see cref="float"/> to a <see cref="Watchdog"/>
        /// </summary>
        /// <param name="watchdog">The <c>Watchdog</c> value to convert</param>
        /// <returns>A new <c>Watchdog</c> value.</returns>
        public static implicit operator Watchdog(float watchdog)
        {
            return new Watchdog(watchdog);
        }
        
        /// <summary>
        /// Parses a string value into a <c>Watchdog</c>.
        /// </summary>
        /// <param name="str">The string value to parse.</param>
        /// <returns>A Watchdog value if the parse was successful, default if not.</returns>
        public static Watchdog Parse(string str)
        {
            float.TryParse(str, out var result);
            return new Watchdog(result);
        }
        
        /// <inheritdoc />
        public override string ToString()
        {
            return _watchdog.ToString(CultureInfo.CurrentCulture);
        }
    }
}