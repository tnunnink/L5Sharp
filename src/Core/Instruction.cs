using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class Instruction : IInstruction
    {
        /// <summary>
        /// Creates a new instance of a <c>Instruction</c> with the provided arguments.
        /// </summary>
        /// <param name="element">The element name of the <c>Instruction</c>.</param>
        /// <param name="arguments">The collection of string arguments.</param>
        /// <exception cref="ArgumentNullException">When element is null.</exception>
        private Instruction(string element, IEnumerable<string>? arguments = null)
        {
            Name = element ?? throw new ArgumentNullException(nameof(element));
            Arguments = arguments is not null 
                ? arguments.Select(a => new Argument(a)) 
                : Enumerable.Empty<Argument>();
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public IEnumerable<Argument> Arguments { get; }

        /// <summary>
        /// Creates a new <c>IInstruction</c> with the provided text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static IInstruction FromText(string text)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));
            
            if (!Regex.IsMatch(text, @"^[a-zA-Z0-9_]+\(.*?\)$", RegexOptions.Compiled))
                throw new FormatException($"Text input '{text}' does not have expected format.");

            var name = Regex.Match(text, @"^[a-zA-Z0-9_]+").Value;
            var arguments = new List<string>(Regex.Match(text, @"(?<=\().+?(?=\))").Value.Split(','));

            return new Instruction(name, arguments);
        }

        /// <inheritdoc />
        public NeutralText ToText() => new($"{Name}({string.Join(",", Arguments.Select(a => a.Reference))})");

        /// <inheritdoc />
        public IInstruction Of(params string[] arguments) => new Instruction(Name, arguments);
    }
}