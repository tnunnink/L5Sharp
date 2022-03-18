using System;
using System.Collections.Generic;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NeutralText : IEquatable<NeutralText>
    {
        private readonly string _text;

        /// <summary>
        /// Creates a new default instance of <c>NeutralText</c>.
        /// </summary>
        public NeutralText()
        {
            _text = ";";
        }

        /// <summary>
        /// Creates a new <see cref="NeutralText"/> object with the provided text input.
        /// </summary>
        /// <param name="text">A string input that represents a neutral text format. The text may contain </param>
        /// <exception cref="ArgumentNullException">When text is null.</exception>
        /// <exception cref="FormatException">When text is null.</exception>
        public NeutralText(string text) : this()
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="NeutralText"/> value has balanced brackets and parenthesis.
        /// </summary>
        public bool IsBalanced => _text.IsBalanced('[', ']') && _text.IsBalanced('(', ')');
        
        /// <summary>
        /// Gets all instruction object that are present in the <see cref="NeutralText"/> instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Instruction> Instructions() => GetInstructions(_text);

        public IEnumerable<TagName> TagNames()
        {
            throw new NotImplementedException();
        }


        private static IEnumerable<Instruction> GetInstructions(string input)
        {
            var instructions = new List<Instruction>();
            
            var instruction = input.FirstInstruction();
            
            instructions.Add(Instruction.Parse(instruction));
            
            var remaining = input.Remove(0, instruction.Length);
            if (remaining.Length > 0)
                instructions.AddRange(GetInstructions(remaining));
            
            return instructions;
        }

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
            return ReferenceEquals(this, other) ||
                   string.Equals(_text, other._text, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as NeutralText);

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
    }
}