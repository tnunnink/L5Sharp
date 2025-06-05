using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents a rule that evaluates whether a <see cref="LogixObject"/> should be discarded during an import process.
/// This rule uses a specified condition to determine discardable objects.
/// </summary>
public class DiscardOperation<TObject>(Func<TObject, bool> predicate) : IImportOperation where TObject : LogixObject
{
    private readonly Func<TObject, bool> _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

    /// <inheritdoc />
    public bool ApplyTo(LogixObject input)
    {
        return _predicate.Invoke((TObject)input);
    }
}