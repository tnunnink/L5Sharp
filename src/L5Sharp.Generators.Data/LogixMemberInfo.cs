using System;
using System.Collections.Generic;
using System.Text;
using L5Sharp.Core;

namespace L5Sharp.Generators.Data;

/// <summary>
/// Represents metadata for a Logix member including essential information such as parent identifier, name,
/// data type, dimension, and description.
/// </summary>
internal record LogixMemberInfo(
    string Parent,
    string Name,
    string DataType,
    int Dimension,
    ExternalAccess Access,
    string? Description = null,
    string? Target = null,
    bool Hidden = false,
    int? BitNumber = null)
{
    public string Parent { get; } = Parent;
    public string Name { get; } = Name;
    public string DataType { get; } = DataType;
    public int Dimension { get; } = Dimension;
    public ExternalAccess Access { get; } = Access;
    public string? Description { get; } = Description;
    public string? Target { get; } = Target;
    public bool Hidden { get; } = Hidden;
    public int? BitNumber { get; } = BitNumber;


    /// <summary>
    /// Computes the memory offset and alignment for the current member based on its data type and dimension.
    /// </summary>
    /// <param name="context">A dictionary containing type information for resolving non-atomic data types.</param>
    /// <param name="alignment">The alignment of the current member, set as an output parameter.</param>
    /// <returns>
    /// The total memory offset required by the current member, considering its data type and dimension.
    /// </returns>
    public int ComputeOffset(Dictionary<string, LogixTypeInfo> context, out int alignment)
    {
        //Logix as a min of 4 byte alignment and a max of 8, depending on the size of nested members.
        const int minAlignment = 4;

        if (LogixType.IsAtomic(DataType))
        {
            var size = DataType switch
            {
                nameof(BOOL) => 0, //The reason it's 0 is that we have the backing hidden member in the type definition.
                nameof(SINT) or nameof(USINT) => 1,
                nameof(INT) or nameof(UINT) => 2,
                nameof(DINT) or nameof(UDINT) or nameof(REAL) or nameof(TIME32) => 4,
                _ => 8
            };

            alignment = Math.Max(minAlignment, size);
            return Dimension > 0 ? size * Dimension : size;
        }

        if (context.TryGetValue(DataType, out var info))
        {
            return info.ComputeOffset(context, out alignment);
        }

        if (LogixType.TryCreate(DataType, out var registered))
        {
            alignment = 4; //todo not sure what to do here. We would have to travers the structure I guess
            return registered.Size;
        }

        //todo We have encountered a member with an undefined data type definition. What can we do?
        alignment = 4;
        return 0;
    }

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
        return new LogixMemberInfo(
            member.Parent?.Name ?? "StructureData",
            member.Name,
            member.DataType == "BIT" ? "BOOL" : member.DataType.SanitizeName(),
            member.Dimension.Length,
            member.ExternalAccess ?? ExternalAccess.ReadWrite,
            member.Description,
            member.Target,
            member.Hidden,
            member.BitNumber
        );
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
        return new LogixMemberInfo(
            parameter.Instruction?.Name ?? "StructureData",
            parameter.Name,
            parameter.DataType.SanitizeName(),
            parameter.Dimension.Length,
            parameter.ExternalAccess ?? ExternalAccess.ReadWrite,
            parameter.Description
        );
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
        var newModifier = Name is "Count" or "Clear" ? "new " : string.Empty;

        var remarks = string.IsNullOrWhiteSpace(Description)
            ? string.Empty
            : $"""

                   /// <remarks>
                   /// {Description?.Replace("\n", "\n    /// ")}
                   /// </remarks>
               """;

        return
            $$"""
                  /// <summary>
                  /// The <c>{{Name}}</c> member of the <see cref="{{Parent}}"/> data type.
                  /// </summary>{{remarks}}
                  public {{newModifier}}{{returnType}} {{Name}}
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
            if (member.Hidden) continue;
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
            if (member.Hidden) continue;
            builder.Append(member.GenerateInitializer());
            builder.Append("\n        ");
        }

        return builder.ToString().TrimEnd();
    }

    /// <summary>
    /// Generates the source code for the class-level member access mapping.
    /// This allows the PlcClient to know what's readable without using Reflection.
    /// </summary>
    internal static string GenerateAccessMetadata(this IEnumerable<LogixMemberInfo> members)
    {
        var builder = new StringBuilder();

        foreach (var member in members)
        {
            if (member.Hidden) continue;
            builder.Append($"nameof({member.Name}) => ExternalAccess.{member.Access},");
            builder.Append("\n        ");
        }

        return builder.ToString().TrimEnd();
    }
}