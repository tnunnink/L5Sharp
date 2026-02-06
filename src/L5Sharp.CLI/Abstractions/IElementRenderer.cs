using L5Sharp.Core;
using Spectre.Console.Rendering;

namespace L5Sharp.CLI.Abstractions;

/// <summary>
/// Defines a contract for rendering collections of <see cref="ILogixElement"/> instances
/// into a format suitable for display or output.
/// </summary>
public interface IElementRenderer
{
    /// <summary>
    /// Renders a single <see cref="ILogixElement"/> instance into a format suitable for display or output to the console.
    /// Optionally specify fields of the provided element to serialize to the output.
    /// </summary>
    /// <typeparam name="TElement">The type of element being rendered, which must implement <see cref="ILogixElement"/>.</typeparam>
    /// <param name="element">The element to render.</param>
    /// <param name="fields">An optional list of field names to be included in the rendered output, specified in order.</param>
    /// <returns>An <see cref="IRenderable"/> object representing the rendered output of the provided element.</returns>
    IRenderable Render<TElement>(TElement element, params string[] fields) where TElement : ILogixElement;

    /// <summary>
    /// Renders a collection of <see cref="ILogixElement"/> instances into a format suitable for display or output to the console.
    /// Optionally specify fields of the provided elements to serialize to the output.
    /// </summary>
    /// <typeparam name="TElement">The type of element being rendered, which must implement <see cref="ILogixElement"/>.</typeparam>
    /// <param name="elements">The collection of elements to render.</param>
    /// <param name="fields">An optional list of field names to be included in the rendered output, specified in order.</param>
    /// <returns>An <see cref="IRenderable"/> object representing the rendered output of the provided elements.</returns>
    IRenderable Render<TElement>(IEnumerable<TElement> elements, params string[] fields) where TElement : ILogixElement;
}