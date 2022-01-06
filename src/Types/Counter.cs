using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    public sealed class Counter : ComplexType
    {
        /// <summary>
        /// Creates a new instance of a counter type.
        /// </summary>
        public Counter() : base(nameof(Counter).ToUpper())
        {
        }
        
        /// <summary>
        /// Creates new instance of a counter type with provided default PRE member value. 
        /// </summary>
        /// <param name="pre">The value of the preset member to initialize</param>
        public Counter(Dint pre) : this()
        {
            PRE.DataType.SetValue(pre);
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new Counter();
        
        public IMember<Dint> PRE = Member.Create<Dint>(nameof(PRE));
        public IMember<Dint> ACC = Member.Create<Dint>(nameof(ACC));
        public IMember<Bool> CU = Member.Create<Bool>(nameof(CU));
        public IMember<Bool> CD = Member.Create<Bool>(nameof(CD));
        public IMember<Bool> DN = Member.Create<Bool>(nameof(DN));
        public IMember<Bool> OV = Member.Create<Bool>(nameof(OV));
        public IMember<Bool> UN = Member.Create<Bool>(nameof(UN));
    }
}