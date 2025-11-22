using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;
using L5Sharp.Model;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// Represents a base command to retrieve information about a specific Logix component based on its name
/// and output the result in a specified format.
/// </summary>
/// <typeparam name="TComponent">The type of the Logix component to retrieve, inheriting from <see cref="ILogixComponent"/>.</typeparam>
public abstract class GetCommand<TComponent> : ICommand where TComponent : LogixComponent<TComponent>
{
    /// <summary>
    /// Gets or initializes the name of the Logix component to retrieve. This property is used as a key to
    /// identify the specific component within the project to be processed by the command.
    /// </summary>
    [CommandParameter(0, Name = "name", Description = "")]
    public string Name { get; init; } = string.Empty;

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
    public Format? Format { get; init; } = Internal.Format.Table;

    /// <inheritdoc />
    public async ValueTask ExecuteAsync(IConsole console)
    {
        var project = await console.ReadXmlAsync();
        var result = project.Get<TComponent>(Name).ToInfo();
        console.Write(result, Format, Select);
    }
}