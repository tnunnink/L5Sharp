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
        private const string InstructionPattern = @"^([A-Za-z_][\w]*)\((.*)\)$";
        private const string OperandSplitPattern = @"(?<!\[\d+)(?<!\[\d+,\d+),";

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
        /// Parses the provided text input into a new <see cref="Instruction"/> object.
        /// </summary>
        /// <param name="text">The instruction text to parse.</param>
        /// <returns>A new <see cref="Instruction"/> instance that represents the provided string text.</returns>
        /// <exception cref="ArgumentNullException">text is null.</exception>
        /// <exception cref="FormatException">The provided text is not a valid instruction text format.</exception>
        public static Instruction Parse(string text)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            var match = Regex.Match(text, InstructionPattern, RegexOptions.Compiled);

            if (!match.Success)
                throw new FormatException($"Text input '{text}' does not have expected instruction format.");

            var name = match.Groups[1].Value;
            var operands = Regex.Split(match.Groups[2].Value, OperandSplitPattern, RegexOptions.Compiled)
                .Select(s => (Operand)s);

            return new Instruction(name, operands);
        }

        /// <summary>
        /// Attempts to parses the provided text input into a new <see cref="Instruction"/> object.
        /// </summary>
        /// <param name="text">The instruction text to parse.</param>
        /// <param name="instruction">When the method returns, the value of the parsed instruction of the parse was
        /// successful; otherwise a null value.</param>
        /// <returns>A new <see cref="Instruction"/> instance that represents the provided string text.</returns>
        /// <exception cref="ArgumentNullException">text is null.</exception>
        public static bool TryParse(string text, out Instruction? instruction)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            var match = Regex.Match(text, InstructionPattern, RegexOptions.Compiled);

            if (!match.Success)
            {
                instruction = null;
                return false;
            }

            var name = match.Groups[1].Value;
            var operands = Regex.Split(match.Groups[2].Value, OperandSplitPattern, RegexOptions.Compiled)
                .Select(s => (Operand)s);

            instruction = new Instruction(name, operands);
            return true;
        }

        /// <summary>
        /// Generates the <see cref="NeutralText"/> value for the current <see cref="Instruction"/>.
        /// </summary>
        /// <returns>A new <see cref="NeutralText"/> object that represents the instruction text.</returns>
        public NeutralText ToText() => new(ToString());

        /// <inheritdoc />
        public override string ToString() => $"{Name}({string.Join(",", Operands)})";
    }
}