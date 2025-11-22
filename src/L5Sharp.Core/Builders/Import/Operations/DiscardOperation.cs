using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents an operation that determines whether a specific <see cref="ILogixEntity"/> object
/// should be discarded based on a given predicate function.
/// </summary>
internal class DiscardOperation(Func<ILogixEntity, bool> predicate) : IImportOperation
{
    /// <inheritdoc />
    public bool ApplyTo(ILogixComponent component)
    {
        return predicate.Invoke(component);
    }
}