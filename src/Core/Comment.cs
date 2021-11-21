namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a string comment for a component member.  
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Creates a new instance of a Comment with the provided operand and comment value
        /// </summary>
        /// <param name="operand">The string operand value.</param>
        /// <param name="value">The string comment value.</param>
        public Comment(string operand, string value)
        {
            Operand = operand;
            Value = value;
        }

        /// <summary>
        /// Gets the value of the <c>Operand</c> for which the comments is applied to.
        /// </summary>
        public string Operand { get; }
        
        /// <summary>
        /// Gets the value of the comment.
        /// </summary>
        public string Value { get; }
    }
    
    
}