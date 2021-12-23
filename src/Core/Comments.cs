using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection of <see cref="L5Sharp.Core.Comment"/>
    /// </summary>
    public class Comments : IEnumerable<Comment>
    {
        private readonly ITag<IDataType> _tag;
        private readonly Dictionary<string, Comment> _comments = new(StringComparer.OrdinalIgnoreCase);

        internal Comments(ITag<IDataType> tag)
        {
            _tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        /// <summary>
        /// Determines if the current comment collection contains a comments for the specified operand.
        /// </summary>
        /// <param name="operand">The value of the operand to search.</param>
        /// <returns>
        /// true if the operand is contained in the comments collection.
        /// false if operand is null, empty, or is not contained in the comments collection.
        /// </returns>
        public bool Contains(string operand) => !string.IsNullOrEmpty(operand) && _comments.ContainsKey(operand);

        /// <summary>
        /// Gets the value of the comment for the specified operand.
        /// </summary>
        /// <param name="operand">The value of the operand to find a comments for.</param>
        /// <returns>The string value of the comment if the operand is found; otherwise, and empty string.</returns>
        /// <exception cref="ArgumentException">Thrown when operand is null.</exception>
        public string Get(string operand)
        {
            if (string.IsNullOrEmpty(operand))
                throw new ArgumentException("Operand can not be null or empty.");

            if (!_comments.ContainsKey(operand))
                throw new InvalidOperationException(
                    $"The specified operand '{operand}' does not exist on the collection.");

            return _comments[operand].Value;
        }

        /// <summary>
        /// Add, updates, or removes a comment from the collection depending on the provided comment value.
        /// </summary>
        /// <remarks>
        /// If the provided comment value is non null or empty and does not currently exist on the collection,
        /// it will be added. If the provided comment is exists and is different, it will be updated. If the provided
        /// comment exists and is a empty string, the comment will be removed from the collection.
        /// </remarks>
        /// <param name="comment">The comment to set.</param>
        /// <exception cref="ArgumentNullException">When comment is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// When the provided comment operand is not a valid member of the current tag.
        /// </exception>
        public void Set(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (!IsValidOperand(comment.Operand))
                throw new InvalidOperationException(
                    $"Could not find operand '{comment.Operand}' on current tag {_tag.Name}");

            var exists = _comments.ContainsKey(comment.Operand);
            
            if (string.IsNullOrEmpty(comment.Value) && !exists)
                _comments.Remove(comment.Operand);

            if (string.IsNullOrEmpty(comment.Value))
                _comments.Remove(comment.Operand);

            if (exists)
                _comments[comment.Operand] = comment;

            _comments.Add(comment.Operand, comment);
        }

        /// <summary>
        /// Clears all comments from the current collection.
        /// </summary>
        public void Clear()
        {
            _comments.Clear();
        }

        /// <inheritdoc />
        public IEnumerator<Comment> GetEnumerator()
        {
            return _comments.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Helper to determine if the provided operand name is a member of the root tag.
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        private bool IsValidOperand(string operand)
        {
            return _tag.GetDeepMembersNames().Any(m => m.Contains(operand));
        }
    }
}