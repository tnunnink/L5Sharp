using System.Collections.Generic;
using System.Xml.Serialization;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp.Components;

/// <summary>
/// 
/// </summary>
[XmlType(L5XName.RLLContent)]
[LogixSerializer(typeof(RllSerializer))]
public sealed class Rll : List<Rung>, ILogixCode
{
    /// <inheritdoc />
    public Rll()
    {
    }

    /// <inheritdoc />
    public Rll(int capacity) : base(capacity)
    {
    }

    /// <inheritdoc />
    public Rll(IEnumerable<Rung> rungs) : base(rungs)
    {
    }
    
    /// <inheritdoc />
    public RoutineType Type => RoutineType.Rll;
}