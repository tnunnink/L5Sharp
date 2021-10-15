using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    public class Sint : Predefined
    {
        public Sint() : base(LoadElement(nameof(Sint).ToUpper()))
        {
        }

        public override object DefaultValue => default(byte);
        public override Radix DefaultRadix => Radix.Decimal;
        
        public override object ParseValue(string value)
        {
            if (byte.TryParse(value, out var result))
                return result;
            
            return null;
        }
            
        public override bool IsValidValue(object value)
        {
            if (value is string)
                value = ParseValue(value.ToString());
            
            return value is byte;
        }
    }
}