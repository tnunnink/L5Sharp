using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Builders
{
    internal class RungBuilderContext
    {
        private const string BranchStartChar = "[";
        private const string BranchAppendChar = ",";
        private const string BranchEndChar = "]";
        private readonly Stack<string> _rungText = new Stack<string>();

        public RungBuilderContext(int number, string comment)
        {
            Builder = new RungBuilder(this, number, comment);
            SegmentBuilder = new RungBuilderSegment(this);
            InputBuilder = new RungBuilderInput(this);
            OutputBuilder = new RungBuilderOutput(this);
            BranchBuilder = new RungBuilderBranch(this);
        }

        public IRungBuilder Builder { get; }
        public IRungBuilderSegment SegmentBuilder { get; }
        public IRungBuilderInput InputBuilder { get; }
        public IRungBuilderOutput OutputBuilder { get; }
        public IRungBuilderBranch BranchBuilder { get; }

        public void AppendSingle(string text)
        {
            _rungText.Push(text);
        }

        public void AppendBranch(Action<IRungBuilderSegment> branch)
        {
            var previous = _rungText.Pop();

            if (previous == BranchEndChar)
            {
                _rungText.Push(BranchAppendChar);
                branch.Invoke(SegmentBuilder);
                _rungText.Push(previous);
                return;
            }
            
            _rungText.Push(BranchStartChar);
            _rungText.Push(previous);
            _rungText.Push(BranchAppendChar);
            branch.Invoke(SegmentBuilder);
            _rungText.Push(BranchEndChar);
        }
        
        public void AppendBranch(string text)
        {
            var previous = _rungText.Pop();

            if (previous == BranchEndChar)
            {
                _rungText.Push(BranchAppendChar);
                _rungText.Push(text);
                _rungText.Push(previous);
                return;
            }
            
            _rungText.Push(BranchStartChar);
            _rungText.Push(previous);
            _rungText.Push(BranchAppendChar);
            _rungText.Push(text);
            _rungText.Push(BranchEndChar);
        }

        public string Compile()
        {
            return string.Join(string.Empty, _rungText.Reverse());
        }
    }
}