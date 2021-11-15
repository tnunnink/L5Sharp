using System;
using System.Globalization;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a scan rate in milliseconds.
    /// <see cref="ScanRate"/> is a property of a <see cref="ITask"/>
    /// </summary>
    public class ScanRate : IEquatable<ScanRate>
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

        /// <summary>
        /// Determines if the current <see cref="ScanRate"/> instance equals another.
        /// </summary>
        /// <param name="other">The other instance to compare</param>
        /// <returns><c>True</c> if both instances refer to the same object or if the scan rate values are
        /// equivalent. </returns>
        public bool Equals(ScanRate other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || _rate.Equals(other._rate);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ScanRate)obj);
        }

        /// <summary>
        /// Generates a hashcode for the current instance.
        /// </summary>
        /// <returns></returns>
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

        /// <inheritdoc />
        public override string ToString()
        {
            return _rate.ToString(CultureInfo.CurrentCulture);
        }
    }
}