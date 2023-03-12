using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
// ReSharper disable InconsistentNaming

namespace L5Sharp.Types.ModuleDefined
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CHANNEL_AI_I : StructureType
    {
        /// <summary>
        /// Creates a new 
        /// </summary>
        public CHANNEL_AI_I() : base("CHANNEL_AI:I:0")
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Module;

        /// <summary>
        /// Gets 
        /// </summary>
        public BOOL Fault { get; set; } = new();
        
        /// <summary>
        /// 
        /// </summary>
        public BOOL Uncertain { get; set; } = new();
        
        /// <summary>
        /// 
        /// </summary>
        public BOOL UnderRange { get; set; } = new();
        
        /// <summary>
        /// 
        /// </summary>
        public BOOL OverRange { get; set; } = new();
        
        /// <summary>
        /// 
        /// </summary>
        public REAL Data { get; set; } = new();
        
        /// <summary>
        /// 
        /// </summary>
        public INT RollingTimeStamp { get; set; } = new();
    }
}