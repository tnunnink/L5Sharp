namespace L5Sharp.Builders
{
    internal class RungBuilderStart : IRungBuilderStart
    {
        private readonly RungBuilderContext _context;

        public RungBuilderStart(RungBuilderContext context)
        {
            _context = context;
        }

        public IRungBuilderInput When(string text)
        {
            _context.Append(text);
            return _context.Input;
        }

        public IRungBuilderOutput Do(string text)
        {
            _context.Append(text);
            return _context.Output;
        }
    }
}