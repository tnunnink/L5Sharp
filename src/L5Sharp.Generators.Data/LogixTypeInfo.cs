using System.Collections.Generic;
using System.Linq;
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
    IEnumerable<LogixMemberInfo> Members,
    string? Description = null,
    string? DerivedType = null)
{
    private const string DefaultNameSpace = "L5Sharp.Data.Generated";

    public string Name { get; } = Name;
    public string? Description { get; } = Description;
    public IEnumerable<LogixMemberInfo> Members { get; } = Members;
    public string DerivedType { get; } = DerivedType ?? "StructureData";
    public string TypeName => Name.SanitizeName();


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
        var description = dataType.Description;
        var derivedType = Equals(dataType.Family, DataTypeFamily.String) ? "StringData" : "StructureData";

        var members = !Equals(dataType.Family, DataTypeFamily.String)
            ? dataType.Members.Where(m => !m.Hidden).Select(LogixMemberInfo.From)
            : [];

        return new LogixTypeInfo(name, members, description, derivedType);
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

        return new LogixTypeInfo(name, members, description);
    }

    /// <summary>
    /// Creates a collection of <see cref="LogixTypeInfo"/> instances from the provided <see cref="LogixData"/> object.
    /// </summary>
    /// <param name="data">
    /// The <see cref="LogixData"/> object containing metadata to be transformed into a collection of <see cref="LogixTypeInfo"/> instances.
    /// </param>
    /// <returns>
    /// A collection of <see cref="LogixTypeInfo"/> instances representing the metadata derived from the provided <see cref="LogixData"/>.
    /// </returns>
    public static IEnumerable<LogixTypeInfo> From(LogixData data)
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

    /// <summary>
    /// Generates source code for a data type class based on the current <see cref="LogixTypeInfo"/> instance.
    /// </summary>
    /// <param name="nameSpace">
    /// The namespace in which the generated class will be placed. If null or empty, the default value "L5Sharp.Data.Generated" will be used.
    /// </param>
    /// <returns>
    /// A string containing the generated source code for the corresponding data type class.
    /// </returns>
    public string GenerateSource(string? nameSpace)
    {
        nameSpace ??= DefaultNameSpace;

        //We will use the data type description as the remarks documentation for the class if available.
        var remarks = string.IsNullOrWhiteSpace(Description)
            ? string.Empty
            : $"""

               /// <remarks>
               /// {Description}
               /// </remarks>
               """;

        return
            $$"""
              using L5Sharp.Core;
              using System.Xml.Linq;
              // Auto-generated file
              // ReSharper disable InconsistentNaming
              // ReSharper disable PartialTypeWithSinglePart

              namespace {{nameSpace}};

              /// <summary>
              /// Represents a <c>{{TypeName}}</c> data type structure.
              /// </summary>{{remarks}}
              [LogixData("{{Name}}")]
              public sealed partial class {{TypeName}} : {{DerivedType}}
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
                  
              {{Members.GenerateProperties()}}
              }
              """;
    }
}