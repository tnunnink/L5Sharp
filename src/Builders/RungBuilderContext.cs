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

        public RungBuilderContext(IRungBuilder builder)
        {
            Builder = builder;
            Start = new RungBuilderStart(this);
            Input = new RungBuilderInput(this);
            Output = new RungBuilderOutput(this);
            Branch = new RungBuilderBranch(this);
        }

        public IRungBuilder Builder { get; }
        public IRungBuilderStart Start { get; }
        public IRungBuilderInput Input { get; }
        public IRungBuilderOutput Output { get; }
        public IRungBuilderBranch Branch { get; }

        public void Append(string text)
        {
            _rungText.Push(text);
        }

        public void BranchStart()
        {
            var previous = _rungText.Pop();
            _rungText.Push(BranchStartChar);
            _rungText.Push(previous);
        }

        public void BranchAppend(string text = null)
        {
            _rungText.Push(BranchAppendChar);

            if (text != null)
                _rungText.Push(text);
        }

        public void BranchEnd()
        {
            _rungText.Push(BranchEndChar);
        }

        public void BranchOutput(string text)
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