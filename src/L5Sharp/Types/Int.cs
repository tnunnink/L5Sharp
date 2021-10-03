using L5Sharp.Enumerations;

namespace L5Sharp.Types
{
    public class Int : Predefined
    {
        public Int() : base(LoadElement(nameof(Int).ToUpper()))
        {
        }

        public override object Default => default(short);

        public override object ParseValue(string value)
        {
            if (short.TryParse(value, out var result))
                return result;
            return null;
        }
            
        public override bool IsValidValue(object value)
        {
            return value is short;
        }
    }
}