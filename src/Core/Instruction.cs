using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Instruction : IInstruction
    {
        private readonly Dictionary<string, IMember<IDataType>>
            _operands = new Dictionary<string, IMember<IDataType>>();

        protected Instruction(string name, string description, IEnumerable<IMember<IDataType>> operands = null)
        {
            Validate.Name(name);

            Name = name;
            Description = description;

            operands ??= Array.Empty<Member<IDataType>>();

            foreach (var operand in operands)
            {
                if (_operands.ContainsKey(operand.Name))
                    throw new ComponentNameCollisionException(operand.Name, typeof(Member<IDataType>));

                _operands.Add(operand.Name, operand);
            }
        }

        public string Name { get; }
        public string Description { get; }
        public string Signature => $"{Name}({string.Join(",", _operands.Values.Select(m => m.Name))})";
        public IEnumerable<IMember<IDataType>> Operands => _operands.Values.AsEnumerable();

        public NeutralText Of(params ITagMember<IDataType>[] tags)
        {
            return new NeutralText(this, tags.Select(t => t.Name));
        }

        public NeutralText Of(params IAtomic[] values)
        {
            return new NeutralText(this, values.Select(v => v.GetValue()));
        }
    }
}