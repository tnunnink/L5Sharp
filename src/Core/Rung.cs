using System;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a Rung of Ladder Logic.
    /// </summary>
    /// <remarks>
    /// Rungs make up the content of the <see cref="ILadderLogic"/> object. Rung's <see cref="Text"/> property contains
    /// the <see cref="Core.NeutralText"/> value which defines the logic for the instance.
    /// </remarks>
    public sealed class Rung
    {
        /// <summary>
        /// Creates a new instance of a <c>Rung</c> with the provided parameters.
        /// </summary>
        /// <param name="number">The number of the <c>Rung</c>.</param>
        /// <param name="type">The type of the <c>Rung</c>.</param>
        /// <param name="comment">The string comment of the <c>Rung</c>.</param>
        /// <param name="text">The <c>NeutralText</c> content of the <c>Rung</c>.</param>
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
        /// <param name="text">The <see cref="Core.NeutralText"/> value to set.</param>
        /// <param name="comment">The string value to set.</param>
        /// <remarks>
        /// 
        /// </remarks>
        public Rung(NeutralText text, string? comment = null)
        {
            Number = 0;
            Type = RungType.Normal;
            Comment = comment ?? string.Empty;
            Text = text;
        }

        /// <summary>
        /// Gets the integer number of the <see cref="Rung"/> instance.
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
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Text.Equals(((Rung)obj).Text);
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(Number, Type, Comment, Text);

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