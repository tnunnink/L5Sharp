// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
namespace L5Sharp.Core;

/// <summary>
/// 
/// </summary>
public class MessageType : LogixEnum<MessageType, string>
{
    private MessageType(string name, string value) : base(name, value)
    {
    }
    
    /// <summary>
    /// Returns the <c>Unconfigured</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType Unconfigured = new(nameof(Unconfigured), "Unconfigured");
    
    /// <summary>
    /// Returns the <c>CIPDataTableRead</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType CIPDataTableRead = new(nameof(CIPDataTableRead), "CIP Data Table Read");
    
    /// <summary>
    /// Returns the <c>CIPDataTableWrite</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType CIPDataTableWrite = new(nameof(CIPDataTableWrite), "CIP Data Table Write");
    
    /// <summary>
    /// Returns the <c>CIPGeneric</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType CIPGeneric = new(nameof(CIPGeneric), "CIP Generic");
    
    /// <summary>
    /// Returns the <c>PLC2UnprotectedRead</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType PLC2UnprotectedRead = new(nameof(PLC2UnprotectedRead), "PLC2 Unprotected Read");
    
    /// <summary>
    /// Returns the <c>PLC2UnprotectedWrite</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType PLC2UnprotectedWrite = new(nameof(PLC2UnprotectedWrite), "PLC2 Unprotected Write");
    
    /// <summary>
    /// Returns the <c>PLC3WordRangeRead</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType PLC3WordRangeRead = new(nameof(PLC3WordRangeRead), "PLC3 Word Range Read");
    
    /// <summary>
    /// Returns the <c>PLC3WordRangeWrite</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType PLC3WordRangeWrite = new(nameof(PLC3WordRangeWrite), "PLC3 Word Range Write");
    
    /// <summary>
    /// Returns the <c>PLC3TypedRead</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType PLC3TypedRead = new(nameof(PLC3TypedRead), "PLC3 Typed Read");
    
    /// <summary>
    /// Returns the <c>PLC3TypedWrite</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType PLC3TypedWrite = new(nameof(PLC3TypedWrite), "PLC3 Typed Write");
    
    /// <summary>
    /// Returns the <c>PLC5WordRangeRead</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType PLC5WordRangeRead = new(nameof(PLC5WordRangeRead), "PLC5 Word Range Read");
    
    /// <summary>
    /// Returns the <c>PLC5WordRangeWrite</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType PLC5WordRangeWrite = new(nameof(PLC5WordRangeWrite), "PLC5 Word Range Write");
    
    /// <summary>
    /// Returns the <c>PLC5TypedRead</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType PLC5TypedRead = new(nameof(PLC5TypedRead), "PLC5 Typed Read");
    
    /// <summary>
    /// Returns the <c>PLC5TypedWrite</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType PLC5TypedWrite = new(nameof(PLC5TypedWrite), "PLC5 Typed Write");
    
    /// <summary>
    /// Returns the <c>SLCTypedRead</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType SLCTypedRead = new(nameof(SLCTypedRead), "SLC Typed Read");
    
    /// <summary>
    /// Returns the <c>SLCTypedWrite</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType SLCTypedWrite = new(nameof(SLCTypedWrite), "SLC Typed Write");
    
    /// <summary>
    /// Returns the <c>BlockTransferRead</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType BlockTransferRead = new(nameof(BlockTransferRead), "Block Transfer Read");
    
    /// <summary>
    /// Returns the <c>BlockTransferWrite</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType BlockTransferWrite = new(nameof(BlockTransferWrite), "Block Transfer Write");
    
    /// <summary>
    /// Returns the <c>ModuleReconfigure</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType ModuleReconfigure = new(nameof(ModuleReconfigure), "Module Reconfigure");
    
    /// <summary>
    /// Returns the <c>SERCOSIDNRead</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType SERCOSIDNRead = new(nameof(SERCOSIDNRead), "SERCOS IDN Read");
    
    /// <summary>
    /// Returns the <c>SERCOSIDNWrite</c> <see cref="MessageType"/> value option.
    /// </summary>
    public static readonly MessageType SERCOSIDNWrite = new(nameof(SERCOSIDNWrite), "SERCOS IDN Write");
}