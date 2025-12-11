using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents the configuration for an import operation within an L5X project.
/// </summary>
internal class Import
{
    /// <summary>
    /// The source L5X content which we are importing from.
    /// </summary>
    public L5X? Source { get; set; }

    /// <summary>
    /// The scope of the target component that we are importing.
    /// </summary>
    public Reference? Target { get; set; }

    /// <summary>
    /// The collection of import operations to perform when running the import process.
    /// </summary>
    public List<IImportOperation> Operations { get; } = [];

    /// <summary>
    /// Executes the current import configuration on the provided L5X document.
    /// </summary>
    public void Execute(L5X content)
    {
        if (content is null)
            throw new ArgumentNullException(nameof(content));

        if (Source is null)
            throw new InvalidOperationException("Cannot run import without a target source file.");

        //Either use the configured target or generate the target scope from the source info.
        var targetScope = Target ?? GenerateTargetScope(Source);

        //Ensure a valid component reference based on the configured target scope.
        var target = Source.Get(targetScope);
        if (target is not ILogixComponent component)
            throw new InvalidOperationException($"The target object '{Target}' is not a valid component object.");

        //1. Import the target component.
        ImportTarget(component, content);

        //2. Import the target's dependencies.
        var dependencies = component.Dependencies().Cast<ILogixComponent>().ToArray();
        ImportDependencies(dependencies, content);
    }

    /// <summary>
    /// Imports a single Logix component into the specified L5X content, applying modifications, handling conflicts,
    /// and adding it to the content if it does not already exist.
    /// </summary>
    /// <param name="import">The Logix component to be imported into the L5X content.</param>
    /// <param name="content">The target L5X content where the Logix component will be processed and added.</param>
    private void ImportTarget(ILogixComponent import, L5X content)
    {
        ApplyModification(import);

        if (content.TryGet(import.Reference, out var match) && match is ILogixComponent current)
            HandleConflict(current, import);
        else
            content.Add(import);

        //Get the new added instance to configure.
        //This is because the old will still contain a reference to the source L5X.
        var component = (ILogixComponent)content.Get(import.Reference);
        ApplyConfiguration(component);
    }

    /// <summary>
    /// Imports a collection of Logix components into the specified L5X content, applying necessary modifications,
    /// resolving conflicts, and adding them to the content if they do not already exist.
    /// </summary>
    /// <param name="imports">The collection of Logix components to be imported into the L5X content.</param>
    /// <param name="content">The target L5X content where the Logix components will be processed and added.</param>
    private void ImportDependencies(ICollection<ILogixComponent> imports, L5X content)
    {
        foreach (var import in imports)
        {
            if (IsDiscardable(import)) continue;
            ApplyModification(import);

            if (content.TryGet(import.Reference, out var match) && match is ILogixComponent current)
                HandleConflict(current, import);
            else
                content.Add(import);
        }
    }

    /// <summary>
    /// Determines whether the specified component should be discarded based on the applied import rules.
    /// </summary>
    private bool IsDiscardable(ILogixComponent import)
    {
        return Operations.Any(o => o is DiscardOperation && o.ApplyTo(import));
    }

    /// <summary>
    /// Applies update rules to the specified Logix component.
    /// </summary>
    private void ApplyModification(ILogixComponent import)
    {
        var operations = Operations.Where(o => o is ModifyOperation).ToArray();

        foreach (var operation in operations)
        {
            operation.ApplyTo(import);
        }
    }

    /// <summary>
    /// Applies the post-import configuration operations to the target component.
    /// </summary>
    private void ApplyConfiguration(ILogixComponent import)
    {
        var operations = Operations.Where(o => o is ConfigureOperation).ToArray();

        foreach (var operation in operations)
        {
            operation.ApplyTo(import);
        }
    }

    /// <summary>
    /// Handles a conflict between two Logix components during the import process.
    /// </summary>
    private void HandleConflict(ILogixComponent current, ILogixComponent import)
    {
        var operations = Operations.Where(o => o is OverwriteOperation && o.ApplyTo(import)).ToArray();

        //The "Target" component will be overwritten if not otherwise specified.
        //Any other type will default to use existing unless it matches a configured overwrite operation predicate.
        if ((import.Use is not null && import.Use == Use.Target) || operations.Length > 0)
        {
            current.Serialize().ReplaceWith(import.Serialize());
        }
    }

    /// <summary>
    /// Generates a target scope for the import process based on the provided source information.
    /// </summary>
    /// <param name="source">The source L5X containing target type and name information.</param>
    /// <returns>A <see cref="Reference"/> object representing the target scope for the import process.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the target type or name information is missing in the source, or if the scope could not be determined.
    /// </exception>
    private static Reference GenerateTargetScope(L5X source)
    {
        if (source.Content.TargetType is null || source.Content.TargetName is null)
            throw new InvalidOperationException(
                "No target type/name information is configured in the specified source.");

        if (source.Content.TargetType == nameof(Routine))
        {
            var program = source.Programs.FirstOrDefault(p => Use.Context.Equals(p.Use));

            if (program is null)
                throw new InvalidOperationException("Could not determine scope for import process.");

            return Reference.To<Routine>(source.Content.TargetName, program.Name);
        }

        var type = ReferenceType.Parse(source.Content.TargetType);
        return Reference.Build(b => b.Type(type).Named(source.Content.TargetName));
    }
}