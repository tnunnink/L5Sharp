using System;

namespace L5Sharp.Core;

public interface IImportUpdateBuilder
{
    /// <summary>
    /// Removes or discards the specified resource from the imported L5X file.
    /// </summary>
    /// <param name="scope">
    /// The <see cref="Scope"/> object representing the component to be discarded or removed.
    /// </param>
    /// <returns>
    /// An instance of <see cref="IImportUpdateBuilder{TObject}"/> allowing for further configuration or chaining of methods.
    /// </returns>
    IImportUpdateBuilder Discard(Scope scope);

    /// <summary>
    /// Configures the import process to use the existing content of the component or type.
    /// </summary>
    /// <param name="scope">
    /// The <see cref="Scope"/> defining the context or location where existing content should be used.
    /// </param>
    /// <returns>
    /// An instance of <see cref="IImportUpdateBuilder{TObject}"/> for further configuration or method chaining.
    /// </returns>
    IImportUpdateBuilder UseExisting(Scope scope);

    /// <summary>
    /// Configures the import process to overwrite the specified component or type.
    /// </summary>
    /// <param name="scope">
    /// The scope of the component to be overwritten. This defines the specific component in the file to overwrite.
    /// </param>
    /// <returns>
    /// An instance of <see cref="IImportUpdateBuilder{TObject}"/> that allows further configuration or chaining of additional methods.
    /// </returns>
    IImportUpdateBuilder Overwrite(Scope scope);

    /// <summary>
    /// Configures a custom conflict resolution strategy for a specified component during the import process.
    /// </summary>
    /// <param name="scope">The <see cref="Scope"/> path that identifies the component for which to apply the conflict handler.</param>
    /// <param name="handler">A conflict handler delegate, providing the original/target <see cref="LogixObject"/> and
    /// the new/source <see cref="LogixObject"/> for resolution.</param>
    /// <returns>
    /// An instance of <see cref="IImportUpdateBuilder{TObject}"/> to enable further configuration or method chaining.
    /// </returns>
    IImportUpdateBuilder OnConflict(Scope scope, Action<LogixObject, LogixObject> handler);

    /// <summary>
    /// Modifies the imported L5X using the provided update action.
    /// </summary>
    /// <param name="update">
    /// An action that defines the modifications to be applied to the imported L5X file.
    /// </param>
    /// <returns>
    /// An instance of <see cref="IImportUpdateBuilder{TObject}"/> allowing for further configuration or chaining of additional methods.
    /// </returns>
    IImportUpdateBuilder Modify(Action<L5X> update);

    /// <summary>
    /// Replaces all occurrences of a specified string with another string within the context of the import builder.
    /// </summary>
    /// <param name="find">
    /// The string to find within the import configuration.
    /// </param>
    /// <param name="replace">
    /// The string to replace the occurrences of the find string with.
    /// </param>
    /// <returns>
    /// An instance of <see cref="IImportUpdateBuilder{TObject}"/> enabling further configuration or chaining of methods.
    /// </returns>
    IImportUpdateBuilder Replace(string find, string replace);
}