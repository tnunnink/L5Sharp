namespace L5Sharp.Core
{
    /// <summary>
    /// Represents the content for a <see cref="IStructuredText"/> Logix Routine. 
    /// </summary>
    public class Line
    {
        internal Line(int number, string text)
        {
            Number = number;
            Text = text;
        }
        
        /// <summary>
        /// Gets the number of the <see cref="Line"/>.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Gets the text value of the <see cref="Line"/>.
        /// </summary>
        public string Text { get; }
        
        /// <inheritdoc />
        public override string ToString() => Text;

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Text.Equals(((Line)obj).Text);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Text.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(Line? left, Line? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Line? left, Line? right) => !Equals(left, right);
    }
}