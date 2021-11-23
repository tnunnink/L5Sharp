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

        private readonly Dictionary<string, Comment> _comments =
            new Dictionary<string, Comment>(StringComparer.OrdinalIgnoreCase);

        internal Comments(ITag<IDataType> tag)
        {
            _tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        /// <summary>
        /// Determines if the current comment collection contains a comments for the specified operand.
        /// </summary>
        /// <param name="operand">The value of the operand to search</param>
        /// <returns>
        /// True if the operand is contained in the comments collection.
        /// False if not, or if the operand is null, empty, or is noty a valid operand for the current tag.
        /// </returns>
        public bool HasComment(string operand)
        {
            if (string.IsNullOrEmpty(operand))
                return false;

            return _tag.GetDeepMembersNames().Any(m => m.Contains(operand)) && _comments.ContainsKey(operand);
        }

        /// <summary>
        /// Gets the value of the comment for the specified operand.
        /// </summary>
        /// <param name="operand">The value of the operand to find a comments for.</param>
        /// <returns>the string value of the comment if is is found, null if not.</returns>
        /// <exception cref="ArgumentException">Thrown when operand is null.</exception>
        public string GetComment(string operand)
        {
            if (string.IsNullOrEmpty(operand))
                throw new ArgumentException("Operand can me null");

            _comments.TryGetValue(operand, out var comment);
            return comment?.Value;
        }

        /// <summary>
        /// Updates or adds a comment to the collection.
        /// </summary>
        /// <param name="comment">The comment to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when the comment is null.</exception>
        public void Override(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (!IsValidOperand(comment.Operand))
                throw new InvalidOperationException(
                    $"Could not find operand '{comment.Operand}' on current tag {_tag.Name}");

            if (_comments.ContainsKey(comment.Operand))
                _comments[comment.Operand] = comment;

            _comments.Add(comment.Operand, comment);
        }

        /// <summary>
        /// Removes the comment from the collection.
        /// </summary>
        /// <param name="operand">The operand name of the comment to remove.</param>
        public void Reset(string operand)
        {
            if (string.IsNullOrEmpty(operand))
                throw new ArgumentException("Operand can not be null or empty");

            if (!IsValidOperand(operand))
                throw new InvalidOperationException(
                    $"Could not find operand '{operand}' on current tag {_tag.Name}");

            if (!_comments.ContainsKey(operand)) return;

            _comments.Remove(operand);
        }

        /// <summary>
        /// Clears all comments from the current collection.
        /// </summary>
        public void Clear(string baseMemberName)
        {
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

        private bool IsValidOperand(string operand)
        {
            return _tag.GetDeepMembersNames().Any(m => m.Contains(operand));
        }
    }
}