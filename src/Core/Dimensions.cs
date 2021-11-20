using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents the dimensions of an array for a Logix component.
    /// </summary>
    /// <remarks>
    /// Logix <c>Dimensions</c> can have one, two, or three dimensions.
    /// These dimensions are represented by the properties X, Y, and Z.
    /// </remarks>
    public class Dimensions : IEquatable<Dimensions>, IPrototype<Dimensions>
    {
        private Dimensions()
        {
        }

        /// <summary>
        /// Creates a one dimensional instance with the provided values.
        /// </summary>
        /// <param name="x">The length of the first dimensional element</param>
        public Dimensions(ushort x)
        {
            X = x;
        }

        /// <summary>
        /// Creates a two dimensional instance with the provided values. 
        /// </summary>
        /// <param name="x">The length of the first dimensional element</param>
        /// <param name="y">The length of the second dimensional element</param>
        /// <exception cref="ArgumentException">
        /// Thrown in the value of <c>x</c> is 0 and <c>y</c> is greater than 0.
        /// </exception>
        public Dimensions(ushort x, ushort y) : this(x)
        {
            if (x == 0 && y > 0)
                throw new ArgumentException("X must be greater than zero to have two dimensions");

            Y = y;
        }

        
        /// <summary>
        /// Creates a three dimensional instance with the provided values. 
        /// </summary>
        /// <param name="x">The length of the first dimensional element</param>
        /// <param name="y">The length of the second dimensional element</param>
        /// <param name="z">The length of the third dimensional element</param>
        /// <exception cref="ArgumentException">
        /// Thrown in the value of <c>y</c> is 0 and <c>z</c> is greater than 0.
        /// </exception>
        public Dimensions(ushort x, ushort y, ushort z) : this(x, y)
        {
            if (y == 0 && z > 0)
                throw new ArgumentException("Y must be greater than zero to have three dimensions");

            Z = z;
        }

        /// <summary>
        /// Gets the value of the <c>X</c> or first dimensional unit.
        /// </summary>
        public ushort X { get; }

        /// <summary>
        /// Gets the value of the <c>Y</c> or second dimensional unit. 
        /// </summary>
        public ushort Y { get; }

        /// <summary>
        /// Gets the value of the <c>Z</c> or third dimensional unit. 
        /// </summary>
        public ushort Z { get; }

        /// <summary>
        /// The total length of the dimension. 
        /// </summary>
        public int Length => Z > 0 ? X * Y * Z : Y > 0 ? X * Y : X;

        /// <summary>
        /// Indicates whether the current <c>Dimensions</c> are empty.
        /// </summary>
        public bool AreEmpty => Length == 0;

        /// <summary>
        /// Indicates whether the current <c>Dimensions</c> are multi-dimensional.
        /// </summary>
        /// <remarks>
        /// Multi-dimensional simply means that the dimensions have a value fo Y or Y and Z.
        /// </remarks>
        public bool AreMultiDimensional => Y > 0;

        /// <summary>
        /// Gets a new instance of empty <c>Dimensions</c>
        /// </summary>
        public static Dimensions Empty => new Dimensions();

        /// <inheritdoc />
        public Dimensions Copy()
        {
            return Z > 0 ? new Dimensions(X, Y, Z) : Y > 0 ? new Dimensions(X, Y) : X > 0 ? new Dimensions(X) : Empty;
        }

        /// <summary>
        /// Parses a string into an instance of <c>Dimensions</c>.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to parse.</param>
        /// <returns>
        /// If the string has a valid format and value, returns an instance of <see cref="Dimensions"/> that represents
        /// the value parsed using the provided sting. If value is null, empty, or 0, returns <see cref="Empty"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the argument does not match to expected pattern or if the pattern does not contain between 1 and 3 arguments.
        /// </exception>
        /// <remarks>
        /// L5X valid string pattern is a set of numbers separated by a single space. The string should only contain up
        /// to three numbers. Each number should represent a <see cref="ushort"/>, or a value between 0 and 65535.
        /// </remarks>
        public static Dimensions Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Empty;

            if (!Regex.IsMatch(value, @"(?=\d+)^[\d\s]+$"))
                throw new ArgumentException($"Value '{value}' does not match expected pattern");

            var numbers = value.Split(' ').Select(v => Convert.ToUInt16(v)).ToList();

            if (numbers.Count > 3 || numbers.Count < 1)
                throw new ArgumentException(
                    $"Value '{value}' has a invalid number of arguments. Expects between 1 and 3 arguments.");

            return numbers.Count switch
            {
                3 => new Dimensions(numbers[0], numbers[1], numbers[2]),
                2 => new Dimensions(numbers[0], numbers[1]),
                1 => new Dimensions(numbers[0]),
                _ => Empty
            };
        }

        /// <summary>
        /// Generates a string value that represents the <c>Dimensions</c> in the valid format.
        /// </summary>
        /// <returns>A string of numbers separated by a single space.</returns>
        public override string ToString()
        {
            return Z > 0 ? $"{X} {Y} {Z}"
                : Y > 0 ? $"{X} {Y}"
                : $"{X}";
        }

        /// <summary>
        /// Generates a collection of string names that represent the index names for an array of elements.
        /// </summary>
        /// <returns>A collection of string names representing the indices of an array</returns>
        public IEnumerable<string> GenerateIndices()
        {
            var indices = new List<string>();

            for (ushort i = 0; i < X; i++)
            {
                if (Y == 0)
                    indices.Add(GenerateIndex(i));

                for (ushort j = 0; j < Y; j++)
                {
                    if (Z == 0)
                        indices.Add(GenerateIndex(i, j));

                    for (ushort k = 0; k < Z; k++)
                        indices.Add(GenerateIndex(i, j, k));
                }
            }

            return indices;
        }

        /// <inheritdoc />
        public bool Equals(Dimensions other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Dimensions)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Dimensions left, Dimensions right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Dimensions left, Dimensions right)
        {
            return !Equals(left, right);
        }

        public static implicit operator Dimensions(ushort length)
        {
            return new Dimensions(length);
        }

        public static implicit operator ushort(Dimensions dimensions)
        {
            return dimensions.X;
        }

        public static implicit operator int(Dimensions dimensions)
        {
            return dimensions.Length;
        }

        private static string GenerateIndex(ushort x)
        {
            return $"[{x}]";
        }

        private static string GenerateIndex(ushort x, ushort y)
        {
            return $"[{x},{y}]";
        }

        private static string GenerateIndex(ushort x, ushort y, ushort z)
        {
            return $"[{x},{y},{z}]";
        }
    }
}