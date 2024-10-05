using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

// ReSharper disable InvertIf
// ReSharper disable UseIndexFromEndExpression
// ReSharper disable ConvertIfStatementToReturnStatement

namespace L5Sharp.Core;

/// <summary>
/// An object that contains scope information which identifies the location of an element in an L5X.
/// </summary>
/// <remarks>
/// <para>
/// Elements can be globally scoped (Controller), or locally scoped (Program/Routine).
/// Scope also includes the <see cref="Type"/> and <see cref="Name"/> of a given target element, therefore it can be
/// viewed as a sort of Uri or resource identifier for any given component/code element in a file.
/// </para>
/// <para>
/// Ultimately scope is defined by <see cref="Path"/> which resembles a Uri. Each part of the path is separated by a
/// '/' character. Paths can be absolute or relative (start with '/').
/// </para>
/// </remarks>
public sealed class Scope
{
    private const char PathSeparator = '/';

    /// <summary>
    /// Creates a default scope instance with empty values.
    /// </summary>
    private Scope()
    {
    }

    /// <summary>
    /// Creates a new scope instance initialized with the parts of the provided string path.
    /// </summary>
    private Scope(string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException("Scope path can not be null or empty.");

        var parts = path.Split(PathSeparator);

        Controller = DetermineController(parts);
        Program = DetermineProgram(parts);
        Routine = ParseRoutine(parts);
        Type = DetermineType(parts);
        Name = DetermineName(parts);
        Path = DeterminePath();
        Level = DetermineLevel();
        Container = DetermineContainer();
    }

    /// <summary>
    /// Creates a new scope instance initialized with the data from the provided element.
    /// </summary>
    private Scope(XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        Controller = GetScopedController(element);
        Program = GetScopedProgram(element);
        Routine = GetScopedRoutine(element);
        Type = GetScopedType(element);
        Name = GetScopedName(element);
        Path = DeterminePath();
        Level = DetermineLevel();
        Container = DetermineContainer();
    }

    /// <summary>
    /// The full path that uniquely identifies the scope of the element, or where in the L5X tree the element is found.
    /// </summary>
    /// <remarks>
    /// Path is esstentiall the "value" of any given scope and is used for equality checks. Path is built like a Uri
    /// using '/' characters to separate th segments of the path. Each part should reprent the name of the containing
    /// element, element type, or element name. Paths can be absolute or relative. Relative paths start with a '/'.
    /// </remarks>
    public string Path { get; } = string.Empty;

    /// <summary>
    /// The <see cref="ScopeLevel"/> indicating whether this is a controller, program, or routine scoped element.
    /// </summary>
    /// <remarks>
    /// You can also check the scope level by using the Is properties to more susinctly determine what "type"
    /// of scope this object represents.
    /// </remarks>
    /// <seealso cref="IsScoped"/>     
    /// <seealso cref="IsGlobal"/>
    /// <seealso cref="IsLocal"/>
    /// <seealso cref="IsProgram"/>
    /// <seealso cref="IsRoutine"/>
    public ScopeLevel Level { get; } = ScopeLevel.Null;

    /// <summary>
    /// The name of the controller, program, or routine that represents the immediate parent container of the element.
    /// </summary>
    /// <remarks>
    /// This value will be the same as <see cref="Controller"/>, <see cref="Program"/>, or <see cref="Routine"/>
    /// depending on what the <see cref="Level"/> is. This property is helpful for instances where you don't care about
    /// the level and just need to know the parent container scope name.
    /// </remarks>
    public string Container { get; } = string.Empty;

    /// <summary>
    /// Gets the name of the parent controller element for this scope instance.
    /// If no parent exists, then an empty string is returned.
    /// </summary>
    /// <remarks>
    /// Controller is the root portion of a scope path.
    /// For absolute paths, the first segment is considered the controller name.
    /// For relative paths (paths that start with '/') the controller name will be empty.
    /// </remarks>
    public string Controller { get; } = string.Empty;

    /// <summary>
    /// Gets the name of the parent program element for this scope instance.
    /// If no parent exists, then an empty string is returned.
    /// </summary>
    /// <remarks>
    /// Program represents the second portion of the scope path when there are more than 3 segments.
    /// Program should only be set for elements contained directly in a program, such as Tag or Routine.
    /// </remarks>
    public string Program { get; } = string.Empty;

    /// <summary>
    /// Gets the name of the parent routine element for this scope instance.
    /// If no parent exists, then an empty string is returned.
    /// </summary>
    /// <remarks>
    /// Routine will only be set for elements contained in routines, such as Rung, Line, or Sheet. This is because only
    /// code elements have <c>Routine</c> scope. Everything else is <c>Controller</c> or <c>Program</c> scoped.
    /// </remarks>
    public string Routine { get; } = string.Empty;

    /// <summary>
    /// Gets the <see cref="ScopeType"/> of the component or code element that identifies this scope.
    /// </summary>
    public ScopeType Type { get; } = ScopeType.Empty;

    /// <summary>
    /// Gets the name of the component or code element that identifies this scope. 
    /// </summary>
    /// <remarks>
    /// For components this will be the name, but for code elements (Rung/Line/Sheet) this will be the number.
    /// The return type is a <see cref="TagName"/> so that we can also parse the name if it represents a nested tag member.
    /// </remarks>
    public TagName Name { get; } = TagName.Empty;

    /// <summary>
    /// Indicates whether this scope path is relative, meaning it starts with a '/' path separator character.
    /// If the path is not relative, then it is considered absolute (starts with the controller name).
    /// </summary>
    public bool IsRelative => Path.StartsWith(PathSeparator);

    /// <summary>
    /// Indicates whether this element is scoped to an L5X container, meaning it is attached to an L5X document, or if
    /// it is an orphaned element. Basically, if the <see cref="ScopeLevel"/> is <c>Null</c> or not.
    /// </summary>
    /// <remarks>
    /// This is a helper so that we don't always need to write the full equality check.
    /// </remarks>
    public bool IsScoped => Level != ScopeLevel.Null;

    /// <summary>
    /// Indicates whether this is a global (Controller) scoped instance.
    /// This is in constrast to <see cref="IsLocal"/>.
    /// </summary>
    /// <remarks>
    /// This is a helper so that we don't always need to write the full equality check.
    /// </remarks>
    public bool IsGlobal => Level == ScopeLevel.Controller;

    /// <summary>
    /// Indicates whether this is a local (Program or Routine) scoped instance.
    /// This is in constrast to <see cref="IsGlobal"/>.
    /// </summary>
    /// <remarks>
    /// This is a helper so that we don't always need to write the full equality check.
    /// </remarks>
    public bool IsLocal => Level == ScopeLevel.Program || Level == ScopeLevel.Routine;

    /// <summary>
    /// Indicates whether this is a Program scoped instance (i.e. Level == Program).
    /// </summary>
    /// <remarks>
    /// This is a helper so that we don't always need to write the full equality check.
    /// Note that a scope can be ISLocal and IsProgram.
    /// </remarks>
    public bool IsProgram => Level == ScopeLevel.Program;

    /// <summary>
    /// Indicates whether this is a Routine scoped instance (i.e. Level == Routine).
    /// </summary>
    /// <remarks>
    /// This is a helper so that we don't always need to write the full equality check.
    /// Note that a scope can be IsLocal and IsRoutine.
    /// </remarks>
    public bool IsRoutine => Level == ScopeLevel.Routine;

    /// <summary>
    /// Creates default empty scope instance.
    /// </summary>
    public static Scope Empty => new();

    /// <summary>
    /// Creates a new <see cref="Scope"/> using the provided string path.
    /// </summary>
    /// <param name="path">The path that defines the scope to create.</param>
    /// <returns>A new <see cref="Scope"/> instance configured with the provided path.</returns>
    public static Scope To(string path) => new(path);

    /// <summary>
    /// Creates and returns a new <see cref="IScopeBuilder"/> that allows fluent configuration of a <c>Scope</c> instance. 
    /// </summary>
    /// <param name="controller">The optional name of the root controller element.
    /// If not provided, then the resulting scope will represent a relative path.</param>
    /// <returns>A <see cref="IScopeBuilder"/> fluent interface for building the scope instance.</returns>
    public static IScopeBuilder Build(string? controller = default)
    {
        return new ScopeBuider(controller ?? string.Empty);
    }

    /// <summary>
    /// Creates a <see cref="Scope"/> instance based on the provided <see cref="XElement"/> element.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> element representing the scope.</param>
    /// <returns>A <see cref="Scope"/> instance configured based on the position of the element in the XML tree.</returns>
    public static Scope Of(XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        return new Scope(element);
    }

    /// <summary>
    /// Appends the specified <see cref="Scope"/> to the current scope.
    /// </summary>
    /// <param name="scope">The scope to be appended.</param>
    /// <returns>A new <see cref="Scope"/> instance with the concatenated path.</returns>
    public Scope Append(Scope scope)
    {
        var left = Path.TrimEnd(PathSeparator);
        var right = scope.Path.TrimStart(PathSeparator);
        return new Scope(string.Concat(left, PathSeparator, right));
    }

    /// <summary>
    /// Determines if this scope has the provided container name as part of its path.
    /// </summary>
    /// <param name="container">The name of the container to check.</param>
    /// <returns><c>true</c> if this scope contains the provided container name; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This is just checking that the <see cref="Path"/> continas the provided string, so this could be the controller,
    /// program, or routine name. This could also be a combination of containers such as "ProgramName/RoutineName/".
    /// </remarks>
    public bool Contains(string container)
    {
        return Path.Contains(container);
    }

    /// <summary>
    /// Generates an XPath query based on the values of the Scope instance.
    /// </summary>
    /// <returns>The XPath query as a string.</returns>
    public string ToXPath()
    {
        var path = new StringBuilder();

        if (!string.IsNullOrEmpty(Controller))
        {
            path.Append($"/Controller[@Name='{Controller}']");
        }

        if (!Program.IsEmpty())
        {
            path.Append($"/Programs/Program[@Name='{Program}']");
        }

        if (!Routine.IsEmpty())
        {
            path.Append($"/Routines/Routine[@Name='{Routine}']");
        }

        if (!IsRoutine)
        {
            path.Append($"/{Type}s/{Type}[@Name='{Name}']");
        }

        if (IsRoutine)
        {
            path.Append($"/{Type}s/{Type}[@Number='{Name}']");
        }

        return path.ToString();
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Scope other => Path.IsEquivalent(other.Path),
            string path => Path.IsEquivalent(path),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => Path.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => Path;

    /// <summary>
    /// Implicit conversion from a string to a Scope instance.
    /// </summary>
    /// <param name="path">The string representing the path to be converted into a Scope instance.</param>
    /// <returns>A new Scope instance represented by the provided string path.</returns>
    public static implicit operator Scope(string path) => new(path);

    /// <summary>
    /// Implicit conversion from a TagName to a Scope instance.
    /// </summary>
    /// <param name="tagName">The TagName representing the path to be converted into a Scope instance.</param>
    /// <returns>A new Scope instance represented by the provided string path.</returns>
    public static implicit operator Scope(TagName tagName) => new(tagName);

    /// <summary>
    /// Implicit conversion from a Scope to a string.
    /// </summary>
    /// <param name="scope">The scope to be converted into a string value.</param>
    /// <returns>A <see cref="string"/> containing the path of the scope instance.</returns>
    public static implicit operator string(Scope scope) => scope.Path;


    #region Internal

    // --------------------------------------
    // Absolute
    // --------------------------------------
    //     Controller/Type/Name
    //     Controller/Program/Type/Name
    //     Controller/Program/Routine/Type/Name
    // --------------------------------------
    // Relative
    // --------------------------------------
    //     /Name
    //     /Type/Name
    //     /Program/Type/Name
    //     /Routine/Type/Name
    //     /Program/Routine/Type/Name
    // --------------------------------------
    // Special
    // --------------------------------------
    //     Name
    //     Type/Name
    // --------------------------------------


    /// <summary>
    /// Determines the controller name from the array of parts.
    /// When 3 or more parts are present, we consider the first item the controller name.
    /// This includes relative paths, since the first element will be an empty string.
    /// See examples above.
    /// </summary>
    private static string DetermineController(string[] parts)
    {
        return parts.Length switch
        {
            >= 3 => parts[0],
            _ => string.Empty
        };
    }

    /// <summary>
    /// Determines the program name from the array of parts.
    /// When 4 or more parts are present, we consider the second item the program name.
    /// For relative paths, we have to check whether the thrid item is a program element or not to differentiate from routine.
    /// See examples above. 
    /// </summary>
    private static string DetermineProgram(string[] parts)
    {
        return parts.Length switch
        {
            4 when parts[0].IsEmpty() && parts[2].IsProgramElement() => parts[1],
            4 when !parts[0].IsEmpty() => parts[1],
            > 4 => parts[1],
            _ => string.Empty
        };
    }

    /// <summary>
    /// Determines the routine name from the array of parts.
    /// When more than 4 parts are present, we consider the third item the routine name.
    /// For relative paths with length of 4, we also check the "type" part and see if it's a routine type.
    /// See examples above. 
    /// </summary>
    private static string ParseRoutine(string[] parts)
    {
        return parts.Length switch
        {
            4 when parts[0].IsEmpty() && parts[2].IsCodeElement() => parts[1],
            > 4 => parts[2],
            _ => string.Empty
        };
    }

    /// <summary>
    /// Determines the scope type of the provided path array.
    /// The type is always considered the second to last part of the array as long as there are at least 2 parts.
    /// If an invalid ScopeType is entered we will throw a parse exception to let the user know it's not a valid scope.
    /// See examples above.  
    /// </summary>
    private static ScopeType DetermineType(string[] parts)
    {
        var type = parts.Length >= 2 ? parts[parts.Length - 2] : string.Empty;
        return ScopeType.Parse(type);
    }

    /// <summary>
    /// Parses the name of the provided path array.
    /// The last part of the array is considered the name part.
    /// This includes the array containing a single item.
    /// </summary>
    private static string DetermineName(string[] parts)
    {
        return parts.Length > 0 ? parts[parts.Length - 1] : string.Empty;
    }

    /// <summary>
    /// Determines the <see cref="ScopeLevel"/> of this instance based on the configured scope names.
    /// </summary>
    private ScopeLevel DetermineLevel()
    {
        if (!string.IsNullOrEmpty(Routine)) return ScopeLevel.Routine;
        if (!string.IsNullOrEmpty(Program)) return ScopeLevel.Program;
        if (!string.IsNullOrEmpty(Controller)) return ScopeLevel.Controller;
        return ScopeLevel.Null;
    }

    /// <summary>
    /// Determines the container of this instance based on the configured scope names.
    /// </summary>
    private string DetermineContainer()
    {
        if (Level == ScopeLevel.Routine) return Routine;
        if (Level == ScopeLevel.Program) return Program;
        if (Level == ScopeLevel.Controller) return Controller;
        return string.Empty;
    }

    /// <summary>
    /// Generates the full path for the scoped element using the configured properties.
    /// </summary>
    private string DeterminePath()
    {
        var builder = new StringBuilder();

        builder.Append(Controller);

        if (!Program.IsEmpty())
            builder.Append(PathSeparator).Append(Program);

        if (!Routine.IsEmpty())
            builder.Append(PathSeparator).Append(Routine);

        builder.Append(PathSeparator).Append(Type);
        builder.Append(PathSeparator).Append(Name);

        return builder.ToString();
    }

    /// <summary>
    /// Gets the name of the containing controller for the provided element.
    /// </summary>
    private static string GetScopedController(XElement node)
    {
        var element = node.Ancestors(L5XName.Controller).FirstOrDefault();
        return element?.LogixName() ?? string.Empty;
    }

    /// <summary>
    /// Gets the name of the containing program or instruction for the provided element.
    /// </summary>
    private static string GetScopedProgram(XElement node)
    {
        var element = node.Ancestors()
            .FirstOrDefault(e => e.L5XType() is L5XName.Program or L5XName.AddOnInstructionDefinition);

        return element?.LogixName() ?? string.Empty;
    }

    /// <summary>
    /// Gets the name of the containing routine for the provided element.
    /// </summary>
    private static string GetScopedRoutine(XElement node)
    {
        var element = node.Ancestors(L5XName.Routine).FirstOrDefault();
        return element?.LogixName() ?? string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    private static ScopeType GetScopedType(XElement element)
    {
        return ScopeType.TryParse(element.L5XType()) ?? ScopeType.Empty;
    }

    /// <summary>
    /// Gets the name or number of the current element depending on which attribute exists.
    /// </summary>
    private static string GetScopedName(XElement node)
    {
        if (node.Attribute(L5XName.Name) is not null)
            return node.LogixName();

        return node.Attribute(L5XName.Number) is not null
            ? node.Attribute(L5XName.Number)?.Value ?? string.Empty
            : string.Empty;
    }

    #endregion
}