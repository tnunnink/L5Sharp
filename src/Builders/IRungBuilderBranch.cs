using System;

namespace L5Sharp.Builders
{
    public interface IRungBuilderBranch
    {
        IRungBuilderBranch Or(string text);
        IRungBuilderBranch Or(Action<IRungBuilderSegment> branch);
        IRungBuilderInput And(string text);
        IRungBuilderOutput Then(string text);
    }
}