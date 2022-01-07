namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a string comment for a component member.  
    /// </summary>
    public sealed class Comment
    {
        /// <summary>
        /// Creates a new instance of a <c>Comment</c> with the provided operand and value.
        /// </summary>
        /// <param name="tagName">The string operand value.</param>
        /// <param name="value">The string comment value.</param>
        internal Comment(TagName tagName, string value)
        {
            TagName = tagName;
            Value = value;
        }

        /// <summary>
        /// Gets the <c>Operand</c>, or member path, for which the comments is applied to.
        /// </summary>
        public TagName TagName { get; }
        
        /// <summary>
        /// Gets the <c>Value</c> of the comment.
        /// </summary>
        public string Value { get; }
    }
    
    
}