using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// Creates a new <see cref="Instruction"/> with the provided name and arguments.
        /// </summary>
        /// <param name="name">The name name of the Instruction.</param>
        /// <param name="arguments">The collection of <see cref="Arguments"/> of the Instruction instance.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        private Instruction(string name, IEnumerable<Argument>? arguments = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Arguments = arguments ?? Enumerable.Empty<Argument>();
        }

        /// <summary>
        /// Gets the element name of the <see cref="Instruction"/> instance.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the <see cref="Argument"/> collection for the <see cref="Instruction"/> instance.
        /// </summary>
        public IEnumerable<Argument> Arguments { get; }

        /// <summary>
        /// Creates a new <see cref="Instruction"/> by parsing the provided string text.
        /// </summary>
        /// <param name="text">The instruction text to parse.</param>
        /// <returns>A new <see cref="Instruction"/> instance that represents the provided string text.</returns>
        /// <exception cref="FormatException">The provided text is not a valid instruction text format.</exception>
        public static Instruction Parse(string text)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));
            
            if (!Regex.IsMatch(text, @"^[a-zA-Z0-9_]+\(.*?\)$", RegexOptions.Compiled))
                throw new FormatException($"Text input '{text}' does not have expected format.");

            var name = Regex.Match(text, @"^[a-zA-Z0-9_]+").Value;
            var arguments = new List<string>(Regex.Match(text, @"(?<=\().+?(?=\))").Value.Split(','))
                .Select(v => new Argument(v));

            return new Instruction(name, arguments);
        }

        /// <summary>
        /// Generates a new <see cref="Instruction"/> instance with the provided arguments parameters.
        /// </summary>
        /// <param name="arguments">Set of string arguments to instantiate the <see cref="Instruction"/> with.</param>
        /// <returns>A new <see cref="Instruction"/> with the provided arguments.</returns>
        public Instruction Build(params Argument[] arguments) => new(Name, arguments);

        /// <summary>
        /// Generates the <see cref="NeutralText"/> value for the current <see cref="Instruction"/>.
        /// </summary>
        /// <returns>A new <see cref="NeutralText"/> object that represents the instruction text.</returns>
        public NeutralText ToText() => new($"{Name}({string.Join(",", Arguments.Select(a => a.Reference))})");
    }
}