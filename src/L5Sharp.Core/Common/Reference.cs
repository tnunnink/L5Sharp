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
/// identify the reference type and location, including scope and id information.
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
    /// The underlying XPath expression that represents the location of the element in the L5X.
    /// </summary>
    private readonly XPathExpression _path;

    /// <summary>
    /// Creates a new scope instance initialized with the parts of the provided string path.
    /// </summary>
    private Reference(string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException("Scope path can not be null or empty.");

        _path = XPathExpression.Compile(path);
    }

    /// <summary>
    /// The full XPath to the element this reference represents. 
    /// </summary>
    public string Path => _path.Expression;

    /// <summary>
    /// Gets the <see cref="ReferenceType"/> that identifies which element type the path is referencing.
    /// </summary>
    /// <remarks>
    /// Only logix entity elements support reference paths. This enumeration gives some static typing and helper
    /// methods for working with references.
    /// </remarks>
    public ReferenceType Type => ParseType(_path.Expression);

    /// <summary>
    /// Gets the value of the component or code element that identifies this reference target.
    /// </summary>
    /// <remarks>
    /// For components this will be the name, but for code elements (Rung/Line/Sheet) this will be the number.
    /// </remarks>
    public string Location => ParseLocation(_path.Expression);

    /// <summary>
    /// Gets the name of the parent container the target element is in. This will be either the Program or AOI
    /// name if found. If not found, then this will be an empty string, representing a controller or unscoped path.
    /// </summary>
    public string Container => ParseContainer(_path.Expression);

    /// <summary>
    /// Gets the name of the parent routine segment from the current reference path if it is present.
    /// If the path does not container a routine container segment, then the value will be an empty string.
    /// </summary>
    /// <remarks>
    /// This only applies to code element references that are contained within routine components. For all other
    /// reference paths this should be empty.
    /// </remarks>
    public string Routine => ParseRoutine(_path.Expression);

    /// <summary>
    /// Attempts to parse the instruction text of the reference path if it exists.
    /// </summary>
    /// <remarks>
    /// The instruction text is embedded in the XPath using the text contains function.
    /// Embedding this additional data allows provides more info regarding the location of a code reference.
    /// </remarks>
    public Instruction Logic => ParseInstruction(_path.Expression);
    
    /// <summary>
    /// Indicates whether this is a controller scope reference.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the current type is a global scope type; otherwise, <c>false</c>.
    /// </returns>
    public bool IsGlobal => Container == string.Empty;

    /// <summary>
    /// Indicates whether this is a type contained in a program scope (i.e., Tag, Routine).
    /// </summary>
    /// <returns>
    /// <c>true</c> if the current type is a program scope type; otherwise, <c>false</c>.
    /// </returns>
    public bool IsLocal => Container != string.Empty;

    /// <summary>
    /// Indicates whether the reference type is related to logic elements such as Rung, Line, or Sheet.
    /// </summary>
    public bool IsLogic => Type == ReferenceType.Rung || Type == ReferenceType.Line || Type == ReferenceType.Sheet;

    /// <summary>
    /// Indicates whether the reference type represents a tag.
    /// </summary>
    public bool IsTag => Type == ReferenceType.Tag;

    /// <summary>
    /// Determines if the reference is scoped to the specified scope, based on the scope's level and container.
    /// </summary>
    /// <param name="scope">The scope to check the reference against.</param>
    /// <returns>
    /// <c>true</c> if both the reference <c>and</c> the scope instance are globally (controller) scoped, or if they are both
    /// locally (program or routine) scoped and have the same container name; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided scope is null.</exception>
    public bool IsScopedTo(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        if (scope.IsController && IsGlobal) return true;
        if (scope.IsLocal && IsLocal && scope.Container.IsEquivalent(Container)) return true;

        return true;
    }

    /// <summary>
    /// Determines whether the reference is visible to the specified scope, based on the scope's level and container.
    /// </summary>
    /// <param name="scope">The scope to check visibility against.</param>
    /// <returns>
    /// <c>true</c> if either the reference <c>or</c> the scope instance are globally (controller) scoped, or if they are both
    /// locally (program or routine) scoped and have the same container name; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided scope is null.</exception>
    /// <remarks>
    /// Note the difference here is that if either is globally scoped, then it is visible to the other. Otherwise,
    /// they need to be in the same local scope.
    /// </remarks>
    public bool IsVisibleTo(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        if (scope.IsController || IsGlobal) return true;
        if (scope.IsLocal && IsLocal && scope.Container.IsEquivalent(Container)) return true;

        return true;
    }

    /// <summary>
    /// Determines whether the reference is local to the specified scope, based on the scope's level and container.
    /// </summary>
    /// <param name="scope">The scope to check the reference against.</param>
    /// <returns>
    /// <c>true</c> if the reference and the scope are both locally (program or routine) scoped and have the same
    /// container name; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided scope is null.</exception>
    /// <remarks>If the provided scope or reference is globally scoped this will return false.</remarks>
    public bool IsLocalTo(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));
        
        return scope.IsLocal && IsLocal && scope.Container.IsEquivalent(Container);
    }

    /// <summary>
    /// Determines whether the reference is a container to the specified scope, based on the scope level, container, and
    /// the reference <see cref="Location"/>.
    /// </summary>
    /// <param name="scope">The scope to check the reference against.</param>
    /// <returns>
    /// <c>true</c> when the scope is program scoped, the reference type is program, and the scope container
    /// matches the reference location -or- the scope is routine scoped, the reference type is AOI, and the scope container
    /// matches the reference location; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided scope is null.</exception>
    /// <remarks>
    /// This just lets us determine if this is a reference to a container element (Program or AOI) that contains
    /// the provided scope instance (which could come from any other element).
    /// </remarks>
    public bool IsContainerTo(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        if (scope.IsProgram && Type == ReferenceType.Program)
            return scope.Container.IsEquivalent(Location);
        
        if (scope.IsRoutine && Type == ReferenceType.Aoi)
            return scope.Container.IsEquivalent(Location);

        return false;
    }
    
    /// <summary>
    /// Converts this reference to a new reference scoped to the same container as the provided reference instance.
    /// </summary>
    /// <param name="other">The reference object used to scope the current reference.</param>
    /// <returns>
    /// Returns a new <see cref="Reference"/> object with the same type and location but scoped to the same
    /// container as the provided reference object.
    /// </returns>
    /// <remarks>
    /// If both references are logic/code references, then this method will also assign the new reference to the same
    /// routine as the provided reference. Otherwise, this method will only assign (or unassign) the container portion
    /// of the reference path.
    /// </remarks>
    public Reference ToScope(Reference other)
    {
        var builder = new StringBuilder();

        builder.Append(L5XName.Controller)
            .AppendIf($"/Programs/Program[@Name='{other.Container}']", () => other.IsLocal)
            .AppendIf($"/Routines/Routine[@Name='{other.Routine}']", () => IsLogic && other.IsLogic)
            .Append($"/{Type.Container}/{Type.Value}[{Type.Identifier}='{Location}']");

        return Reference.To(builder.ToString());
    }

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
        var builder = new StringBuilder();

        builder.Append(L5XName.Controller)
            .AppendIf($"/Programs/Program[@Name='{scope.Container}']", () => scope.IsLocal)
            .Append($"/{Type.Container}/{Type.Value}[{Type.Identifier}='{Location}']");

        return Reference.To(builder.ToString());
    }

    /// <summary>
    /// Converts the current reference to a logic-specific reference by adding the provided logic instruction.
    /// </summary>
    /// <param name="logic">The logic instruction to be appended to the reference path.</param>
    /// <returns>A new <see cref="Reference"/> instance with the updated path including the logic instruction.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="logic"/> parameter is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the current reference is not of a logic type.</exception>
    public Reference ToLogic(Instruction logic)
    {
        if (logic is null)
            throw new ArgumentNullException(nameof(logic));

        if (!IsLogic)
            throw new InvalidOperationException("This operation is only valid for Logic type references");
        
        var builder = new StringBuilder();
        builder.Append(Path).Append($"/Text[contains(text(), \"{logic}\")]");
        return new Reference(builder.ToString());
    }

    /// <summary>
    /// Creates a new reference scoped to a specified tag member of a tag-based reference.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> representing the member to which this reference should be scoped.</param>
    /// <returns>A new <see cref="Reference"/> instance representing the scoped member.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the specified <paramref name="tagName"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the current reference is not of type Tag, as scoping to a member is only supported by Tag-based references.
    /// </exception>
    public Reference ToTag(TagName tagName)
    {
        if (tagName is null)
            throw new ArgumentNullException(nameof(tagName));

        if (!IsTag)
            throw new InvalidOperationException("This operation is only valid for Tag type references");
        
        var path = Path.Replace(Location, tagName);
        return Reference.To(path);
    }

    /// <summary>
    /// Creates a new <see cref="Reference"/> using the provided string path.
    /// </summary>
    /// <param name="path">The path that defines the scope to create.</param>
    /// <returns>A new <see cref="Reference"/> instance configured with the provided path.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="path"/> is null or empty.</exception>
    /// <exception cref="XPathException">Thrown when <paramref name="path"/> does not represent a valid XPath syntax.</exception>
    public static Reference To(string path) => new(path);

    /// <summary>
    /// Creates a new <see cref="Reference"/> instance representing a specific reference within the system.
    /// </summary>
    /// <param name="type">The type of reference, which determines its classification and characteristics.</param>
    /// <param name="location">The location of the reference as a string, which uniquely identifies it within its type.</param>
    /// <param name="program">An optional program name to associate the reference with, if applicable.</param>
    /// <param name="routine">An optional routine name to associate the reference with, if applicable.</param>
    /// <returns>A new <see cref="Reference"/> object initialized with the specified parameters.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="type"/> argument is null.</exception>
    public static Reference To(ReferenceType type, string location, string? program = null, string? routine = null)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        var builder = new StringBuilder();

        builder.Append("Controller")
            .AppendIf($"/Programs/Program[@Name='{program}']", () => !string.IsNullOrEmpty(program))
            .AppendIf($"/Routines/Routine[@Name='{routine}']", () => !string.IsNullOrEmpty(routine))
            .Append($"/{type.Container}/{type.Value}[{type.Identifier}='{location}']");
        
        return new Reference(builder.ToString());
    }

    /// <summary>
    /// Creates a new <see cref="Reference"/> instance for a specified <typeparamref name="TComponent"/> type,
    /// initialized with the provided name and optional program context.
    /// </summary>
    /// <param name="name">The identifier of the <typeparamref name="TComponent"/> to create a reference for.</param>
    /// <param name="program">Optional program name used to scope the reference. Defaults to null.</param>
    /// <typeparam name="TComponent">The type of <see cref="ILogixComponent"/> for which the reference is being created.</typeparam>
    /// <returns>A newly created <see cref="Reference"/> object representing the component and its context.</returns>
    public static Reference To<TComponent>(string name, string? program = null) where TComponent : LogixComponent<TComponent>
    {
        var type = ReferenceType.Parse(typeof(TComponent).Name);

        var builder = new StringBuilder();
        
        builder.Append("Controller")
            .AppendIf($"/Programs/Program[@Name='{program}']", () => !string.IsNullOrEmpty(program))
            .Append($"/{type.Container}/{type.Value}[{type.Identifier}='{name}']");
        
        return new Reference(builder.ToString());
    }

    /// <summary>
    /// Creates a new scope instance based on the provided target, program, routine, and type information.
    /// </summary>
    /// <param name="number">The identifier of the target component within the program routine.</param>
    /// <param name="program">The name of the program containing the routine.</param>
    /// <param name="routine">The name of the routine containing the target component.</param>
    /// <typeparam name="TCode">The type of the LogixCode for which the scope is being created.</typeparam>
    /// <returns>A new <see cref="Reference"/> object configured for the provided target and context.</returns>
    public static Reference To<TCode>(int number, string program, string routine) where TCode : LogixCode<TCode>
    {
        var type = ReferenceType.Parse(typeof(TCode).Name);
        
        var builder = new StringBuilder();
        
        builder.Append("Controller")
            .Append(Slash).Append($"Programs")
            .Append(Slash).Append($"Program[@Name='{program}']")
            .Append(Slash).Append($"Routines")
            .Append(Slash).Append($"Routine[@Name='{routine}']")
            .Append(Slash).Append($"{type.Container}")
            .Append(Slash).Append($"{type.Value}[{type.Identifier}='{number}']");
        
        return new Reference(builder.ToString());
    }

    /// <summary>
    /// Creates a new <see cref="Reference"/> instance from the provided XML element.
    /// </summary>
    /// <param name="element">The XML element containing the information to create the reference.</param>
    /// <returns>A <see cref="Reference"/> instance representing the provided XML element.</returns>
    public static Reference To(XElement element)
    {
        //Handle module tags specifically we need to view these as controller tags.
        if (element.IsModuleTagElement())
        {
            var moduleTag = $"Controller/Tags/Tag[@Name='{element.ModuleTagName()}']";
            return new Reference(moduleTag);
        }

        var builder = new StringBuilder();

        var elements = element.AncestorsAndSelf()
            .TakeWhile(x => x.Name.LocalName != L5XName.RSLogix5000Content)
            .ToArray();

        //Iterate in reverse to build the path from root down to this element.
        for (var i = elements.Length - 1; i >= 0; i--)
        {
            if (i < elements.Length - 1) builder.Append('/');
            var identifier = Identifier(elements[i]);
            builder.Append(identifier);
        }

        var path = builder.ToString();
        return new Reference(path);
    }
    
    /// <summary>
    /// Constructs a new <see cref="Reference"/> using the action provided to configure the builder.
    /// </summary>
    /// <param name="action">The configuration action to define the reference using the <see cref="IReferenceTypeBuilder"/>.</param>
    /// <returns>A new instance of <see cref="Reference"/> with the configuration applied.</returns>
    public static Reference Build(Action<IReferenceTypeBuilder> action)
    {
        var builder = new ReferenceBuilder();
        action.Invoke(builder);
        return builder.Build();
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Reference other => _path.Expression.IsEquivalent(other._path.Expression),
            string path => _path.Expression.IsEquivalent(path),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(_path.Expression);

    /// <inheritdoc />
    public override string ToString() => _path.Expression;

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
    public static implicit operator Reference(string path) => new(path);

    /// <summary>
    /// Implicit conversion from a Scope to a string.
    /// </summary>
    /// <param name="reference">The scope to be converted into a string value.</param>
    /// <returns>A <see cref="string"/> containing the path of the scope instance.</returns>
    public static implicit operator string(Reference reference) => reference._path.Expression;

    /// <summary>
    /// Implicit conversion from a TagName to a Scope instance.
    /// </summary>
    /// <param name="tagName">The TagName representing the path to be converted into a Scope instance.</param>
    /// <returns>A new Scope instance represented by the provided string path.</returns>
    public static implicit operator Reference(TagName tagName) => new(tagName);


    #region Internal
    
    /// <summary>
    /// Traverse from the end of the path to the first segment that has a parsable reference type name.
    /// This will be the location or target of the reference, since it is the node that the expression should return.
    /// Note that with the instruction syntax this might not be the last segment but should in most cases.
    /// </summary>
    private static ReferenceType ParseType(string path)
    {
        var segments = path.Split(Slash, StringSplitOptions.RemoveEmptyEntries);
        
        for (var i = segments.Length - 1; i >= 0; i--)
        {
            var segment = segments[i];
            var length = segment.Contains('[') ? segment.IndexOf('[') : segment.Length;
            var value = segment.Substring(0, length);
            var type = ReferenceType.TryParse(value);
            if (type is not null) return type;
        }

        return ReferenceType.Null;
    }
    
    /// <summary>
    /// Traverse from the end of the path to the first segment containing a name/number identifier.
    /// This will be the location or target of the reference, since it is the node that the expression should return.
    /// Note that with the instruction syntax this might not be the last segment but should in most cases.
    /// </summary>
    private static string ParseLocation(string path)
    {
        var segments = path.Split(Slash, StringSplitOptions.RemoveEmptyEntries);
        
        for (var i = segments.Length - 1; i >= 0; i--)
        {
            var segment = segments[i];
            
            if (!segment.Contains("[@Name=") && !segment.Contains("[@Number=")) 
                continue;
            
            var start = segment.IndexOf('\'') + 1;
            var length = segment.LastIndexOf('\'') - start;
            return (start > 0 && length > 0) ? segment.Substring(start, length) : string.Empty;
        }

        return string.Empty;
    }
    
    /// <summary>
    /// Travers from the end of the path to the first container segment. A container segment is either a program or
    /// AOI selection expression. If neither is found, then the container will be empty, and we assume this is either
    /// a controller=scoped or relative reference. Note that this cannot be the last segment, because if it is, then
    /// it is the target of the reference, not the container.
    /// </summary>
    private static string ParseContainer(string path)
    {
        var segments = path.Split(Slash, StringSplitOptions.RemoveEmptyEntries);
        
        for (var i = segments.Length - 1; i >= 0; i--)
        {
            var segment = segments[i];
            
            if (segment.StartsWith("Program[@Name=") && i != segments.Length - 1)
                return segment.Substring(15, segment.Length - 17);
            
            if (segment.StartsWith("AddOnInstructionDefinition[@Name=") && i != segments.Length - 1)
                return segment.Substring(34, segment.Length - 36);
        }

        return string.Empty;
    }
    
    /// <summary>
    /// Traverse from the end of the path to the first routine segment. If not found, then return an empty string.
    /// Note that this cannot be the last segment, as that would indicate it is the target of the reference, not the
    /// routine container.
    /// </summary>
    private static string ParseRoutine(string path)
    {
        var segments = path.Split(Slash, StringSplitOptions.RemoveEmptyEntries);
        
        for (var i = segments.Length - 1; i >= 0; i--)
        {
            var segment = segments[i];
            
            if (segment.StartsWith("Routine[@Name=") && i != segments.Length - 1)
                return segment.Substring(15, segment.Length - 17);
        }

        return string.Empty;
    }
    
    /// <summary>
    /// Parse the instruction text into a <see cref="Instruction"/> instance if the reference path contains
    /// the expected expression selector. The expression for selecting a code element with
    /// a specific text is: "Text[contains(text(), 'code']". This should always be the last segment of a
    /// reference if it exists because there are no child nodes from a Text element. The main reason we are using/parsing
    /// this is so we can embed specific instruction information for components that are referenced in logic.
    /// </summary>
    private Instruction ParseInstruction(string path)
    {
        var segments = path.Split(Slash, StringSplitOptions.RemoveEmptyEntries);
        
        var segment = segments.Length > 0 ? segments[segments.Length - 1] : string.Empty;

        if (!segment.StartsWith("Text")) return Instruction.Unkown;
        
        var start = segment.IndexOf('"') + 1;
        var length = segment.LastIndexOf('"') - start;
        var text = (start > 0 && length > 0) ? segment.Substring(start, length) : string.Empty;
        return !text.IsEmpty() ? Instruction.Parse(text) : Instruction.Unkown;
    }
    
    /// <summary>
    /// Generates an identifier for the specified XML element based on its attributes and type.
    /// </summary>
    /// <param name="element">The XML element for which the identifier is generated.</param>
    /// <returns>A string representing the identifier for the XML element.</returns>
    private static string Identifier(XElement element)
    {
        if (element.IsTagElement() || element.Name.LocalName.EndsWith(L5XName.Member))
        {
            return $"Tag[@Name='{element.TagName()}']";
        }

        if (element.IsComponentElement())
        {
            return $"{element.Name.LocalName}[@Name='{element.LogixName()}']";
        }

        if (element.IsCodeElement())
        {
            return $"{element.Name.LocalName}[@Number='{element.Attribute(L5XName.Number)?.Value ?? string.Empty}']";
        }

        if (element.Attribute(L5XName.ID) is not null)
        {
            return $"{element.Name.LocalName}[@ID='{element.Attribute(L5XName.ID)?.Value ?? string.Empty}']";
        }

        return element.Name.LocalName;
    }

    #endregion
}