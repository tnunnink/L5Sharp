using System;
using L5Sharp.Atomics;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents an argument for an <see cref="Instruction"/> object.
    /// </summary>
    public class Operand : IEquatable<Operand>
    {
        private readonly object _operand;

        private Operand(object operand)
        {
            _operand = operand ?? throw new ArgumentNullException(nameof(operand));
        }
        
        /// <summary>
        /// Indicates whether the operand is a tag reference, or has the tag name pattern.
        /// </summary>
        public bool IsExpression => _operand.ToString().IsTagName();

        /// <summary>
        /// Indicates whether the operand is a tag reference, or has the tag name pattern.
        /// </summary>
        public bool IsReference => _operand.ToString().IsTagName();

        /// <summary>
        /// Indicates whether the operand represents a constant or immediate value. 
        /// </summary>
        public bool IsValue => Radix.TryParseValue(_operand.ToString(), out _);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="TagName"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(TagName value) => new(value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="string"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(string value) => new(value);

        /// <summary>
        /// Performs implicit conversions from <see cref="sbyte"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(sbyte value) => new(value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="byte"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(byte value) => new(value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="short"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(short value) => new(value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="ushort"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(ushort value) => new(value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="int"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(int value) => new(value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="uint"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(uint value) => new(value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="long"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(long value) => new(value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="ulong"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(ulong value) => new(value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="float"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(float value) => new(value);

        /// <summary>
        /// Performs implicit conversions from <see cref="Sint"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(Sint value) => new(value.Value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="Int"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(Int value) => new(value.Value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="Dint"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(Dint value) => new(value.Value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="Lint"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(Lint value) => new(value.Value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="Real"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(Real value) => new(value.Value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="USint"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(USint value) => new(value.Value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="UInt"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(UInt value) => new(value.Value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="UDint"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(UDint value) => new(value.Value);
        
        /// <summary>
        /// Performs implicit conversions from <see cref="ULint"/> to <see cref="Operand"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="Operand"/> object representing the provided value.</returns>
        public static implicit operator Operand(ULint value) => new(value.Value);

        /// <inheritdoc />
        public override string ToString() => _operand.ToString();

        /// <inheritdoc />
        public bool Equals(Operand? other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || _operand.Equals(other._operand);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as Operand);

        /// <inheritdoc />
        public override int GetHashCode() => _operand.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(Operand? left, Operand? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Operand? left, Operand? right) => !Equals(left, right);
    }
}