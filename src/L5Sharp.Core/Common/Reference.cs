using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

// ReSharper disable ReplaceSubstringWithRangeIndexer
// ReSharper disable InvertIf
// ReSharper disable UseIndexFromEndExpression
// ReSharper disable ConvertIfStatementToReturnStatement

namespace L5Sharp.Core;

/// <summary>
/// Represents a unique path to a logix entity in the L5X context. This type provides properties to help analyze or
/// identify the reference type and Identifier, including scope and id information.
/// </summary>
/// <remarks>
/// This type is a simple wrapper around an  <see cref="XPathExpression"/> which can be used to select the
/// specific element from the L5X document.
/// </remarks>
public sealed class Reference
{
    /// <summary>
    /// The character that separates the segments of the path.
    /// </summary>
    private const char Slash = '/';
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Reference"/> class with the specified parameters.
    /// </summary>
    /// <param name="type">The <see cref="ReferenceType"/> that identifies which element type the reference is targeting.</param>
    /// <param name="scope">The <see cref="Scope"/> that defines the contextual container within which the reference exists.</param>
    /// <param name="id">The unique identifier of the component or code element. For components, this is the name; for code elements (Rung/Line/Sheet), this is the number.</param>
    /// <param name="fragment">An optional <see cref="Instruction"/> that represents the logic instruction associated with the reference. Defaults to <see cref="Instruction.Unkown"/> if not provided.</param>
    /// <exception cref="ArgumentException">Thrown when the path cannot be built from the provided parameters.</exception>
    private Reference(ReferenceType type, Scope scope, string id, string? fragment = null)
    {
        Path = BuildPath(type, scope, id, fragment);
        Type = type;
        Scope = scope;
        Id = id;
        Fragment = fragment;
    }
    
    /// <summary>
    /// Gets the string representation of the reference path, which uniquely identifies the location and type of logix entity.
    /// </summary>
    /// <value>
    /// A string in the format <c>type://[container/][routine/]id[#fragment]</c> where type is the reference type (e.g., tag, rung),
    /// optional container and routine define the scope, id is the unique identifier, and optional fragment provides additional specificity.
    /// </value>
    public string Path { get; }

    /// <summary>
    /// Gets the type of the reference, which indicates the kind of logix entity being referenced (e.g., Tag, Rung, DataType, Module).
    /// </summary>
    /// <value>
    /// A <see cref="ReferenceType"/> value that categorizes the referenced entity.
    /// </value>
    public ReferenceType Type { get; }

    /// <summary>
    /// Gets the scope that defines the contextual container within which the reference exists.
    /// </summary>
    /// <value>
    /// A <see cref="Scope"/> object representing the hierarchical location (Controller, Program, or Routine level) of the referenced entity.
    /// </value>
    public Scope Scope { get; }

    /// <summary>
    /// Gets the unique identifier of the referenced entity within its scope.
    /// </summary>
    /// <value>
    /// A string containing the name for components (tags, data types, modules) or the number for code elements (rungs, lines, sheets).
    /// </value>
    public string Id { get; }

    /// <summary>
    /// Gets the optional fragment that provides additional specificity to the reference, such as a particular instruction or sub-element.
    /// </summary>
    /// <value>
    /// A string representing the fragment portion of the reference path, or null if no fragment was specified.
    /// </value>
    public string? Fragment { get; }

    /// <summary>
    /// Converts this reference to a new reference scoped to the same container as the provided <see cref="Scope"/> object.
    /// </summary>
    /// <param name="scope">The scope object that defines the context for the reference.</param>
    /// <returns>A new <see cref="Reference"/> instance representing the constructed path within the specified scope.</returns>
    /// <remarks>
    /// This method will only handle assigning the container portion of the new path since <see cref="Scope"/> knows
    /// nothing about the reference type (i.e., scope doesn't container Routine level info).
    /// </remarks>
    public Reference ToScope(Scope scope)
    {
        return new Reference(Type, scope, Id, Fragment);
    }

    /// <summary>
    /// Creates a new <see cref="Reference"/> instance with the specified fragment appended to the existing path.
    /// </summary>
    /// <param name="fragment">The additional fragment to be appended to the reference path.
    /// This identifies a specific part or detail of the target entity.</param>
    /// <returns>A new <see cref="Reference"/> instance with the updated path, including the provided fragment.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided fragment is null or empty.</exception>
    public Reference At(string fragment)
    {
        if (string.IsNullOrEmpty(fragment))
            throw new ArgumentException();

        return new Reference(Type, Scope, Id, fragment);
    }

    /// <summary>
    /// Determines if the current <see cref="Reference"/> instance contains a valid logic instruction and outputs that instruction.
    /// </summary>
    /// <param name="logic">When this method returns, contains the <see cref="Instruction"/> parsed from the reference, if valid; otherwise, null.</param>
    /// <returns>True if the <see cref="Reference"/> contains a valid logic instruction; otherwise, false.</returns>
    public bool HasLogic(out Instruction logic)
    {
        if (Type.IsLogic && Instruction.TryParse(Fragment, out logic))
        {
            return true;
        }

        logic = null!;
        return false;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is not Reference other)
            return false;

        return Type == other.Type &&
               Scope == other.Scope &&
               StringComparer.OrdinalIgnoreCase.Equals(Id, other.Id) && 
               StringComparer.OrdinalIgnoreCase.Equals(Fragment, other.Fragment);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var typeHash = Type.GetHashCode();
        var scopeHash = Scope.GetHashCode();
        var idHash = StringComparer.OrdinalIgnoreCase.GetHashCode(Id);
        var fragmentHash = Fragment is not null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Fragment) : 0;
        
        return typeHash ^ scopeHash ^ idHash ^ fragmentHash;
    }

    /// <inheritdoc />
    public override string ToString() => Path;

    /// <summary>
    /// Generates the XML XPath representation of the reference based on its scope, type, and associated elements.
    /// </summary>
    /// <returns>
    /// A string containing the computed XPath that represents the hierarchical structure and corresponding
    /// elements of the reference within the context of the controller or program.
    /// </returns>
    public string ToXPath()
    {
        var builder = new StringBuilder();
        
        builder.Append(L5XName.Controller);
        builder.AppendIf(Scope, s => s.IsProgram, s => $"/Programs/Program[@Name='{s.Container}']");
        builder.AppendIf(Scope, s => s.IsAoi, s => $"/AddOnInstructionDefinitions/AddOnInstructionDefinition[@Name='{s.Container}']");
        builder.AppendIf(Scope, s => s.IsLogic, s => $"/Routines/Routine[@Name='{s.Routine}']");
        builder.Append(Type.GetXPath(Id));

        return builder.ToString();
    }

    /// <summary>
    /// Creates a <see cref="Reference"/> from the specified path string by parsing its content to extract relevant components.
    /// </summary>
    /// <param name="path">The path string used to identify and construct the reference.</param>
    /// <returns>A new <see cref="Reference"/> instance constructed from the specified path.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided path is null, empty, or is not in a valid format for parsing.
    /// </exception>
    public static Reference To(string path) => ParsePath(path);

    /// <summary>
    /// Creates a new <see cref="Reference"/> instance for a specified logix component by name and scope.
    /// </summary>
    /// <typeparam name="TComponent">The type of the logix component for which the reference is being created. Must inherit from <see cref="LogixComponent{TComponent}"/>.</typeparam>
    /// <param name="name">The name of the component to reference. Cannot be null or empty.</param>
    /// <param name="scope">The scope within which the component exists. If null, defaults to <see cref="Scope.Controller"/>.</param>
    /// <returns>A <see cref="Reference"/> object representing the specified component.</returns>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="name"/> is null or empty.</exception>
    /// <remarks>
    /// This factory method uses the type parameter to determine the reference type and combines it with the
    /// provided name and scope to construct a reference path. If no scope is specified, the reference defaults
    /// to the controller scope.
    /// </remarks>
    public static Reference To<TComponent>(string name, Scope? scope = null) 
        where TComponent : LogixComponent<TComponent>
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name is required to build valid reference.");
        
        var type = ReferenceType.FromType<TComponent>();
        scope ??= Scope.Controller;
        return new Reference(type, scope, name);
    }

    /// <summary>
    /// Creates a new <see cref="Reference"/> instance for a specified logix code element by number, program, and routine.
    /// </summary>
    /// <typeparam name="TCode">The type of the logix code element for which the reference is being created. Must inherit from <see cref="LogixCode{TCode}"/>.</typeparam>
    /// <param name="number">The number that identifies the code element (Rung/Line/Sheet).</param>
    /// <param name="program">The name of the program that contains the routine. Cannot be null or empty.</param>
    /// <param name="routine">The name of the routine that contains the code element. Cannot be null or empty.</param>
    /// <returns>A <see cref="Reference"/> object representing the specified code element.</returns>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="program"/> or <paramref name="routine"/> is null or empty.</exception>
    /// <remarks>
    /// This factory method uses the type parameter to determine the reference type (Rung, Line, or Sheet) and combines it with the
    /// provided number, program, and routine to construct a reference path for a code element within a routine.
    /// </remarks>
    public static Reference To<TCode>(uint number, string program, string routine) 
        where TCode : LogixCode<TCode>
    {
        if (string.IsNullOrEmpty(program))
            throw new ArgumentException("Program is required to build valid reference.");
        
        if (string.IsNullOrEmpty(routine))
            throw new ArgumentException("Routine is required to build valid reference.");
        
        var type = ReferenceType.FromType<TCode>();
        var scope = Scope.Program(program, routine);
        return new Reference(type, scope, number.ToString());
    }

    /// <summary>
    /// Creates a new <see cref="Reference"/> instance for a tag identified by the specified <see cref="TagName"/>.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> object representing the tag to reference. Cannot be null.</param>
    /// <returns>A <see cref="Reference"/> object representing the specified tag within its associated scope.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="tagName"/> is null.</exception>
    /// <remarks>
    /// This factory method extracts the scope and base name from the provided <see cref="TagName"/> to construct
    /// a reference path for the tag. The reference uses the tag's scope context and base identifier.
    /// </remarks>
    public static Reference To(TagName tagName)
    {
        if (tagName is null)
            throw new ArgumentNullException(nameof(tagName));

        return new Reference(ReferenceType.Tag, tagName.Scope, tagName.LocalPath);
    }


    /// <summary>
    /// Creates a new <see cref="Reference"/> instance from the specified XML element.
    /// </summary>
    /// <param name="element">The XML element representing a logix entity for which the reference is being created. Cannot be null.</param>
    /// <returns>A <see cref="Reference"/> object representing the specified XML element within its associated scope.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="element"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the element's local name does not correspond to a valid reference type.</exception>
    /// <remarks>
    /// This factory method extracts the reference type, scope, and identifier from the provided XML element to construct
    /// a reference path. Module tags are treated as a special case and are converted to controller-scoped tag references.
    /// </remarks>
    public static Reference To(XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));
        
        //Module tags are a special case. We need to view these as controller tags.
        if (element.IsModuleTagElement())
            return new Reference(ReferenceType.Tag, Scope.Controller, element.ModuleTagName());
        
        if (!ReferenceType.TryParse(element.Name.LocalName, out var type))
            throw new ArgumentException($"Provided element {element.Name.LocalName} is not a valid reference type.");
        
        var scope = Scope.Of(element);
        var id = element.LogixId();
        
        return new Reference(type, scope, id);
    }

    /// <summary>
    /// Determines if the provided objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are equal; otherwise, false.</returns>
    public static bool operator ==(Reference? left, Reference? right) => Equals(left, right);

    /// <summary>
    /// Determines if the provided objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
    public static bool operator !=(Reference? left, Reference? right) => !Equals(left, right);

    /// <summary>
    /// Implicit conversion from a string to a Scope instance.
    /// </summary>
    /// <param name="path">The string representing the path to be converted into a Scope instance.</param>
    /// <returns>A new Scope instance represented by the provided string path.</returns>
    public static implicit operator Reference(string path) => ParsePath(path);

    /// <summary>
    /// Implicit conversion from a Scope to a string.
    /// </summary>
    /// <param name="reference">The scope to be converted into a string value.</param>
    /// <returns>A <see cref="string"/> containing the path of the scope instance.</returns>
    public static implicit operator string(Reference reference) => reference.Path;

    /// <summary>
    /// Builds a path string representation based on the provided reference type, scope, and id.
    /// </summary>
    private static string BuildPath(ReferenceType type, Scope scope, string id, string? fragment)
    {
        var builder = new StringBuilder();

        builder.Append($"{type.Name}://");
        builder.AppendIf(scope, s => s.IsLocal, s => $"{s.Container}/");
        builder.AppendIf(scope, s => s.IsLogic, s => $"{s.Routine}/");
        builder.Append(id);
        builder.AppendIf(fragment, x => x is not null, x => $"#{x}");

        return builder.ToString();
    }

    /// <summary>
    /// Parses a string path into a <see cref="Reference"/> instance.
    /// </summary>
    /// <remarks>
    /// The path must be in the format type://scope/id#fragment where <c>type</c> is the reference type,
    /// <c>scope</c> is either a controller, program, or AOI container, and <c>id</c> specifies the target
    /// reference. Optional additional segments may define further specificity to the reference context.
    /// </remarks>
    private static Reference ParsePath(string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException("Can not parse Reference from null or empty path");

        var schemaIndex = path.IndexOf("://");
        if (schemaIndex <= 0)
            throw new FormatException($"Invalid reference path '{path}'. Expected format 'Type://[Container/][Routine/]Id'");
        
        var typeName = path.Substring(0, schemaIndex);
        if (!ReferenceType.TryParse(typeName, out var type))
            throw new ArgumentException($"Invalid reference type '{typeName}' in path '{path}'");
        
        // Calculate where the path data actually starts and ends using the schema index and fragment index (or end of string)
        var fragmentIndex = path.IndexOf("#");
        var pathStart = schemaIndex + 3;
        var pathEnd = fragmentIndex > 0 ? fragmentIndex : path.Length;
        var pathLength = pathEnd - pathStart;
        var segments = path.Substring(pathStart, pathLength).Split(Slash, StringSplitOptions.RemoveEmptyEntries);
        
        var id = segments.Last();
        var fragment = fragmentIndex > 0 ? path.Substring(fragmentIndex + 1) : null;

        var scope = segments.Length switch
        {
            1 => Scope.Controller,
            2 => Scope.Program(segments[0]),
            3 => Scope.Program(segments[0], segments[1]),
            _ => throw new ArgumentOutOfRangeException(nameof(segments), segments.Length, 
                $"Invalid number of path segments: expected 1-3, but got {segments.Length}")
        };

        return new Reference(type, scope, id, fragment);
    }
}