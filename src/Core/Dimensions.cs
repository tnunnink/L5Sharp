using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Enums;

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
        /// Gets the value of the X or first dimensional unit.
        /// </summary>
        public ushort X { get; }

        /// <summary>
        /// Gets the value of the Y or second dimensional unit. 
        /// </summary>
        public ushort Y { get; }

        /// <summary>
        /// Gets the value of the Z or third dimensional unit. 
        /// </summary>
        public ushort Z { get; }

        /// <summary>
        /// Gets the total length of the Dimensions. 
        /// </summary>
        public int Length => Z > 0 ? X * Y * Z : Y > 0 ? X * Y : X;

        /// <summary>
        /// Indicates whether the current Dimensions are empty.
        /// </summary>
        public bool AreEmpty => Length == 0;

        /// <summary>
        /// Indicates whether the current Dimensions are multi-dimensional.
        /// </summary>
        /// <remarks>
        /// Multi-dimensional simply means that the dimensions have a value for Y or Z.
        /// </remarks>
        public bool AreMultiDimensional => Y > 0;

        /// <summary>
        /// Gets a new instance of empty <c>Dimensions</c>
        /// </summary>
        public static Dimensions Empty => new();

        /// <inheritdoc />
        public Dimensions Copy()
        {
            return Z > 0 ? new Dimensions(X, Y, Z) : Y > 0 ? new Dimensions(X, Y) : X > 0 ? new Dimensions(X) : Empty;
        }

        /// <summary>
        /// Parses a string into an instance of Dimensions.
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

            return numbers.Count switch
            {
                3 => new Dimensions(numbers[0], numbers[1], numbers[2]),
                2 => new Dimensions(numbers[0], numbers[1]),
                1 => new Dimensions(numbers[0]),
                _ => throw new ArgumentOutOfRangeException(nameof(numbers.Count),
                    $"Value '{value}' has a invalid number of arguments. Expects between 1 and 3 arguments.")
            };
        }

        /// <summary>
        /// Generates a string value that represents the Dimensions in the L5X format.
        /// </summary>
        /// <returns>A string of numbers separated by a single space.</returns>
        public override string ToString()
        {
            return Z > 0 ? $"{X} {Y} {Z}"
                : Y > 0 ? $"{X} {Y}"
                : $"{X}";
        }

        /// <summary>
        /// Generates a collection of typed Members using the current Dimensions and provided member parameters. 
        /// </summary>
        /// <param name="seedType">The data type to instantiate for each element of the collection.</param>
        /// <param name="radix">The Radix value to set for each element of the collection.</param>
        /// <param name="access">The ExternalAccess to set for each element of the collection.</param>
        /// <param name="description">The description to set for each element of the collection.</param>
        /// <typeparam name="TDataType">The IDataType that the collection represents.</typeparam>
        /// <returns>A collection of members of the specified TDataType.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see cref="seedType"/> is null.</exception>
        public IEnumerable<IMember<TDataType>> GenerateMembers<TDataType>(TDataType seedType,
            Radix? radix = null, ExternalAccess? access = null, string? description = null)
            where TDataType : IDataType
        {
            if (seedType is null)
                throw new ArgumentNullException(nameof(seedType));

            var indices = GenerateIndices();

            return indices.Select(i =>
                new Member<TDataType>(i, (TDataType)seedType.Instantiate(), Empty, radix, access, description));
        }

        /// <summary>
        /// Generates a collection of typed Members using the current Dimensions and provided member parameters.
        /// </summary>
        /// <param name="elements">A collection of data type elements to initialize the member collection.</param>
        /// <param name="radix">The Radix value to set for each element of the collection.</param>
        /// <param name="access">The ExternalAccess to set for each element of the collection.</param>
        /// <param name="description">The description to set for each element of the collection.</param>
        /// <typeparam name="TDataType">The IDataType that the collection represents.</typeparam>
        /// <returns>A collection of members of the specified TDataType.</returns>
        /// <exception cref="ArgumentNullException">Throw if elements is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if elements count is 0 or greater than the length of the dimension length.
        /// </exception>
        public IEnumerable<IMember<TDataType>> GenerateMembers<TDataType>(List<TDataType> elements,
            Radix? radix = null, ExternalAccess? access = null, string? description = null)
            where TDataType : IDataType
        {
            if (elements is null)
                throw new ArgumentNullException(nameof(elements));

            if (elements.Count <= 0 || elements.Count > Length)
                throw new ArgumentOutOfRangeException(
                    $"Elements count '{elements.Count}' must be greater than zero and less than or equal to the length of the Dimensions.");

            var indices = GenerateIndices().ToArray();

            var members = new List<IMember<TDataType>>();

            for (var i = 0; i < Length; i++)
            {
                var instance = i < elements.Count ? elements[i] : elements[0].Instantiate();

                members.Add(new Member<TDataType>(indices[i], (TDataType)instance, Empty, radix, access, description));
            }

            return members;
        }

        /// <summary>
        /// Converts the provided <see cref="ushort"/> to a <see cref="Dimensions"/> value. 
        /// </summary>
        /// <param name="length">The length value to convert.</param>
        /// <returns>
        /// A Dimensions value representing the provided ushort length.
        /// </returns>
        public static implicit operator Dimensions(ushort length)
        {
            return new Dimensions(length);
        }

        /// <summary>
        /// Converts the provided <see cref="Dimensions"/> to a <see cref="ushort"/> value. 
        /// </summary>
        /// <param name="dimensions">The Dimensions value to convert.</param>
        /// <returns>
        /// A ushort value representing the provided Dimensions instance. 
        /// </returns>
        public static implicit operator ushort(Dimensions dimensions)
        {
            return dimensions.X;
        }

        /// <summary>
        /// Converts the provided <see cref="Dimensions"/> to a <see cref="int"/> value. 
        /// </summary>
        /// <param name="dimensions">The Dimensions value to convert.</param>
        /// <returns>
        /// A int value representing the provided Dimensions <see cref="Length"/>. 
        /// </returns>
        public static implicit operator int(Dimensions dimensions)
        {
            return dimensions.Length;
        }

        /// <inheritdoc />
        public bool Equals(Dimensions? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
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