namespace L5Sharp.Core;

/// <summary>
/// Enum representing options when loading or parsing an <see cref="L5X"/> instnace.
/// </summary>
public enum L5XOptions
{
    /// <summary>
    /// No additional options for loading or parsing the L5X file are enabled.
    /// </summary>
    /// <remarks>
    /// This means that the L5X file will be loaded or parsed with the default behavior.
    /// Any call to one of the <see cref="ILogixLookup"/> methods will use XPath lookup for elements.
    /// If you need fast lookups, consider selecting <see cref="Index"/> to have the content indexed upon a load.
    /// </remarks>
    None,

    /// <summary>
    /// This option enables indexing of the L5X file, allowing for fast lookups using the <see cref="ILogixLookup"/> API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This will slightly increase the load time, but it significantly speeds up the performance
    /// of lookup operations using any <see cref="ILogixLookup"/> API. 
    /// This option is useful when the user plans to execute many element lookups.
    /// </para>
    /// <para>
    /// Any mutation of scoped elements will not be reflected in the index as it does not track changes.
    /// The <c>Index</c> option is primarily intended for read-only interaction on the file. If you make changes and need
    /// to then perform fast lookups, you can load or parse a new instance of an L5X.
    /// </para> 
    /// </remarks>
    Index
}