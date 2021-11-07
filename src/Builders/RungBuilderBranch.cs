using System;

namespace L5Sharp.Builders
{
    internal class RungBuilderBranch : IRungBuilderBranch
    {
        private readonly RungBuilderContext _context;

        public RungBuilderBranch(RungBuilderContext context)
        {
            _context = context;
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

        public IRungBuilderInput And(string text)
        {
            _context.AppendSingle(text);
            return _context.InputBuilder;
        }

        public IRungBuilderOutput Then(string text)
        {
            _context.AppendSingle(text);
            return _context.OutputBuilder;
        }
    }
}