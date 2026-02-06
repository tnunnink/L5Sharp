using System.Dynamic;
using L5Sharp.Core;

namespace L5Sharp.CLI.Abstractions;

/// <summary>
/// Provides an abstract base class for projecting instances of Logix elements into dynamic objects
/// with selectable fields. This class enables querying and extracting specific properties from elements
/// of type <typeparamref name="TElement"/> in a flexible manner, supporting both single element projection
/// and collection-based projection scenarios.
/// </summary>
/// <typeparam name="TElement">The type of Logix element to project. Must implement <see cref="ILogixElement"/> and have a parameterless constructor.</typeparam>
public abstract class ElementSchema<TElement> : IElementSchema<TElement> where TElement : ILogixElement, new()
{
    /// <inheritdoc />
    public abstract IReadOnlyList<Field<TElement>> Fields { get; }

    /// <summary>
    /// Creates a dynamic object by projecting the specified fields of the given element instance.
    /// </summary>
    /// <param name="element">The element instance from which field values will be extracted.</param>
    /// <param name="fields">An optional array of field names to include in the dynamic object. If no fields are provided, all available fields are included.</param>
    /// <returns>An <see cref="ExpandoObject"/> representing the projected fields and their values.</returns>
    /// <exception cref="InvalidOperationException">Thrown when a provided field name is not valid for the element type.</exception>
    public ExpandoObject Map(TElement element, params string[] fields)
    {
        var result = new ExpandoObject();
        var all = Fields.ToList();
        var selected = fields.Length > 0 ? fields : all.Where(f => f.IsDefault).Select(f => f.Name).ToArray();
        var lookup = all.ToDictionary(x => x.Name, x => x.Getter, StringComparer.OrdinalIgnoreCase);

        foreach (var field in selected)
        {
            if (!lookup.TryGetValue(field, out var getter))
                throw new InvalidOperationException($"Field '{field}' is not valid for {typeof(TElement).Name}.");

            var value = getter(element);
            result.TryAdd(field, value);
        }

        return result;
    }

    /// <summary>
    /// Creates a collection of dynamic objects by projecting the specified fields of the given element instances.
    /// </summary>
    /// <param name="elements">The collection of element instances from which field values will be extracted.</param>
    /// <param name="fields">An optional array of field names to include in the dynamic objects. If no fields are provided, all available fields are included.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ExpandoObject"/> instances representing the projected fields and their values.</returns>
    /// <exception cref="InvalidOperationException">Thrown when a provided field name is not valid for the element type.</exception>
    public IEnumerable<ExpandoObject> Map(IEnumerable<TElement> elements, params string[] fields)
    {
        var results = new List<ExpandoObject>();
        var all = Fields.ToList();
        var selected = fields.Length > 0 ? fields : all.Where(f => f.IsDefault).Select(f => f.Name).ToArray();
        var lookup = all.ToDictionary(x => x.Name, x => x.Getter, StringComparer.OrdinalIgnoreCase);

        foreach (var element in elements)
        {
            var result = new ExpandoObject();

            foreach (var field in selected)
            {
                if (!lookup.TryGetValue(field, out var getter))
                    throw new InvalidOperationException($"Field '{field}' is not valid for {typeof(TElement).Name}.");

                var value = getter(element);

                result.TryAdd(field, value);
            }

            results.Add(result);
        }

        return results;
    }
}