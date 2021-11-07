using L5Sharp.Core;

namespace L5Sharp.Builders
{
    public class RungBuilder : IRungBuilder
    {
        private readonly int _number;
        private readonly string _comment;
        private readonly RungBuilderContext _context;

        internal RungBuilder(RungBuilderContext context, int number, string comment = null)
        {
            _context = context;
            _number = number;
            _comment = comment;
        }

        public static IRungBuilderSegment New(int number, string comment = null)
        {
            var context = new RungBuilderContext(number, comment);
            return context.SegmentBuilder;
        }

        public IRung Build()
        {
            return new Rung(_number, _comment, _context.Compile());
        }
    }
}