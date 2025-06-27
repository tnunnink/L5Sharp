namespace L5Sharp.Core;

/// <summary>
/// Defines an operation that can be applied to a <see cref="LogixComponent"/> during an import process.
/// </summary>
internal interface IImportOperation
{
    /// <summary>
    /// Applies an operation to the specified <see cref="LogixComponent"/> instance.
    /// </summary>
    /// <param name="input">The <see cref="LogixComponent"/> instance to which the operation should be applied.</param>
    /// <returns>True if the operation was successfully applied, otherwise false.</returns>
    bool ApplyTo(LogixComponent input);
}