using System.Collections;
using L5Sharp.CLI.Abstractions;
using L5Sharp.Core;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace L5Sharp.CLI.Rendering;

/// <summary>
/// Provides a mechanism for rendering a table-based output from instances of <see cref="ILogixElement"/>.
/// This renderer is capable of formatting and displaying both individual elements and collections of elements
/// by mapping their properties to tabular structures for visualization and analysis.
/// </summary>
public class TableRenderer(IServiceProvider provider) : IElementRenderer
{
    /// <inheritdoc />
    public IRenderable Render<TElement>(TElement element, params string[] fields) where TElement : ILogixElement
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        // Resolve the projector for the specified element type and use it to generate the output records.
        var schema = provider.GetRequiredService<IElementSchema<TElement>>();
        var record = schema.Map(element, fields);

        // Build the table using the projected record.
        var table = new Table().Border(TableBorder.Rounded)
            .AddColumn("Property")
            .AddColumn("Value");

        foreach (var (key, value) in record)
        {
            var property = new Text(key);
            var formatted = new Text(FormatValueForTable(value));
            table.AddRow(property, formatted);
        }

        return table;
    }

    /// <inheritdoc />
    public IRenderable Render<TElement>(IEnumerable<TElement> elements, params string[] fields)
        where TElement : ILogixElement
    {
        if (elements is null) throw new ArgumentNullException(nameof(elements));

        // Resolve the projector for the specified element type and use it to generate the output records.
        var schema = provider.GetRequiredService<IElementSchema<TElement>>();
        var records = schema.Map(elements, fields).ToList();

        if (records.Count == 0)
            throw new InvalidOperationException("");

        // Build the table using the projected records. Use the first record to add the column headers.
        var columns = records[0].Select(x => new TableColumn(x.Key)).ToArray();
        var table = new Table().Border(TableBorder.Rounded).AddColumns(columns);

        records.ForEach(r =>
        {
            var row = r.Select(x => new Text(FormatValueForTable(x.Value)));
            table.AddRow(row);
        });

        return table;
    }

    /// <summary>
    /// Formats the provided value for display in a console table.
    /// </summary>
    /// <param name="value">The value to format. It can be null, a string, an enumerable, or any other object.</param>
    /// <returns>
    /// A string representation of the value. Returns an empty string if the value is null,
    /// the value itself if it is a string, the count of elements if it is an <see cref="IEnumerable"/>,
    /// or the string representation of the object.
    /// </returns>
    private static string FormatValueForTable(object? value)
    {
        return value switch
        {
            null => string.Empty,
            string s => s,
            IEnumerable enumerable => $"{enumerable.Cast<object>().Count()}",
            _ => value.ToString() ?? string.Empty
        };
    }
}