using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming

namespace L5Sharp.Types.ModuleDefined
{
    /// <summary>
    /// A Rockwell module defined logix type. 
    /// </summary>
    public sealed class CHANNEL_AI_I : StructureType
    {
        /// <summary>
        /// Creates a new <see cref="CHANNEL_AI_I"/> data type instance.
        /// </summary>
        public CHANNEL_AI_I() : base("CHANNEL_AI:I:0")
        {
            Fault = new BOOL();
            Uncertain = new BOOL();
            UnderRange = new BOOL();
            OverRange = new BOOL();
            Data = new REAL();
            RollingTimeStamp = new INT();
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Module;

        /// <summary>
        /// Gets the <see cref="Fault"/> member of the <see cref="CHANNEL_AI_I"/> data type.
        /// </summary>
        public BOOL Fault
        {
            get => GetMember<BOOL>();
            set => SetMember(value);
        }

        /// <summary>
        /// Gets the <see cref="Uncertain"/> member of the <see cref="CHANNEL_AI_I"/> data type.
        /// </summary>
        public BOOL Uncertain
        {
            get => GetMember<BOOL>();
            set => SetMember(value);
        }

        /// <summary>
        /// Gets the <see cref="UnderRange"/> member of the <see cref="CHANNEL_AI_I"/> data type.
        /// </summary>
        public BOOL UnderRange
        {
            get => GetMember<BOOL>();
            set => SetMember(value);
        }

        /// <summary>
        /// Gets the <see cref="OverRange"/> member of the <see cref="CHANNEL_AI_I"/> data type.
        /// </summary>
        public BOOL OverRange
        {
            get => GetMember<BOOL>();
            set => SetMember(value);
        }

        /// <summary>
        /// Gets the <see cref="Data"/> member of the <see cref="CHANNEL_AI_I"/> data type.
        /// </summary>
        public REAL Data
        {
            get => GetMember<REAL>();
            set => SetMember(value);
        }

        /// <summary>
        /// Gets the <see cref="RollingTimeStamp"/> member of the <see cref="CHANNEL_AI_I"/> data type.
        /// </summary>
        public INT RollingTimeStamp
        {
            get => GetMember<INT>();
            set => SetMember(value);
        }
    }
}