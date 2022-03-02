using L5Sharp.Abstractions;
using L5Sharp.Atomics;
using L5Sharp.Creators;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming I want to keep the naming consistent with Logix (for now).

namespace L5Sharp.Predefined
{
    /// <summary>
    /// A predefined or built in data type in Logix that is a part of the alarm instruction set.
    /// </summary>
    public sealed class Alarm : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="Alarm"/> data type instance.
        /// </summary>
        public Alarm() : base(nameof(Alarm).ToUpper())
        {
        }

        /// <inheritdoc />
        public override string Name { get; } = nameof(Alarm).ToUpper();

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;
 
        /// <inheritdoc />
        protected override IDataType New() => new Alarm();

        /// <summary>
        /// Gets the <see cref="EnableIn"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> EnableIn = Member.Create<Bool>(nameof(EnableIn));
        
        /// <summary>
        /// Gets the <see cref="In"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Real> In = Member.Create<Real>(nameof(In));
        
        /// <summary>
        /// Gets the <see cref="HHLimit"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Real> HHLimit = Member.Create<Real>(nameof(HHLimit));
        
        /// <summary>
        /// Gets the <see cref="HLimit"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Real> HLimit = Member.Create<Real>(nameof(HLimit));
        
        /// <summary>
        /// Gets the <see cref="LLimit"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Real> LLimit = Member.Create<Real>(nameof(LLimit));
        
        /// <summary>
        /// Gets the <see cref="LLLimit"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Real> LLLimit = Member.Create<Real>(nameof(LLLimit));
        
        /// <summary>
        /// Gets the <see cref="Deadband"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Real> Deadband = Member.Create<Real>(nameof(Deadband));
        
        /// <summary>
        /// Gets the <see cref="ROCPosLimit"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Real> ROCPosLimit = Member.Create<Real>(nameof(ROCPosLimit));
        
        /// <summary>
        /// Gets the <see cref="ROCNegLimit"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Real> ROCNegLimit = Member.Create<Real>(nameof(ROCNegLimit));
        
        /// <summary>
        /// Gets the <see cref="ROCPeriod"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Real> ROCPeriod = Member.Create<Real>(nameof(ROCPeriod));
        
        /// <summary>
        /// Gets the <see cref="EnableOut"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> EnableOut = Member.Create<Bool>(nameof(EnableOut));
        
        /// <summary>
        /// Gets the <see cref="HHAlarm"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> HHAlarm = Member.Create<Bool>(nameof(HHAlarm));
        
        /// <summary>
        /// Gets the <see cref="HAlarm"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> HAlarm = Member.Create<Bool>(nameof(HAlarm));
        
        /// <summary>
        /// Gets the <see cref="LAlarm"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> LAlarm = Member.Create<Bool>(nameof(LAlarm));
        
        /// <summary>
        /// Gets the <see cref="LLAlarm"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> LLAlarm = Member.Create<Bool>(nameof(LLAlarm));
        
        /// <summary>
        /// Gets the <see cref="ROCPosAlarm"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosAlarm = Member.Create<Bool>(nameof(ROCPosAlarm));
        
        /// <summary>
        /// Gets the <see cref="ROCNegAlarm"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegAlarm = Member.Create<Bool>(nameof(ROCNegAlarm));
        
        /// <summary>
        /// Gets the <see cref="ROC"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Real> ROC = Member.Create<Real>(nameof(ROC));
        
        /// <summary>
        /// Gets the <see cref="Status"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Dint> Status = Member.Create<Dint>(nameof(Status));
        
        /// <summary>
        /// Gets the <see cref="InstructFault"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> InstructFault = Member.Create<Bool>(nameof(InstructFault));
        
        /// <summary>
        /// Gets the <see cref="DeadbandInv"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> DeadbandInv = Member.Create<Bool>(nameof(DeadbandInv));
        
        /// <summary>
        /// Gets the <see cref="ROCPosLimitInv"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosLimitInv = Member.Create<Bool>(nameof(ROCPosLimitInv));
        
        /// <summary>
        /// Gets the <see cref="ROCNegLimitInv"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegLimitInv = Member.Create<Bool>(nameof(ROCNegLimitInv));
        
        /// <summary>
        /// Gets the <see cref="ROCPeriodInv"/> member of the <see cref="Alarm"/> data type.
        /// </summary>
        public IMember<Bool> ROCPeriodInv = Member.Create<Bool>(nameof(ROCPeriodInv));
    }
}