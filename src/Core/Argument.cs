using System.Text.RegularExpressions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents an argument for an <see cref="IInstruction"/> object.
    /// </summary>
    public class Argument
    {
        internal Argument(string reference)
        {
            Reference = reference;
        }

        /// <summary>
        /// Gets the string value that represents the argument reference.
        /// </summary>
        public string Reference { get; }

        /// <summary>
        /// Indicates whether <see cref="Reference"/> represents a tag name.
        /// </summary>
        public bool IsTag => Regex.IsMatch(Reference, @"^[a-zA-Z_][a-zA-Z_0-9\.]*$", RegexOptions.Compiled);
        
        /// <summary>
        /// Indicates whether <see cref="Reference"/> represents an immediate value.
        /// </summary>
        public bool IsValue => Radix.TryParseValue(Reference, out _);
    }
}