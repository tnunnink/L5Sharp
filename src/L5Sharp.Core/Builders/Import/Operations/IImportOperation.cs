namespace L5Sharp.Core;

/// <summary>
/// Defines an operation that can be applied to a <see cref="ILogixComponent"/> during an import process.
/// </summary>
internal interface IImportOperation
{
    /// <summary>
    /// Applies an operation to the specified <see cref="ILogixComponent"/> instance.
    /// </summary>
    /// <param name="component">The <see cref="ILogixComponent"/> instance to which the operation should be applied.</param>
    /// <returns>True if the operation was successfully applied, otherwise false.</returns>
    bool ApplyTo(ILogixComponent component);
}