using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents an import operation that applies a modification to a <see cref="LogixComponent"/> prior to being imported.
/// This operation performs a custom action on the provided component as part of the import process.
/// </summary>
internal class ModifyOperation(Action<LogixComponent> action) : IImportOperation
{
    /// <inheritdoc />
    public bool ApplyTo(LogixComponent input)
    {
        action.Invoke(input);
        return true;
    }
}