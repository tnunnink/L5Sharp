using L5Sharp.Core;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    public sealed class Timer : Predefined
    {
        public Timer() : base(nameof(Timer).ToUpper())
        {
            RegisterMemberProperties();
        }
        
        public Timer(Dint pre) : this()
        {
            PRE.DataType.SetValue(pre);
        }

        public override string Description => $"RSLogix {Name} DataType";
        protected override IDataType New()
        {
            return new Timer();
        }

        public IMember<Dint> PRE => Member.OfType<Dint>(nameof(PRE));
        public IMember<Dint> ACC => Member.OfType<Dint>(nameof(ACC));
        public IMember<Bool> EN => Member.OfType<Bool>(nameof(EN));
        public IMember<Bool> TT => Member.OfType<Bool>(nameof(TT));
        public IMember<Bool> DN => Member.OfType<Bool>(nameof(DN));
    }
}