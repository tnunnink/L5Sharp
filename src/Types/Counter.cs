using L5Sharp.Core;
// ReSharper disable InconsistentNaming

namespace L5Sharp.Types
{
    public sealed class Counter : Predefined
    {
        public Counter() : base(LoadElement(nameof(Counter).ToUpper()))
        {
                
        }
        
        public IMember<Dint> PRE => GetMember<Dint>(nameof(PRE));
        public IMember<Dint> ACC => GetMember<Dint>(nameof(ACC));
        public IMember<Bool> CU => GetMember<Bool>(nameof(CU));
        public IMember<Bool> CD => GetMember<Bool>(nameof(CD));
        public IMember<Bool> DN => GetMember<Bool>(nameof(DN));
        public IMember<Bool> OV => GetMember<Bool>(nameof(OV));
        public IMember<Bool> UN => GetMember<Bool>(nameof(UN));
    }
}