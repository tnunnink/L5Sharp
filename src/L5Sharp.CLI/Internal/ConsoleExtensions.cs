using System.Collections;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Xml;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using CsvHelper;
using L5Sharp.Core;
using Spectre.Console;
using Spectre.Console.Json;

namespace L5Sharp.CLI.Internal;

public static class ConsoleExtensions
{
    /// <summary>
    /// The cached typed property mapping and getter functions. For performance, we will cache the types we encounter
    /// and their property map, using function expression instead of reflection each time.
    /// </summary>
    private static readonly Lazy<Dictionary<Type, Dictionary<string, Func<object, object?>>>> Mappings = new();

    /// <summary>
    /// Creates an instance of <see cref="IAnsiConsole"/> using the specified console
    /// output, applying settings for ANSI support and color system detection.
    /// </summary>
    /// <param name="console">
    /// The console to use for output. This represents the underlying console interface
    /// where ANSI-rendered output will be written.
    /// </param>
    /// <returns>An instance of <see cref="IAnsiConsole"/> configured with the specified settings.</returns>
    public static IAnsiConsole Ansi(this IConsole console)
    {
        return AnsiConsole.Create(new AnsiConsoleSettings
        {
            Ansi = AnsiSupport.Detect,
            ColorSystem = ColorSystemSupport.Detect,
            Out = new AnsiConsoleOutput(console.Output)
        });
    }

    /// <summary>
    /// Reads and parses L5X input from the console's redirected input stream.
    /// </summary>
    /// <param name="console">The console instance used for input.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> for propagating notification that
    /// operations should be canceled.</param>
    /// <returns>An instance of <see cref="L5X"/> representing the parsed L5X input content.</returns>
    /// <exception cref="CommandException">Thrown when the input is not redirected, empty, or contains an invalidL5X format.</exception>
    public static async Task<L5X> ReadXmlAsync(this IConsole console, CancellationToken token = default)
    {
        if (!console.IsInputRedirected)
            throw new CommandException("No input provided. Pipe L5X content to this command.", ExitCodes.NoInput);

        //If no token is provided, register one with the console.
        token = token == CancellationToken.None ? console.RegisterCancellationHandler() : token;

        try
        {
            var xml = await console.Input.ReadToEndAsync(token);

            if (string.IsNullOrWhiteSpace(xml))
                throw new CommandException("Empty input provided. Expected L5X content.", ExitCodes.NoInput);

            return L5X.Parse(xml);
        }
        catch (XmlException e)
        {
            throw new CommandException($"Invalid L5X format: {e.Message}", ExitCodes.FormatError);
        }
        catch (Exception e)
        {
            throw new CommandException($"Failed to read L5X input: {e.Message}", ExitCodes.InternalError);
        }
    }

    /// <summary>
    /// Writes the specified <see cref="L5X"/> project to the console output in its serialized format.
    /// </summary>
    /// <param name="console">The console instance to which the serialized L5X project will be written.</param>
    /// <param name="element">The <see cref="L5X"/> project to serialize and write to the console.</param>
    public static void WriteXml(this IConsole console, LogixElement element)
    {
        console.Output.Write(element.Serialize().ToString());
    }

    /// <summary>
    /// Writes an object to the console in the specified format with optional fields.
    /// </summary>
    /// <param name="console">The target console output interface.</param>
    /// <param name="item">The object to write.</param>
    /// <param name="format">Output format (Table, Json, Csv). Defaults to Table for console and Json for redirected output.</param>
    /// <param name="fields">Optional fields to include in output.</param>
    /// <exception cref="ArgumentNullException">If item is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If format is not supported.</exception>
    public static void Write(this IConsole console, object item, Format? format, params string[] fields)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        //If not format is specified use JSON if the output is piped or table for display otherwise.
        format ??= console.IsOutputRedirected ? Format.Json : Format.Table;

        switch (format)
        {
            case Format.Table:
                console.Ansi().Write(BuildTable(item, fields));
                break;
            case Format.Json:
                var options = new JsonSerializerOptions { WriteIndented = !console.IsOutputRedirected };
                console.Ansi().Write(BuildJson(item, fields, options));
                break;
            case Format.Csv:
                console.Ansi().Write(BuildCsv([item], fields));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }
    }

    /// <summary>
    /// Writes a collection of items to the console using the specified format and optional fields.
    /// </summary>
    /// <param name="console">The console instance where the items will be written. This represents the output destination.</param>
    /// <param name="items">The collection of items to write to the console. This must not be null.</param>
    /// <param name="format">The output format to use for writing the items (e.g., Table, JSON, CSV).</param>
    /// <param name="fields">An optional array of field names to include in the output.
    /// This allows for customized output based on the specified fields.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="items"/> parameter is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified <paramref name="format"/> is invalid or unsupported.</exception>
    public static void Write(this IConsole console, object[] items, Format? format, params string[] fields)
    {
        if (items is null)
            throw new ArgumentNullException(nameof(items));

        //If not format is specified use JSON if the output is piped or table for display otherwise.
        format ??= console.IsOutputRedirected ? Format.Json : Format.Table;

        switch (format)
        {
            case Format.Table:
                console.Ansi().Write(BuildTable(items, fields));
                break;
            case Format.Json:
                var options = new JsonSerializerOptions { WriteIndented = !console.IsOutputRedirected };
                console.Ansi().Write(BuildJson(items, fields, options));
                break;
            case Format.Csv:
                console.Ansi().Write(BuildCsv(items, fields));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }
    }

    /// <summary>
    /// Builds and returns a table representation of the specified object, using the provided fields.
    /// </summary>
    /// <param name="item">The object whose properties will be included in the table. Must not be null.</param>
    /// <param name="fields">An array of property names to include in the table. Only the specified properties will be displayed.</param>
    /// <returns>A <see cref="Table"/> instance with the specified properties and their corresponding values.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="item"/> is null.</exception>
    private static Table BuildTable(object item, string[] fields)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        var map = GetPropertyMap(item.GetType());
        var columns = fields.Length > 0 ? fields : map.Keys.ToArray();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("Property")
            .AddColumn("Value");

        foreach (var column in columns)
        {
            if (!map.TryGetValue(column, out var getter)) continue;
            var value = FormatValueForTable(getter(item));
            table.AddRow(new Text(column), new Text(value));
        }

        return table;
    }

    /// <summary>
    /// Constructs a table representation of the specified items, mapping designated fields to table columns.
    /// </summary>
    /// <param name="items">The collection of items to be displayed in the table.</param>
    /// <param name="fields">The list of field names to be included as columns in the table.</param>
    /// <returns>A <see cref="Spectre.Console.Table"/> representing the specified items and fields.</returns>
    private static Table BuildTable(object[] items, string[] fields)
    {
        if (items is null) throw new ArgumentNullException(nameof(items));
        if (items.Length == 0) return new Table();

        var map = GetPropertyMap(items.GetType());
        var columns = fields.Length > 0 ? fields : map.Keys.ToArray();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumns(fields);

        foreach (var item in items)
        {
            var row = columns.Select(c =>
            {
                if (!map.TryGetValue(c, out var getter)) return new Text(string.Empty);
                var value = FormatValueForTable(getter(item));
                return new Text(value);
            });

            table.AddRow(row);
        }

        return table;
    }

    /// <summary>
    /// Builds a JSON representation of the specified object using the provided fields and serialization options.
    /// </summary>
    /// <param name="item">The object to serialize into JSON.</param>
    /// <param name="fields">A collection of property names to include in the JSON output.</param>
    /// <param name="options">The JSON serializer options to configure formatting and behavior during serialization.</param>
    /// <returns>A <see cref="JsonText"/> containing the serialized JSON representation of the object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="item"/> is null.</exception>
    private static JsonText BuildJson(object item, string[] fields, JsonSerializerOptions options)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));

        var map = GetPropertyMap(item.GetType());
        var properties = fields.Length > 0 ? fields.ToList() : map.Keys.ToList();

        var data = new Dictionary<string, object?>();

        foreach (var property in properties)
        {
            if (!map.TryGetValue(property, out var getter)) continue;
            data[property] = getter(item);
        }

        return new JsonText(JsonSerializer.Serialize(data, options));
    }

    /// <summary>
    /// Builds a JSON representation of the provided items, including only the specified fields
    /// and using the provided serializer options for customization of the output.
    /// </summary>
    /// <param name="items">The collection of items to be serialized to JSON. This cannot be null.</param>
    /// <param name="fields">The fields to include in the JSON output. Only these fields will be serialized for each item.</param>
    /// <param name="options">The JSON serializer options to configure the serialization behavior.</param>
    /// <returns>A <see cref="JsonText"/> object containing the serialized JSON representation of the filtered fields for the provided collection.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="items"/> parameter is null.</exception>
    private static JsonText BuildJson(object[] items, string[] fields, JsonSerializerOptions options)
    {
        if (items is null) throw new ArgumentNullException(nameof(items));
        if (items.Length == 0) return new JsonText(string.Empty);

        var map = GetPropertyMap(items[0].GetType());
        var properties = fields.Length > 0 ? fields.ToList() : map.Keys.ToList();

        var data = items.Select(x =>
        {
            var kvp = new Dictionary<string, object?>();

            //todo should we go with this approach instead?
            /*properties.ForEach(p =>
            {
                if (!map.TryGetValue(p, out var getter))
                    throw new ArgumentException("");
                
                kvp[p] = getter(x); 
            });*/
            foreach (var property in properties)
            {
                if (!map.TryGetValue(property, out var getter)) continue;
                kvp[property] = getter(x);
            }

            return kvp;
        });

        return new JsonText(JsonSerializer.Serialize(data, options));
    }

    /// <summary>
    /// Generates a CSV-formatted string from a collection of objects, using specified object fields as columns.
    /// </summary>
    /// <param name="items">The collection of objects to be serialized into CSV format.</param>
    /// <param name="fields">An array of field names to be used as headers and data columns in the CSV output.
    /// Each field corresponds to a property of the object.</param>
    /// <returns>A string containing the CSV representation of the input collection with the specified fields as columns.</returns>
    private static string BuildCsv(object[] items, string[] fields)
    {
        if (items is null) throw new ArgumentNullException(nameof(items));
        if (items.Length == 0) return string.Empty;

        var map = GetPropertyMap(items[0].GetType());
        var columns = fields.Length > 0 ? fields.ToList() : map.Keys.ToList();

        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        columns.ForEach(c => csv.WriteField(c));
        csv.NextRecord();

        foreach (var item in items)
        {
            columns.ForEach(c =>
            {
                var value = map.TryGetValue(c, out var getter)
                    ? getter(item)?.ToString() ?? string.Empty
                    : string.Empty;

                csv.WriteField(value);
            });

            csv.NextRecord();
        }

        return writer.ToString();
    }

    /// <summary>
    /// Creates a mapping dictionary that associates property names of a given type
    /// with delegates capable of retrieving the corresponding property values from an object.
    /// </summary>
    /// <param name="type">
    /// The type whose properties will be mapped to their respective value retrieval functions.
    /// </param>
    /// <returns>
    /// A dictionary where the keys are property names as strings, and the values are
    /// functions that retrieve the associated property values from instances of the specified type.
    /// </returns>
    private static Dictionary<string, Func<object, object?>> GetPropertyMap(Type type)
    {
        if (Mappings.Value.TryGetValue(type, out var existing))
        {
            return existing;
        }

        var propertyMap = type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p =>
            {
                var parameter = Expression.Parameter(typeof(object), "x");
                var cast = Expression.Convert(parameter, type);
                var property = Expression.Property(cast, p.Name);
                var convert = Expression.Convert(property, typeof(object));
                var function = Expression.Lambda<Func<object, object?>>(convert, parameter).Compile();
                return new KeyValuePair<string, Func<object, object?>>(p.Name, function);
            })
            .ToDictionary(k => k.Key, v => v.Value, StringComparer.OrdinalIgnoreCase);

        Mappings.Value[type] = propertyMap;
        return propertyMap;
    }

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