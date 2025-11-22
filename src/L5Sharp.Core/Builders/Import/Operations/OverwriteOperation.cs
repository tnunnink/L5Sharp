using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents an import operation that overwrites an existing <see cref="ILogixEntity"/> based on a specified condition.
/// </summary>
internal class OverwriteOperation(Func<ILogixEntity, bool> predicate) : IImportOperation
{
    /// <inheritdoc />
    public bool ApplyTo(ILogixComponent component)
    {
        return predicate.Invoke(component);
    }
}