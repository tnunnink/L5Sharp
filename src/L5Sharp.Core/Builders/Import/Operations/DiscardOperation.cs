using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents an operation that determines whether a specific <see cref="LogixScoped"/> object
/// should be discarded based on a given predicate function.
/// </summary>
internal class DiscardOperation(Func<LogixScoped, bool> predicate) : IImportOperation
{
    /// <inheritdoc />
    public bool ApplyTo(LogixComponent input)
    {
        return predicate.Invoke(input);
    }
}