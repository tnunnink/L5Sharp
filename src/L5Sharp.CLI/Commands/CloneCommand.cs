using CliFx.Attributes;
using CliFx.Exceptions;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// Represents a command to create a duplicate of a specific Logix component within an L5X project.
/// </summary>
/// <typeparam name="TComponent">
/// The type of Logix component to be cloned. It must be a class derived from
/// <see cref="LogixComponent{TComponent}"/>.
/// </typeparam>
/// <remarks>
/// This command takes a source component, duplicates it, and assigns the duplicate a new name.
/// If the target component already exists in the project or the source component does not exist,
/// the command will throw a <see cref="CommandException"/>.
/// The mutation logic is applied through the override of the <see cref="Mutate"/> method
/// from the base class <see cref="MutateCommand"/>.
/// </remarks>
public abstract class CloneCommand<TComponent> : MutateCommand where TComponent : LogixComponent<TComponent>
{
    [CommandParameter(0, Name = "source", Description = "")]
    public string Source { get; init; } = string.Empty;

    [CommandParameter(1, Name = "target", Description = "")]
    public string Target { get; init; } = string.Empty;

    protected override void Mutate(L5X project)
    {
        if (project.Contains(Reference.To<TComponent>(Target)))
            throw new CommandException(
                $"Project already contains {typeof(TComponent).Name}: '{Target}'",
                ExitCodes.ComponentNotFound
            );

        if (!project.TryGet<TComponent>(Source, out var source))
            throw new CommandException("");

        //This will create/configure/add to the current project.
        source.Duplicate(c => c.Name = Target);
    }
}