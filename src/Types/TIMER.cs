using L5Sharp.Abstractions;
using L5Sharp.Creators;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    /// <summary>
    /// A predefined data type that is built into Logix and used with timer instructions.
    /// </summary>
    public sealed class TIMER : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="TIMER"/> data type instance.
        /// </summary>
        public TIMER() : base(nameof(TIMER))
        {
        }

        /// <summary>
        /// Creates a new <see cref="TIMER"/> data type instance with the provided <see cref="PRE"/> value. 
        /// </summary>
        /// <param name="pre">The <see cref="DINT"/> value to initialize <see cref="PRE"/> with.</param>
        public TIMER(DINT pre) : this()
        {
            PRE.DataType.SetValue(pre);
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new TIMER();

        
        /// <summary>
        /// Gets the <see cref="PRE"/> member of the <see cref="TIMER"/> data type.
        /// </summary>
        /// <remarks>
        /// The preset value specifies the value (1 msec units) which the accumulated value must reach
        /// before the instruction sets the .DN bit.
        /// </remarks>
        public IMember<DINT> PRE = Member.Create<DINT>(nameof(PRE));

        /// <summary>
        /// Gets the <see cref="ACC"/> member of the <see cref="TIMER"/> data type.
        /// </summary>
        /// <remarks>
        /// The accumulated value specifies the number of milliseconds that have elapsed since the
        /// Timer instruction was enabled.
        /// </remarks>
        public IMember<DINT> ACC = Member.Create<DINT>(nameof(ACC));

        /// <summary>
        /// Gets the <see cref="EN"/> member of the <see cref="TIMER"/> data type.
        /// </summary>
        /// <remarks>
        /// The enable bit indicates that the Timer instruction is enabled.
        /// </remarks>
        public IMember<BOOL> EN = Member.Create<BOOL>(nameof(EN));

        /// <summary>
        /// Gets the <see cref="TT"/> member of the <see cref="TIMER"/> data type.
        /// </summary>
        /// <remarks>
        /// The timing bit indicates that a timing operation is in process
        /// </remarks>
        public IMember<BOOL> TT = Member.Create<BOOL>(nameof(TT));

        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="TIMER"/> data type.
        /// </summary>
        /// <remarks>
        /// The done bit is set when .ACC ≥ .PRE.
        /// </remarks>
        public IMember<BOOL> DN = Member.Create<BOOL>(nameof(DN));
    }
}