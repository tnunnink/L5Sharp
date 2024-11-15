using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace L5Sharp.Core;

/// <summary>
/// A thin wrapper around the textual representation of logic notation called neutral text. This could represent a
/// single instruction signature, a rung of logic (combination of instructions), or a line of structured text. The
/// purpose of this class is to provide a way to parse the text into strongly typed objects that are easier to work with. 
/// </summary>
/// <remarks>
/// Neutral text can represent a single instruction or a full rung (collection of instructions).
/// Each instruction contains sets of tag names and values known as arguments or operands.
/// This class provides functions for extracting the textual information into strongly type classes that are easier
/// to work with.
/// </remarks>
/// <seealso cref="Instruction"/>
/// <seealso cref="TagName"/>
/// <seealso cref="Keyword"/>
public sealed class NeutralText : ILogixParsable<NeutralText>
{
    private readonly string _text;

    /// <summary>
    /// Creates a new <see cref="NeutralText"/> object with the provided text input.
    /// </summary>
    /// <param name="text">A string input that represents a neutral text format.</param>
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
    /// Indicates whether the current neutral text value is an empty string.
    /// </summary>
    /// <value><c>true</c> if the text empty; otherwise <c>false</c>.</value>
    public bool IsEmpty => _text.IsEquivalent(string.Empty);

    /// <summary>
    /// Represents a new empty instance of the <see cref="NeutralText"/>.
    /// </summary>
    /// <returns>An empty <see cref="NeutralText"/> object.</returns>
    public static NeutralText Empty => new(string.Empty);

    /// <summary>
    /// Parses the provided string into a <see cref="NeutralText"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="NeutralText"/> representing the parsed value.</returns>
    public static NeutralText Parse(string value) => new(value);

    /// <summary>
    /// Tries to parse the provided string into a <see cref="NeutralText"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="NeutralText"/> representing the parsed value if successful; Otherwise, <c>null</c>.</returns>
    public static NeutralText? TryParse(string? value) => value is not null ? new NeutralText(value) : null;

    /// <summary>
    /// Returns a value indicating whether a specified instruction key occurs within this neutral text.
    /// </summary>
    /// <param name="value">The instruction name to seek.</param>
    /// <returns><c>true</c> if this text contains the instruction key; otherwise, false.</returns>
    public bool Contains(string value) => _text.Contains(value);

    /// <summary>
    /// Runs the provided regex pattern against the neutral text and indicates whether the patterns are matched.
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
        // ReSharper disable once RedundantEnumerableCastCall required for .NET Standard
        return Regex.Matches(_text, Instruction.Pattern).Cast<Match>().Select(m => Instruction.Parse(m.Value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IEnumerable<Instruction> Instructions(string key)
    {
        var matches = Regex.Matches(_text, Instruction.Pattern);

        foreach (Match match in matches)
        {
            var instruction = Instruction.Parse(match.Value);
            if (instruction.Key != key) continue;
            yield return instruction;
        }
    }

    /// <summary>
    /// Returns a collection of <see cref="Instruction"/> objects that were found in the current neutral text value.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="Instruction"/> objects found in the text.</returns>
    public IEnumerable<Instruction> Instructions(Instruction instruction)
    {
        var matches = Regex.Matches(_text, Instruction.Pattern);

        foreach (Match match in matches)
        {
            var i = Instruction.Parse(match.Value);
            if (i != instruction) continue;
            yield return i;
        }
    }

    /// <summary>
    /// Gets a collection of keywords found in the current neutral text.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Keyword"/> values found.</returns>
    public IEnumerable<Keyword> Keywords()
    {
        return Keyword.All().Where(k => _text.Contains(k.Value));
    }

    /// <summary>
    /// Gets a collection of tag names found in the current neutral text.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> values that were in from the current text.</returns>
    /// <seealso cref="TagsIn(string)"/>
    public IEnumerable<TagName> Tags()
    {
        var matches = Regex.Matches(_text, TagName.SearchPattern);
        
        foreach (Match match in matches)
        {
            yield return new TagName(match.Value);
        }
    }

    /// <summary>
    /// Gets a collection of tag names found in the current neutral text that are operands or arguments to a specific instruction.
    /// </summary>
    /// <param name="instruction">The instruction for which to find tags as arguments to.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing tag names found in the specified instruction.</returns>
    public IEnumerable<TagName> TagsIn(string instruction) => Instructions(instruction).SelectMany(i => i.Text.Tags());

    /// <inheritdoc />
    public override string ToString() => _text;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        return obj switch
        {
            NeutralText other => _text.IsEquivalent(other._text),
            string other => _text.IsEquivalent(other),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(_text);

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

    private static bool TextIsBalanced(string value, char opening, char closing)
    {
        var characters = new Stack<char>();

        foreach (var c in value)
        {
            if (Equals(c, opening))
                characters.Push(c);

            if (!Equals(c, closing)) continue;

            if (characters.Count == 0)
                return false;

            characters.Pop();
        }

        return characters.Count == 0;
    }
}