using System.Globalization;
using CsvHelper;
using L5Sharp.CLI.Abstractions;
using L5Sharp.Core;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Rendering;
using Text = Spectre.Console.Text;

namespace L5Sharp.CLI.Rendering;

/// <summary>
/// A renderer that converts collections of <see cref="ILogixElement"/> instances into
/// CSV format for display or output purposes. This class implements the <see cref="IElementRenderer"/>
/// interface and provides functionality to customize the rendered output by selecting specific fields.
/// </summary>
public class CsvRenderer(IServiceProvider provider) : IElementRenderer
{
    /// <inheritdoc />
    public IRenderable Render<TElement>(TElement element, params string[] fields) where TElement : ILogixElement
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        var schema = provider.GetRequiredService<IElementSchema<TElement>>();
        var record = schema.Map(element, fields);

        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteDynamicHeader(record);
        csv.NextRecord();
        csv.WriteRecord(record);

        return new Text(writer.ToString());
    }

    /// <inheritdoc />
    public IRenderable Render<TElement>(IEnumerable<TElement> elements, params string[] fields)
        where TElement : ILogixElement
    {
        if (elements is null) throw new ArgumentNullException(nameof(elements));

        var schema = provider.GetRequiredService<IElementSchema<TElement>>();
        var records = schema.Map(elements, fields).ToArray();

        // Abort early if there are no records to write.
        if (records.Length == 0)
            return new Text(string.Empty); //todo should this actually throw so we can write error

        // Use the CsvWriter to build the table from the projected records.
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        // Use the first record to build the column header of the CSV.
        //var columns = records[0].Select(r => r.Key).ToList();
        //columns.ForEach(c => csv.WriteField(c));

        csv.WriteDynamicHeader(records[0]);
        csv.NextRecord();

        foreach (var record in records)
        {
            csv.WriteRecord(record);
            csv.NextRecord();
        }

        return new Text(writer.ToString());
    }
}