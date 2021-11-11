using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Instruction : IInstruction
    {
        private readonly Dictionary<string, IMember<IDataType>>
            _parameters = new Dictionary<string, IMember<IDataType>>();

        protected Instruction(string name, string description, IEnumerable<IMember<IDataType>> parameters = null)
        {
            Validate.Name(name);

            Name = name;
            Description = description;

            parameters ??= Array.Empty<IMember<IDataType>>();

            foreach (var parameter in parameters)
            {
                if (_parameters.ContainsKey(parameter.Name))
                    throw new ComponentNameCollisionException(parameter.Name, typeof(IMember<IDataType>));

                _parameters.Add(parameter.Name, parameter);
            }
        }

        public string Name { get; }
        public string Description { get; }
        public NeutralText Signature { get; internal set; }
        public IEnumerable<IMember<IDataType>> Parameters => _parameters.Values.AsEnumerable();

        public IMember<IDataType> GetParameter(string name)
        {
            _parameters.TryGetValue(name, out var parameter);
            return parameter;
        }
        
        public IMember<TType> GetParameter<TType>(string name) where TType : IDataType
        {
            return _parameters.TryGetValue(name, out var member) && member.DataType is TType 
                ? (IMember<TType>) member 
                : null;
        }

        public NeutralText Of(params ITagMember<IDataType>[] tags)
        {
            return new NeutralText(this, tags.Select(t => t.Name));
        }

        public NeutralText Of(params object[] values)
        {
            return new NeutralText(this, values);
        }
    }
}