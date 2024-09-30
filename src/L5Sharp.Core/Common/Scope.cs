using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

// ReSharper disable InvertIf
// ReSharper disable UseIndexFromEndExpression
// ReSharper disable ConvertIfStatementToReturnStatement

namespace L5Sharp.Core;

/// <summary>
/// An object that contains scope information which identifies where in an L5X file an element exists. Elements can be
/// globally scoped (Controller), or locally scoped (Program/Routine). Scope also includes the <see cref="Type"/> and
/// <see cref="Name"/> of a given target element, therefore it can be viewed as a sort of Uri or resource identifier
/// for any given component/code element in a file.
/// </summary>
/// <remarks>
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
            throw new ArgumentException("Path can not be null or empty.");

        var parts = path.Split(PathSeparator);

        if (parts.Length is < 3 or > 5)
            throw new ArgumentOutOfRangeException(
                $"The provided scope {Path} has an invalid length. Expecting between 3 and 5 segments.");

        Controller = ParseController(parts);
        Program = ParseProgram(parts);
        Routine = ParseRoutine(parts);
        Type = ParseElement(parts);
        Name = ParseName(parts);
        Path = DeterminePath();
        Level = DetermineType();
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
        Type = element.Name.LocalName;
        Name = GetScopedName(element);
        Task = GetScopedTask(element);
        Path = DeterminePath();
        Level = DetermineType();
        Container = DetermineContainer();
    }

    /// <summary>
    /// The full path that identifies the scope of the element.
    /// </summary>
    public string Path { get; } = string.Empty;

    /// <summary>
    /// The <see cref="ScopeLevel"/> value indicates whether this scope is a controller, program, or instruction scoped
    /// element.
    /// </summary>
    public ScopeLevel Level { get; } = ScopeLevel.Null;

    /// <summary>
    /// The name of the controller, program, or routine that represents the scope or immediate parent container
    /// of the element. 
    /// </summary>
    public string Container { get; } = string.Empty;

    /// <summary>
    /// Gets the scoped controller name of this element.
    /// </summary>
    /// <remarks>
    /// Controller is the root portion of a scope path. If a scope path contains any text, the first segment is
    /// considered the controller name.
    /// This may be the same as <see cref="Container"/> if the element is controller scoped. Sometimes it may be
    /// helpful to easily retrieve this value specifically to identify objects across L5X files.
    /// </remarks>
    public string Controller { get; } = string.Empty;

    /// <summary>
    /// Gets the scoped program name of this element.
    /// </summary>
    /// <remarks>
    /// This may be the same as <see cref="Container"/> if the element is program scoped. Sometimes it may be
    /// helpful to easily retrieve both the program name and controller name to identify objects across L5X files.
    /// </remarks>
    public string Program { get; } = string.Empty;

    /// <summary>
    /// Gets the scoped routine name of this element.
    /// </summary>
    /// <remarks>
    /// Routine will only be set for elements contained in routines, such as Rung, Line, or Sheet. This is because only
    /// code elements have <c>Routine</c> scope. Everything else is <c>Controller</c> or <c>Program</c> scoped.
    /// </remarks>
    public string Routine { get; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Type { get; } = string.Empty;

    /// <summary>
    /// The name of the component or code that further identifies this element. 
    /// </summary>
    public string Name { get; } = string.Empty;

    /// <summary>
    /// Gets the task name that this element is schedule in.
    /// </summary>
    public string Task { get; } = string.Empty;

    /// <summary>
    /// Indicates whether this element is scoped to an L5X container, meaning it is attached to an L5X document, or if
    /// it is an orphaned element. Basically, if the <see cref="ScopeLevel"/> is <c>Null</c> or not.
    /// </summary>
    public bool IsScoped => Level != ScopeLevel.Null;

    /// <summary>
    /// Indicates whether this is a global (Controller) scoped instance.
    /// This is in constrast to <see cref="IsLocal"/>.
    /// </summary>
    public bool IsGlobal => Level == ScopeLevel.Controller;

    /// <summary>
    /// Indicates whether this is a local (Program or Routine) scoped instance.
    /// This is in constrast to <see cref="IsGlobal"/>.
    /// </summary>
    public bool IsLocal => Level == ScopeLevel.Program || Level == ScopeLevel.Routine;

    /// <summary>
    /// Indicates whether this is a Program scoped instance (i.e. Level == Program).
    /// </summary>
    public bool IsProgram => Level == ScopeLevel.Program;
    
    /// <summary>
    /// Indicates whether this is a Program scoped instance (i.e. Level == Routine).
    /// </summary>
    public bool IsRoutine => Level == ScopeLevel.Routine;

    /// <summary>
    /// Creates default empty/null scope instance.
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
    /// <param name="controller">The name of the root controller element.</param>
    /// <returns>A <see cref="IScopeBuilder"/> containing the current scope config.</returns>
    public static IScopeBuilder Build(string controller)
    {
        return new ScopeBuider(controller);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static Scope Of(LogixElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        return new Scope(element.Serialize());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    internal static Scope Of(XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        return new Scope(element);
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

        if (!Type.IsEmpty() && ComponentType.All().Any(x => x.Value == Type))
        {
            path.Append($"/{Type}s/{Type}[@Name='{Name}']");
        }

        if (!Type.IsEmpty() && Type is L5XName.Rung or L5XName.Line or L5XName.Sheet)
        {
            path.Append($"/{Type}s/{Type}[@Number='{Name}']");
        }

        return path.ToString();
    }

    /// <inheritdoc />
    public override string ToString() => Path;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Scope other => Path.IsEquivalent(other.Path) && Level == other.Level,
            string path => Equals(this, To(path)),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Level.GetHashCode();
    }


    //

    #region Internal

    private static string ParseController(string[] parts)
    {
        return parts.Length switch
        {
            0 => string.Empty,
            _ => parts[0]
        };
    }

    private static string ParseProgram(string[] parts)
    {
        // Controller/Program/Type/Name = 4
        // Controller/Program/Routine/Type/Name = 5
        // /Program/Routine/Type/Name = 5
        // /Program/Type/Name = 4
        return parts.Length switch
        {
            4 when parts[0].IsEmpty() && parts[2].IsProgramElement() => parts[1],
            4 when !parts[0].IsEmpty() => parts[1],
            > 4 => parts[1],
            _ => string.Empty
        };
    }

    private static string ParseRoutine(string[] parts)
    {
        // Controller/Program/Routine/Type/Name = 5
        // /Program/Routine/Type/Name = 5
        // /Routine/Type/Name = 4
        return parts.Length switch
        {
            0 => string.Empty,
            4 when parts[0].IsEmpty() && parts[2].IsCodeElement() => parts[1],
            _ => parts.Length > 4 ? parts[2] : string.Empty
        };
    }

    private static string ParseElement(string[] parts)
    {
        return parts.Length >= 3 ? parts[parts.Length - 2] : string.Empty;
    }

    private static string ParseName(string[] parts)
    {
        return parts.Length >= 2 ? parts[parts.Length - 1] : string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    private ScopeLevel DetermineType()
    {
        if (!string.IsNullOrEmpty(Routine)) return ScopeLevel.Routine;
        if (!string.IsNullOrEmpty(Program)) return ScopeLevel.Program;
        if (!string.IsNullOrEmpty(Controller)) return ScopeLevel.Controller;
        return ScopeLevel.Null;
    }

    /// <summary>
    /// 
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
        var parts = new[] { Controller, Program, Routine, Type, Name }.Where(part => !part.IsEmpty());
        var path = parts.Combine(PathSeparator);
        return !Controller.IsEmpty() ? path : string.Concat(PathSeparator, path);
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
    /// Gets the name of the containing routine element from the provided element.
    /// </summary>
    private static string GetScopedName(XElement node)
    {
        if (node.Attribute(L5XName.Name) is not null)
            return node.LogixName();

        return node.Attribute(L5XName.Number) is not null
            ? node.Attribute(L5XName.Number)?.Value ?? string.Empty
            : string.Empty;
    }

    /// <summary>
    /// Gets the name of the scheduled task element from the provided element.
    /// </summary>
    private static string GetScopedTask(XElement node)
    {
        var container = node.Ancestors()
            .FirstOrDefault(e => e.Name.LocalName is L5XName.Routine or L5XName.Program or L5XName.Controller)?
            .LogixName();

        var element = node.Ancestors(L5XName.RSLogix5000Content)
            .Descendants(L5XName.Task)
            .FirstOrDefault(e => e.Descendants(L5XName.ScheduledProgram)
                .Any(p => p.LogixName().IsEquivalent(container)));

        return element?.LogixName() ?? string.Empty;
    }

    #endregion
}

/// <summary>
/// A fluent API for building up a scope path that identifies a specific location or element in the L5X tree.
/// </summary>
public interface IScopeBuilder
{
    /// <summary>
    /// Specifies the name of the program the object is scoped to. 
    /// </summary>
    /// <param name="program">The name of the program.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    IScopeBuilderProgram In(string program);

    /// <summary>
    /// Specifies the type name of the element for the scope instance. This can be any component or code name, such
    /// as Tag, Module, Rung, etc. 
    /// </summary>
    /// <param name="type">The element type name.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    IScopeBuilderName Type(string type);

    /// <summary>
    /// Specifies the scope as a path to a data type with the provided name. 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope DataType(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Module(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Aoi(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Tag(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Program(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Task(string name);
}

/// <summary>
/// A fluent API for building up a scope path that identifies a specific location or element in the L5X tree.
/// This interface is scoped to a single program, hence limiting the API to Routine/Tag, or allowing further scoping
/// into a specific Routine.
/// </summary>
public interface IScopeBuilderProgram
{
    /// <summary>
    /// Specifies the name of the program the object is scoped to. 
    /// </summary>
    /// <param name="routine">The name of the program.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    IScopeBuilderRoutine In(string routine);

    /// <summary>
    /// Specifies the type name of the element for the scope instance. This can be any component or code name, such
    /// as Tag, Module, Rung, etc. 
    /// </summary>
    /// <param name="type">The element type name.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    IScopeBuilderName Type(string type);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Tag(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Routine(string name);
}

/// <summary>
/// A fluent API for building up a scope path that identifies a specific location or element in the L5X tree.
/// </summary>
public interface IScopeBuilderRoutine
{
    /// <summary>
    /// Specifies the type name of the element for the scope instance. This can be any component or code name, such
    /// as Tag, Module, Rung, etc. 
    /// </summary>
    /// <param name="type">The element type name.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    IScopeBuilderName Type(string type);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The next step in the build fluent interface.</returns>
    Scope Rung(int number);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The next step in the build fluent interface.</returns>
    Scope Line(int number);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The next step in the build fluent interface.</returns>
    Scope Sheet(int number);
}

/// <summary>
/// A nested builder interface for specifying the target name of the a <see cref="Scope"/> instance.
/// </summary>
public interface IScopeBuilderName
{
    /// <summary>
    /// Specifies the element type the object is scoped to. 
    /// </summary>
    /// <param name="name">The name of the target.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    Scope Named(string name);
}

/// <summary>
/// Internal class that implements the builder interface.
/// </summary>
file class ScopeBuider(string controller) :
    IScopeBuilder, IScopeBuilderProgram, IScopeBuilderRoutine, IScopeBuilderName
{
    private const char PathSeparator = '/';
    private string _program = string.Empty;
    private string _routine = string.Empty;
    private string _type = string.Empty;
    private string _name = string.Empty;

    public IScopeBuilderProgram In(string program)
    {
        _program = program;
        return this;
    }

    IScopeBuilderRoutine IScopeBuilderProgram.In(string routine)
    {
        _routine = routine;
        return this;
    }

    public IScopeBuilderName Type(string type)
    {
        _type = type;
        return this;
    }

    public Scope DataType(string name)
    {
        _type = L5XName.DataType;
        _name = name;
        return Build();
    }

    public Scope Module(string name)
    {
        _type = L5XName.Module;
        _name = name;
        return Build();
    }

    public Scope Aoi(string name)
    {
        _type = L5XName.AddOnInstructionDefinition;
        _name = name;
        return Build();
    }

    public Scope Tag(string name)
    {
        _type = L5XName.Tag;
        _name = name;
        return Build();
    }

    public Scope Routine(string name)
    {
        _type = L5XName.Routine;
        _name = name;
        return Build();
    }

    public Scope Program(string name)
    {
        _type = L5XName.Program;
        _name = name;
        return Build();
    }

    public Scope Task(string name)
    {
        _type = L5XName.Task;
        _name = name;
        return Build();
    }

    public Scope Rung(int number)
    {
        _type = L5XName.Rung;
        _name = number.ToString();
        return Build();
    }

    public Scope Line(int number)
    {
        _type = L5XName.Line;
        _name = number.ToString();
        return Build();
    }

    public Scope Sheet(int number)
    {
        _type = L5XName.Sheet;
        _name = number.ToString();
        return Build();
    }

    public Scope Named(string name)
    {
        _name = name;
        return Build();
    }

    private Scope Build()
    {
        var parts = new[] { controller, _program, _routine, _type, _name }.Where(part => !part.IsEmpty());
        var path = parts.Combine(PathSeparator);
        return Scope.To(path);
    }
}