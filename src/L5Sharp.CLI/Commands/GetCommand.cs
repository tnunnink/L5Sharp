using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using JetBrains.Annotations;
using L5Sharp.CLI.Abstractions;
using L5Sharp.CLI.Common;
using L5Sharp.CLI.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// Represents the command to retrieve a specific reference from a Logix project and display it in the desired format.
/// </summary>
/// <remarks>
/// The <c>GetCommand</c> allows users to extract data from a Logix project by specifying a reference name.
/// The output format and property selection can be customized using additional options.
/// </remarks>
/// <example>
/// This command is primarily used in CLI tools for interacting with Logix projects, facilitating easy data extraction and visualization.
/// </example>
[PublicAPI]
[Command("get", Description = "")]
public class GetCommand(IServiceProvider provider) : LogixCommand
{
    /// <summary>
    /// Gets or sets the hierarchical path to the specific reference within the Logix project.
    /// This property specifies the unique identifier or navigation path to locate the target object
    /// that the command should process or retrieve.
    /// </summary>
    [CommandParameter(0, Name = "path", Description = "")]
    public string Path { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the property names to include in the command output. This property allows
    /// specifying a subset of attributes or fields of the processed objects that should be displayed
    /// in the output, based on the command execution context.
    /// </summary>
    [CommandOption("select", Description = "The property names to select for the output.")]
    public string[] Select { get; init; } = [];

    /// <summary>
    /// Gets or sets the command output format. This property specifies how the output
    /// of the command is formatted and supports options such as table, JSON, CSV, or XML.
    /// </summary>
    [CommandOption("format", 'f', Description = "Command output format [table, xml, json, csv].")]
    public Format Format { get; init; } = Format.Table;

    /// <inheritdoc />
    public override async ValueTask ExecuteAsync(IConsole console)
    {
        try
        {
            var project = await ReadProject(console);

            if (!project.TryGet(Path, out var entity))
                throw new CommandException($"No item found at path: {Path}", ExitCodes.NotFound);

            var renderer = provider.GetRequiredKeyedService<IElementRenderer>(Format);
            var output = renderer.Render(entity, Select);
            console.Write(output);
        }
        catch (Exception e)
        {
            throw new CommandException($"Command Failed: {e.Message}", ExitCodes.InternalError);
        }
    }
}