using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Components
{
    /// <summary>
    /// Represents a Rung of Ladder Logic, or the logix content that is contained by the <see cref="RllRoutine"/> component.
    /// </summary>
    [LogixSerializer(typeof(RungSerializer))]
    public sealed class Rung
    {
        /// <summary>
        /// The <c>Rung</c> number or index of the rung's position within it's containing <c>Routine</c>.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The <c>Rung</c> type, indicating edit information of the rung.
        /// </summary>
        public RungType Type { get; set; } = RungType.Normal;

        /// <summary>
        /// The comment of the <c>Rung</c>.
        /// </summary>
        /// <value>A <see cref="string"/> containing the text comment of the Rung.</value>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// The <see cref="Core.NeutralText"/> representing the Rung logic.
        /// </summary>
        public NeutralText Text { get; set; } = NeutralText.Empty;

        /// <inheritdoc />
        public override string ToString() => Text;
    }
}