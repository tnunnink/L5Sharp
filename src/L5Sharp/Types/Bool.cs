using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    public sealed class Bool : Predefined
    {
        public Bool() : base(LoadElement(nameof(Bool).ToUpper()))
        {
        }

        public override object DefaultValue => default(bool);

        public override Radix DefaultRadix => Radix.Decimal;

        public override bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex;
        }

        public override object ParseValue(string value)
        {
            if (bool.TryParse(value, out var result))
                return result;

            if (string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "True", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "Yes", StringComparison.OrdinalIgnoreCase))
                return true;

            if (string.Equals(value, "0", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "False", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "No", StringComparison.OrdinalIgnoreCase))
                return false;

            return null;
        }

        public override bool IsValidValue(object value)
        {
            if (value is string)
                return ParseValue(value.ToString()) is bool;
            
            return value is bool;
        }
    }
}