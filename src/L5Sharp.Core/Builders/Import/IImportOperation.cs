namespace L5Sharp.Core;

/// <summary>
/// Represents a rule used during the import process of a <see cref="LogixObject"/>.
/// Rules are applied to evaluate or manipulate objects during the import operation.
/// </summary>
public interface IImportOperation
{
    /// <summary>
    /// Applies the rule to the specified <see cref="LogixObject"/> input.
    /// </summary>
    /// <param name="input">The <see cref="LogixObject"/> to which the rule will be applied.</param>
    /// <returns><c>true</c> if the rule was successfully applied; otherwise, <c>false</c>.</returns>
    bool ApplyTo(LogixObject input);
}