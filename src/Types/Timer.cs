using L5Sharp.Core;
// ReSharper disable InconsistentNaming

namespace L5Sharp.Types
{
    public class Timer : Predefined
    {
        public Timer() : base(LoadElement(nameof(Timer).ToUpper()))
        {
                
        }

        public IMember<Dint> PRE => GetMember<Dint>(nameof(PRE));
        public Dint ACC => GetMember(nameof(ACC)).DataType is Dint d ? d : default;
        public Bool EN => GetMember(nameof(EN)).DataType is Bool b ? b : default;
        public Bool TT => GetMember(nameof(TT)).DataType is Bool b ? b : default;
        public Bool DN => GetMember(nameof(DN)).DataType is Bool b ? b : default;
    }
}