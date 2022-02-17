using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Factories;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    /// <summary>
    /// A predefined data type that is built into Logix and used with timer instructions.
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
        /// Gets the <see cref="PRE"/> member of the <see cref="Timer"/> data type.
        /// </summary>
        /// <remarks>
        /// The preset value specifies the value (1 msec units) which the accumulated value must reach
        /// before the instruction sets the .DN bit.
        /// </remarks>
        public IMember<Dint> PRE = Member.Create<Dint>(nameof(PRE));

        /// <summary>
        /// Gets the <see cref="ACC"/> member of the <see cref="Timer"/> data type.
        /// </summary>
        /// <remarks>
        /// The accumulated value specifies the number of milliseconds that have elapsed since the
        /// Timer instruction was enabled.
        /// </remarks>
        public IMember<Dint> ACC = Member.Create<Dint>(nameof(ACC));

        /// <summary>
        /// Gets the <see cref="EN"/> member of the <see cref="Timer"/> data type.
        /// </summary>
        /// <remarks>
        /// The enable bit indicates that the Timer instruction is enabled.
        /// </remarks>
        public IMember<Bool> EN = Member.Create<Bool>(nameof(EN));

        /// <summary>
        /// Gets the <see cref="TT"/> member of the <see cref="Timer"/> data type.
        /// </summary>
        /// <remarks>
        /// The timing bit indicates that a timing operation is in process
        /// </remarks>
        public IMember<Bool> TT = Member.Create<Bool>(nameof(TT));

        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="Timer"/> data type.
        /// </summary>
        /// <remarks>
        /// The done bit is set when .ACC ≥ .PRE.
        /// </remarks>
        public IMember<Bool> DN = Member.Create<Bool>(nameof(DN));
    }
}