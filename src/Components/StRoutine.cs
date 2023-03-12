using System.Collections.Generic;
using System.Xml.Serialization;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>StRoutine</c> component. Contains the properties that comprise the L5X Structured Text Routine element.
/// </summary>
[LogixSerializer(typeof(StRoutineSerializer))]
[XmlType(L5XName.Routine)]
public class StRoutine : Routine
{
    /// <inheritdoc />
    public override RoutineType Type => RoutineType.St;

    /// <summary>
    /// The collection of <see cref="Line"/> or structured text the make up the logic or code of the Routine.
    /// </summary>
    public List<Line> Content { get; set; } = new();
}