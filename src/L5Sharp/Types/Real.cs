using L5Sharp.Enumerations;

namespace L5Sharp.Types
{
    public class Real : Predefined
    {
        public Real() : base(LoadElement(nameof(Real).ToUpper()))
        {
        }

        public override object DefaultValue => default(float);

        public override bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Float || radix == Radix.Exponential;
        }

        public override object ParseValue(string value)
        {
            if (float.TryParse(value, out var result))
                return result;
            return null;
        }
            
        public override bool IsValidValue(object value)
        {
            if (value is string)
                value = ParseValue(value.ToString());
            
            return value is float;
        }
    }
}