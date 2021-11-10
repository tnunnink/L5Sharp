using System;
using L5Sharp.Core;

namespace L5Sharp.Builders
{
    internal class RungBuilderSegment : IRungBuilderSegment
    {
        private readonly RungBuilderContext _context;

        public RungBuilderSegment(RungBuilderContext context)
        {
            _context = context;
        }

        public IRungBuilderInput When(string text)
        {
            _context.AppendSingle(text);
            return _context.InputBuilder;
        }

        public IRungBuilderInput When<TInstruction>(Func<TInstruction, NeutralText> of) where TInstruction : IInstruction, new()
        {
            var instruction = new TInstruction();
            var text = of.Invoke(instruction);
            _context.AppendSingle(text.Signature);
            return _context.InputBuilder;
        }

        public IRungBuilderOutput Do(string text)
        {
            _context.AppendSingle(text);
            return _context.OutputBuilder;
        }
    }
}