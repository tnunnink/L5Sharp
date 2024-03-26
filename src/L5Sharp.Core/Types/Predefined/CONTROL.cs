using System.Xml.Linq;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Core;

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
        LEN = new DINT();
        POS = new DINT();
        EN = new BOOL();
        EU = new BOOL();
        DN = new BOOL();
        EM = new BOOL();
        ER = new BOOL();
        UL = new BOOL();
        IN = new BOOL();
        FD = new BOOL();
    }

    /// <inheritdoc />
    public CONTROL(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the <see cref="LEN"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public DINT LEN
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="POS"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public DINT POS
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="EN"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="EU"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL EU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="DN"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="EM"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL EM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ER"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL ER
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="UL"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL UL
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="IN"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL IN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="FD"/> member of the <see cref="CONTROL"/> data type.
    /// </summary>
    public BOOL FD
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}