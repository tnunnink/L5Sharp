using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// Represents a base command used to perform a mutation operation on an L5X project.
/// </summary>
/// <remarks>
/// This class provides a template for commands that read an L5X project, apply specific
/// modifications or additions, and then writes the modified project back.
/// Derived classes must implement the <see cref="Mutate"/> method to define the specific mutation logic.
/// </remarks>
public abstract class MutateCommand : ICommand
{
    /// <inheritdoc />
    public async ValueTask ExecuteAsync(IConsole console)
    {
        try
        {
            var project = await console.ReadXmlAsync();
            Mutate(project);
            console.WriteXml(project);
        }
        catch (Exception e)
        {
            throw new CommandException($"Command Failed: {e.Message}", ExitCodes.InternalError, false, e);
        }
    }

    /// <summary>
    /// Mutates the provided L5X project by making specific modifications or additions
    /// as defined in the implementation.
    /// </summary>
    /// <param name="project">The L5X project to be mutated.</param>
    protected abstract void Mutate(L5X project);
}