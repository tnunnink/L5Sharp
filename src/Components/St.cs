using System.Collections.Generic;
using System.Xml.Serialization;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp.Components;

/// <summary>
/// 
/// </summary>
[XmlType(L5XName.STContent)]
[LogixSerializer(typeof(StSerializer))]
public sealed class St : List<Line>, ILogixCode
{
    /// <inheritdoc />
    public St()
    {
    }

    /// <inheritdoc />
    public St(int capacity) : base(capacity)
    {
    }

    /// <inheritdoc />
    public St(IEnumerable<Line> lines) : base(lines)
    {
    }
    
    /// <inheritdoc />
    public RoutineType Type => RoutineType.St;
}