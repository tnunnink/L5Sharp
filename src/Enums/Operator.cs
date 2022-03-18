using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class Operator : SmartEnum<Operator, string>
    {
        private Operator(string name, string value) : base(name, value)
        {
        }

        public static readonly Operator Assignment = new(nameof(Assignment), ":=");
        public static readonly Operator Equal = new(nameof(Equals), "=");
        public static readonly Operator NotEqual = new(nameof(Equals), "<>");
        public static readonly Operator GreaterThan = new(nameof(Equals), ">");
        public static readonly Operator GreaterThanOrEqual = new(nameof(Equals), ">=");
        public static readonly Operator LessThan = new(nameof(Equals), "<");
        public static readonly Operator LessThanOrEqual = new(nameof(Equals), "<=");
        public static readonly Operator Add = new(nameof(Equals), "+");
        public static readonly Operator Subtract = new(nameof(Equals), "-");
        public static readonly Operator Multiply = new(nameof(Equals), "*");
        public static readonly Operator Exponent = new(nameof(Equals), "**");
        public static readonly Operator Divide = new(nameof(Equals), "/");
        public static readonly Operator Modulo = new(nameof(Equals), "MOD");
        public static readonly Operator And = new(nameof(Equals), "AND");
        public static readonly Operator Or = new(nameof(Equals), "OR");
        public static readonly Operator Xor = new(nameof(Equals), "XOR");
        public static readonly Operator Not = new(nameof(Equals), "NOT");
        
    }
}