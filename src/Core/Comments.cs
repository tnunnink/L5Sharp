using System;
using System.Collections;
using System.Collections.Generic;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection tag name and comment value pairs for a certain tag.
    /// </summary>
    public class Comments : IEnumerable<KeyValuePair<TagName, string>>
    {
        private readonly Dictionary<TagName, string> _comments;

        /// <summary>
        /// Creates a new <see cref="Comments"/> collection with the provided comments.
        /// </summary>
        /// <param name="comments">An optional collection of comments to initialize the collection with.</param>
        public Comments(IEnumerable<KeyValuePair<TagName, string>>? comments = null)
        {
            if (comments is null)
            {
                _comments = new Dictionary<TagName, string>();
                return;
            }

            _comments = new Dictionary<TagName, string>(comments);
        }

        /// <summary>
        /// Applies the provided comment to the specified tag name.
        /// </summary>
        /// <remarks>
        /// If the provided comment value is non-null or empty it will be added or updated depending on its existence in
        /// the current collection. If the provided comment is an empty or null string,
        /// the comment will be removed (or reset) from the collection.
        /// </remarks>
        /// <param name="tagName">The <see cref="TagName"/> for which to set the comment.</param>
        /// <param name="comment">The string comment value.</param>
        /// <exception cref="ArgumentNullException"><c>tagName</c> is null.</exception>
        public void Apply(TagName tagName, string? comment)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));
            
            if (string.IsNullOrEmpty(comment))
            {
                _comments.Remove(tagName);
                return;
            }

            _comments[tagName] = comment;
        }

        /// <summary>
        /// Determines whether <see cref="Comments"/> contains the specified tag name.
        /// </summary>
        /// <param name="comment">The comment to locate in the collection.</param>
        /// <returns>
        /// true if <see cref="Comments"/> contains the specified comment.
        /// false if comment is null, empty, or is not contained in <see cref="Comments"/>.
        /// </returns>
        public bool ContainsValue(string comment) => !string.IsNullOrEmpty(comment) && _comments.ContainsValue(comment);

        /// <summary>
        /// Determines whether <see cref="Comments"/> contains the specified tag name.
        /// </summary>
        /// <param name="tagName">The tag name to locate in the collection.</param>
        /// <returns>
        /// true if <see cref="Comments"/> contains the specified tag name.
        /// false if tagName is null, empty, or is not contained in <see cref="Comments"/>.
        /// </returns>
        public bool ContainsTag(TagName? tagName) => tagName is not null && _comments.ContainsKey(tagName);

        /// <summary>
        /// Gets the comment value for the specified tag name.
        /// </summary>
        /// <param name="tagName">The tag name for which to find a comment.</param>
        /// <returns>The comment value of the specified tag name if found; otherwise, an empty string.</returns>
        /// <exception cref="ArgumentNullException">tagName is null.</exception>
        public string Get(TagName? tagName)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            return _comments.TryGetValue(tagName, out var comment) ? comment : string.Empty;
        }

        /// <summary>
        /// Removes the provided tag name key from the comments collection if it is found.
        /// </summary>
        /// <param name="tagName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Reset(TagName tagName) => _comments.Remove(tagName);

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<TagName, string>> GetEnumerator() => _comments.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}