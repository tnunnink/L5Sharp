using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    /// <summary>
    /// A wrapper class around the textual representation of the ladder logic notation called neutral text. 
    /// </summary>
    /// <remarks>
    /// Neutral text can represent a single instruction or a full rung (collection of instructions).
    /// Each instruction contains sets of tag names and values known as operands.
    /// This class provides functions for extracting the textual information into strongly type classes that are easier
    /// to work with.
    /// </remarks>
    /// <seealso cref="Instruction"/>
    /// <seealso cref="TagName"/>
    public sealed class NeutralText : IEquatable<NeutralText>
    {
        private readonly string _text;

        /// <summary>
        /// The regex pattern for Logix tag names without starting and ending anchors.
        /// This pattern also includes a negative lookahead for removing text prior to parenthesis (i.e. instruction keys)
        /// Use this patter for tag names within text, such as longer
        /// </summary>
        private const string TagNamePattern =
            @"(?!\w*\()[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?";

        /// <summary>
        /// Pattern for identifying any instruction and the contents of it's signature. This expression should
        /// capture everything enclosed or between the instruction parentheses. This includes nested parenthesis.
        /// This works on the assumption that the text has balances opening/closing parentheses.
        /// </summary>
        private const string InstructionPattern = @"[A-Za-z_]\w{1,39}\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\)";

        /// <summary>
        /// Pattern finds all text prior to opening parentheses, which is the instruction name or key that identifies
        /// the instruction. 
        /// </summary>
        private const string InstructionKeysPattern = @"[A-Za-z_]\w{1,39}(?=\()";

        /// <summary>
        /// Pattern finds all text prior to opening parentheses, which is the instruction name or key that identifies
        /// the instruction. 
        /// </summary>
        private const string SignaturePattern = @"\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\)";

        private const string OperandsPattern =
            @"(?<=[A-Za-z_]\w{1,39}\()(?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))(?=\))";

        private const string KeyOperandGroupPattern =
            @"([A-Za-z_]\w{1,39})\(((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!)))\)";


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
        /// Indicates whether the current neutral text value has balanced brackets and parentheses.
        /// </summary>
        /// <value><c>true</c> if the text has balanced brackets and parentheses; otherwise, <c>false</c>.</value>
        public bool IsBalanced => TextIsBalanced(_text, '[', ']') && TextIsBalanced(_text, '(', ')');

        /// <summary>
        /// Indicates whether the current neutral text value is a single instruction.
        /// </summary>
        /// <value><c>true</c> if the text represents a single instruction. <c>false</c> if not, meaning the text is a
        /// collection of multiple instruction patterns.</value>
        public bool IsMany => !Regex.IsMatch(_text, $"^{InstructionPattern}$");

        /// <summary>
        /// Indicates whether the current neutral text value is an empty string.
        /// </summary>
        /// <value><c>true</c> if the text empty; otherwise <c>false</c>.</value>
        public bool IsEmpty => _text.IsEmpty();

        /// <summary>
        /// Returns a value indicating whether a specified instruction signature occurs within this neutral text.
        /// </summary>
        /// <param name="instruction">The instruction for which to seek.</param>
        /// <returns><c>true</c> if this text contains the signature patter defined by <c>instruction</c>.</returns>
        public bool ContainsSignature(Instruction instruction) => Regex.IsMatch(_text, instruction.Signature);

        /// <summary>
        /// Returns a value indicating whether a specified instruction key occurs within this neutral text.
        /// </summary>
        /// <param name="instructionKey">The instruction name to seek.</param>
        /// <returns><c>true</c> if this text contains the instruction key; otherwise, false..</returns>
        public bool ContainsKey(string instructionKey) => Regex.Matches(_text, InstructionKeysPattern).Any(m =>
            string.Equals(m.Value, instructionKey, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Returns a value indication whether a specified tag name occurs within this neutral text.
        /// </summary>
        /// <param name="tagName">The tag name to seek.</param>
        /// <param name="comparer">The optional equality comparer to user for evaluating tag equivalence.</param>
        /// <returns><c>true</c> if <c>tagName</c> is contained withing the text and passes the equality check
        /// specified by <c>comparer</c>.</returns>
        public bool ContainsTag(TagName tagName, IEqualityComparer<TagName>? comparer = null)
        {
            comparer ??= TagNameComparer.FullName;
            return Tags().Any(t => comparer.Equals(t, tagName));
        }

        /// <summary>
        /// Returns a value indicating whether a specified subtext occurs within this neutral text.
        /// </summary>
        /// <param name="text">The neutral text to seek.</param>
        /// <returns><c>true</c> if <c>text</c> occurs within this text, or if text is an empty string ("");
        /// otherwise, <c>false</c>.</returns>
        public bool ContainsText(NeutralText text) => _text.Contains(text);

        /// <summary>
        /// Runs the provided regex pattern against the neutral text and indicates whether the patterns is matched.
        /// </summary>
        /// <param name="regex">The regex pattern to test against.</param>
        /// <returns><c>true</c> if <c>regex</c> is a match against this neutral text value.</returns>
        public bool HasPattern(string regex) => Regex.IsMatch(_text, regex);

        /// <summary>
        /// Returns a collection of <see cref="Instruction"/> objects that were found in the current neutral text value.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="Instruction"/> objects found in the text.</returns>
        public IEnumerable<Instruction> Instructions()
        {
            var results = new List<Instruction>();

            var keys = Regex.Matches(_text, InstructionKeysPattern).Select(m => m.Value);

            foreach (var key in keys)
            {
                if (Instruction.TryFromName(key, out var instruction) && instruction is not null)
                {
                    results.Add(instruction);
                    continue;
                }

                //Just assuming an unknown instruction is an AOI and is by default destructive.
                results.Add(new Instruction(key, true));
            }

            return results;
        }

        /// <summary>
        /// Gets a collection of instruction keys or names that occur in the current neutral text.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> containing the names/keys of al found instructions.</returns>
        public IEnumerable<string> Keys() => Regex.Matches(_text, InstructionKeysPattern).Select(m => m.Value);

        /// <summary>
        /// Returns a collection of all found operands in the current neutral text value.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the instruction(s) operand arguments found in the current text.</returns>
        /// <remarks>This will return a flat list of operands for all instructions. If you want to get operands
        /// and corresponding instruction key, use the <see cref="OperandsByKey()"/> methods, which will return found
        /// operands along with the corresponding instruction.
        /// </remarks>
        /// <seealso cref="OperandsByKey()"/>
        public IEnumerable<string> Operands() =>
            Regex.Matches(_text, OperandsPattern).SelectMany(m => m.Value.Split(","));

        /// <summary>
        /// Returns a collection of <see cref="KeyValuePair"/> with the key being the instruction and corresponding
        /// operand arguments as the value.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair"/> objects containing instruction key
        /// and corresponding operands values.</returns>
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> OperandsByKey() =>
            Regex.Matches(_text, KeyOperandGroupPattern).Select(m =>
                new KeyValuePair<string, IEnumerable<string>>(m.Groups[1].Value, m.Groups[2].Value.Split(",")));

        /// <summary>
        /// Returns a collection of <see cref="KeyValuePair"/> with the key being the instruction and corresponding
        /// operand arguments as the value.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair"/> objects containing instruction key
        /// and corresponding operands values.</returns>
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> OperandsByKey(string key) =>
            Regex.Matches(_text, @$"(?<={key}\()(?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))(?=\))")
                .Select(m => new KeyValuePair<string, IEnumerable<string>>(key, m.Value.Split(',')));

        /// <summary>
        /// Returns a collection of <see cref="KeyValuePair"/> with the key being the instruction and corresponding
        /// operand arguments as the value.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair"/> objects containing instruction key
        /// and corresponding operands values.</returns>
        public IEnumerable<KeyValuePair<Instruction, IEnumerable<string>>> OperandsByKey(Instruction instruction) =>
            Regex.Matches(_text, @$"(?<={instruction.Name}\()(?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))(?=\))")
                .Select(m => new KeyValuePair<Instruction, IEnumerable<string>>(instruction, m.Value.Split(',')));
        
        /// <summary>
        /// Returns a collection of <see cref="KeyValuePair{TKey,TValue}"/> with the key being a tag found in the
        /// neutral text instance, and the value being it's corresponding instruction sub text where it is referenced
        /// within the neutral text instance. 
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair"/> objects containing
        /// <see cref="TagName"/> and <see cref="NeutralText"/> pairs.</returns>
        public IEnumerable<KeyValuePair<TagName, NeutralText>> References()
        {
            return Regex.Matches(_text, KeyOperandGroupPattern).SelectMany(m =>
            {
                var tags = Regex.Matches(m.Value, TagNamePattern).Select(tn => new TagName(tn.Value));
                return tags.Select(t => new KeyValuePair<TagName, NeutralText>(t, m.Value));
            });
        }

        /// <summary>
        /// Splits the current neutral text into a collection of sub-texts representing each individual instruction found
        /// in the current text value.
        /// </summary>
        /// <returns>An collection of <see cref="NeutralText"/> objects that represent each individual instruction text.</returns>
        public IEnumerable<NeutralText> Split() =>
            Regex.Matches(_text, InstructionPattern).Select(m => new NeutralText(m.Value));

        /// <summary>
        /// Splits the current neutral text into a collection of sub-texts representing a specific individual instruction.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="NeutralText"/> objects that were split.</returns>
        public IEnumerable<NeutralText> SplitFor(Instruction instruction) =>
            Regex.Matches(_text, instruction.Signature).Select(m => new NeutralText(m.Value));

        /// <summary>
        /// Splits the current neutral text into a collection of sub-texts representing a specific individual instruction key.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="NeutralText"/> objects that were split.</returns>
        public IEnumerable<NeutralText> SplitFor(string key) =>
            Regex.Matches(_text, $"{key}{SignaturePattern}").Select(m => new NeutralText(m.Value));

        /// <summary>
        /// Gets a collection of tag names found in the current neutral text.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> values that were in from the current text.</returns>
        /// <seealso cref="TagsIn(L5Sharp.Enums.Instruction)"/>
        /// <seealso cref="TagsIn(System.String)"/>
        public IEnumerable<TagName> Tags() => Regex.Matches(_text, TagNamePattern).Select(m => new TagName(m.Value));
        
        /*/// <summary>
        /// Returns a collection of <see cref="KeyValuePair"/> with the key being the instruction and corresponding
        /// operand arguments as the value.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair"/> objects containing instruction key
        /// and corresponding operands values.</returns>
        public IEnumerable<KeyValuePair<string, IEnumerable<TagName>>> TagsByKey() =>
            Regex.Matches(_text, KeyOperandGroupPattern).Select(m =>
                new KeyValuePair<string, IEnumerable<string>>(m.Groups[1].Value, m.Groups[2].Value.Split(",")));*/

        /// <summary>
        /// Gets a collection of tag names found in the current neutral text that are operands or arguments to a specific instruction.
        /// </summary>
        /// <param name="instruction">The instruction for which to find tags as arguments to.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing tag names found in the specified instruction.</returns>
        /// <seealso cref="TagsIn(System.String)"/>
        public IEnumerable<TagName> TagsIn(Instruction instruction) =>
            Regex.Matches(_text, instruction.Signature)
                .SelectMany(m => Regex.Matches(m.Value, TagNamePattern))
                .Select(m => new TagName(m.Value));

        /// <summary>
        /// Gets all tags found in the current neutral text that are arguments to a specific instruction key value. 
        /// </summary>
        /// <param name="key">The name of the instruction for which to get tags.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing tag names found in the specified instruction.</returns>
        /// <seealso cref="TagsIn(L5Sharp.Enums.Instruction)"/>
        public IEnumerable<TagName> TagsIn(string key) =>
            Regex.Matches(_text, $"{key}{SignaturePattern}")
                .SelectMany(m => Regex.Matches(m.Value, TagNamePattern))
                .Select(m => new TagName(m.Value));

        /// <summary>
        /// Represents a new empty instance of the <see cref="NeutralText"/>.
        /// </summary>
        /// <returns>An empty <see cref="NeutralText"/> object.</returns>
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

        private bool TextIsBalanced(string value, char opening, char closing)
        {
            var characters = new Stack<char>();

            foreach (var c in value)
            {
                if (Equals(c, opening))
                    characters.Push(c);

                if (!Equals(c, closing)) continue;

                if (!characters.TryPop(out _))
                    return false;
            }

            return characters.Count == 0;
        }
    }
}