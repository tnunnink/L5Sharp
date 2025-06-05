using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents an import plan that defines a set of rules and operations for merging components
/// between source and target L5X files.
/// </summary>
public class ImportPlan
{
    private readonly List<IImportOperation> _operations = [];

    /// <summary>
    /// Adds an import rule to the current import plan.
    /// </summary>
    /// <param name="operation">The import rule to be added. Must implement the <see cref="IImportOperation"/> interface.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided rule is null.</exception>
    public void AddOperation(IImportOperation operation)
    {
        if (operation is null)
            throw new ArgumentNullException(nameof(operation));

        _operations.Add(operation);
    }

    /// <summary>
    /// Executes the import operation from the source L5X file to the target L5X file by merging components.
    /// </summary>
    /// <param name="target">The target L5X file where components will be imported to. Must not be null.</param>
    /// <param name="source">The source L5X file from which components will be imported. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown when either the target or source parameter is null.</exception>
    public void Execute(L5X target, L5X source)
    {
        ImportComponents(target.DataTypes, source.DataTypes);
        ImportComponents(target.Instructions, source.Instructions);
        ImportComponents(target.Modules, source.Modules);
        ImportComponents(target.Tags, source.Tags);
        ImportComponents(target.Programs, source.Programs);
        ImportComponents(target.Tasks, source.Tasks);
    }

    private void ImportComponents<TComponent>(ICollection<TComponent> existing, ICollection<TComponent> importing)
        where TComponent : LogixComponent
    {
        foreach (var component in importing)
        {
            if (IsDiscardable(component)) continue;
            ApplyModification(component);

            var match = existing.FirstOrDefault(c => c.Name == component.Name);
            if (match is null)
            {
                existing.Add(component);
                continue;
            }

            HandleConflict(match, component);
        }
    }

    /// <summary>
    /// Determines whether the specified component should be discarded based on the applied import rules.
    /// </summary>
    private bool IsDiscardable<TComponent>(TComponent component) where TComponent : LogixComponent
    {
        return _operations.Any(o => o is DiscardOperation<TComponent> && o.ApplyTo(component));
    }

    /// <summary>
    /// Applies update rules to the specified Logix component.
    /// </summary>
    private void ApplyModification<TComponent>(TComponent component) where TComponent : LogixComponent
    {
        var operations = _operations.Where(o => o is ModifyOperation<TComponent>).ToArray();

        foreach (var operation in operations)
        {
            operation.ApplyTo(component);
        }
    }

    /// <summary>
    /// Handles a conflict between two Logix components during the import process.
    /// </summary>
    private void HandleConflict<TComponent>(TComponent target, TComponent source) where TComponent : LogixComponent
    {
        var operations = _operations.Where(o => o is OverwriteOperation<TComponent> && o.ApplyTo(source)).ToArray();

        //Target components will be overwritten if not otherwise specified.
        //Any other type will default to use existing unless it matches a configured rule predicate.
        if (source.Use is not null && source.Use == Use.Target || operations.Length > 0)
        {
            target.Replace(source);
        }
    }
}