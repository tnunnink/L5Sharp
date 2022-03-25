using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of known Logix operators.
    /// </summary>
    public class Operator : SmartEnum<Operator, string>
    {
        private Operator(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents the Assignment Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Assignment = new(nameof(Assignment), ":=");
        
        /// <summary>
        /// Represents the Equal Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Equal = new(nameof(Equal), "=");
        
        /// <summary>
        /// Represents the NotEqual Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator NotEqual = new(nameof(NotEqual), "<>");
        
        /// <summary>
        /// Represents the GreaterThan Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator GreaterThan = new(nameof(GreaterThan), ">");
        
        /// <summary>
        /// Represents the GreaterThanOrEqual Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator GreaterThanOrEqual = new(nameof(GreaterThanOrEqual), ">=");
        
        /// <summary>
        /// Represents the LessThan Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator LessThan = new(nameof(LessThan), "<");
        
        /// <summary>
        /// Represents the LessThanOrEqual Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator LessThanOrEqual = new(nameof(LessThanOrEqual), "<=");
        
        /// <summary>
        /// Represents the Add Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Add = new(nameof(Add), "+");
        
        /// <summary>
        /// Represents the Subtract Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Subtract = new(nameof(Subtract), "-");
        
        /// <summary>
        /// Represents the Multiply Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Multiply = new(nameof(Multiply), "*");
        
        /// <summary>
        /// Represents the Exponent Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Exponent = new(nameof(Exponent), "**");
        
        /// <summary>
        /// Represents the Divide Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Divide = new(nameof(Divide), "/");
        
        /// <summary>
        /// Represents the Modulo Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Modulo = new(nameof(Modulo), "MOD");
        
        /// <summary>
        /// Represents the And Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator And = new(nameof(And), "AND");
        
        /// <summary>
        /// Represents the Or Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Or = new(nameof(Or), "OR");
        
        /// <summary>
        /// Represents the Xor Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Xor = new(nameof(Xor), "XOR");
        
        /// <summary>
        /// Represents the Not Logix <see cref="Operator"/>.
        /// </summary>
        public static readonly Operator Not = new(nameof(Not), "NOT");
        
    }
}