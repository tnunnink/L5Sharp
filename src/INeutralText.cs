using System;

namespace L5Sharp
{
    public interface INeutralText
    {
        string Instruction { get; }
        string Text { get; }
        void Assign(int index, ITagMember tag);
        void Assign(int index, object value);
    }
    
    public interface INeutralText<out TInstruction> : INeutralText where TInstruction : IInstruction
    {
        void Assign(Func<TInstruction, IMember> expression, ITagMember tag);
        void Assign(Func<TInstruction, IMember> expression, object value);
    }
}