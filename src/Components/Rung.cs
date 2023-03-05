using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Components
{
    /// <summary>
    /// Represents a Rung of Ladder Logic, or the logix content that is contained by the <see cref="RllRoutine"/> component.
    /// </summary>
    /// <remarks>
    /// </remarks>
    [LogixSerializer(typeof(RungSerializer))]
    public sealed class Rung
    {
        /// <summary>
        /// Gets the number of the <see cref="Rung"/> for which it is contained within a routine.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets the <see cref="Enums.RungType"/> of the <see cref="Rung"/> instance.
        /// </summary>
        public RungType Type { get; set; } = RungType.Normal;

        /// <summary>
        /// Gets the string comment value of the <see cref="Rung"/> instance.
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// Gets the <see cref="Core.NeutralText"/> value of the <see cref="Rung"/> instance.
        /// </summary>
        public NeutralText Text { get; set; } = NeutralText.Empty;

        /// <inheritdoc />
        public override string ToString() => Text;
    }
}