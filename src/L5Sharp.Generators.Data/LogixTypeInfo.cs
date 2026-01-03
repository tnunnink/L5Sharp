using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Core;

namespace L5Sharp.Generators.Data;

/// <summary>
/// Represents metadata information for a Logix controller data type.
/// </summary>
/// <remarks>
/// This class encapsulates details about a specific Logix data type, including its name, members,
/// description, and derived type. It serves as an intermediary representation allowing metadata to be
/// used for code generation or other processing tasks.
/// </remarks>
internal record LogixTypeInfo(
    string Name,
    DataTypeClass Class,
    DataTypeFamily Family,
    IEnumerable<LogixMemberInfo> Members,
    string? Description = null)
{
    private const string DefaultNameSpace = "L5Sharp.Data.Generated";

    public string TypeName => Name.SanitizeName();
    public string Name { get; } = Name;
    public DataTypeClass Class { get; } = Class;
    public DataTypeFamily Family { get; } = Family;
    public string? Description { get; } = Description;
    public IEnumerable<LogixMemberInfo> Members { get; } = Members;


    /// <summary>
    /// Creates an instance of <see cref="LogixTypeInfo"/> from the provided <see cref="DataType"/> object.
    /// </summary>
    /// <param name="dataType">
    /// The <see cref="DataType"/> object to be transformed into a <see cref="LogixTypeInfo"/> instance.
    /// </param>
    /// <returns>
    /// A new <see cref="LogixTypeInfo"/> instance containing metadata derived from the provided object.
    /// </returns>
    public static LogixTypeInfo From(DataType dataType)
    {
        var name = dataType.Name;
        var typeClass = dataType.Class;
        var typeFamily = dataType.Family;
        var description = dataType.Description;

        var members = !Equals(dataType.Family, DataTypeFamily.String)
            ? dataType.Members.Select(LogixMemberInfo.From)
            : [];

        return new LogixTypeInfo(name, typeClass, typeFamily, members, description);
    }

    /// <summary>
    /// Creates an instance of <see cref="LogixTypeInfo"/> from the provided <see cref="AddOnInstruction"/> object.
    /// </summary>
    /// <param name="aoi">
    /// The <see cref="AddOnInstruction"/> object to be transformed into a <see cref="LogixTypeInfo"/> instance.
    /// </param>
    /// <returns>
    /// A new <see cref="LogixTypeInfo"/> instance containing metadata derived from the provided object.
    /// </returns>
    public static LogixTypeInfo From(AddOnInstruction aoi)
    {
        var name = aoi.Name;
        var description = aoi.Description;

        var members = aoi.Parameters
            .Where(p => p.Usage == TagUsage.Input || p.Usage == TagUsage.Output)
            .Select(LogixMemberInfo.From);

        return new LogixTypeInfo(name, DataTypeClass.User, DataTypeFamily.None, members, description);
    }

    // todo I need to consider if this is even a viable solution. With tag data we can't know the real size and backing members of a given type.
    /*public static IEnumerable<LogixTypeInfo> From(LogixData data)
    {
        var types = new List<LogixTypeInfo>();

        // ReSharper disable once ConvertIfStatementToSwitchStatement
        if (data is StructureData structure)
        {
            if (LogixType.IsRegistered(structure.Name)) return types;

            var typeName = structure.Name.SanitizeName();
            var members = structure.Members.Select(m => LogixMemberInfo.From(m, typeName));
            var type = new LogixTypeInfo(structure.Name, members);
            types.Add(type);

            var nestedType = structure.Members.SelectMany(m => From(m.Value));
            types.AddRange(nestedType);
        }

        if (data is ArrayData array)
        {
            //We need to get nested structures inside arrays
            var arrayTypes = From(array.Members.First().Value);
            types.AddRange(arrayTypes);
        }

        return types;
    }
    */

    /// <summary>
    /// Generates the source code for the Logix type based on the provided namespace and context information.
    /// </summary>
    /// <param name="nameSpace">
    /// The namespace in which the generated Logix type will reside. If null, a default namespace will be used.
    /// </param>
    /// <param name="context">
    /// A dictionary containing context information about other Logix types used to generate necessary type dependencies.
    /// </param>
    /// <returns>
    /// A string representation of the generated source code.
    /// </returns>
    public string GenerateSource(string? nameSpace, Dictionary<string, LogixTypeInfo> context)
    {
        nameSpace ??= DefaultNameSpace;
        var derivedType = Family == DataTypeFamily.String ? "StringData" : "StructureData";

        //We will use the data type description as the remarks documentation for the class if available.
        var remarks = string.IsNullOrWhiteSpace(Description)
            ? string.Empty
            : $"""

               /// <remarks>
               /// {Description?.Replace("\n", "\n/// ")}
               /// </remarks>
               """;

        return
            $$"""
              using L5Sharp.Core;
              using System.Xml.Linq;
              // Auto-generated type definition
              // ReSharper disable InconsistentNaming
              // ReSharper disable PartialTypeWithSinglePart
              // ReSharper disable MemberCanBePrivate.Global

              namespace {{nameSpace}};

              /// <summary>
              /// Represents a <c>{{TypeName}}</c> data type structure.
              /// </summary>{{remarks}}
              [LogixData("{{Name}}")]
              public sealed partial class {{TypeName}} : {{derivedType}}
              {
                  /// <summary>
                  /// Creates a new <see cref="{{TypeName}}"/> instance initialized with default values.
                  /// </summary>
                  public {{TypeName}}() : base("{{Name}}")
                  {
                      {{Members.GenerateInitializers()}}
                  }
                  
                  /// <summary>
                  /// Creates a new <see cref="{{TypeName}}"/> instance initialized with the provided element.
                  /// </summary>
                  public {{TypeName}}(XElement element) : base(element)
                  {
                  }
                  
              {{GenerateSizeAndUpdateOverrides(context)}}
                  
              {{Members.GenerateProperties()}}
              
                  
              }
              """;
    
        //todo could add this to generator to get access for members.
    /*/// <inheritdoc />
    public override ExternalAccess GetAccess(string memberName) => memberName switch
    {
        {{Members.GenerateAccessMetadata()}}
            _ => ExternalAccess.ReadWrite
    };*/
    }

    /// <summary>
    /// Computes the memory offset for the current data type, taking into account its members and their alignments.
    /// </summary>
    /// <param name="context">
    /// A dictionary containing metadata for various Logix data types, where the key is the data type name
    /// and the value is its corresponding <see cref="LogixTypeInfo"/> instance.
    /// </param>
    /// <param name="alignment">
    /// An output parameter representing the alignment requirement of the current data type,
    /// as calculated based on its members.
    /// </param>
    /// <returns>
    /// The total memory offset, adjusted for the computed alignment.
    /// </returns>
    public int ComputeOffset(Dictionary<string, LogixTypeInfo> context, out int alignment)
    {
        var offset = 0;
        alignment = 0;

        foreach (var member in Members)
        {
            offset += member.ComputeOffset(context, out var memberAlign);
            alignment = Math.Max(alignment, memberAlign);
        }

        return (offset + alignment - 1) & ~(alignment - 1);
    }

    /// <summary>
    /// Generates the size and custom overrides for type definitions and updates the provided context with relevant mappings.
    /// </summary>
    /// <param name="context">
    /// A dictionary containing existing <see cref="LogixTypeInfo"/> instances mapped by their names,
    /// which is used to resolve dependencies and update with new or modified type information.
    /// </param>
    /// <returns>
    /// An object containing the calculated size of the type and necessary method overrides
    /// for processing byte stream data related to the type.
    /// </returns>
    private object GenerateSizeAndUpdateOverrides(Dictionary<string, LogixTypeInfo> context)
    {
        var builder = new StringBuilder();
        var offset = 0;
        var alignment = 0;
        var hidden = new Dictionary<string, int>();

        foreach (var member in Members)
        {
            if (!member.Hidden && member.Target is not null && hidden.TryGetValue(member.Target, out var target))
            {
                // Calculate which specific byte in the 4-byte DINT the bit belongs to
                var byteOffset = target + member.BitNumber / 8;
                var bitInByte = member.BitNumber % 8;

                var line = $"{member.Name}.Update((data[offset + {byteOffset}] & (1 << {bitInByte})) != 0);";
                builder.AppendLine(line);
                builder.Append("        ");
                continue;
            }

            if (member.Hidden)
            {
                hidden.Add(member.Name, offset);
            }
            else
            {
                //todo take this out once we upgrade to next version which has update extension.
                var arrayExtension = member.Dimension > 0 ? ".ToArrayData()" : string.Empty;
                builder.AppendLine($"{member.Name}{arrayExtension}.Update(data, offset + {offset});");
                builder.Append("        ");
            }

            offset += member.ComputeOffset(context, out var align);
            alignment = Math.Max(alignment, align);
        }

        var size = (offset + alignment - 1) & ~(alignment - 1);
        var mapping = builder.ToString();

        return
            $$"""
                  /// <inheritdoc />
                  /// <remarks>
                  /// This value was generated based on the type definition exported from Studio 5k.
                  /// We need this value to correctly read data from a byte stream in <see cref="Update"/>.
                  /// </remarks>
                  public override int Size => {{size}};

                  /// <inheritdoc />
                  /// <remarks>
                  /// This mapping was generated based on the type definition exported from Studio 5K.
                  /// Rockwell predefined types don't follow normal UDT packing rules and have to be handled manually.
                  /// </remarks>
                  public override int Update(byte[] data, int offset)
                  {
                      {{mapping}}
                      return offset + Size;
                  }
              """;
    }
}