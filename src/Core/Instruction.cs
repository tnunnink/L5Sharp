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
        private const string InstructionPattern = @"^([A-Za-z_][\w]+)\((.*)\)$";
        private const string NamePattern = @"^[a-zA-Z0-9_]+";
        private const string OperandsPattern = @"^[a-zA-Z0-9_]+";

        /// <summary>
        /// Creates a new <see cref="Instruction"/> with the provided name and operands.
        /// </summary>
        /// <param name="name">The name name of the instruction.</param>
        /// <param name="operands">The operands or arguments of the instruction.</param>
        /// <exception cref="ArgumentNullException">name or operands is null.</exception>
        public Instruction(string name, params Operand[] operands)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Operands = operands ?? throw new ArgumentNullException(nameof(operands));
        }
        
        /// <summary>
        /// Creates a new <see cref="Instruction"/> with the provided name and operands.
        /// </summary>
        /// <param name="name">The name name of the instruction.</param>
        /// <param name="operands">The operands or arguments of the instruction.</param>
        /// <exception cref="ArgumentNullException">name or operands is null.</exception>
        public Instruction(string name, IEnumerable<Operand> operands)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Operands = operands ?? throw new ArgumentNullException(nameof(operands));
        }

        /// <summary>
        /// Gets the element name of the <see cref="Instruction"/> instance.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the <see cref="Operand"/> collection for the <see cref="Instruction"/> instance.
        /// </summary>
        public IEnumerable<Operand> Operands { get; }

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

            var match = Regex.Match(text, InstructionPattern, RegexOptions.Compiled);

            if (!match.Success)
                throw new FormatException($"Text input '{text}' does not have expected instruction format.");

            var name = match.Groups[0].Value;
            var operands = match.Groups[1].Value.Split(",").Select(v => (Operand)v);
            
            return new Instruction(name, operands);
        }

        /// <summary>
        /// Generates the <see cref="NeutralText"/> value for the current <see cref="Instruction"/>.
        /// </summary>
        /// <returns>A new <see cref="NeutralText"/> object that represents the instruction text.</returns>
        public NeutralText ToText() => new(ToString());

        /// <inheritdoc />
        public override string ToString() => $"{Name}({string.Join(",", Operands)})";

        /// <summary>
        /// Creates a XIC instruction with the provided tag name value.
        /// </summary>
        /// <param name="tagName">The tag name operand of the instruction.</param>
        /// <returns>A new <see cref="Instruction"/> with the provided value.</returns>
        public static Instruction XIC(TagName tagName) => new(nameof(XIC), tagName);

        /// <summary>
        /// Creates a OTE instruction with the provided tag name value.
        /// </summary>
        /// <param name="tagName">The tag name operand of the instruction.</param>
        /// <returns>A new <see cref="Instruction"/> with the provided value.</returns>
        public static Instruction OTE(TagName tagName) => new(nameof(OTE), tagName);
    }
}