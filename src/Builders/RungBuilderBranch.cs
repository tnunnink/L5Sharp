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

        public IRungBuilderBranch Or(Action<IRungBuilderStart> branch)
        {
            _context.BranchAppend();
            branch.Invoke(_context.Start);
            return _context.Branch;
        }

        public IRungBuilderInput And(string text)
        {
            _context.BranchEnd();
            _context.Append(text);
            return _context.Input;
        }

        public IRungBuilderOutput Then(string text)
        {
            _context.BranchEnd();
            _context.Append(text);
            return _context.Output;
        }
    }
}