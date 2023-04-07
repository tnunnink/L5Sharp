using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types.Predefined;

/// <summary>
/// A predefined or built in data type used with ... instructions. 
/// </summary>
public sealed class CONTROL : StructureType
{
    /// <summary>
    /// Creates a new <see cref="CONTROL"/> data type instance.
    /// </summary>
    public CONTROL() : base(nameof(CONTROL))
    {
    }

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Predefined;

    /// <summary>
    /// Gets the <see cref="LEN"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public DINT LEN { get; set; } = new();
        
    /// <summary>
    /// Gets the <see cref="POS"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public DINT POS { get; set; } = new();
        
    /// <summary>
    /// Gets the <see cref="EN"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL EN { get; set; } = new();
        
    /// <summary>
    /// Gets the <see cref="EU"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL EU { get; set; } = new();
        
    /// <summary>
    /// Gets the <see cref="DN"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL DN { get; set; } = new();
        
    /// <summary>
    /// Gets the <see cref="EM"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL EM { get; set; } = new();
        
    /// <summary>
    /// Gets the <see cref="ER"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL ER { get; set; } = new();
        
    /// <summary>
    /// Gets the <see cref="UL"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL UL { get; set; } = new();
        
    /// <summary>
    /// Gets the <see cref="IN"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL IN { get; set; } = new();
        
    /// <summary>
    /// Gets the <see cref="FD"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL FD { get; set; } = new();
}