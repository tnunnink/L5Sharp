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
    DataTypeFamily Family,
    IEnumerable<LogixMemberInfo> Members,
    string? Description = null)
{
    private const string DefaultNameSpace = "L5Sharp.Data.Generated";

    public string TypeName => Name.SanitizeName();
    public string Name { get; } = Name;
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
        var family = dataType.Family;
        var description = dataType.Description;
        var members = dataType.Members.Select(LogixMemberInfo.From);

        return new LogixTypeInfo(name, family, members, description);
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

        return new LogixTypeInfo(name, DataTypeFamily.None, members, description);
    }

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
        var implementation = Family == DataTypeFamily.String ? GenerateStringClass() : GenerateStandardClass();
        var remarks = GetClassRemarks(Description);

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
              {{implementation}}
              }
              """;
    }

    /// <summary>
    /// Generates formatted class remarks based on the description of the Logix type.
    /// </summary>
    /// <returns>
    /// A formatted string containing the remarks section of the generated class documentation,
    /// or an empty string if no description is available.
    /// </returns>
    private static string GetClassRemarks(string? description)
    {
        if (description is null || string.IsNullOrWhiteSpace(description))
            return string.Empty;

        return
            $"""

             /// <remarks>
             /// {description.Replace("\n", "\n/// ")}
             /// </remarks>
             """;
    }

    /// <summary>
    /// Generates the implementation of a standard class structure for a Logix data type,
    /// including member initializations and property definitions.
    /// </summary>
    /// <returns>
    /// A string containing the source code for the standard class implementation
    /// based on the associated Logix data type.
    /// </returns>
    private string GenerateStandardClass()
    {
        return
            $$"""
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
              """;
    }

    /// <summary>
    /// Generates the source code for the string class representation of the current <see cref="LogixTypeInfo"/> instance.
    /// </summary>
    /// <returns>
    /// A string containing the source code for the string-specific implementation of the class.
    /// </returns>
    private string GenerateStringClass()
    {
        var capacity = Members.SingleOrDefault(m => m.Name == "DATA")?.Dimension ?? 0;
        
        return
            $$"""
                  /// <summary>
                  /// Creates a new <see cref="{{TypeName}}"/> instance initialized with default values.
                  /// </summary>
                  public {{TypeName}}() : base("{{Name}}")
                  {
                  }
                  
                  /// <summary>
                  /// Creates a new <see cref="{{TypeName}}"/> instance initialized with the provided value.
                  /// </summary>
                  public {{TypeName}}(string value) : base("{{Name}}", value)
                  {
                  }
                  
                  /// <summary>
                  /// Creates a new <see cref="{{TypeName}}"/> instance initialized with the provided element.
                  /// </summary>
                  public {{TypeName}}(XElement element) : base(element)
                  {
                  }

                  /// <inheritdoc />
                  public override int Capacity => {{capacity}};
              """;
    }
}