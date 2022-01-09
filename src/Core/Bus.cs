using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a Logix <see cref="Bus"/> or value type that determines the size of a IO rack.
    /// </summary>
    public readonly struct Bus : IEquatable<Bus>
    {
        private readonly byte _size;
        
        /// <summary>
        /// Creates a new <see cref="Bus"/> value with the specified size parameter.
        /// </summary>
        /// <param name="size"></param>
        public Bus(byte size)
        {
            _size = size;
        }

        /// <summary>
        /// Determines if the provided slot number is within range of the current bus size.
        /// </summary>
        /// <param name="slot">The slot number to evaluate.</param>
        /// <returns>
        /// true if the provided number less than or equal to the size of the <see cref="Bus"/>.
        /// </returns>
        public bool IsValidSlot(byte slot) => slot <= _size;

        /// <summary>
        /// Gets a value indicating whether the bus has a size.
        /// </summary>
        public bool IsEmpty => _size == 0;

        /// <summary>
        /// Gets a value of an empty <see cref="Bus"/>.
        /// </summary>
        public static Bus Empty => new();

        /// <inheritdoc />
        public override string ToString() => _size.ToString();

        /// <summary>
        /// Converts a <see cref="Bus"/> to a <see cref="byte"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Bus"/> value to convert.</param>
        /// <returns>A new <see cref="byte"/> value.</returns>
        public static implicit operator byte(Bus value) => value._size;
        
        /// <summary>
        /// Converts a <see cref="byte"/> to a <see cref="Bus"/> value.
        /// </summary>
        /// <param name="value">The <see cref="byte"/> value to convert.</param>
        /// <returns>A new <see cref="Bus"/> value.</returns>
        public static implicit operator Bus(byte value) => new(value);

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="Bus"/> value.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to convert.</param>
        /// <returns>A new <see cref="Bus"/> value.</returns>
        public static implicit operator Bus(string value) => new(byte.Parse(value));


        /// <inheritdoc />
        public bool Equals(Bus other) => _size == other._size;

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Bus other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => _size.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(Bus left, Bus right) => left.Equals(right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Bus left, Bus right) => !left.Equals(right);
    }
}