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
        /// Creates a new <see cref="Timer"/> data type instance.
        /// </summary>
        public Timer() : base(nameof(Timer).ToUpper())
        {
        }

        /// <summary>
        /// Creates a new <see cref="Timer"/> data type instance with the provided <see cref="PRE"/> value. 
        /// </summary>
        /// <param name="pre">The <see cref="Dint"/> value to initialize <see cref="PRE"/> with.</param>
        public Timer(Dint pre) : this()
        {
            PRE.DataType.SetValue(pre);
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new Timer();

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