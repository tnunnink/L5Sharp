using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents a rule that determines whether an operation should overwrite a <see cref="LogixObject"/> during an import process.
/// </summary>
internal class OverwriteOperation<TObject>(Func<TObject, bool> predicate) : IImportOperation where TObject : LogixObject
{
    /// <inheritdoc />
    public bool ApplyTo(LogixObject input)
    {
        return predicate.Invoke((TObject)input);
    }
}