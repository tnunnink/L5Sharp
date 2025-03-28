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
    /// <summary>
    /// The character that separates the segments of the path.
    /// </summary>
    private const char PathSeparator = '/';

    /// <summary>
    /// The array of string segments that make up the scope path.
    /// </summary>
    private readonly string[] _segments = [];

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

        Path = path;
        _segments = Path.Split(PathSeparator).ToArray();
    }

    /// <summary>
    /// Creates a new scope instance initialized with the data from the provided element.
    /// </summary>
    private Scope(XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        Path = DeterminePath(element);
        _segments = Path.Split(PathSeparator).ToArray();
    }

    /// <summary>
    /// The full path that uniquely identifies the scope of the element, or where in the L5X tree the element is found.
    /// </summary>
    /// <remarks>
    /// Path is essentially the "value" of any given scope and is used for equality checks. Path is built like a Uri
    /// using '/' characters to separate th segments of the path. Each part should represent the name of the containing
    /// element, element type, or element name. Paths can be absolute or relative. Relative paths start with a '/'.
    /// </remarks>
    public string Path { get; } = string.Empty;

    /// <summary>
    /// The <see cref="ScopeLevel"/> indicating whether this is a controller, program, or routine scoped element.
    /// </summary>
    /// <remarks>
    /// You can also check the scope level by using the Is properties to more succinctly determine what "type"
    /// of scope this object represents.
    /// </remarks>
    /// <seealso cref="IsScoped"/>     
    /// <seealso cref="IsGlobal"/>
    /// <seealso cref="IsLocal"/>
    public ScopeLevel Level => DetermineLevel();

    /// <summary>
    /// The name of the controller, program, or routine that represents the immediate parent container of the element.
    /// </summary>
    /// <remarks>
    /// This value will be the same as <see cref="Controller"/>, <see cref="Program"/>, or <see cref="Routine"/>
    /// depending on what the <see cref="Level"/> is. This property is helpful for instances where you don't care about
    /// the level and just need to know the parent container scope name.
    /// </remarks>
    public string Container => DetermineContainer();

    /// <summary>
    /// Gets the local path of the scope object. This is just the <see cref="Path"/> without the
    /// <c>Controller</c> potion. (i.e. relative path).
    /// </summary>
    public string LocalPath => DetermineLocalPath();

    /// <summary>
    /// The name of the controller the element is scoped to.
    /// For relative or unscoped paths, this value will be empty.
    /// </summary>
    /// <remarks>
    /// Controller is the root portion of a scope path.
    /// The first segment is considered the controller name.
    /// For relative paths (paths that start with '/') the controller name will be empty.
    /// </remarks>
    public string Controller => DetermineController();

    /// <summary>
    /// Gets the name of the parent program element for this scope instance.
    /// If no parent exists, then an empty string is returned.
    /// </summary>
    /// <remarks>
    /// Program represents the second portion of the scope path when there are more than 3 segments.
    /// Program should only be set for elements contained directly in a program, such as Tag or Routine.
    /// </remarks>
    public string Program => DetermineProgram();

    /// <summary>
    /// Gets the name of the parent routine element for this scope instance.
    /// If no parent exists, then an empty string is returned.
    /// </summary>
    /// <remarks>
    /// Routine will only be set for elements contained in routines, such as Rung, Line, or Sheet. This is because only
    /// code elements have <c>Routine</c> scope. Everything else is <c>Controller</c> or <c>Program</c> scoped.
    /// </remarks>
    public string Routine => DetermineRoutine();

    /// <summary>
    /// Gets the <see cref="ScopeType"/> that identifies what type of element this scope represents
    /// (e.g., Tag, Rung, Program, etc.).
    /// </summary>
    public ScopeType Type => DetermineType();

    /// <summary>
    /// Gets the name of the component or code element that identifies this scope. 
    /// </summary>
    /// <remarks>
    /// For components this will be the name, but for code elements (Rung/Line/Sheet) this will be the number.
    /// The return type is a <see cref="TagName"/> so that we can also parse the name if it represents a nested tag member.
    /// </remarks>
    public TagName Name => DetermineName();

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
    /// This is in contrast to <see cref="IsLocal"/>.
    /// </summary>
    /// <remarks>
    /// This is a helper so that we don't always need to write the full equality check.
    /// </remarks>
    public bool IsGlobal => Level == ScopeLevel.Controller;

    /// <summary>
    /// Indicates whether this is a local (Program or Routine) scoped instance.
    /// This is in contrast to <see cref="IsGlobal"/>.
    /// </summary>
    /// <remarks>
    /// This is a helper so that we don't always need to write the full equality check.
    /// </remarks>
    public bool IsLocal => Level == ScopeLevel.Program || Level == ScopeLevel.Routine;

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
    /// Creates a <see cref="Scope"/> instance based on the provided <see cref="XElement"/> element.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> element representing the scope.</param>
    /// <returns>A <see cref="Scope"/> instance configured based on the position of the element in the XML tree.</returns>
    public static Scope Of(XElement element) => new(element);

    /// <summary>
    /// Creates and returns a new <see cref="IScopeBuilder"/> that allows fluent configuration of a <c>Scope</c> instance. 
    /// </summary>
    /// <param name="controller">The optional name of the root controller element.
    /// If not provided, then the resulting scope will represent a relative path.</param>
    /// <returns>A <see cref="IScopeBuilder"/> fluent interface for building the scope instance.</returns>
    public static IScopeBuilder Build(string? controller = null) => new ScopeBuilder(controller ?? string.Empty);

    /// <summary>
    /// Determines whether the current scope is shared with another scope by comparing the controller and program names.
    /// </summary>
    /// <param name="other">The other scope to compare with.</param>
    /// <returns>Returns true if the controller and program names of both scopes are the same; otherwise, false.</returns>
    public bool Shares(Scope other)
    {
        return Controller == other.Controller && Program == other.Program;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scope"></param>
    /// <returns></returns>
    public bool IsVisibleTo(Scope scope)
    {
        if (Controller != scope.Controller) return false;
        if (IsGlobal || scope.IsGlobal) return true;
        return Program == scope.Program;
    }

    /// <summary>
    /// Appends the provided <see cref="Scope"/> to the current scope by concatenating the two scope paths,
    /// and removing any redundant '/' separator.
    /// </summary>
    /// <param name="scope">The scope to be appended.</param>
    /// <returns>A new <see cref="Scope"/> instance with the concatenated path.</returns>
    public Scope Append(Scope scope)
    {
        if (scope is null) throw new ArgumentNullException(nameof(scope));
        var left = Path.TrimEnd(PathSeparator);
        var right = scope.Path.TrimStart(PathSeparator);
        return new Scope(string.Concat(left, PathSeparator, right));
    }

    /// <summary>
    /// Merges the provided <see cref="Scope"/> with the current scope by taking the segments of the provided scope
    /// that are not empty, and using the 
    /// </summary>
    /// <param name="scope">The scope to be merged.</param>
    /// <returns>A new <see cref="Scope"/> instance with the merged path.</returns>
    public Scope Merge(Scope scope)
    {
        if (scope is null) throw new ArgumentNullException(nameof(scope));

        var controller = !scope.Controller.IsEmpty() ? scope.Controller : Controller;
        var program = !scope.Program.IsEmpty() ? scope.Program : Program;
        var routine = !scope.Routine.IsEmpty() ? scope.Routine : Routine;
        var type = scope.Type != ScopeType.Null ? scope.Type : Type;
        var name = scope.Name != TagName.Empty ? scope.Name : Name;

        var path = BuildPath(controller, program, routine, type, name);
        return new Scope(path);
    }

    /// <summary>
    /// Determines if the current Scope is within another Scope based on their Container values.
    /// </summary>
    /// <param name="other">The other Scope to compare against.</param>
    /// <returns>True if the current Scope is within the other Scope based on their Container values, false otherwise.</returns>
    public bool IsIn(Scope? other)
    {
        return other?.Container.Contains(Container) is true;
    }

    /// <summary>
    /// Generates an XPath query based on the values of the Scope instance.
    /// </summary>
    /// <returns>The XPath query as a string.</returns>
    public string ToXPath()
    {
        var path = new StringBuilder();

        if (!Controller.IsEmpty())
            path.Append($"/Controller[@Name='{Controller}']");

        if (!Program.IsEmpty())
            path.Append($"/Programs/Program[@Name='{Program}']");

        if (!Routine.IsEmpty())
            path.Append($"/Routines/Routine[@Name='{Routine}']");

        if (Type != ScopeType.Null)
            path.Append($"/{Type}s");

        if (Name != TagName.Empty)
            path.Append(Level != ScopeLevel.Routine ? $"/{Type}[@Name='{Name}']" : $"/{Type}[@Number='{Name}']");

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
    /// Determines if the provided objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are equal; otherwise, false.</returns>
    public static bool operator ==(Scope? left, Scope? right) => Equals(left, right);

    /// <summary>
    /// Determines if the provided objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
    public static bool operator !=(Scope? left, Scope? right) => !Equals(left, right);

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
    //  Controller/Type/Name
    //  Controller/Program/Type/Name
    //  Controller/Program/Routine/Type/Name
    // --------------------------------------
    // Relative
    // --------------------------------------
    //  /Name
    //  /Type/Name
    //  /Program/Type/Name
    //  /Routine/Type/Name
    //  /Program/Routine/Type/Name
    // --------------------------------------
    // Partial
    // --------------------------------------
    //  /Type/
    //  /Program//
    //  /Routine//
    //  /Program/Type/
    //  /Routine/Type/
    //  /Program/Routine/Type/
    //  
    //  Controller//
    //  Controller/Type/
    //  Controller/Program//
    //  Controller/Program/Routine//
    // --------------------------------------


    /// <summary>
    /// Determines the controller name based on the number of segments.
    /// If present, the controller is always the first value of the segment array.
    /// This includes relative paths, since the first element will be an empty string.
    /// If there are 2 or fewer segments we won't have a controller name.
    /// </summary>
    private string DetermineController()
    {
        return _segments.Length switch
        {
            > 2 => _segments[0],
            _ => string.Empty
        };
    }

    /// <summary>
    /// Determines the program name based on the number of segments and current Type.
    /// If present, the program is always the second value of the segment array.
    /// Program is only assumed in a scope with 4 or more segments.
    /// When we have exactly 4 segments and the type is a routine type, we assume no program name.
    /// </summary>
    private string DetermineProgram()
    {
        return _segments.Length switch
        {
            > 4 => _segments[1],
            4 when !Type.InRoutine => _segments[1],
            _ => string.Empty
        };
    }

    /// <summary>
    /// Determines the program name based on the number of segments and current Type.
    /// If present, the routine is either the second or third value of the segment array.
    /// Routine is only assumed in a scope with 4 or more segments.
    /// When we have exactly 4 segments and the type is a routine type, the second segment the routine name.
    /// </summary>
    private string DetermineRoutine()
    {
        return _segments.Length switch
        {
            > 4 => _segments[2],
            4 when Type.InRoutine => _segments[1],
            _ => string.Empty
        };
    }

    /// <summary>
    /// Determines the scope type of the provided path array.
    /// The type is always considered the second to last part of the array as long as there are at least 2 parts.
    /// If an invalid ScopeType is entered we will throw a parse exception to let the user know it's not a valid scope.
    /// See examples above.  
    /// </summary>
    private ScopeType DetermineType()
    {
        var type = _segments.Length switch
        {
            >= 2 => _segments[_segments.Length - 2],
            _ => string.Empty
        };

        return ScopeType.TryParse(type) ?? ScopeType.Null;
    }

    /// <summary>
    /// Determines the name of the path based on the number of segments.
    /// If present, the name is the last segment of the array.
    /// This could technically be an empty string for cases like "/Program/Type/"
    /// If we only get single string with no separator then it will be considered the name.
    /// </summary>
    private string DetermineName()
    {
        return _segments.Length > 0 ? _segments[_segments.Length - 1] : string.Empty;
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
    /// Determines the container of the path based on the number of segments.
    /// The container is really the portion of the path that excludes type/name
    /// </summary>
    private string DetermineContainer()
    {
        return _segments.Length switch
        {
            // ReSharper disable once ReplaceSubstringWithRangeIndexer not supported by .NET Standard 2.0
            > 1 => Path.Substring(0, Path.LastIndexOf(PathSeparator) + 1),
            _ => string.Empty
        };
    }

    /// <summary>
    /// Determines the local path based on the current scope Path.
    /// </summary>
    private string DetermineLocalPath()
    {
        var index = Path.IndexOf(PathSeparator);
        return index > -1 ? Path.Substring(index, Path.Length - index) : Path;
    }

    /// <summary>
    /// Gets the name of the containing controller for the provided element.
    /// </summary>
    private static string DeterminePath(XElement node)
    {
        var controller = node.Ancestors(L5XName.Controller).FirstOrDefault()?.LogixName() ?? string.Empty;
        var program = node.Ancestors().FirstOrDefault(IsProgram)?.LogixName() ?? string.Empty;
        var routine = node.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty;
        var type = node.IsTagElement() ? ScopeType.Tag : ScopeType.Parse(node.Name.LocalName);
        var name = IsCode(node)
            ? node.Attribute(L5XName.Number)?.Value ?? string.Empty
            : node.IsTagElement()
                ? node.TagName()
                : node.LogixName();

        return BuildPath(controller, program, routine, type, name);

        bool IsProgram(XElement element) => element.Name.LocalName is L5XName.Program or L5XName.AddOnInstruction;
        bool IsCode(XElement element) => element.Name.LocalName is L5XName.Rung or L5XName.Line or L5XName.Sheet;
    }

    /// <summary>
    /// Given the segments, build the path for the scope. Only append the program, routine, or type if the values
    /// are not empty. 
    /// </summary>
    private static string BuildPath(string controller, string program, string routine, ScopeType type, string name)
    {
        var builder = new StringBuilder();

        builder.Append(controller);
        if (!program.IsEmpty()) builder.Append(PathSeparator).Append(program);
        if (!routine.IsEmpty()) builder.Append(PathSeparator).Append(routine);
        if (type != ScopeType.Null) builder.Append(PathSeparator).Append(type);
        builder.Append(PathSeparator).Append(name);

        return builder.ToString();
    }

    #endregion
}