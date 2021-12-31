using System;
using System.Collections.Generic;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class MOV : Instruction
    {
        public MOV() : base(nameof(MOV), "Move", GetMembers())
        {
        }

        public NeutralText Of(IAtomicType source, ITagMember<IAtomicType> destination)
        {
            return new NeutralText(this, source, destination.Name);
        }

        public NeutralText Of(ITagMember<IAtomicType> source, ITagMember<IAtomicType> destination)
        {
            return new NeutralText(this, source.Name, destination.Name);
        }

        public IMember<Dint> Source => GetParameter<Dint>(nameof(Source));
        public IMember<Dint> Destination => GetParameter<Dint>(nameof(Destination));

        private static IEnumerable<IMember<IDataType>> GetMembers()
        {
            return new List<IMember<IDataType>>
            {
                Member.Create(nameof(Source), new Dint()),
                Member.Create(nameof(Destination), new Dint())
            };
        }
    }
}