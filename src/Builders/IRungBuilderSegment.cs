using System;
using L5Sharp.Core;

namespace L5Sharp.Builders
{
    public interface IRungBuilderSegment
    {
        IRungBuilderInput When(string text);

        IRungBuilderInput When<TInstruction>(Func<TInstruction, NeutralText> of)
            where TInstruction : IInstruction, new();

        IRungBuilderOutput Do(string text);
    }
}