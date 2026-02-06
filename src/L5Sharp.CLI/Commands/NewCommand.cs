using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using L5Sharp.CLI.Common;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// Creates a new L5X project with the specified name, processor, and optional revision.
/// </summary>
/// <remarks>
/// This command generates a new L5X project file by looking up the processor module information
/// from the catalog and emits the resulting XML to the console output.
/// </remarks>
[Command("new", Description = "Creates a new L5X project with the specified processor.")]
public class NewCommand : LogixCommand
{
    /// <summary>
    /// Gets or sets the name of the project to create.
    /// </summary>
    [CommandParameter(0, Name = "name", Description = "The name of the project to create.")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the processor catalog number for the project.
    /// </summary>
    /// <remarks>
    /// The catalog number is used to look up the processor module information from the <see cref="RockwellCatalogDatabase"/>.
    /// </remarks>
    [CommandParameter(1, Name = "processor", Description = "The catalog number of the processor (e.g., 1756-L83E).")]
    public string Processor { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional revision number for the processor.
    /// </summary>
    /// <remarks>
    /// If not specified, the latest revision from the catalog will be used.
    /// </remarks>
    [CommandOption("revision", 'r', Description = "The processor revision number (optional).")]
    public ushort? Revision { get; init; }


    /// <inheritdoc />
    public override ValueTask ExecuteAsync(IConsole console)
    {
        try
        {
            //Only evaluate revision when 
            var revision = Revision.HasValue ? new Revision(Revision.Value) : null;

            //The new method will internally handle module info lookups.
            var project = L5X.New(Name, Processor, revision);

            //Emit to the console so the caller can chain to the next command.
            WriteProject(console, project);
        }
        catch (Exception e)
        {
            throw new CommandException($"Command Failed: {e.Message}", ExitCodes.InternalError, false, e);
        }

        return ValueTask.CompletedTask;
    }
}