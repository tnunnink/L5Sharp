using L5Sharp.CLI.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.CLI.Schemas;

/// <summary>
/// Provides a specialized projector for extracting and projecting field values from instances of <see cref="DataType"/>.
/// This class inherits the base functionality of the <see cref="ElementSchema{TElement}"/> class and implements
/// the necessary field mappings specific to the <see cref="DataType"/> type.
/// </summary>
public class DataTypeSchema : ElementSchema<DataType>
{
    /// <inheritdoc />
    public override IReadOnlyList<Field<DataType>> Fields =>
    [
        Field<DataType>.For(d => d.Name, "The name of the data type", true),
        Field<DataType>.For(x => x.Class, "The class of the data type (e.g., User, Predefined)", true),
        Field<DataType>.For(x => x.Family, "The family category of the data type (e.g., None, String)"),
        Field<DataType>.For(x => x.Description, "A textual description of the data type", true),
        Field<DataType>.For(x => x.Members, "The number of member elements within the data type"),
        Field<DataType>.For(x => x.Use, "The usage context of the data type"),
        Field<DataType>.For(x => x.Reference, "The reference path for the data type")
    ];
}