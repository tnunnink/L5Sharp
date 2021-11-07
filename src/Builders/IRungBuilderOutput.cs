using System;

namespace L5Sharp.Builders
{
    public interface IRungBuilderOutput
    {
        IRungBuilderOutput And(string text);
        IRungBuilderOutput And(Action<IRungBuilderSegment> branch);
        
        IRungBuilder Compile();
    }
}