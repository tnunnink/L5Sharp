using System.Collections.Generic;

namespace L5Sharp
{
    public interface IInstruction : ILogixComponent
    {
        string Signature { get; }
        IEnumerable<IMember> Operands { get; }
        INeutralText GenerateText(params ITagMember[] tags);
        INeutralText GenerateText(params object[] tags);

        INeutralText<TInstruction> GenerateText<TInstruction>(params ITagMember[] tags)
            where TInstruction : IInstruction;
    }
}