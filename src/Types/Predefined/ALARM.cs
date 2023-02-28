using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming I want to keep the naming consistent with Logix (for now).

namespace L5Sharp.Types.Predefined
{
    /// <summary>
    /// A predefined or built in data type in Logix that is a part of the alarm instruction set.
    /// </summary>
    public sealed class ALARM : StructureType
    {
        /// <summary>
        /// Creates a new <see cref="ALARM"/> data type instance.
        /// </summary>
        public ALARM() : base(nameof(ALARM))
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <summary>
        /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL EnableIn { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="In"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public REAL In { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public REAL HHLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public REAL HLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public REAL LLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public REAL LLLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Deadband"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public REAL Deadband { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public REAL ROCPosLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegLimit"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public REAL ROCNegLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPeriod"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public REAL ROCPeriod { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="EnableOut"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL EnableOut { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL HHAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL HAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL LAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL LLAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL ROCPosAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegAlarm"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL ROCNegAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROC"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public REAL ROC { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Status"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public DINT Status { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InstructFault"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL InstructFault { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="DeadbandInv"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL DeadbandInv { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosLimitInv"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL ROCPosLimitInv { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegLimitInv"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL ROCNegLimitInv { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPeriodInv"/> member of the <see cref="ALARM"/> data type.
        /// </summary>
        public BOOL ROCPeriodInv { get; set; } = new();
    }
}