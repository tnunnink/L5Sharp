using System.Dynamic;
using System.Linq.Expressions;
using L5Sharp.Core;

namespace L5Sharp.CLI.Abstractions;

/// <summary>
/// Defines a schema for mapping and projecting instances of <see cref="ILogixElement"/> to dynamic objects.
/// This interface provides metadata about available fields and methods to transform element data
/// into <see cref="ExpandoObject"/> instances suitable for rendering, serialization, or analysis.
/// Implementations specify which properties or computed values can be extracted from elements of type <typeparamref name="TElement"/>.
/// </summary>
/// <typeparam name="TElement">The type of logix element this schema applies to. Must implement <see cref="ILogixElement"/>.</typeparam>
public interface IElementSchema<TElement> where TElement : ILogixElement
{
    /// <summary>
    /// Gets the collection of fields defined by the schema for the specified Logix element type.
    /// Each field represents a property or computed value that can be extracted from an instance of <typeparamref name="TElement"/>.
    /// The fields provide metadata such as the field name, type, a getter function, default designation, and a description.
    /// </summary>
    IReadOnlyList<Field<TElement>> Fields { get; }

    /// <summary>
    /// Maps a single element to an <see cref="ExpandoObject"/> containing the specified fields.
    /// If no fields are specified, all default fields defined in the schema will be included.
    /// </summary>
    /// <param name="element">The element to map. Must not be null.</param>
    /// <param name="fields">The names of the fields to include in the output. If empty, default fields are used.</param>
    /// <returns>An <see cref="ExpandoObject"/> containing the requested field values extracted from the element.</returns>
    ExpandoObject Map(TElement element, params string[] fields);

    /// <summary>
    /// Maps a collection of elements to a sequence of <see cref="ExpandoObject"/> instances, each containing the specified fields.
    /// If no fields are specified, all default fields defined in the schema will be included for each element.
    /// </summary>
    /// <param name="elements">The collection of elements to map. Must not be null.</param>
    /// <param name="fields">The names of the fields to include in each output object. If empty, default fields are used.</param>
    /// <returns>A sequence of <see cref="ExpandoObject"/> instances, one for each element in the input collection.</returns>
    IEnumerable<ExpandoObject> Map(IEnumerable<TElement> elements, params string[] fields);
}

/// <summary>
/// Represents a field definition within an element schema, encapsulating metadata and functionality
/// for extracting and describing a specific property or computed value from elements.
/// </summary>
/// <param name="Name">The display name of the field as it appears in projections and output.</param>
/// <param name="Type">The data type name of the field value (e.g., "String", "Int32", "Boolean").</param>
/// <param name="Getter">A function that extracts the field value from an element instance. May return null.</param>
/// <param name="IsDefault">Indicates whether this field is included by default when no specific fields are requested during mapping.</param>
/// <param name="Description">A human-readable description of the field's purpose and meaning.</param>
public readonly record struct Field<TElement>(
    string Name,
    Type Type,
    Func<TElement, object?> Getter,
    bool IsDefault,
    string Description
) where TElement : ILogixElement
{
    /// <summary>
    /// Builds a <see cref="Field{TElement}"/> instance by extracting metadata and a getter function
    /// from a specified member of an element.
    /// </summary>
    /// <param name="memberSelector">A lambda expression selecting the member of the element to define the field for.
    /// Must be a member access expression.</param>
    /// <param name="description">A human-readable description of the field's purpose and meaning.</param>
    /// <param name="isDefault">A boolean value indicating whether the field should be included by default
    /// when no specific fields are requested.</param>
    /// <returns>
    /// A new <see cref="Field{TElement}"/> instance containing the name, type, getter function,
    /// default state, and description extracted from the specified member.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided expression is not a valid member access expression.
    /// </exception>
    public static Field<TElement> For(Expression<Func<TElement, object?>> memberSelector,
        string description, bool isDefault = false)
    {
        // Extract the member expression from the body, handling potential unary conversions
        var memberExpression = memberSelector.Body is UnaryExpression unary
            ? unary.Operand as MemberExpression
            : memberSelector.Body as MemberExpression;

        if (memberExpression is null)
            throw new ArgumentException("Expression must be a member access expression.", nameof(memberSelector));

        // Get the member name and type
        var name = memberExpression.Member.Name;
        var type = memberExpression.Type;

        // Compile the expression to create the getter function
        var getter = memberSelector.Compile();

        return new Field<TElement>(name, type, getter, isDefault, description);
    }
}