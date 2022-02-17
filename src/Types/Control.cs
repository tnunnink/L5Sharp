using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Factories;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    /// <summary>
    /// A predefined or built in data type used with ... instructions. 
    /// </summary>
    public sealed class Control : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="Control"/> data type instance.
        /// </summary>
        public Control() : base(nameof(Control).ToUpper())
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new Control();

        /// <summary>
        /// Gets the <see cref="LEN"/> member of the <see cref="Control"/> data type.
        /// </summary>
        public IMember<Dint> LEN = Member.Create<Dint>(nameof(LEN));
        
        /// <summary>
        /// Gets the <see cref="POS"/> member of the <see cref="Control"/> data type.
        /// </summary>
        public IMember<Dint> POS = Member.Create<Dint>(nameof(POS));
        
        /// <summary>
        /// Gets the <see cref="EN"/> member of the <see cref="Control"/> data type.
        /// </summary>
        public IMember<Bool> EN = Member.Create<Bool>(nameof(EN));
        
        /// <summary>
        /// Gets the <see cref="EU"/> member of the <see cref="Control"/> data type.
        /// </summary>
        public IMember<Bool> EU = Member.Create<Bool>(nameof(EU));
        
        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="Control"/> data type.
        /// </summary>
        public IMember<Bool> DN = Member.Create<Bool>(nameof(DN));
        
        /// <summary>
        /// Gets the <see cref="EM"/> member of the <see cref="Control"/> data type.
        /// </summary>
        public IMember<Bool> EM = Member.Create<Bool>(nameof(EM));
        
        /// <summary>
        /// Gets the <see cref="ER"/> member of the <see cref="Control"/> data type.
        /// </summary>
        public IMember<Bool> ER = Member.Create<Bool>(nameof(ER));
        
        /// <summary>
        /// Gets the <see cref="UL"/> member of the <see cref="Control"/> data type.
        /// </summary>
        public IMember<Bool> UL = Member.Create<Bool>(nameof(UL));
        
        /// <summary>
        /// Gets the <see cref="IN"/> member of the <see cref="Control"/> data type.
        /// </summary>
        public IMember<Bool> IN = Member.Create<Bool>(nameof(IN));
        
        /// <summary>
        /// Gets the <see cref="FD"/> member of the <see cref="Control"/> data type.
        /// </summary>
        public IMember<Bool> FD = Member.Create<Bool>(nameof(FD));
    }
}