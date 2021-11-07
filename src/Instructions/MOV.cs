using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class MOV : Instruction
    {
        public MOV() : base(nameof(MOV), "Move", GetMembers())
        {
        }

        public static NeutralText Of(ITagMember<IDataType> source, ITagMember<IDataType> destination)
        {
            if (destination.DataType != source.DataType)
                throw new InvalidOperationException();
            
            return new NeutralText(new MOV(), source.Name, destination.Name);
        }

        public static NeutralText Of(IAtomic source, ITagMember<IDataType> destination)
        {
            if (destination.DataType != source.Name)
                throw new InvalidOperationException();
            
            return new NeutralText(new MOV(), source.GetValue(), destination.Name);
        }

        public IMember<IDataType> Source => Operands.SingleOrDefault(o => o.Name == nameof(Source));

        public IMember<IDataType> Destination => Operands.SingleOrDefault(o => o.Name == nameof(Destination));

        private static IEnumerable<IMember<IDataType>> GetMembers()
        {
            return new List<IMember<IDataType>>
            {
                new Member<IDataType>(nameof(Source), new Dint()),
                new Member<IDataType>(nameof(Destination), new Dint())
            };
        }
    }
}