using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    public sealed class Int : Atomic
    {
        public Int() : base(nameof(Int).ToUpper())
        {
        }

        public override object DefaultValue => default(short);

        public override bool IsValidValue(object value)
        {
            if (value is string)
                value = ParseValue(value.ToString());
            
            return value is short;
        }

        public override object ParseValue(string value)
        {
            if (short.TryParse(value, out var result))
                return result;
            return null;
        }
    }
}