using System.Collections.Generic;
using System.Text;
using L5Sharp.Core;

namespace L5Sharp.Generators.Data;

/// <summary>
/// Represents metadata for a Logix member including essential information such as parent identifier, name,
/// data type, dimension, and description.
/// </summary>
internal record LogixMemberInfo(string Parent, string Name, string DataType, int Dimension, string? Description = null)
{
    public string Parent { get; } = Parent;
    public string Name { get; } = Name;
    public string DataType { get; } = DataType;
    public int Dimension { get; } = Dimension;
    public string? Description { get; } = Description;

    /// <summary>
    /// Creates an instance of <see cref="LogixMemberInfo"/> from the provided <see cref="DataTypeMember"/>.
    /// </summary>
    /// <param name="member">The <see cref="DataTypeMember"/> representing the source information for the Logix member.</param>
    /// <returns>
    /// A new <see cref="LogixMemberInfo"/> object populated with the parent name, member name, data type, dimension,
    /// and description derived from the provided <see cref="DataTypeMember"/>.
    /// </returns>
    public static LogixMemberInfo From(DataTypeMember member)
    {
        var parentType = member.Parent?.Name ?? "StructureData";
        var name = member.Name;
        var dataType = member.DataType == "BIT" ? "BOOL" : member.DataType.SanitizeName();
        var dimension = member.Dimension.Length;
        var description = member.Description;

        return new LogixMemberInfo(parentType, name, dataType, dimension, description);
    }

    /// <summary>
    /// Creates an instance of <see cref="LogixMemberInfo"/> from the provided <see cref="Parameter"/>.
    /// </summary>
    /// <param name="parameter">The <see cref="Parameter"/> representing the source information for the Logix member.</param>
    /// <returns>
    /// A new <see cref="LogixMemberInfo"/> object populated with the parent name, member name, data type, dimension,
    /// and description derived from the provided <see cref="Parameter"/>.
    /// </returns>
    public static LogixMemberInfo From(Parameter parameter)
    {
        var parentType = parameter.Instruction?.Name ?? "StructureData";
        var name = parameter.Name;
        var dataType = parameter.DataType.SanitizeName();
        var dimension = parameter.Dimension.Length;
        var description = parameter.Description;

        return new LogixMemberInfo(parentType, name, dataType, dimension, description);
    }

    /// <summary>
    /// Creates a new instance of <see cref="LogixMemberInfo"/> using the provided <see cref="LogixMember"/>
    /// and the specified parent type name.
    /// </summary>
    /// <param name="member">The <see cref="LogixMember"/> containing the metadata to be transformed into a Logix member.</param>
    /// <param name="parentType">The name of the parent type to associate with the Logix member.</param>
    /// <returns>
    /// A new <see cref="LogixMemberInfo"/> instance populated with the parent type, member name, data type, dimension,
    /// and other relevant details derived from the provided <see cref="LogixMember"/>.
    /// </returns>
    public static LogixMemberInfo From(LogixMember member, string parentType)
    {
        var name = member.Name;
        var dataType = member.Value.Name.SanitizeName();
        var dimension = member.Value is ArrayData array ? array.Dimensions.Length : 0;

        return new LogixMemberInfo(parentType, name, dataType, dimension);
    }

    /// <summary>
    /// Generates the source code representation of the current Logix member information.
    /// </summary>
    /// <returns>
    /// A string containing the generated source code for the member, including its XML documentation
    /// and property definition.
    /// </returns>
    public string GenerateProperty()
    {
        var isArray = Dimension > 0;
        var returnType = isArray ? $"{DataType}[]" : DataType;
        var methodSuffix = isArray ? "Array" : "Member";

        var remarks = string.IsNullOrWhiteSpace(Description)
            ? string.Empty
            : $"""

                   /// <remarks>
                   /// {Description}
                   /// </remarks>
               """;

        return
            $$"""
                  /// <summary>
                  /// The <c>{{Name}}</c> member of the <see cref="{{Parent}}"/> data type.
                  /// </summary>{{remarks}}
                  public {{returnType}} {{Name}}
                  {
                      get => Get{{methodSuffix}}<{{DataType}}>();
                      set => Set{{methodSuffix}}(value);
                  }
              """;
    }

    /// <summary>
    /// Generates an initializer string for the current Logix member based on its data type and dimension.
    /// </summary>
    /// <returns>
    /// A string representing the initializer for the Logix member. If the member has a dimension, the
    /// initializer will include array syntax; otherwise, a constructor call will be used.
    /// </returns>
    public string GenerateInitializer()
    {
        var typeName = DataType.SanitizeName();
        var initializer = Dimension > 0 ? $"{typeName}[{Dimension}]" : $"{typeName}()";
        return $"{Name} = new {initializer};";
    }
}

/// <summary>
/// Provides extension methods for collections of <see cref="LogixMemberInfo"/> objects, enabling the generation of property definitions
/// and initializer expressions as string representations.
/// </summary>
internal static class LogixMembersInfoExtension
{
    /// <summary>
    /// Generates a string containing the property definitions for a collection of <see cref="LogixMemberInfo"/> objects.
    /// </summary>
    /// <param name="members">The collection of <see cref="LogixMemberInfo"/> objects for which to generate property definitions.</param>
    /// <returns>
    /// A string containing the concatenated property definitions for the provided <see cref="LogixMemberInfo"/> objects.
    /// </returns>
    internal static string GenerateProperties(this IEnumerable<LogixMemberInfo> members)
    {
        var builder = new StringBuilder();

        foreach (var member in members)
        {
            builder.AppendLine(member.GenerateProperty());
            builder.AppendLine();
        }

        return builder.ToString().TrimEnd();
    }

    /// <summary>
    /// Generates a string containing the initializer expressions for a collection of <see cref="LogixMemberInfo"/> objects.
    /// </summary>
    /// <param name="members">The collection of <see cref="LogixMemberInfo"/> objects for which to generate initializer expressions.</param>
    /// <returns>
    /// A string containing the concatenated initializer expressions for the provided <see cref="LogixMemberInfo"/> objects.
    /// </returns>
    internal static string GenerateInitializers(this IEnumerable<LogixMemberInfo> members)
    {
        var builder = new StringBuilder();

        foreach (var member in members)
        {
            builder.Append(member.GenerateInitializer());
            builder.Append("\n        ");
        }

        return builder.ToString().TrimEnd();
    }
}