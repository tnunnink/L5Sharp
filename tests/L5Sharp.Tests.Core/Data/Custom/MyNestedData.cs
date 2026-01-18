using System.Xml.Linq;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace L5Sharp.Tests.Core.Data.Custom;

/// <summary>
/// A test type used to test nested complex data structure code
/// </summary>
[LogixData(nameof(MyNestedData))]
public class MyNestedData : StructureData
{
    public MyNestedData() : base(nameof(MyNestedData))
    {
        Indy = new BOOL();
        Str = new STRING();
        Tmr = new TIMER();
        Simple = new MySimpleData();
        Flags = new ArrayData<BOOL>(10);
        Counters = new ArrayData<COUNTER>(3);
        Names = new ArrayData<STRING>(5);
    }

    public MyNestedData(XElement element) : base(element)
    {
    }

    /// <summary>
    /// A simple boolean member
    /// </summary>
    public BOOL Indy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// A string member
    /// </summary>
    public STRING Str
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }

    /// <summary>
    /// A nested timer member
    /// </summary>
    public TIMER Tmr
    {
        get => GetMember<TIMER>();
        set => SetMember(value);
    }

    /// <summary>
    /// A nested user defined type
    /// </summary>
    public MySimpleData Simple
    {
        get => GetMember<MySimpleData>();
        set => SetMember(value);
    }

    /// <summary>
    /// A nested array of atomic values.
    /// </summary>
    public ArrayData<BOOL> Flags
    {
        get => GetArray<BOOL>();
        set => SetArray(value);
    }

    /// <summary>
    /// A nested array of structure types.
    /// </summary>
    public ArrayData<COUNTER> Counters
    {
        get => GetArray<COUNTER>();
        set => SetArray(value);
    }

    /// <summary>
    /// A nested array of atomic values.
    /// </summary>
    public ArrayData<STRING> Names
    {
        get => GetArray<STRING>();
        set => SetArray(value);
    }
}