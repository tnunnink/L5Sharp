using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents a rule used to modify a <see cref="LogixObject"/> that matches specified criteria.
/// This is achieved by providing a name, type, and associated action to invoke when the condition is met.
/// </summary>
internal class ModifyOperation<TComponent>(Func<TComponent, bool> predicate, Action<TComponent> action) : IImportOperation
    where TComponent : LogixComponent
{
    /// <inheritdoc />
    public bool ApplyTo(LogixObject input)
    {
        if (input is not LogixComponent component) return false;
        if (component.GetType() != typeof(TComponent)) return false;
        if (!predicate.Invoke((TComponent)component)) return false;

        action.Invoke((TComponent)input);
        return true;
    }
}