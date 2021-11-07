using System.Linq;
using L5Sharp.Core;
// ReSharper disable InconsistentNaming

namespace L5Sharp.Types
{
    public sealed class Counter : Predefined
    {
        public Counter() : base(LoadElement(nameof(Counter).ToUpper()))
        {
                
        }
        
        public Dint PRE => GetMember(nameof(PRE)).DataType is Dint d ? d : default;
        public Dint ACC => GetMember(nameof(ACC)).DataType is Dint d ? d : default;
        public Bool CU => GetMember(nameof(CU)).DataType is Bool b ? b : default;
        public Bool CD => GetMember(nameof(CD)).DataType is Bool b ? b : default;
        public Bool DN => GetMember(nameof(DN)).DataType is Bool b ? b : default;
        public Bool OV => GetMember(nameof(OV)).DataType is Bool b ? b : default;
        public Bool UN => GetMember(nameof(UN)).DataType is Bool b ? b : default;
    }
}