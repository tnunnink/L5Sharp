using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Factories;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    /// <summary>
    /// A predefined or built in data type used with counter instructions. 
    /// </summary>
    public sealed class Counter : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="Counter"/> data type instance.
        /// </summary>
        public Counter() : base(nameof(Counter).ToUpper())
        {
        }
        
        /// <summary>
        /// Creates a new <see cref="Counter"/> data type with the provided default <see cref="PRE"/> member value. 
        /// </summary>
        /// <param name="pre">The value of the preset for the timer.</param>
        public Counter(Dint pre) : this()
        {
            PRE.DataType.SetValue(pre);
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new Counter();
        
        /// <summary>
        /// Gets the <see cref="PRE"/> member of the <see cref="Counter"/> data type.
        /// </summary>
        public IMember<Dint> PRE = Member.Create<Dint>(nameof(PRE));
        
        /// <summary>
        /// Gets the <see cref="ACC"/> member of the <see cref="Counter"/> data type.
        /// </summary>
        public IMember<Dint> ACC = Member.Create<Dint>(nameof(ACC));
        
        /// <summary>
        /// Gets the <see cref="CU"/> member of the <see cref="Counter"/> data type.
        /// </summary>
        public IMember<Bool> CU = Member.Create<Bool>(nameof(CU));
        
        /// <summary>
        /// Gets the <see cref="CD"/> member of the <see cref="Counter"/> data type.
        /// </summary>
        public IMember<Bool> CD = Member.Create<Bool>(nameof(CD));
        
        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="Counter"/> data type.
        /// </summary>
        public IMember<Bool> DN = Member.Create<Bool>(nameof(DN));
        
        /// <summary>
        /// Gets the <see cref="OV"/> member of the <see cref="Counter"/> data type.
        /// </summary>
        public IMember<Bool> OV = Member.Create<Bool>(nameof(OV));
        
        /// <summary>
        /// Gets the <see cref="UN"/> member of the <see cref="Counter"/> data type.
        /// </summary>
        public IMember<Bool> UN = Member.Create<Bool>(nameof(UN));
    }
}