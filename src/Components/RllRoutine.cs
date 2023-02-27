using System.Collections.Generic;
using System.Xml.Serialization;
using L5Sharp.Attributes;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    [LogixSerializer(typeof(RllRoutineSerializer))]
    [XmlType(L5XName.Routine)]
    public sealed class RllRoutine : Routine
    {
        /// <inheritdoc />
        public override RoutineType Type => RoutineType.Rll;

        /// <summary>
        /// 
        /// </summary>
        public List<Rung> Content { get; set; } = new();
    }
}