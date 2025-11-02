using L5Sharp.Core;

namespace L5Sharp.Model;

/// <summary>
/// A record structure for the <see cref="DataType"/> component.
/// </summary>
public record DataTypeInfo()
{
    public DataTypeInfo(DataType dataType) : this()
    {
        Name = dataType.Name;
        Description = dataType.Description;
        Family = dataType.Family?.Name ?? DataTypeFamily.None;
        Class = dataType.Class?.Name ?? DataTypeClass.User;
        Members = dataType.Members.Select(m => new DataTypeMemberInfo(m)).ToArray();
    }

    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Family { get; init; } = DataTypeFamily.None;
    public string Class { get; init; } = DataTypeClass.User;
    public DataTypeMemberInfo[] Members { get; init; } = [];

    /// <summary>
    /// Defines an implicit conversion from a <see cref="DataType"/> object to a <see cref="DataTypeInfo"/> object.
    /// </summary>
    /// <param name="dataType">The <see cref="DataType"/> instance to be converted.</param>
    /// <returns>A <see cref="DataTypeInfo"/> instance created using the provided <see cref="DataType"/>.</returns>
    public static implicit operator DataTypeInfo(DataType dataType) => new(dataType);

    /// <summary>
    /// Defines an implicit conversion from a <see cref="DataTypeInfo"/> object to a <see cref="DataType"/> object.
    /// </summary>
    /// <param name="info">The <see cref="DataTypeInfo"/> instance to be converted.</param>
    /// <returns>A new <see cref="DataType"/> object containing the converted data type information.</returns>
    public static implicit operator DataType(DataTypeInfo info) => new()
    {
        Name = info.Name,
        Description = info.Name,
        Family = DataTypeFamily.Parse(info.Family),
        Class = DataTypeClass.Parse(info.Class),
        Members = new LogixContainer<DataTypeMember>(info.Members.Select(x => (DataTypeMember)x))
    };
}