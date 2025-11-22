using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents an import operation that applies a modification to a <see cref="ILogixComponent"/> prior to being imported.
/// This operation performs a custom action on the provided component as part of the import process.
/// </summary>
internal class ModifyOperation(Action<ILogixComponent> action) : IImportOperation
{
    /// <inheritdoc />
    public bool ApplyTo(ILogixComponent component)
    {
        action.Invoke(component);
        return true;
    }
}