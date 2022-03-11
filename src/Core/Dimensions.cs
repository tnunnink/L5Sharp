using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents the dimensions of an array for a Logix component.
    /// </summary>
    /// <remarks>
    /// Logix <c>Dimensions</c> can have one, two, or three dimensions.
    /// These dimensions are represented by the properties X, Y, and Z.
    /// </remarks>
    public sealed class Dimensions : IEquatable<Dimensions>
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
        /// Gets the value of the X or first dimensional unit of the <see cref="Dimensions"/> object.
        /// </summary>
        /// <value>
        /// An unsigned short that represents the length the first dimensional parameter.
        /// </value>
        public ushort X { get; }

        /// <summary>
        /// Gets the value of the Y or second dimensional unit of the <see cref="Dimensions"/> object. 
        /// </summary>
        /// <value>
        /// An unsigned short that represents the length the second dimensional parameter.
        /// </value>
        public ushort Y { get; }

        /// <summary>
        /// Gets the value of the Z or third dimensional unit of the <see cref="Dimensions"/> object. 
        /// </summary>
        /// <value>
        /// An unsigned short that represents the length the third dimensional parameter.
        /// </value>
        public ushort Z { get; }

        /// <summary>
        /// Gets the value of the total length of <see cref="Dimensions"/>. 
        /// </summary>
        /// <value>
        /// An integer that represents the combined length of all three dimensional parameters.
        /// </value>
        public int Length => Z > 0 ? X * Y * Z : Y > 0 ? X * Y : X;

        /// <summary>
        /// Indicates whether <see cref="Dimensions"/> are empty or <see cref="Length"/> equals zero.
        /// </summary>
        public bool IsEmpty => Length == 0;

        /// <summary>
        /// Gets the set of indices for the <see cref="Dimensions"/> object.
        /// </summary>
        /// <value>
        /// An enumerable collection of bracket notation index strings that represent the indices of a
        /// dimension array. If <see cref="Dimensions"/> are empty, then an empty collection.
        /// </value>
        /// <remarks>
        /// The indices are determined be the dimensions (x, y, z) of the object.
        /// </remarks>
        public IEnumerable<string> Indices => GenerateIndices();

        /// <summary>
        /// Indicates whether <see cref="Dimensions"/> are multi-dimensional.
        /// </summary>
        /// <remarks>
        /// Multi-dimensional simply means that the dimensions have a value for Y or Z.
        /// </remarks>
        public bool IsMultiDimensional => Y > 0;

        /// <summary>
        /// Gets the value for the number or parameters or coordinates in the <see cref="Dimensions"/> object.
        /// </summary>
        /// <value>
        /// An integer value 1, 2, or 3 that represents the number of dimensional parameters for the current object. 
        /// </value>
        /// <remarks>
        /// More plainly, this property indicates whether the current <see cref="Dimensions"/> are one, two, or three
        /// dimensional based on the values of X, Y, and Z. 
        /// </remarks>
        public int DegreesOfFreedom => Z > 0 ? 3 : Y > 0 ? 2 : X > 0 ? 1 : 0;

        /// <summary>
        /// Represents an empty <see cref="Dimensions"/> object, or object with all three dimensional parameters equal
        /// to zero.
        /// </summary>
        public static Dimensions Empty => new();

        /// <summary>
        /// Parses a string into a <see cref="Dimensions"/> object value.
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
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            if (value.IsEmpty()) return Empty;

            if (!Regex.IsMatch(value, @"(?=\d+)^[\d\s]+$"))
                throw new ArgumentException($"Value '{value}' does not match expected pattern");

            var numbers = value.Split(' ').Select(v => Convert.ToUInt16(v)).ToList();

            return numbers.Count switch
            {
                3 => new Dimensions(numbers[0], numbers[1], numbers[2]),
                2 => new Dimensions(numbers[0], numbers[1]),
                1 => new Dimensions(numbers[0]),
                _ => throw new ArgumentOutOfRangeException(nameof(numbers.Count),
                    $"Value '{value}' has a invalid number of arguments. Expecting between 1 and 3 arguments.")
            };
        }

        /// <summary>
        /// Creates a new instance of the current <see cref="Dimensions"/> with the same value.
        /// </summary>
        /// <returns>A new <see cref="Dimensions"/> object with the same value.</returns>
        public Dimensions Copy() =>
            Z > 0 ? new Dimensions(X, Y, Z) : Y > 0 ? new Dimensions(X, Y) : X > 0 ? new Dimensions(X) : Empty;

        /// <summary>
        /// Generates a string value that represents the <see cref="Dimensions"/> in the L5X format.
        /// </summary>
        /// <returns>A string of numbers separated by a single space.</returns>
        public override string ToString() => Z > 0 ? $"{X} {Y} {Z}" : Y > 0 ? $"{X} {Y}" : $"{X}";

        /// <summary>
        /// Generates a string value that represents the <see cref="Dimensions"/> in the array bracket notation.
        /// </summary>
        /// <example>
        /// 
        /// </example>
        public string ToBracketNotation() => Z > 0 ? $"[{X},{Y},{Z}]" : Y > 0 ? $"[{X},{Y}]" : X > 0 ? $"[{X}]" : "[]";

        /// <summary>
        /// Converts the provided <see cref="ushort"/> to a <see cref="Dimensions"/> value. 
        /// </summary>
        /// <param name="length">The length value to convert.</param>
        /// <returns>
        /// A Dimensions value representing the provided ushort length.
        /// </returns>
        public static implicit operator Dimensions(ushort length) => new(length);

        /// <summary>
        /// Converts the provided <see cref="Dimensions"/> to a <see cref="int"/> value. 
        /// </summary>
        /// <param name="dimensions">The Dimensions value to convert.</param>
        /// <returns>
        /// A int value representing the provided Dimensions <see cref="Length"/>. 
        /// </returns>
        public static implicit operator int(Dimensions dimensions) => dimensions.Length;

        /// <inheritdoc />
        public bool Equals(Dimensions? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as Dimensions);

        /// <inheritdoc />
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();

        /// <summary>
        /// Determines whether the objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are equal, otherwise, false.</returns>
        public static bool operator ==(Dimensions left, Dimensions right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Dimensions left, Dimensions right) => !Equals(left, right);

        /// <summary>
        /// Generates a collection of index names for the current dimensions instance.
        /// </summary>
        /// <returns>A collection of string index names</returns>
        private IEnumerable<string> GenerateIndices()
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

        private static string GenerateIndex(ushort x) => $"[{x}]";

        private static string GenerateIndex(ushort x, ushort y) => $"[{x},{y}]";

        private static string GenerateIndex(ushort x, ushort y, ushort z) => $"[{x},{y},{z}]";
    }
}