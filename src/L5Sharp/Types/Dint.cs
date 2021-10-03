using L5Sharp.Enumerations;

namespace L5Sharp.Types
{
    public class Dint : Predefined
    {
        public Dint() : base(LoadElement(nameof(Dint).ToUpper()))
        {
        }

        public override object Default => default(int);

        public override object ParseValue(string value)
        {
            if (int.TryParse(value, out var result))
                return result;
            return null;
        }
            
        public override bool IsValidValue(object value)
        {
            return value is int;
        }
    }
}