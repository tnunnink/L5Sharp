using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Components
{
    /// <summary>
    /// Represents a Rung of Ladder Logic, or the logix content that is contained by the <see cref="Rll"/> component.
    /// </summary>
    [LogixSerializer(typeof(RungSerializer))]
    public sealed class Rung : ILogixCode
    {
        /// <summary>
        /// The name of the program in which the <c>Rung</c> is contained.
        /// </summary>
        /// <value>A <see cref="string"/> representing the name of the containing program.</value>
        /// <remarks>
        /// This is only used in deserialization of a <see cref="Rung"/> component.
        /// This helper property makes it easier to filter rungs. This property is not serialized back to an L5X file.
        /// </remarks>
        public string Container { get; set; } = string.Empty;

        /// <summary>
        /// The name of the routine in which the <c>Rung</c> is contained.
        /// </summary>
        /// <value>A <see cref="string"/> representing the name of the containing routine.</value>
        /// <remarks>
        /// This is only used in deserialization of a <see cref="Rung"/> component.
        /// This helper property makes it easier to filter rungs. This property is not serialized back to an L5X file.
        /// </remarks>
        public string Routine { get; set; } = string.Empty;
        
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