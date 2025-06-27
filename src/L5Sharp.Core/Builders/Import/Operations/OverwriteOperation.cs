using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents an import operation that overwrites an existing <see cref="LogixScoped"/> based on a specified condition.
/// </summary>
internal class OverwriteOperation(Func<LogixScoped, bool> predicate) : IImportOperation
{
    /// <inheritdoc />
    public bool ApplyTo(LogixComponent input)
    {
        return predicate.Invoke(input);
    }
}