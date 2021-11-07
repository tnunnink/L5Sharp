using System;

namespace L5Sharp.Builders
{
    internal class RungBuilderInput : IRungBuilderInput
    {
        private readonly RungBuilderContext _context;

        public RungBuilderInput(RungBuilderContext context)
        {
            _context = context;
        }

        public IRungBuilderInput And(string text)
        {
            _context.AppendSingle(text);
            return this;
        }

        public IRungBuilderBranch Or(string text)
        {
            _context.AppendBranch(text);
            return _context.BranchBuilder;
        }

        public IRungBuilderBranch Or(Action<IRungBuilderSegment> branch)
        {
            _context.AppendBranch(branch);
            return _context.BranchBuilder;
        }

        public IRungBuilderOutput Then(string text)
        {
            _context.AppendSingle(text);
            return _context.OutputBuilder;
        }
    }
}