using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a Rung of Ladder Logic, or the logix content that is contained by the <see cref="LadderLogic"/> component.
    /// </summary>
    /// <remarks>
    /// Rungs make up the content of the <see cref="ILadderLogic"/> object. Rung's <see cref="Text"/> property contains
    /// the <see cref="Core.NeutralText"/> value which defines the logic for the instance.
    /// </remarks>
    public sealed class Rung
    {
        /// <summary>
        /// Creates a new <see cref="Rung"/> with the provided parameters.
        /// </summary>
        /// <param name="number">The number of the rung.</param>
        /// <param name="type">The type of the rung.</param>
        /// <param name="comment">The comment of the rung.</param>
        /// <param name="text">The <see cref="Core.NeutralText"/> content of the rung.</param>
        internal Rung(int number = 0, RungType? type = null, string? comment = null, NeutralText? text = null)
        {
            Number = number;
            Type = type ?? RungType.Normal;
            Comment = comment ?? string.Empty;
            Text = text ?? NeutralText.Empty;
        }

        /// <summary>
        /// Creates a new <see cref="Rung"/> instance with the provided text and comment values.
        /// </summary>
        /// <param name="text">The <see cref="Core.NeutralText"/> content of the rung.</param>
        /// <param name="comment">The optional comment of the rung. Will default to empty string.</param>
        public Rung(NeutralText text, string? comment = null)
        {
            Number = 0;
            Type = RungType.Normal;
            Comment = comment ?? string.Empty;
            Text = text;
        }

        /// <summary>
        /// Gets the number of the <see cref="Rung"/> for which it is contained within a routine.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Gets the <see cref="Enums.RungType"/> of the <see cref="Rung"/> instance.
        /// </summary>
        public RungType Type { get; }

        /// <summary>
        /// Gets the string comment value of the <see cref="Rung"/> instance.
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// Gets the <see cref="Core.NeutralText"/> value of the <see cref="Rung"/> instance.
        /// </summary>
        public NeutralText Text { get; }

        /// <inheritdoc />
        public override string ToString() => Text;

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Text.Equals(((Rung)obj).Text);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Text.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(Rung? left, Rung? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Rung? left, Rung? right) => !Equals(left, right);
    }
}