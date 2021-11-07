using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    public interface IInstruction : ILogixComponent
    {
        string Signature { get; }
        IEnumerable<IMember<IDataType>> Operands { get; }
        NeutralText Of(params ITagMember<IDataType>[] tags);
        NeutralText Of(params IAtomic[] values);
    }

    public interface IInstruction<in T1> : IInstruction 
        where T1 : IDataType
    {
        NeutralText Of(ITagMember<T1> tag);
        NeutralText Of(T1 value);
    }
    
    public interface IInstruction<in T1, in T2> : IInstruction 
        where T1 : IDataType
        where T2 : IDataType
    {
        NeutralText Of(ITagMember<T1> tag1, ITagMember<T2> tag2);
        NeutralText Of(T1 value1, T2 value2);
    }
    
    public interface IInstruction<in T1, in T2, in T3> : IInstruction 
        where T1 : IDataType
        where T2 : IDataType
        where T3 : IDataType
    {
        NeutralText Of(ITagMember<T1> tag1, ITagMember<T2> tag2, ITagMember<T3> tag3);
        NeutralText Of(T1 value1, T2 value2, T3 value3);
    }
    
    public interface IInstruction<in T1, in T2, in T3, in T4> : IInstruction 
        where T1 : IDataType
        where T2 : IDataType
        where T3 : IDataType
        where T4 : IDataType
    {
        NeutralText Of(ITagMember<T1> tag1, ITagMember<T2> tag2, ITagMember<T3> tag3, ITagMember<T4> tag4);
        NeutralText Of(T1 value1, T2 value2, T3 value3, T4 value4);
    }
}