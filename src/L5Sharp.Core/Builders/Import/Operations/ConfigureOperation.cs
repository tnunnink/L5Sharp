using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents an operation that applies a configuration action to an <see cref="ILogixComponent"/>.
/// </summary>
internal class ConfigureOperation(Action<ILogixComponent> action) : IImportOperation
{
    public bool ApplyTo(ILogixComponent component)
    {
        action.Invoke(component);
        return true;
    }
}