using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// Represents the configuration for an import operation within an L5X project.
/// </summary>
internal class ImportConfig(L5X context, L5X source, Scope target)
{
    /// <summary>
    /// The current L5X context which we are importing content into.
    /// </summary>
    public L5X Context { get; } = context;

    /// <summary>
    /// The path to the source L5X file that will be imported
    /// </summary>
    public L5X Source { get; } = source;

    /// <summary>
    /// The target scope of the component or element that we are importing.
    /// </summary>
    public Scope Target { get; } = target;

    /// <summary>
    /// The collection of import operations to perform when running the import process.
    /// </summary>
    public List<IImportOperation> Operations { get; } = [];
}