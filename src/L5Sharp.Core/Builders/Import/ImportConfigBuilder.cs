using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core;

internal abstract class ImportConfigBuilder<TBuilder>(ImportConfig config) : IImportConfigBuilder<TBuilder>
{
    public TBuilder Rename(string name)
    {
        config.Operations.Add(new ModifyOperation(
            x =>
            {
                if (x is not LogixComponent component) return false;
                return component.Scope == config.Target;
            },
            x =>
            {
                if (x is not LogixComponent component) return;
                component.Name = name;
            }));

        return GetBuilder();
    }

    public TBuilder Rename<TComponent>(string current, string updated)
        where TComponent : LogixComponent
    {
        config.Operations.Add(new ModifyOperation(
            x =>
            {
                if (x is not TComponent component) return false;
                return component.Name == current;
            },
            x =>
            {
                if (x is not TComponent component) return;
                component.Name = updated;
            }));

        return GetBuilder();
    }

    public TBuilder Force()
    {
        config.Operations.Add(new OverwriteOperation(_ => true));

        return GetBuilder();
    }

    public TBuilder Overwrite<TComponent>(Func<TComponent, bool> predicate)
        where TComponent : LogixComponent
    {
        config.Operations.Add(new OverwriteOperation(x =>
        {
            if (x is not TComponent component) return false;
            return predicate.Invoke(component);
        }));

        return GetBuilder();
    }

    public TBuilder Discard<TComponent>(Func<TComponent, bool> predicate)
        where TComponent : LogixComponent
    {
        config.Operations.Add(new DiscardOperation(x =>
        {
            if (x is not TComponent component) return false;
            return predicate.Invoke(component);
        }));

        return GetBuilder();
    }

    public TBuilder Modify<TComponent>(Func<TComponent, bool> predicate, Action<TComponent> action)
        where TComponent : LogixComponent
    {
        config.Operations.Add(new ModifyOperation(
            x =>
            {
                if (x is not TComponent component) return false;
                return predicate.Invoke(component);
            },
            x =>
            {
                if (x is not TComponent component) return;
                action.Invoke(component);
            }));

        return GetBuilder();
    }

    protected abstract TBuilder GetBuilder();

    public void Run()
    {
        var target = config.Source.Get(config.Target).As<LogixComponent>();
        ImportTarget(target, config.Context);

        var dependencies = target.Dependencies().ToArray();
        ImportDependencies(dependencies, config.Context);
    }

    /// <summary>
    /// Imports a single Logix component into the specified L5X content, applying modifications, handling conflicts,
    /// and adding it to the content if it does not already exist.
    /// </summary>
    /// <param name="import">The Logix component to be imported into the L5X content.</param>
    /// <param name="content">The target L5X content where the Logix component will be processed and added.</param>
    private void ImportTarget(LogixComponent import, L5X content)
    {
        ApplyModification(import);

        if (content.TryGet(import.Scope, out var match) && match is LogixComponent current)
            HandleConflict(current, import);
        else
            content.Add(import);

        //Get the new added instance to configure.
        //This is because the old will still contain a reference to the source L5X.
        var component = content.Get(import.Scope.LocalPath).As<LogixComponent>();
        ApplyConfiguration(component);
    }

    /// <summary>
    /// Imports a collection of Logix components into the specified L5X content, applying necessary modifications,
    /// resolving conflicts, and adding them to the content if they do not already exist.
    /// </summary>
    /// <param name="imports">The collection of Logix components to be imported into the L5X content.</param>
    /// <param name="content">The target L5X content where the Logix components will be processed and added.</param>
    private void ImportDependencies(ICollection<LogixComponent> imports, L5X content)
    {
        foreach (var import in imports)
        {
            if (IsDiscardable(import)) continue;
            ApplyModification(import);

            if (content.TryGet(import.Scope, out var match) && match is LogixComponent current)
                HandleConflict(current, import);
            else
                content.Add(import);
        }
    }

    /// <summary>
    /// Determines whether the specified component should be discarded based on the applied import rules.
    /// </summary>
    private bool IsDiscardable(LogixComponent import)
    {
        return config.Operations.Any(o => o is DiscardOperation && o.ApplyTo(import));
    }

    /// <summary>
    /// Applies update rules to the specified Logix component.
    /// </summary>
    private void ApplyModification(LogixComponent import)
    {
        var operations = config.Operations.Where(o => o is ModifyOperation).ToArray();

        foreach (var operation in operations)
        {
            operation.ApplyTo(import);
        }
    }

    /// <summary>
    /// Applies the post-import configuration operations to the target component.
    /// </summary>
    private void ApplyConfiguration(LogixComponent import)
    {
        var operations = config.Operations.Where(o => o is ConfigureOperation).ToArray();

        foreach (var operation in operations)
        {
            operation.ApplyTo(import);
        }
    }

    /// <summary>
    /// Handles a conflict between two Logix components during the import process.
    /// </summary>
    private void HandleConflict(LogixComponent current, LogixComponent import)
    {
        var operations = config.Operations.Where(o => o is OverwriteOperation && o.ApplyTo(import)).ToArray();

        //The "Target" component will be overwritten if not otherwise specified.
        //Any other type will default to use existing unless it matches a configured overwrite operation predicate.
        if ((import.Use is not null && import.Use == Use.Target) || operations.Length > 0)
        {
            current.Replace(import);
        }
    }
}