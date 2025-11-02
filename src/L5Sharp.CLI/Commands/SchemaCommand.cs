using System.Reflection;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using L5Sharp.CLI.Internal;
using Spectre.Console;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// Represents an abstract command used to display the schema of a specified Logix component supported by the application.
/// </summary>
/// <typeparam name="TRecord">The type of the Logix component for which the schema will be displayed. </typeparam>
/// <remarks>
/// The class is intended to be inherited to create specific schema commands for different Logix component types.
/// It uses reflection to extract the public properties of the specified type and displays them in a tabular format
/// using ANSI-compatible console output.
/// </remarks>
[Command("schema", Description = "Displays the schema of a specified logix component the application supports.")]
public abstract class SchemaCommand<TRecord> : ICommand
{
    /// <inheritdoc />
    public ValueTask ExecuteAsync(IConsole console)
    {
        var type = typeof(TRecord);
        var table = new Table().AddColumns("Property", "Type");

        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToList()
            .ForEach(p => table.AddRow(
                new Text(p.Name, Style.Parse("darkturquoise")),
                new Text(p.PropertyType.GetTypeDisplayName(), Style.Parse("mediumpurple4")))
            );

        console.Ansi().Write(table);

        return ValueTask.CompletedTask;
    }
}