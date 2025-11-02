using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// Represents a base command for listing components within a Logix project.
/// </summary>
/// <typeparam name="TObject">The type of the Logix component to query.</typeparam>
/// <typeparam name="TRecord">The type of the record that represents the output of the listing operation.</typeparam>
public abstract class ListCommand<TObject, TRecord> : ICommand where TObject : LogixObject
{
    /// <summary>
    /// Gets or sets the name filter for the command. This property allows users to specify a pattern
    /// for filtering records by their names. The filter supports wildcards, such as '*' and '?',
    /// to match multiple or single characters, respectively.
    /// </summary>
    [CommandOption("name", 'n', Description = "Filters records by name (supports wildcards '*' and '?').")]
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the expression used to filter items in the command. This property allows users
    /// to define a filtering condition that is applied to the query, enabling more precise control
    /// over the items to be included in the results.
    /// </summary>
    [CommandOption("where", 'w', Description = "Filters items based on the expression.")]
    public string? Where { get; init; }
    
    /// <summary>
    /// Gets or sets the expression used to filter items in the command. This property allows users
    /// to define a filtering condition that is applied to the query, enabling more precise control
    /// over the items to be included in the results.
    /// </summary>
    [CommandOption("order-by", 'o', Description = "")]
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
    public Format? Format { get; init; } = Internal.Format.Table;

    /// <inheritdoc />
    public async ValueTask ExecuteAsync(IConsole console)
    {
        try
        {
            var project = await console.ReadXmlAsync();
            var results = ExecuteQuery(project);
            console.Write(results, Format, Select);
        }
        catch (Exception e)
        {
            throw new CommandException($"Command Failed: {e.Message}", ExitCodes.InternalError);
        }
    }

    /// <summary>
    /// Executes a query against the specified Logix project and retrieves a collection of records
    /// matching the defined criteria.
    /// </summary>
    /// <param name="project">The target Logix project to query.</param>
    /// <returns>
    /// A collection of records of type <typeparamref name="TRecord"/> that satisfy the query conditions.
    /// </returns>
    protected abstract IEnumerable<TRecord> ExecuteQuery(L5X project);
}