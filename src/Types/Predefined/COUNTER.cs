using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types.Predefined
{
    /// <summary>
    /// A predefined or built in data type used with counter instructions. 
    /// </summary>
    public sealed class COUNTER : StructureType
    {
        /// <summary>
        /// Creates a new <see cref="COUNTER"/> data type instance.
        /// </summary>
        public COUNTER() : base(nameof(COUNTER))
        {
        }
        
        /// <summary>
        /// Creates a new <see cref="COUNTER"/> data type with the provided default <see cref="PRE"/> member value. 
        /// </summary>
        /// <param name="pre">The value of the preset for the timer.</param>
        public COUNTER(DINT pre) : this()
        {
            PRE = pre;
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <summary>
        /// Gets the <see cref="PRE"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public DINT PRE { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ACC"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public DINT ACC { get; set; } = new();
        
        /// <summary>
        /// Gets the <see cref="CU"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public BOOL CU { get; set; } = new();
        
        /// <summary>
        /// Gets the <see cref="CD"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public BOOL CD { get; set; } = new();
        
        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public BOOL DN { get; set; } = new();
        
        /// <summary>
        /// Gets the <see cref="OV"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public BOOL OV { get; set; } = new();
        
        /// <summary>
        /// Gets the <see cref="UN"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public BOOL UN { get; set; } = new();
    }
}