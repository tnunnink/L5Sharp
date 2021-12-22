using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// The preset value specifies the value (1 msec units) which the accumulated value must reach
        /// before the instruction sets the .DN bit.
        /// </summary>
        public IMember<Dint> PRE = Member.Create<Dint>(nameof(PRE));
        
        /// <summary>
        /// The accumulated value specifies the number of milliseconds that have elapsed since the
        /// Timer instruction was enabled.
        /// </summary>
        public IMember<Dint> ACC = Member.Create<Dint>(nameof(ACC));
        
        /// <summary>
        /// The enable bit indicates that the Timer instruction is enabled.
        /// </summary>
        public IMember<Bool> EN = Member.Create<Bool>(nameof(EN));
        
        /// <summary>
        /// The timing bit indicates that a timing operation is in process
        /// </summary>
        public IMember<Bool> TT = Member.Create<Bool>(nameof(TT));
        
        /// <summary>
        /// The done bit is set when .ACC ≥ .PRE.
        /// </summary>
        public IMember<Bool> DN = Member.Create<Bool>(nameof(DN));
    }
}