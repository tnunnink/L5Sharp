using System;
using System.Globalization;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a scan rate in milliseconds.
    /// </summary>
    /// <remarks>
    /// <c>ScanRate</c> is a property of a <see cref="ITask"/>. It represents the rate at which the task with evaluate
    /// logic contained within the task.
    /// </remarks>
    public readonly struct ScanRate : IEquatable<ScanRate>
    {
        private readonly float _rate;

        /// <summary>
        /// Creates a new instance of <see cref="ScanRate"/> with the specified rate value.
        /// </summary>
        /// <param name="rate">The scan rate value in milliseconds. Valid range is between 0.1 and 2M</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when the provided rate is outside the specified range</exception>
        public ScanRate(float rate)
        {
            if (rate < 0.1f || rate > 2000000.0f)
                throw new ArgumentOutOfRangeException(nameof(rate),
                    "Rate must be value between 0.1 and 2,000,000.0 ms");

            _rate = rate;
        }

        /// <inheritdoc />
        public bool Equals(ScanRate other)
        {
            return _rate.Equals(other._rate);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is ScanRate other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _rate.GetHashCode();
        }

        /// <summary>
        /// Determines if two <see cref="ScanRate"/> objects are equal.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns>True if they are equal</returns>
        public static bool operator ==(ScanRate left, ScanRate right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines if two <see cref="ScanRate"/> objects are not equal.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns>True if they are not equal</returns>
        public static bool operator !=(ScanRate left, ScanRate right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Implicitly converts from a <see cref="ScanRate"/> to a <see cref="float"/>
        /// </summary>
        /// <param name="rate">The instance of the <see cref="ScanRate"/></param>
        /// <returns>A <see cref="float"/> representing the value of the <see cref="ScanRate"/></returns>
        public static implicit operator float(ScanRate rate)
        {
            return rate._rate;
        }

        /// <summary>
        /// Implicitly converts from a <see cref="float"/> to a <see cref="ScanRate"/>.
        /// </summary>
        /// <param name="rate">the value of the rate.</param>
        /// <returns>A <c>ScanRate</c> representing the value provided by rate.</returns>
        public static implicit operator ScanRate(float rate)
        {
            return new ScanRate(rate);
        }
        
        /// <summary>
        /// Parses a string value into a <c>ScanRate</c>.
        /// </summary>
        /// <param name="str">The string value to parse.</param>
        /// <returns>A ScanRate value if the parse was successful, default if not.</returns>
        public static ScanRate Parse(string str)
        {
            float.TryParse(str, out var result);
            return new ScanRate(result);
        }
        
        /// <inheritdoc />
        public override string ToString()
        {
            return _rate.ToString(CultureInfo.CurrentCulture);
        }
    }
}