using System;

namespace L5Sharp.Builders
{
    internal class RungBuilderOutput : IRungBuilderOutput
    {
        private readonly RungBuilderContext _context;

        public RungBuilderOutput(RungBuilderContext context)
        {
            _context = context;
        }

        public IRungBuilderOutput And(string text)
        {
            _context.AppendBranch(text);
            return this;
        }

        public IRungBuilderOutput And(Action<IRungBuilderSegment> branch)
        {
            _context.AppendBranch(branch);
            return _context.OutputBuilder;
        }

        public IRungBuilder Compile()
        {
            return _context.Builder;
        }
    }
}