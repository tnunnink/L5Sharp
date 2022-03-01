using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NeutralText : IEquatable<NeutralText>
    {
        private const string InstructionPattern = @"[a-zA-Z0-9_]+\(.*?\)";
        private readonly string _text;
        private readonly List<Instruction> _instructions;

        /// <summary>
        /// Creates a new default instance of <c>NeutralText</c>.
        /// </summary>
        public NeutralText()
        {
            _text = ";";
            _instructions = new List<Instruction>();
        }

        /// <summary>
        /// Creates a new instance of <c>NeutralText</c> with the provided string text input.
        /// </summary>
        /// <param name="text">A string input that represents a neutral text format. The text may contain </param>
        /// <exception cref="ArgumentNullException">When text is null.</exception>
        /// <exception cref="FormatException">When text is null.</exception>
        public NeutralText(string text) : this()
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            if (text.IsEmpty()) return;

            ValidateText(text);

            _text = text;

            var instructions = Regex.Matches(text, InstructionPattern, RegexOptions.Compiled)
                .Select(m => Instruction.Parse(m.Value));

            _instructions.AddRange(instructions);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Instruction> Instructions => _instructions;

        /// <summary>
        /// Represents a new default instance of the 
        /// </summary>
        /// <returns></returns>
        public static NeutralText Empty => new();

        /// <summary>
        /// Converts a <c>NeutralText</c> object to a <c>string</c> object.
        /// </summary>
        /// <param name="text">the <c>NeutralText</c> instance to convert.</param>
        /// <returns>A <c>string</c> that represents the value of the <c>NeutralText</c>.</returns>
        public static implicit operator string(NeutralText text) => text._text;

        /// <summary>
        /// Converts a <c>string</c> object to a <c>NeutralText</c> object.
        /// </summary>
        /// <param name="text">the <c>string</c> instance to convert.</param>
        /// <returns>A <c>NeutralText</c> that represents the value of the <c>string</c>.</returns>
        public static implicit operator NeutralText(string text) => new(text);

        /// <inheritdoc />
        public override string ToString() => _text;

        /// <inheritdoc />
        public bool Equals(NeutralText? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _text == other._text;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) || obj is NeutralText other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => _text.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(NeutralText? left, NeutralText? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(NeutralText? left, NeutralText? right) => !Equals(left, right);

        private static void ValidateText(string text)
        {
            if (text.Contains('[') && !text.IsBalanced('[', ']'))
                throw new FormatException("Text input must have balanced '[]' characters.");

            if (text.Contains('(') && !text.IsBalanced('(', ')'))
                throw new FormatException("Text input must have balanced '()' characters.");
        }
    }
}