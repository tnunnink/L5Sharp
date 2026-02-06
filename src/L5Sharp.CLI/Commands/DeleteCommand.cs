using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using L5Sharp.CLI.Common;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// A command that deletes Logix components from a project based on the specified reference type and optional filters.
/// Components can be filtered by a where expression and/or restricted to only unused (unreferenced) components.
/// </summary>
[Command("delete", Description = "Deletes Logix components from a project based on type and optional filters")]
public class DeleteCommand : LogixCommand
{
    /// <summary>
    /// The reference type of components to delete (e.g., tag, program, routine, datatype, module, etc.).
    /// </summary>
    [CommandParameter(0, Name = "path",
        Description = "The reference type of components to delete (e.g., tag, program, routine, datatype)")]
    public string Type { get; init; } = string.Empty;

    /// <summary>
    /// An optional filter expression to limit which components are deleted based on their properties.
    /// </summary>
    [CommandOption("where", Description = "A filter expression to limit which items are deleted.")]
    public string? Where { get; init; }

    /// <summary>
    /// When true, restricts deletion to only components that have no references (are not being used elsewhere in the project).
    /// </summary>
    [CommandOption("unused", Description = "When true, only delete components that are not being used.")]
    public bool Unused { get; init; }

    /// <inheritdoc />
    public override async ValueTask ExecuteAsync(IConsole console)
    {
        if (!ReferenceType.TryParse(Type, out var type))
        {
            throw new CommandException(
                $"Invalid type '{Type}'. Valid types are: {string.Join(", ", LogixEnum.Names<ReferenceType>())}",
                ExitCodes.InvalidType
            );
        }

        try
        {
            var project = await ReadProject(console);

            project.Query(type)
                .FilterByExpression(Where)
                .FilterBySwitch(e => e is ILogixComponent c && !c.References().Any(), Unused)
                .ToList()
                .ForEach(e => project.Remove(e.Reference));

            WriteProject(console, project);
        }
        catch (Exception e)
        {
            throw new CommandException($"Command Failed: {e.Message}", ExitCodes.InternalError);
        }
    }
}