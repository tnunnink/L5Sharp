using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Dimensions : IEquatable<Dimensions>
    {
        private Dimensions()
        {
        }

        public Dimensions(ushort x)
        {
            X = x;
        }

        public Dimensions(ushort x, ushort y) : this(x)
        {
            if (x == 0)
                throw new ArgumentException("X must be non zero");
            
            if (y == 0)
                throw new ArgumentException("Y must be non zero");

            Y = y;
        }

        public Dimensions(ushort x, ushort y, ushort z) : this(x, y)
        {
            if (z == 0)
                throw new ArgumentException("Z must be non zero");

            Z = z;
        }

        public ushort X { get; }
        public ushort Y { get; }
        public ushort Z { get; }
        public int Length => Z > 0 ? X * Y * Z : Y > 0 ? X * Y : X;
        public bool AreEmpty => Length == 0;
        public bool IsMultiDimensional => Y > 0;

        public static Dimensions Empty => new Dimensions();

        public static Dimensions Parse(string value)
        {
            switch (value)
            {
                case null:
                    throw new ArgumentNullException(nameof(value));
                case "":
                    return Empty;
            }

            if (!Regex.IsMatch(value, @"(?=\d+)^[\d\s]+$"))
                throw new ArgumentException($"Value '{value}' does not match expected pattern");

            var numbers = value.Split(' ').Select(v => Convert.ToUInt16(v)).ToList();

            if (numbers.Count > 3)
                throw new ArgumentException($"Value '{value}' has more than three dimensions. Must have three or less");

            return numbers.Count switch
            {
                3 => new Dimensions(numbers[0], numbers[1], numbers[2]),
                2 => new Dimensions(numbers[0], numbers[1]),
                1 => new Dimensions(numbers[0]),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override string ToString()
        {
            return Z > 0 ? $"{X} {Y} {Z}"
                : Y > 0 ? $"{X} {Y}"
                : $"{X}";
        }

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

        public bool Equals(Dimensions other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Dimensions)obj);
        }

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