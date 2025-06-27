using System;

namespace L5Sharp.Core;

/// <summary>
/// A class that defines an operation to modify a <see cref="LogixScoped"/> object based on a predicate and an action.
/// </summary>
internal class ModifyOperation(Func<LogixScoped, bool> predicate, Action<LogixScoped> action) : IImportOperation
{
    /// <inheritdoc />
    public bool ApplyTo(LogixComponent input)
    {
        if (!predicate.Invoke(input)) return false;
        action.Invoke(input);
        return true;
    }
}