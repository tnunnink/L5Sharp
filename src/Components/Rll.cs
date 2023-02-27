using System.Collections.Generic;
using L5Sharp.Attributes;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    [LogixSerializer(typeof(RllSerializer))]
    public sealed class Rll : Routine
    {
        /// <inheritdoc />
        public override RoutineType Type => RoutineType.Rll;

        /// <summary>
        /// 
        /// </summary>
        public List<Rung> Content { get; set; } = new();
    }
}