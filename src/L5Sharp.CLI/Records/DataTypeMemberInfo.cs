using System.Text.Json.Serialization;
using L5Sharp.Core;

namespace L5Sharp.CLI.Records;

public record DataTypeMemberInfo()
{
    public DataTypeMemberInfo(DataTypeMember element) : this()
    {
        Name = element.Name;
        Description = element.Description;
        DataType = element.DataType;
        Dimension = element.Dimension?.ToString() ?? Dimensions.Empty.ToString();
        Radix = element.Radix ?? Core.Radix.Null;
        ExternalAccess = element.ExternalAccess ?? Core.Access.ReadWrite;
        Parent = element.Parent?.Name;
    }

    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string DataType { get; init; } = nameof(DINT);
    public string Dimension { get; init; } = Dimensions.Empty.ToString();
    public string Radix { get; init; } = Core.Radix.Decimal;
    public string ExternalAccess { get; init; } = Core.Access.ReadWrite;
    [JsonIgnore] public string? Parent { get; init; }

    /// <summary>
    /// Defines an implicit conversion operator to convert a <see cref="DataTypeMember"/> object
    /// to its corresponding <see cref="DataTypeMemberInfo"/> representation.
    /// </summary>
    /// <param name="member">The <see cref="DataTypeMember"/> instance to be converted to a <see cref="DataTypeMemberInfo"/>.</param>
    /// <returns>A new <see cref="DataTypeMemberInfo"/> object representing the data of the given <see cref="DataTypeMember"/>.</returns>
    public static implicit operator DataTypeMemberInfo(DataTypeMember member) => new(member);

    /// <summary>
    /// Defines an implicit conversion operator to convert a specified object of one type
    /// into its corresponding representation in another type.
    /// </summary>
    /// <param name="info">The instance of the source type to be converted.</param>
    /// <returns>A new instance of the target type derived from the provided source type.</returns>
    public static implicit operator DataTypeMember(DataTypeMemberInfo info) => new()
    {
        Name = info.Name,
        Description = info.Description,
        DataType = info.DataType,
        Dimension = Dimensions.Parse(info.Dimension),
        Radix = Core.Radix.Parse(info.Radix),
        ExternalAccess = Core.Access.Parse(info.ExternalAccess)
    };
}