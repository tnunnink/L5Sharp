using L5Sharp.Abstractions;
using L5Sharp.Creators;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    /// <summary>
    /// A predefined or built in data type used with ... instructions. 
    /// </summary>
    public sealed class CONTROL : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="CONTROL"/> data type instance.
        /// </summary>
        public CONTROL() : base(nameof(CONTROL))
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new CONTROL();

        /// <summary>
        /// Gets the <see cref="LEN"/> member of the <see cref="CONTROL"/> data type.
        /// </summary>
        public IMember<DINT> LEN = Member.Create<DINT>(nameof(LEN));
        
        /// <summary>
        /// Gets the <see cref="POS"/> member of the <see cref="CONTROL"/> data type.
        /// </summary>
        public IMember<DINT> POS = Member.Create<DINT>(nameof(POS));
        
        /// <summary>
        /// Gets the <see cref="EN"/> member of the <see cref="CONTROL"/> data type.
        /// </summary>
        public IMember<BOOL> EN = Member.Create<BOOL>(nameof(EN));
        
        /// <summary>
        /// Gets the <see cref="EU"/> member of the <see cref="CONTROL"/> data type.
        /// </summary>
        public IMember<BOOL> EU = Member.Create<BOOL>(nameof(EU));
        
        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="CONTROL"/> data type.
        /// </summary>
        public IMember<BOOL> DN = Member.Create<BOOL>(nameof(DN));
        
        /// <summary>
        /// Gets the <see cref="EM"/> member of the <see cref="CONTROL"/> data type.
        /// </summary>
        public IMember<BOOL> EM = Member.Create<BOOL>(nameof(EM));
        
        /// <summary>
        /// Gets the <see cref="ER"/> member of the <see cref="CONTROL"/> data type.
        /// </summary>
        public IMember<BOOL> ER = Member.Create<BOOL>(nameof(ER));
        
        /// <summary>
        /// Gets the <see cref="UL"/> member of the <see cref="CONTROL"/> data type.
        /// </summary>
        public IMember<BOOL> UL = Member.Create<BOOL>(nameof(UL));
        
        /// <summary>
        /// Gets the <see cref="IN"/> member of the <see cref="CONTROL"/> data type.
        /// </summary>
        public IMember<BOOL> IN = Member.Create<BOOL>(nameof(IN));
        
        /// <summary>
        /// Gets the <see cref="FD"/> member of the <see cref="CONTROL"/> data type.
        /// </summary>
        public IMember<BOOL> FD = Member.Create<BOOL>(nameof(FD));
    }
}