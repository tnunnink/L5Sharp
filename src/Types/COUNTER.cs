using L5Sharp.Abstractions;
using L5Sharp.Creators;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    /// <summary>
    /// A predefined or built in data type used with counter instructions. 
    /// </summary>
    public sealed class COUNTER : ComplexType
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
            PRE.DataType.SetValue(pre);
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new COUNTER();
        
        /// <summary>
        /// Gets the <see cref="PRE"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public IMember<DINT> PRE = Member.Create<DINT>(nameof(PRE));
        
        /// <summary>
        /// Gets the <see cref="ACC"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public IMember<DINT> ACC = Member.Create<DINT>(nameof(ACC));
        
        /// <summary>
        /// Gets the <see cref="CU"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public IMember<BOOL> CU = Member.Create<BOOL>(nameof(CU));
        
        /// <summary>
        /// Gets the <see cref="CD"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public IMember<BOOL> CD = Member.Create<BOOL>(nameof(CD));
        
        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public IMember<BOOL> DN = Member.Create<BOOL>(nameof(DN));
        
        /// <summary>
        /// Gets the <see cref="OV"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public IMember<BOOL> OV = Member.Create<BOOL>(nameof(OV));
        
        /// <summary>
        /// Gets the <see cref="UN"/> member of the <see cref="COUNTER"/> data type.
        /// </summary>
        public IMember<BOOL> UN = Member.Create<BOOL>(nameof(UN));
    }
}