using L5Sharp.CLI.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.CLI.Schemas;

/// <summary>
/// A specialized projector for transforming <see cref="Tag"/> elements into dynamically generated
/// objects with selected fields. The <see cref="TagSchema"/> class provides functionality
/// to extract and emit properties of <see cref="Tag"/> elements, as defined by the field configuration.
/// </summary>
public class TagSchema : ElementSchema<Tag>
{
    /// <inheritdoc />
    public override IReadOnlyList<Field<Tag>> Fields =>
    [
        Field<Tag>.For(x => x.Reference, "The reference path to the tag element"),
        Field<Tag>.For(d => d.TagName, "The name of the tag", true),
        Field<Tag>.For(x => x.Value, "The data value of the tag", true),
        Field<Tag>.For(x => x.DataType, "The data type of the tag", true),
        Field<Tag>.For(x => x.AliasFor, "The tag name this tag is an alias for (empty if not an alias)"),
        Field<Tag>.For(x => x.Dimensions, "The array dimensions of the tag if it is an array type"),
        Field<Tag>.For(x => x.Radix, "The radix (number format) of the tag if it is an atomic type"),
        Field<Tag>.For(x => x.ExternalAccess, "The external access level for the tag"),
        Field<Tag>.For(x => x.OpcUAAccess, "The OPC UA access level for the tag"),
        Field<Tag>.For(x => x.Description, "The description of the tag", true),
        Field<Tag>.For(x => x.Scope, "The scope or container name of the tag", true),
        Field<Tag>.For(x => x.Constant, "Indicates whether the tag is a constant"),
        Field<Tag>.For(x => x.Usage, "The usage type of the tag (e.g., Normal, Local, Input, Output"),
        Field<Tag>.For(x => x.TagType, "The tag type classification of the tag (Base/Alias/Produced/Consumed)"),
        Field<Tag>.For(x => x.Class, "The component class of the tag (Standard/Safety)"),
    ];
}