using System;

namespace L5Sharp.Builders
{
    public interface IRungBuilderBranch
    {
        IRungBuilderBranch Or(Action<IRungBuilderStart> branch);
        IRungBuilderInput And(string text);
        IRungBuilderOutput Then(string text);
    }
}