using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core
{
    public class NeutralText : INeutralText
    {
        private readonly string _signature;
        private readonly List<string> _arguments;

        public NeutralText(string text)
        {
            //todo parse text into signature?
        }

        public NeutralText(IInstruction instruction)
        {
            _signature = instruction.Signature;
            _arguments = new List<string>(instruction.Operands.Count());
        }

        public NeutralText(IInstruction instruction, params ITagMember[] tags) : this(instruction)
        {
            //todo add given tags ot arguments
        }
        
        public static INeutralText<TInstruction> Create<TInstruction>()
            where TInstruction : IInstruction, new()
        {
            var instruction = new TInstruction();
            return new NeutralText<TInstruction>(instruction);
        }
        
        public static INeutralText<TInstruction> Create<TInstruction>(params ITagMember[] tags)
            where TInstruction : IInstruction, new()
        {
            var instruction = new TInstruction();
            return new NeutralText<TInstruction>(instruction);
        }
        
        public static INeutralText<TInstruction> Create<TInstruction>(params object[] tags)
            where TInstruction : IInstruction, new()
        {
            var instruction = new TInstruction();
            return new NeutralText<TInstruction>(instruction);
        }

        public string Instruction => _signature[1..];
        public string Text => $"{Instruction}({string.Join(",", _arguments)})";

        public void Assign(int index, ITagMember tag)
        {
            //todo valid index?
            //todo tag member not null?

            _arguments[index] = tag.FullName;
        }

        public void Assign(int index, object value)
        {
            //todo valid index?
            //todo value not null?

            _arguments[index] = value.ToString();
        }
    }
    
    public class NeutralText<TInstruction> : NeutralText, INeutralText<TInstruction> where TInstruction : IInstruction
    {
        private readonly IInstruction _instruction;

        public NeutralText(IInstruction instruction) : base(instruction)
        {
            _instruction = instruction;
        }

        public NeutralText(IInstruction instruction, params ITag[] tags) : base(instruction, tags)
        {
            _instruction = instruction;
        }

        public void Assign(Func<TInstruction, IMember> expression, ITagMember tag)
        {
            throw new NotImplementedException();
        }

        public void Assign(Func<TInstruction, IMember> expression, object value)
        {
            throw new NotImplementedException();
        }
    }
}