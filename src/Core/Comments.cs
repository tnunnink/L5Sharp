using System;
using System.Collections;
using System.Collections.Generic;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection of <see cref="L5Sharp.Core.Comment"/>
    /// </summary>
    public class Comments : IEnumerable<Comment>
    {
        private readonly ITag<IDataType> _tag;
        private readonly Dictionary<TagName, Comment> _comments = new();

        internal Comments(ITag<IDataType> tag)
        {
            _tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        /// <summary>
        /// Determines if the current comment collection contains a comments for the specified tag name.
        /// </summary>
        /// <param name="tagName">The value of the operand to search.</param>
        /// <returns>
        /// true if the operand is contained in the comments collection.
        /// false if operand is null, empty, or is not contained in the comments collection.
        /// </returns>
        public bool Contains(TagName tagName) => !string.IsNullOrEmpty(tagName) && _comments.ContainsKey(tagName);

        /// <summary>
        /// Gets the value of the comment for the specified tag name.
        /// </summary>
        /// <param name="tagName">The value of the tag name to find a comments for.</param>
        /// <returns>The string value of the comment if the operand is found; otherwise, and empty string.</returns>
        /// <exception cref="ArgumentException">Thrown when operand is null.</exception>
        public string Get(TagName tagName)
        {
            if (string.IsNullOrEmpty(tagName))
                throw new ArgumentException("TagName can not be null or empty.");

            if (!_comments.ContainsKey(tagName))
                throw new InvalidOperationException(
                    $"The specified operand '{tagName}' does not exist on the collection.");

            return _comments[tagName].Value;
        }

        /// <summary>
        /// Adds, updates, or removes a comment from the collection depending on the provided comment value.
        /// </summary>
        /// <remarks>
        /// If the provided comment value is non-null or empty and does not currently exist on the collection,
        /// it will be added. If the provided comment exists and is different, it will be updated. If the provided
        /// comment exists and is a empty or null value, the comment will be removed from the collection.
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

            if (!IsValidTagName(comment.TagName))
                throw new InvalidOperationException(
                    $"Could not find operand '{comment.TagName}' on current tag {_tag.Name}");

            if (string.IsNullOrEmpty(comment.Value))
            {
                if (_comments.ContainsKey(comment.TagName))
                    _comments.Remove(comment.TagName);
                return;
            }

            if (_comments.ContainsKey(comment.TagName))
            {
                _comments[comment.TagName] = comment;
                return;
            }
            
            _comments.Add(comment.TagName, comment);
        }

        /// <inheritdoc />
        public IEnumerator<Comment> GetEnumerator() => _comments.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Helper to determine if the provided tag name exists for the current root tag.
        /// </summary>
        /// <param name="tagName">The <see cref="TagName"/> value to verify.</param>
        /// <returns>
        /// true if the tag name is the name of the root tag or if it is the name of one of the tag's members; otherwise, false.
        /// </returns>
        private bool IsValidTagName(TagName tagName) => _tag.TagName.Equals(tagName) || _tag.Contains(tagName);
    }
}