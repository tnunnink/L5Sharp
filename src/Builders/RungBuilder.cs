using L5Sharp.Core;

namespace L5Sharp.Builders
{
    public class RungBuilder : IRungBuilder
    {
        private readonly int _number;
        private readonly string _comment;
        private readonly RungBuilderContext _context;

        private RungBuilder(int number, string comment = null)
        {
            _number = number;
            _comment = comment;
            _context = new RungBuilderContext(this);
        }

        public static IRungBuilderStart New(int number, string comment = null)
        {
            var builder = new RungBuilder(number, comment);
            return new RungBuilderStart(builder._context);
        }

        public IRung Build()
        {
            return new Rung(_number, _comment, _context.Compile());
        }
    }
}