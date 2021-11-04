using System;

namespace L5Sharp.Builders
{
    public interface IRungBuilderInput
    {
        IRungBuilderInput And(INeutralText text);
        IRungBuilderInput Or(INeutralText text);
        IRungBuilderInput Branch(INeutralText text, Action<IRungBuilderInput> branch);
        IRungBuilderThen AreEnergized();
    }
}