using L5Sharp.Abstractions;
using L5Sharp.Creators;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming I want to keep the naming consistent with Logix (for now).

namespace L5Sharp.Types
{
    /// <summary>
    /// A predefined or built in data type in Logix that is a part of the alarm instruction set.
    /// </summary>
    public sealed class ALARM : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="ALARM"/> data type instance.
        /// </summary>
        public ALARM() : base(nameof(ALARM))
        {
        }

        /// <inheritdoc />
        public override string Name { get; } = nameof(ALARM).ToUpper();

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;
 
        /// <inheritdoc />
        protected override IDataType New() => new ALARM();

        /// <summary>
        /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> EnableIn = Member.Create<BOOL>(nameof(EnableIn));
        
        /// <summary>
        /// Gets the <see cref="In"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<REAL> In = Member.Create<REAL>(nameof(In));
        
        /// <summary>
        /// Gets the <see cref="HHLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<REAL> HHLimit = Member.Create<REAL>(nameof(HHLimit));
        
        /// <summary>
        /// Gets the <see cref="HLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<REAL> HLimit = Member.Create<REAL>(nameof(HLimit));
        
        /// <summary>
        /// Gets the <see cref="LLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<REAL> LLimit = Member.Create<REAL>(nameof(LLimit));
        
        /// <summary>
        /// Gets the <see cref="LLLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<REAL> LLLimit = Member.Create<REAL>(nameof(LLLimit));
        
        /// <summary>
        /// Gets the <see cref="Deadband"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<REAL> Deadband = Member.Create<REAL>(nameof(Deadband));
        
        /// <summary>
        /// Gets the <see cref="ROCPosLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<REAL> ROCPosLimit = Member.Create<REAL>(nameof(ROCPosLimit));
        
        /// <summary>
        /// Gets the <see cref="ROCNegLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<REAL> ROCNegLimit = Member.Create<REAL>(nameof(ROCNegLimit));
        
        /// <summary>
        /// Gets the <see cref="ROCPeriod"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<REAL> ROCPeriod = Member.Create<REAL>(nameof(ROCPeriod));
        
        /// <summary>
        /// Gets the <see cref="EnableOut"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> EnableOut = Member.Create<BOOL>(nameof(EnableOut));
        
        /// <summary>
        /// Gets the <see cref="HHAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> HHAlarm = Member.Create<BOOL>(nameof(HHAlarm));
        
        /// <summary>
        /// Gets the <see cref="HAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> HAlarm = Member.Create<BOOL>(nameof(HAlarm));
        
        /// <summary>
        /// Gets the <see cref="LAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> LAlarm = Member.Create<BOOL>(nameof(LAlarm));
        
        /// <summary>
        /// Gets the <see cref="LLAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> LLAlarm = Member.Create<BOOL>(nameof(LLAlarm));
        
        /// <summary>
        /// Gets the <see cref="ROCPosAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosAlarm = Member.Create<BOOL>(nameof(ROCPosAlarm));
        
        /// <summary>
        /// Gets the <see cref="ROCNegAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegAlarm = Member.Create<BOOL>(nameof(ROCNegAlarm));
        
        /// <summary>
        /// Gets the <see cref="ROC"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<REAL> ROC = Member.Create<REAL>(nameof(ROC));
        
        /// <summary>
        /// Gets the <see cref="Status"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<DINT> Status = Member.Create<DINT>(nameof(Status));
        
        /// <summary>
        /// Gets the <see cref="InstructFault"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> InstructFault = Member.Create<BOOL>(nameof(InstructFault));
        
        /// <summary>
        /// Gets the <see cref="DeadbandInv"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> DeadbandInv = Member.Create<BOOL>(nameof(DeadbandInv));
        
        /// <summary>
        /// Gets the <see cref="ROCPosLimitInv"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosLimitInv = Member.Create<BOOL>(nameof(ROCPosLimitInv));
        
        /// <summary>
        /// Gets the <see cref="ROCNegLimitInv"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegLimitInv = Member.Create<BOOL>(nameof(ROCNegLimitInv));
        
        /// <summary>
        /// Gets the <see cref="ROCPeriodInv"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPeriodInv = Member.Create<BOOL>(nameof(ROCPeriodInv));
    }
}