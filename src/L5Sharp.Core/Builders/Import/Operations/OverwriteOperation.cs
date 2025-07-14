using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents an import operation that overwrites an existing <see cref="LogixEntity"/> based on a specified condition.
/// </summary>
internal class OverwriteOperation(Func<LogixEntity, bool> predicate) : IImportOperation
{
    /// <inheritdoc />
    public bool ApplyTo(LogixComponent input)
    {
        return predicate.Invoke(input);
    }
}