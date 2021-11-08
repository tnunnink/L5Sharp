using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class MOV : Instruction,
        IInstruction<IAtomic, ITagMember<IAtomic>>,
        IInstruction<ITagMember<IAtomic>, ITagMember<IAtomic>>
    {
        public MOV() : base(nameof(MOV), "Move", GetMembers())
        {
        }

        public NeutralText Of(IAtomic source, ITagMember<IAtomic> destination)
        {
            return new NeutralText(this, source, destination.Name);
        }

        public NeutralText Of(ITagMember<IAtomic> source, ITagMember<IAtomic> destination)
        {
            return new NeutralText(this, source.Name, destination.Name);
        }

        public IMember<Dint> Source => GetParameter<Dint>(nameof(Source));
        public IMember<Dint> Destination => GetParameter<Dint>(nameof(Destination));

        private static IEnumerable<IMember<IDataType>> GetMembers()
        {
            return new List<IMember<IDataType>>
            {
                Member.New(nameof(Source), new Dint()),
                Member.New(nameof(Destination), new Dint())
            };
        }
    }
}