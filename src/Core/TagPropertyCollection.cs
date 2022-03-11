using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection tag name and comment value pairs for a certain tag.
    /// </summary>
    public class TagPropertyCollection<TProperty> : IEnumerable<KeyValuePair<TagName, TProperty>>
    {
        private readonly Dictionary<TagName, TProperty> _properties;

        /// <summary>
        /// Creates a new empty <see cref="TagPropertyCollection{TProperty}"/> of the specified property type.
        /// </summary>
        public TagPropertyCollection()
        {
            _properties = new Dictionary<TagName, TProperty>();
        }
        
        /// <summary>
        /// Creates a new <see cref="TagPropertyCollection{TProperty}"/> with the provided key value pair collection.
        /// </summary>
        /// <param name="properties">the collection to initialize the <see cref="TagPropertyCollection{TProperty}"/>.</param>
        public TagPropertyCollection(IEnumerable<KeyValuePair<TagName, TProperty>>? properties = null)
        {
            properties ??= Enumerable.Empty<KeyValuePair<TagName, TProperty>>();
            _properties = new Dictionary<TagName, TProperty>(properties);
        }

        /// <summary>
        /// Gets a property value for the specified tag name key.
        /// </summary>
        /// <param name="tagName">The tag name key for which to get the property value.</param>
        public TProperty this[TagName tagName] => _properties[tagName];

        /// <summary>
        /// Applies the provided comment to the specified tag name.
        /// </summary>
        /// <remarks>
        /// If the provided comment value is non-null or empty it will be added or updated depending on its existence in
        /// the current collection. If the provided comment is an empty or null string,
        /// the comment will be removed (or reset) from the collection.
        /// </remarks>
        /// <param name="tagName">The <see cref="TagName"/> for which to set the comment.</param>
        /// <param name="property">The string comment value.</param>
        /// <exception cref="ArgumentNullException"><c>tagName</c> is null.</exception>
        public void Configure(TagName tagName, TProperty property)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));
            
            if (property is null)
                throw new ArgumentNullException(nameof(property));

            _properties[tagName] = property;
        }

        /// <summary>
        /// Determines whether <see cref="TagPropertyCollection{TProperty}"/> contains a property for the specified tag name.
        /// </summary>
        /// <param name="tagName">The tag name to search for in the collection.</param>
        /// <returns>
        /// true if the collection contains the specified tag name; otherwise, false.
        /// </returns>
        public bool ContainsTag(TagName tagName) => _properties.ContainsKey(tagName);
        
        /// <summary>
        /// Determines whether <see cref="TagPropertyCollection{TProperty}"/> contains the specified property value.
        /// </summary>
        /// <param name="property">The value to search for in the collection.</param>
        /// <returns>
        /// true if the collection contains the specified property value; otherwise, false.
        /// </returns>
        public bool ContainsValue(TProperty property) => _properties.ContainsValue(property);

        /// <summary>
        /// Removes the provided tag name key from the property collection if it is found.
        /// </summary>
        /// <param name="tagName">The tag name key value.</param>
        public bool Reset(TagName tagName) => _properties.Remove(tagName);

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<TagName, TProperty>> GetEnumerator() => _properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}