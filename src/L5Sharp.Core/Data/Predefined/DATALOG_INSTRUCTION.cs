using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DATALOG_INSTRUCTION</c> data type structure.
/// </summary>
[LogixData("DATALOG_INSTRUCTION")]
public sealed partial class DATALOG_INSTRUCTION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DATALOG_INSTRUCTION"/> instance initialized with default values.
    /// </summary>
    public DATALOG_INSTRUCTION() : base("DATALOG_INSTRUCTION")
    {
        FLAGS = new DINT();
        EN = new BOOL();
        DN = new BOOL();
        ER = new BOOL();
        PC = new BOOL();
        IP = new BOOL();
        ERR = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="DATALOG_INSTRUCTION"/> instance initialized with the provided element.
    /// </summary>
    public DATALOG_INSTRUCTION(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>FLAGS</c> member of the <see cref="DATALOG_INSTRUCTION"/> data type.
    /// </summary>
    public DINT FLAGS
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EN</c> member of the <see cref="DATALOG_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="DATALOG_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ER</c> member of the <see cref="DATALOG_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL ER
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PC</c> member of the <see cref="DATALOG_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL PC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>IP</c> member of the <see cref="DATALOG_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL IP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ERR</c> member of the <see cref="DATALOG_INSTRUCTION"/> data type.
    /// </summary>
    public DINT ERR
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}