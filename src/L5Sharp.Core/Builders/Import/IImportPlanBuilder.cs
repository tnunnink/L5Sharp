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
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    IImportPlanBuilder Overwrite<TComponent>(Func<TComponent, bool> predicate)
        where TComponent : LogixComponent;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    IImportPlanBuilder Discard<TComponent>(Func<TComponent, bool> predicate)
        where TComponent : LogixComponent;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="config"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    IImportPlanBuilder Modify<TComponent>(Func<TComponent, bool> predicate, Action<TComponent> config)
        where TComponent : LogixComponent;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="current"></param>
    /// <param name="updated"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    IImportPlanBuilder Rename<TComponent>(string current, string updated)
        where TComponent : LogixComponent;
}