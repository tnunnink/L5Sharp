using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using JetBrains.Annotations;
using L5Sharp.CLI.Abstractions;
using L5Sharp.CLI.Common;
using L5Sharp.Core;
using Microsoft.Extensions.DependencyInjection;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// Represents a command that queries and lists Logix components from an L5X project file.
/// Supports filtering, ordering, selecting specific properties, limiting results, and formatting output.
/// </summary>
[PublicAPI]
[Command("list", Description = "Lists Logix components from an L5X with optional filtering and formatting.")]
public class ListCommand(IServiceProvider provider) : LogixCommand
{
    /// <summary>
    /// Gets or sets the type filter for the command.
    /// </summary>
    [CommandParameter(0, Name = "type", Description = "The type to query (e.g., tag, program, aoi, module, rung).")]
    public string Type { get; init; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    [CommandOption("match", 'm', Description = "")]
    public string? Match { get; init; }

    /// <summary>
    /// Gets or sets the expression used to filter items in the command. This property allows users
    /// to define a filtering condition that is applied to the query, enabling more precise control
    /// over the items to be included in the results.
    /// </summary>
    [CommandOption("where", 'w', Description = "Filters items based on the expression.")]
    public string? Where { get; init; }

    /// <summary>
    /// Gets or sets the expression used to order items in the command. This property allows users
    /// to define a sorting condition that is applied to the query results, enabling control
    /// over the ordering of the items displayed.
    /// </summary>
    [CommandOption("order", 'o', Description = "Orders items based on the specified property expression.")]
    public string? Order { get; init; }

    /// <summary>
    /// Gets or sets the property names to include in the command output. This property allows
    /// specifying a subset of attributes or fields of the processed objects that should be displayed
    /// in the output, based on the command execution context.
    /// </summary>
    [CommandOption("select", Description = "The property names to select for the output.")]
    public string[] Select { get; init; } = [];

    /// <summary>
    /// Gets or sets the maximum number of records to return in the command output. This property
    /// allows users to limit the returned results to a specific count, providing better control
    /// over the amount of data displayed or processed.
    /// </summary>
    [CommandOption("take", Description = "Specifies the number of records to return.")]
    public int Take { get; init; } = 100;

    /// <summary>
    /// Gets or sets the command output format. This property specifies how the output
    /// of the command is formatted and supports options such as table, JSON, CSV, or XML.
    /// </summary>
    [CommandOption("format", 'f', Description = "Command output format [table, xml, json, csv].")]
    public Format Format { get; init; } = Format.Table;

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

            var elements = project.Query(type)
                //.FilterByPattern(x => x.Name, Name)
                //.FilterByPattern(Match)
                .FilterByExpression(Where)
                .Take(Take);
            //todo order by;

            var renderer = provider.GetRequiredKeyedService<IElementRenderer>(Format);
            var output = renderer.Render(elements, Select);
            console.Ansi().Write(output);
        }
        catch (Exception e)
        {
            throw new CommandException($"Command Failed: {e.Message}", ExitCodes.InternalError);
        }
    }
}