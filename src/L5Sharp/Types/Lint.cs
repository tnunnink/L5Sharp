using L5Sharp.Enumerations;

namespace L5Sharp.Types
{
    public class Lint : Predefined
    {
        public Lint() : base(LoadElement(nameof(Lint).ToUpper()))
        {
        }

        public override object DefaultValue => default(long);
        
        public override Radix DefaultRadix => Radix.Decimal;

        public override bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex
                   || radix == Radix.Ascii || radix == Radix.DateTime || radix == Radix.DateTimeNs;
        }

        public override object ParseValue(string value)
        {
            if (long.TryParse(value, out var result))
                return result;
            return null;
        }
            
        public override bool IsValidValue(object value)
        {
            if (value is string)
                value = ParseValue(value.ToString());
            
            return value is long;
        }
    }
}