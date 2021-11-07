using System;

namespace L5Sharp.Builders
{
    public interface IRungBuilderInput
    {
        IRungBuilderInput And(string text);
        IRungBuilderBranch Or(string text);
        IRungBuilderBranch Or(Action<IRungBuilderSegment> branch);
        IRungBuilderOutput Then(string text);
    }
}