using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace L5Sharp.Core
{
    public readonly struct NeutralText
    {
        private readonly List<object> _arguments;

        public NeutralText(string text)
        {
            if (!Regex.IsMatch(text, @"^[a-zA-Z0-9_]*\(.*?\)$"))
                throw new ArgumentException();

            Instruction = Regex.Match(text, @"^[a-zA-Z0-9_]+").Value;
            _arguments = new List<object>(Regex.Match(text, @"(?<=\().+?(?=\))").Value.Split(','));
        }

        public NeutralText(IInstruction instruction)
        {
            Instruction = instruction.Name;
            _arguments = new List<object>(instruction.Parameters.Count());
        }

        public NeutralText(IInstruction instruction, params object[] args) : this(instruction)
        {
            //todo validate arrays
            _arguments = new List<object>(args);
        }

        public string Instruction { get; }
        public string Signature => $"{Instruction}({string.Join(",", _arguments)})";
        public IEnumerable<object> Arguments => _arguments;
    }
}