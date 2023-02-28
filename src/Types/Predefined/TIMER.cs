using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types.Predefined
{
    /// <summary>
    /// A predefined data type that is built into Logix and used with timer instructions.
    /// </summary>
    public sealed class TIMER : StructureType
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
            PRE = pre;
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <summary>
        /// Gets the <see cref="PRE"/> member of the <see cref="TIMER"/> data type.
        /// </summary>
        /// <remarks>
        /// The preset value specifies the value (1 msec units) which the accumulated value must reach
        /// before the instruction sets the .DN bit.
        /// </remarks>
        public DINT PRE { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ACC"/> member of the <see cref="TIMER"/> data type.
        /// </summary>
        /// <remarks>
        /// The accumulated value specifies the number of milliseconds that have elapsed since the
        /// Timer instruction was enabled.
        /// </remarks>
        public DINT ACC { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="EN"/> member of the <see cref="TIMER"/> data type.
        /// </summary>
        /// <remarks>
        /// The enable bit indicates that the Timer instruction is enabled.
        /// </remarks>
        public BOOL EN { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="TT"/> member of the <see cref="TIMER"/> data type.
        /// </summary>
        /// <remarks>
        /// The timing bit indicates that a timing operation is in process
        /// </remarks>
        public BOOL TT { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="TIMER"/> data type.
        /// </summary>
        /// <remarks>
        /// The done bit is set when .ACC ≥ .PRE.
        /// </remarks>
        public BOOL DN { get; set; } = new();
    }
}