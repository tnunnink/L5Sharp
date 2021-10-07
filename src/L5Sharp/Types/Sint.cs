using System.ComponentModel;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enumerations;

namespace L5Sharp.Types
{
    public class Sint : Predefined
    {
        public Sint() : base(LoadElement(nameof(Sint).ToUpper()))
        {
        }

        public override object DefaultValue => default(byte);
        public override Radix DefaultRadix => Radix.Decimal;
        
        public IMember B0 => Members.SingleOrDefault(x => x.Name == nameof(B0).Remove(0, 1));
        public IMember B1 => Members.SingleOrDefault(x => x.Name == nameof(B1).Remove(0, 1));
        public IMember B2 => Members.SingleOrDefault(x => x.Name == nameof(B2).Remove(0, 1));
        public IMember B3 => Members.SingleOrDefault(x => x.Name == nameof(B3).Remove(0, 1));
        public IMember B4 => Members.SingleOrDefault(x => x.Name == nameof(B4).Remove(0, 1));
        public IMember B5 => Members.SingleOrDefault(x => x.Name == nameof(B5).Remove(0, 1));
        public IMember B6 => Members.SingleOrDefault(x => x.Name == nameof(B6).Remove(0, 1));
        public IMember B7 => Members.SingleOrDefault(x => x.Name == nameof(B7).Remove(0, 1));
        

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