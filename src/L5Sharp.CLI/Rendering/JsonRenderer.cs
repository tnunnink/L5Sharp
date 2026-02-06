using System.Text.Json;
using System.Text.Json.Serialization;
using L5Sharp.CLI.Abstractions;
using L5Sharp.Core;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Json;
using Spectre.Console.Rendering;

namespace L5Sharp.CLI.Rendering;

/// <summary>
/// A renderer that converts collections of <see cref="ILogixElement"/> instances into formatted JSON output
/// for display or processing.
/// </summary>
/// <remarks>
/// The <see cref="JsonRenderer"/> utilizes dependency injection to resolve a <see cref="IElementSchema{TElement}"/> for mapping
/// elements to dynamic objects. The resulting data structure is serialized to JSON using customizable serializer options
/// with support for indented formatting and cycle handling.
/// </remarks>
public class JsonRenderer(IServiceProvider provider) : IElementRenderer
{
    /// <summary>
    /// The JSON serializer options used to configure the serialization behavior for the JSON output.
    /// This includes formatting settings such as whether the JSON should be indented for readability.
    /// </summary>
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };

    /// <inheritdoc />
    public IRenderable Render<TElement>(TElement element, params string[] fields) where TElement : ILogixElement
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        // Resolve the schema for the specified element type using the service provider.
        var schema = provider.GetRequiredService<IElementSchema<TElement>>();

        // Map the element to a dynamic expando object with the selected fields in the specified order.
        var data = schema.Map(element, fields);

        // Serialize the object and return the rendered JSON data.
        var json = JsonSerializer.Serialize(data, _options);
        return new JsonText(json);
    }

    /// <inheritdoc />
    public IRenderable Render<TElement>(IEnumerable<TElement> elements, params string[] fields)
        where TElement : ILogixElement
    {
        if (elements is null) throw new ArgumentNullException(nameof(elements));

        // Resolve the schema for the specified element type using the service provider.
        var schema = provider.GetRequiredService<IElementSchema<TElement>>();

        // Map the element to a dynamic expando object with the selected fields in the specified order.
        var data = schema.Map(elements, fields);

        // Serialize the object and return the rendered JSON data.
        var json = JsonSerializer.Serialize(data, _options);
        return new JsonText(json);
    }
}