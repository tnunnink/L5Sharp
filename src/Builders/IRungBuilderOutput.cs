using System;

namespace L5Sharp.Builders
{
    public interface IRungBuilderOutput
    {
        IRungBuilderOutput And(INeutralText text);
        IRungBuilderOutput Branch(INeutralText text, Action<IRungBuilderOutput> branch);
        IRungBuilder Return();
    }
}