using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>TIMER</c> data type structure.
/// </summary>
[LogixData("TIMER")]
public sealed partial class TIMER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="TIMER"/> instance initialized with default values.
    /// </summary>
    public TIMER() : base("TIMER")
    {
        PRE = new DINT();
        ACC = new DINT();
        EN = new BOOL();
        TT = new BOOL();
        DN = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="TIMER"/> instance initialized with the provided element.
    /// </summary>
    public TIMER(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>PRE</c> member of the <see cref="TIMER"/> data type.
    /// </summary>
    public DINT PRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ACC</c> member of the <see cref="TIMER"/> data type.
    /// </summary>
    public DINT ACC
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EN</c> member of the <see cref="TIMER"/> data type.
    /// </summary>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TT</c> member of the <see cref="TIMER"/> data type.
    /// </summary>
    public BOOL TT
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="TIMER"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
