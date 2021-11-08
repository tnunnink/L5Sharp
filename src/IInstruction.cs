using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    public interface IInstruction : ILogixComponent
    {
        NeutralText Signature { get; }
        IEnumerable<IMember<IDataType>> Parameters { get; }
        IMember<IDataType> GetParameter(string name);
        IMember<TType> GetParameter<TType>(string name) where TType : IDataType;
        NeutralText Of(params ITagMember<IDataType>[] tags);
        NeutralText Of(params object[] values);
    }

    public interface IInstruction<in T1> : IInstruction
    {
        NeutralText Of(T1 parameter1);
    }
    
    public interface IInstruction<in T1, in T2> : IInstruction
    {
        NeutralText Of(T1 parameter1, T2 parameter2);
    }
    
    public interface IInstruction<in T1, in T2, in T3> : IInstruction
    {
        NeutralText Of(T1 parameter1, T2 parameter2, T3 parameter3);
    }
    
    public interface IInstruction<in T1, in T2, in T3, in T4> : IInstruction
    {
        NeutralText Of(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4);
    }
}