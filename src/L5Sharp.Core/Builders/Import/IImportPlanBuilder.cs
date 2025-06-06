using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents a builder interface for creating and customizing import plans for Logix components.
/// </summary>
public interface IImportPlanBuilder
{
    /// <summary>
    /// Configures the import process to overwrite all existing components with the same name and type.
    /// </summary>
    /// <returns>An updated <see cref="IImportPlanBuilder"/> configured to allow overwriting of components.</returns>
    IImportPlanBuilder Overwrite();

    /// <summary>
    /// Configures the import process to overwrite all existing components of the specified type that satisfy the given condition.
    /// </summary>
    /// <typeparam name="TComponent">The type of the components to be evaluated for overwriting.</typeparam>
    /// <param name="predicate">A function that determines whether a specific component instance should be overwritten.</param>
    /// <returns>An updated <see cref="IImportPlanBuilder"/> with the overwrite operation applied for the specified components.</returns>
    IImportPlanBuilder Overwrite<TComponent>(Func<TComponent, bool> predicate)
        where TComponent : LogixComponent;

    /// <summary>
    /// Configures the import process to discard the specified components that match the provided predicate.
    /// </summary>
    /// <typeparam name="TComponent">The type of the Logix component to evaluate.</typeparam>
    /// <param name="predicate">A function that determines which components to discard.</param>
    /// <returns>An updated <see cref="IImportPlanBuilder"/> configured to discard matching components during the import process.</returns>
    IImportPlanBuilder Discard<TComponent>(Func<TComponent, bool> predicate)
        where TComponent : LogixComponent;

    /// <summary>
    /// Modifies components of the specified type that satisfy the given predicate with the provided configuration logic.
    /// </summary>
    /// <typeparam name="TComponent">The type of the component to modify. Must inherit from <see cref="LogixComponent"/>.</typeparam>
    /// <param name="predicate">A function defining the condition to identify the components to modify.</param>
    /// <param name="config">An action specifying the configuration or modification to apply to the matched components.</param>
    /// <returns>An updated <see cref="IImportPlanBuilder"/> instance with the modification configuration applied.</returns>
    IImportPlanBuilder Modify<TComponent>(Func<TComponent, bool> predicate, Action<TComponent> config)
        where TComponent : LogixComponent;

    /// <summary>
    /// Configures the import process to rename an existing component of the specified type from the current name to the updated name.
    /// </summary>
    /// <typeparam name="TComponent">The type of the Logix component to be renamed.</typeparam>
    /// <param name="current">The current name of the component to be renamed.</param>
    /// <param name="updated">The new name to assign to the component.</param>
    /// <returns>An updated <see cref="IImportPlanBuilder"/> configured to rename the specified component.</returns>
    IImportPlanBuilder Rename<TComponent>(string current, string updated)
        where TComponent : LogixComponent;
}