using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    public sealed class Timer : ComplexType
    {
        /// <summary>
        /// Creates a new instance of a timer type.
        /// </summary>
        public Timer() : base(nameof(Timer).ToUpper())
        {
        }
        
        /// <summary>
        /// Creates new instance of a timer type with provided default PRE member value. 
        /// </summary>
        /// <param name="pre">The value of the preset member to initialize</param>
        public Timer(Dint pre) : this()
        {
            PRE.DataType.SetValue(pre);
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New()
        {
            return new Timer();
        }

        public IMember<Dint> PRE = Member.Create<Dint>(nameof(PRE));
        public IMember<Dint> ACC = Member.Create<Dint>(nameof(ACC));
        public IMember<Bool> EN = Member.Create<Bool>(nameof(EN));
        public IMember<Bool> TT = Member.Create<Bool>(nameof(TT));
        public IMember<Bool> DN = Member.Create<Bool>(nameof(DN));
    }
}