using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    /// <summary>
    /// A wrapper class around the textual representation of the ladder logix notation called neutral text. 
    /// </summary>
    /// <remarks>
    /// Neutral text cna represent a single instruction or a full rung (collection of instructions).
    /// Each instruction likely contains sets of tag names.
    /// This class provides functions for extracting the textual information into strongly type classes that are easier
    /// to work with.
    /// </remarks>
    /// <seealso cref="Instruction"/>
    /// <seealso cref="TagName"/>
    public sealed class NeutralText : IEquatable<NeutralText>
    {
        private readonly string _text;
        
        /// <summary>
        /// Creates a new <see cref="NeutralText"/> object with the provided text input.
        /// </summary>
        /// <param name="text">A string input that represents a neutral text format. The text may contain </param>
        /// <exception cref="ArgumentNullException">When text is null.</exception>
        /// <exception cref="FormatException">When text is null.</exception>
        public NeutralText(string text)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="NeutralText"/> value has balanced brackets and parenthesis.
        /// </summary>
        public bool IsBalanced => _text.IsBalanced('[', ']') && _text.IsBalanced('(', ')');

        /// <summary>
        /// Gets a collection of <see cref="Instruction"/> objects that are present in the <see cref="NeutralText"/> instance.
        /// </summary>
        /// <returns>An collection of <see cref="Instruction"/> objects that were parsed.</returns>
        public IEnumerable<Instruction> Instructions() => GetInstructions(_text);

        /// <summary>
        /// Gets a collection of <see cref="TagName"/> values that are present in the <see cref="NeutralText"/> instance.
        /// </summary>
        /// <returns>A new collection of <see cref="TagName"/> values that were extracted from the current text.</returns>
        public IEnumerable<TagName> TagNames()
        {
            return Regex.Matches(_text, @"(?!\w*\()[A-Za-z_][\w:.,[\]]+", RegexOptions.Compiled)
                .SelectMany(m => Regex.Split(m.Value, @"(?<!\[\d+)(?<!\[\d+,\d+),", RegexOptions.Compiled))
                .Where(s => !string.IsNullOrEmpty(s) && Regex.IsMatch(s, @"[A-Za-z][\w:.,[\]]+", RegexOptions.Compiled))
                .Select(t => new TagName(t))
                .ToList();
        }

        /// <summary>
        /// Represents a new default instance of the 
        /// </summary>
        /// <returns></returns>
        public static NeutralText Empty => new(string.Empty);

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
        public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(_text.GetHashCode());

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

        /// <summary>
        /// Iterates over the the provided input value and parses out instruction test into <see cref="Instruction"/> objects.
        /// </summary>
        /// <param name="input">The text to parse.</param>
        /// <returns>A collection of <see cref="Instruction"/> that were parsed from the provided text.</returns>
        /// <remarks>
        /// This method will only return instructions that were able to be parsed using the <see cref="Instruction.TryParse"/>
        /// method. This means invalid neutral text will only 
        /// </remarks>
        private static IEnumerable<Instruction> GetInstructions(string input)
        {
            var instructions = new List<Instruction>();

            while (input.Length > 0)
            {
                input = ConsumeInstruction(input, out var value);

                if (!string.IsNullOrEmpty(value) &&
                    Instruction.TryParse(value, out var instruction) &&
                    instruction is not null)
                {
                    instructions.Add(instruction);
                }
            }

            return instructions;
        }

        /// <summary>
        /// Iterates of the the provided string and captures the first found sequence of characters that represent an
        /// instruction.
        /// </summary>
        /// <param name="value">The input string text to process.</param>
        /// <param name="instruction">When the method returns, the value of the instruction text that was found.</param>
        /// <returns>The remaining string without the consumed instruction text.</returns>
        /// <remarks>
        /// Opted to process instruction text by iteration instead of Regex since the lookahead and/or lookbehind patterns
        /// needed to accommodate nested '(' and ')' characters was overly complex and perhaps non-performant.
        /// </remarks>
        private static string ConsumeInstruction(string value, out string instruction)
        {
            var characters = new List<char>();
            var length = 0;
            var opened = 0;
            var closed = 0;

            foreach (var c in value)
            {
                length++;

                //Before the opening parenthesis, add only if the character is a valid word pattern (\w) or '('.
                //When inside opening parenthesis, add every character.
                if (opened == 0 && Regex.IsMatch(c.ToString(), @"[\w(]$", RegexOptions.Compiled) || opened > 0)
                    characters.Add(c);

                switch (c)
                {
                    case '(':
                        opened++;
                        break;
                    case ')':
                        closed++;
                        break;
                }

                if (opened > 0 && opened == closed)
                    break;
            }

            instruction = new string(characters.ToArray());
            return value.Remove(0, length);
        }
    }
}