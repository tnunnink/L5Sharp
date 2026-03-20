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
    string? Description = null,
    bool Hidden = false,
    string? Target = null,
    int? BitNumber = null)
{
    public string Parent { get; } = Parent;
    public string Name { get; } = Name;
    public string DataType { get; } = DataType;
    public int Dimension { get; } = Dimension;
    public string? Description { get; } = Description;
    public bool Hidden { get; } = Hidden;
    public string? Target { get; } = Target;
    public int? BitNumber { get; } = BitNumber;

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
            member.Parent?.Name.SanitizeName() ?? "StructureData",
            member.Name,
            member.DataType == "BIT" ? "BOOL" : member.DataType.SanitizeName(),
            member.Dimension.Length,
            member.Description,
            member.Hidden,
            member.Target,
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
            parameter.Parent?.Name.SanitizeName() ?? "StructureData",
            parameter.Name,
            parameter.DataType.SanitizeName(),
            parameter.Dimension.Length,
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
        var returnType = isArray ? $"ArrayData<{DataType}>" : DataType;
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
        var initializer = Dimension > 0 ? $"ArrayData<{typeName}>({Dimension})" : $"{typeName}()";
        return $"{Name} = new {initializer};";
    }

    /// <summary>
    /// Computes the size in bytes of the current Logix member and determines its alignment requirement.
    /// This method handles atomic types, boolean arrays, bit-packed members, and nested complex types,
    /// accounting for multidimensional arrays where applicable.
    /// </summary>
    /// <param name="context">A dictionary mapping type names to <see cref="LogixTypeInfo"/> objects,
    /// providing size and structure information for nested complex types within the current L5X context.</param>
    /// <param name="alignment">When this method returns, contains the alignment requirement (in bytes)
    /// for this member, which influences how Logix structures are padded and aligned in memory.</param>
    /// <returns>
    /// The total size in bytes occupied by this member, including any array dimensions.
    /// Returns 0 for bit-packed members or members with undefined data types.
    /// </returns>
    public int ComputeSize(Dictionary<string, LogixTypeInfo> context, out int alignment)
    {
        // Logix will align structures to 4/8 bytes based on the largest nested member.
        // To support that, we need to track the largest member type and surface it to the caller.
        alignment = 4;

        // Bit and boolean member size can't be determined from the member itself since they
        // are packed into other byte members of the containing type. 
        if (Target is not null || (DataType is nameof(BOOL) && Dimension == 0))
            return 0;

        // Boolean arrays are special case and should be sized to the size of the array.
        // Logix will align boolean arrays to 4/8 byte boundaries as necessary.
        if (DataType is nameof(BOOL) && Dimension > 0)
            return ComputeDimensionalSize(1, Dimension / 8);

        // Once we hit an atomic type, we can determine the size based on name and dimensions.
        // This is also where we can override the default alignment as needed.
        if (LogixType.IsAtomic(DataType))
        {
            var size = LogixType.SizeOf(DataType);
            alignment = Math.Max(alignment, size);
            return ComputeDimensionalSize(size, Dimension);
        }

        // We hit a nested complex type in the context of the current L5X file used for generation.
        if (context.TryGetValue(DataType, out var info))
        {
            //todo we technically need to figure out the proper alignment here.
            return ComputeDimensionalSize(info.ComputeSize(context, out alignment), Dimension);
        }

        // We hit a nested complex type that was previously registered and that should have a predetermined size.
        if (LogixType.TryCreate(DataType, out var registered))
        {
            //todo we technically need to figure out the proper alignment here.
            return ComputeDimensionalSize(registered.GetSize(), Dimension);
        }

        //todo these needs the context of the generator ...
        /*context.ReportDiagnostic(
            Diagnostic.Create(DiagnosticDescriptors.L5XParseWarning, Location.None, project.Path, e.Message)
        );*/
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="size"></param>
    /// <param name="dimensions"></param>
    /// <returns></returns>
    private static int ComputeDimensionalSize(int size, int dimensions)
    {
        return dimensions > 0 ? size * dimensions : size;
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
    /// Generates the UpdateData override method implementation for a collection of <see cref="LogixMemberInfo"/> objects,
    /// handling byte-level data updates including bit packing for boolean members and proper offset calculations for all data types.
    /// </summary>
    /// <param name="members">The collection of <see cref="LogixMemberInfo"/> objects for which to generate the UpdateData override implementation.</param>
    /// <param name="context">A dictionary mapping type names to <see cref="LogixTypeInfo"/> objects,
    /// providing size and structure information for nested complex types to calculate proper byte offsets.</param>
    /// <returns>
    /// A string containing the generated C# code statements for the UpdateData method body, with each member's
    /// UpdateData call positioned at the correct byte offset within the data buffer.
    /// </returns>
    internal static string GenerateUpdateOverride(this IEnumerable<LogixMemberInfo> members,
        Dictionary<string, LogixTypeInfo> context)
    {
        var builder = new StringBuilder();
        var offset = 0;
        var bitNumber = 0;
        var hidden = new Dictionary<string, int>();

        foreach (var member in members)
        {
            switch (member)
            {
                // For bit members with a target backing field, we have the necessary info to determine the bit location.
                // The backing field handles the offset, so we just need to generate the function.
                case { DataType: "BIT", Target: not null, Hidden: false }
                    when hidden.TryGetValue(member.Target, out var target):
                {
                    // Calculate which specific byte in the 4-byte DINT the bit belongs to
                    var byteOffset = target + member.BitNumber / 8;
                    var bitInByte = member.BitNumber % 8;

                    var line = $"{member.Name}.UpdateData((data[offset + {byteOffset}] & (1 << {bitInByte})) != 0);";
                    builder.AppendLine(line);
                    builder.Append("        ");
                    continue;
                }
                // For some reason, certain types (Predefined/AOIs/Manipulated UDTs) can still have bool members that need
                // to handle packing as well. The best we can do is count each bit member until we hit the next byte.
                case { DataType: nameof(BOOL), Dimension: 0, Hidden: false }:
                {
                    //todo this is wrong for some scenarios. Sometimes Logix uses full 4 bytes for even 1 bool. But we can't know since there is no backing field shown.
                    if (bitNumber == 0) offset++;
                    //todo I'm actually not sure how we know where the offset is if the bit number won't reset it might just be the location of the first boolean
                    var line = $"{member.Name}.UpdateData((data[offset + {offset}] & (1 << {bitNumber})) != 0);";
                    builder.AppendLine(line);
                    builder.Append("        ");

                    bitNumber = (bitNumber + 1) % 8;
                    continue;
                }
            }

            // If we get here, we have a non-boolean member, and it's either a hidden member that we need to track
            // or a public member that we can generate a function for.
            if (member.Hidden)
            {
                hidden.Add(member.Name, offset);
            }
            else
            {
                builder.AppendLine($"{member.Name}.UpdateData(data, offset + {offset});");
                builder.Append("        ");
            }

            // Increment the offset by the size of the member. We don't care about alignment here.
            offset += member.ComputeSize(context, out _);
        }

        return builder.ToString();
    }
}