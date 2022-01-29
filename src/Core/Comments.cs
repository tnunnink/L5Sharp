using System;
using System.Collections;
using System.Collections.Generic;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection tag name and comment value pairs for a certain tag.
    /// </summary>
    internal class Comments : IEnumerable<KeyValuePair<TagName, string>>
    {
        private readonly Dictionary<TagName, string> _comments = new();

        /// <summary>
        /// Creates a new <see cref="Comments"/> collection with the provided comments.
        /// </summary>
        /// <param name="comments">An optional collection of comments to initialize the collection with.</param>
        public Comments(IEnumerable<KeyValuePair<TagName, string>>? comments = null)
        {
            if (comments is null) 
                return;

            _comments = new Dictionary<TagName, string>(comments);
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
        /// <exception cref="ArgumentException">When tagName is null.</exception>
        public string Get(TagName tagName)
        {
            if (string.IsNullOrEmpty(tagName))
                throw new ArgumentException("TagName can not be null or empty.");

            return _comments[tagName];
        }

        /// <summary>
        /// Adds, updates, or removes a comment from the collection depending on the provided comment value.
        /// </summary>
        /// <remarks>
        /// If the provided comment value is non-null or empty and does not currently exist on the collection,
        /// it will be added. If the provided comment exists and is different, it will be updated. If the provided
        /// comment exists and is a empty or null value, the comment will be removed from the collection.
        /// </remarks>
        /// <param name="tagName">The <see cref="TagName"/> for which to set the comment.</param>
        /// <param name="comment">The string comment value.</param>
        /// <exception cref="ArgumentNullException">When comment is null.</exception>
        public void Set(TagName tagName, string comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrEmpty(comment))
            {
                if (_comments.ContainsKey(tagName))
                    _comments.Remove(comment);
                return;
            }

            if (_comments.ContainsKey(tagName))
            {
                _comments[tagName] = comment;
                return;
            }
            
            _comments.Add(tagName, comment);
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<TagName, string>> GetEnumerator() => _comments.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}