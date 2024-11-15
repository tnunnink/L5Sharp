using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// The internal implementation that indexes elements and references and stores them in the local dictionaries.
/// This class is then used by <see cref="L5X"/> to find components, tags, and references quickly.
/// </summary>
internal class LogixIndex : ILogixLookup
{
    /// <summary>
    /// The root controller element of the project.
    /// </summary>
    private readonly XElement _content;

    /// <summary>
    /// The name of the root controller this index is attached to.
    /// </summary>
    private readonly string _controller;

    /// <summary>
    /// An index of all logix elements (that we care to index) in the L5X file for fast lookups.
    /// </summary>
    private readonly Dictionary<Scope, XElement> _elements = new();

    /// <summary>
    /// An index of all named references found in code or tags in the L5X file for fast lookups.
    /// </summary>
    private readonly Dictionary<string, List<CrossReference>> _references = new();

    /// <summary>
    /// The internal scope generator that can generate scope keys to be used in lookup operations.
    /// </summary>
    private readonly L5XScopeGenerator _generator;

    public LogixIndex(XElement content)
    {
        _content = content;
        _controller = _content.LogixName();
        _generator = new L5XScopeGenerator(_content);

        Index();
    }

    #region API

    public bool Contains(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = _generator.GenerateSingle(scope);

        return _elements.ContainsKey(key);
    }

    public bool Contains(Func<IScopeBuilder, Scope> builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var controller = Scope.Build(_controller);
        var scope = builder(controller);

        return _elements.ContainsKey(scope);
    }

    public IEnumerable<LogixScoped> Find(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var results = new List<LogixScoped>();

        var scopes = _generator.GenerateAll(scope).ToList();

        foreach (var key in scopes)
        {
            if (!_elements.TryGetValue(key, out var value)) continue;
            var element = value.Deserialize<LogixScoped>();
            var result = element is Tag tag ? tag.Member(scope.Name.Path) : element;
            if (result is null) continue;
            results.Add(result);
        }

        return results;
    }

    public IEnumerable<TScoped> Find<TScoped>(Scope scope) where TScoped : LogixScoped
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var results = new List<TScoped>();

        var scopes = _generator.GenerateAll(scope, typeof(TScoped)).ToList();

        foreach (var s in scopes)
        {
            if (!_elements.TryGetValue(s, out var value)) continue;
            var element = value.Deserialize<TScoped>();
            var result = element is Tag tag ? tag.Member(scope.Name.Path) as LogixScoped : element;
            if (result is not TScoped typed) continue;
            results.Add(typed);
        }

        return results;
    }

    public LogixScoped Get(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = _generator.GenerateSingle(scope);

        if (!_elements.TryGetValue(key, out var value))
            throw new KeyNotFoundException($"No element with the provided scope was found: {key}");

        var result = value.Deserialize<LogixScoped>();
        return result is Tag tag ? tag[scope.Name.Path] : result;
    }

    public TScoped Get<TScoped>(Scope scope) where TScoped : LogixScoped
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = _generator.GenerateSingle(scope, typeof(TScoped));

        if (!_elements.TryGetValue(key, out var value))
            throw new KeyNotFoundException($"No element with the provided scope was found: {key}");

        var result = value.Deserialize<TScoped>();
        return result is Tag tag ? (TScoped)(LogixObject)tag[scope.Name.Path] : result;
    }

    public LogixScoped Get(Func<IScopeBuilder, Scope> builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(_controller);
        var scope = builder(root);
        //we still need to use the generator since the scope could be a tag member
        var key = _generator.GenerateSingle(scope);

        if (!_elements.TryGetValue(key, out var value))
            throw new KeyNotFoundException($"No element with the provided scope was found: {scope}");

        var element = value.Deserialize<LogixScoped>();
        return element is Tag tag ? tag[scope.Name.Path] : element;
    }

    public TScope Get<TScope>(Func<IScopeBuilder, Scope> builder) where TScope : LogixScoped
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(_controller);
        var scope = builder(root);
        //we still need to use the generator since the scope could be a tag member
        var key = _generator.GenerateSingle(scope);

        if (!_elements.TryGetValue(key, out var value))
            throw new KeyNotFoundException($"No element with the provided scope was found: {scope}");

        var element = value.Deserialize<TScope>();
        return element is Tag tag ? (TScope)(LogixObject)tag[scope.Name.Path] : element;
    }

    public bool TryGet(Scope scope, out LogixScoped element)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = _generator.GenerateSingle(scope);

        if (!_elements.TryGetValue(key, out var value))
        {
            element = default!;
            return false;
        }

        var result = value.Deserialize<LogixScoped>();
        var target = result is Tag tag ? tag.Member(scope.Name.Path) : result;
        return IsNull(target, out element);
    }

    public bool TryGet<TScoped>(Scope scope, out TScoped element) where TScoped : LogixScoped
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = _generator.GenerateSingle(scope, typeof(TScoped));

        if (!_elements.TryGetValue(key, out var value))
        {
            element = default!;
            return false;
        }

        var result = value.Deserialize<TScoped>();
        var target = result is Tag tag ? tag.Member(scope.Name.Path)?.As<LogixObject>() : result;
        return IsNull(target as TScoped, out element);
    }

    public bool TryGet(Func<IScopeBuilder, Scope> builder, out LogixScoped element)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(_controller);
        var scope = builder(root);
        //we still need to use the generator since the scope could be a tag member
        var key = _generator.GenerateSingle(scope);

        if (!_elements.TryGetValue(key, out var value))
        {
            element = default!;
            return false;
        }

        var result = value.Deserialize<LogixScoped>();
        var target = result is Tag tag ? tag.Member(scope.Name.Path)?.As<LogixScoped>() : result;
        return IsNull(target, out element);
    }

    public bool TryGet<TScoped>(Func<IScopeBuilder, Scope> builder, out TScoped element) where TScoped : LogixScoped
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(_controller);
        var scope = builder(root);
        //we still need to use the generator since the scope could be a tag member
        var key = _generator.GenerateSingle(scope);

        if (!_elements.TryGetValue(key, out var value))
        {
            element = default!;
            return false;
        }

        var result = value.Deserialize<TScoped>();
        var target = result is Tag tag ? tag.Member(scope.Name.Path)?.As<LogixObject>() : result;
        return IsNull(target as TScoped, out element);
    }

    public IEnumerable<CrossReference> References(LogixComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));
        
        var key = $"{component.L5XType}:{component.Name}";

        if (!_references.TryGetValue(key, out var references))
            return [];

        return references.Where(r => component.Scope.IsVisibleTo(r.Scope));
    }

    #endregion

    #region Internal

    /// <summary>
    /// Clears all current data and kicks off indexing of all the elements and references for the L5X. Sets flag to
    /// false.
    /// </summary>
    private void Index()
    {
        //Elements
        IndexControllerScopedComponents();
        IndexControllerScopedModuleTags();
        IndexProgramScopedElements();

        //References
        IndexTagReferences();
        IndexRoutineReferences();
        IndexRungReferences();
        IndexLineReferences();
        IndexSheetReferences();
    }

    /// <summary>
    /// Handles finding and indexing all named component elements in the L5X.
    /// </summary>
    private void IndexControllerScopedComponents()
    {
        HashSet<string> types =
        [
            L5XName.DataType,
            L5XName.AddOnInstructionDefinition,
            L5XName.Module,
            L5XName.Tag,
            L5XName.Program,
            L5XName.Task
        ];

        var elements = _content.Descendants()
            .Where(e => types.Contains(e.L5XType()) && e.Attribute(L5XName.Name) is not null);

        foreach (var element in elements)
        {
            var scope = Scope
                .Build(_controller)
                .Type(element.L5XType())
                .Named(element.LogixName());

            _elements[scope] = element;
        }
    }

    /// <summary>
    /// Handles finding and indexing all module defined tag elements in the L5X.
    /// </summary>
    private void IndexControllerScopedModuleTags()
    {
        var elements = _content.Descendants(L5XName.Module).Where(e => e.IsModuleTagElement());

        foreach (var element in elements)
        {
            var tagName = element.TagName();
            var scope = Scope.Build(_controller).Tag(tagName);
            _elements[scope] = element;
        }
    }

    /// <summary>
    /// Handles finding and indexing all content in the program scoped elements.
    /// </summary>
    private void IndexProgramScopedElements()
    {
        var programs = _content.Descendants(L5XName.Program);

        foreach (var program in programs)
        {
            IndexProgramScopedTags(program);
            IndexProgramScopedRoutines(program);
            IndexProgramScopedRungs(program);
            IndexProgramScopedLines(program);
            IndexProgramScopedSheets(program);
        }
    }

    /// <summary>
    /// Indexes all the tag component elements in the provided program element.
    /// </summary>
    private void IndexProgramScopedTags(XElement program)
    {
        var programName = program.LogixName();

        foreach (var tag in program.Descendants(L5XName.Tag))
        {
            var scope = Scope
                .Build(_controller)
                .In(programName)
                .Type(ScopeType.Tag)
                .Named(tag.LogixName());

            _elements[scope] = tag;
        }
    }

    /// <summary>
    /// Indexes all the routine component elements in the provided program element.
    /// </summary>
    private void IndexProgramScopedRoutines(XElement program)
    {
        var programName = program.LogixName();

        foreach (var routine in program.Descendants(L5XName.Routine))
        {
            var scope = Scope
                .Build(_controller)
                .In(programName)
                .Type(ScopeType.Routine)
                .Named(routine.LogixName());

            _elements[scope] = routine;
        }
    }

    /// <summary>
    /// Indexes all the rung elements in the provided program element.
    /// </summary>
    private void IndexProgramScopedRungs(XElement program)
    {
        var programName = program.LogixName();
        var rungs = program.Descendants(L5XName.Rung).ToList();

        //We won't rely on number but rather the order/index since that is all Studio cares about.
        for (var i = 0; i < rungs.Count; i++)
        {
            var rung = rungs[i];
            var routineName = rung.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty;

            var scope = Scope
                .Build(_controller)
                .In(programName)
                .In(routineName)
                .Type(ScopeType.Rung)
                .Named(i.ToString());

            _elements[scope] = rung;
        }
    }

    /// <summary>
    /// Indexes all the line elements in the provided program element.
    /// </summary>
    private void IndexProgramScopedLines(XElement program)
    {
        var programName = program.LogixName();
        var lines = program.Descendants(L5XName.Line).ToList();

        //We won't rely on number but rather the oder/index since that is all Studio cares about.
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var routineName = line.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty;

            var scope = Scope.Build(_controller)
                .In(programName)
                .In(routineName)
                .Type(ScopeType.Line)
                .Named(i.ToString());

            _elements[scope] = line;
        }
    }

    /// <summary>
    /// Indexes all the sheet elements in the provided program element.
    /// </summary>
    private void IndexProgramScopedSheets(XElement program)
    {
        var programName = program.LogixName();
        var sheets = program.Descendants(L5XName.Sheet).ToList();

        //We won't rely on number but rather the oder/index since that is all Studio cares about.
        for (var i = 0; i < sheets.Count; i++)
        {
            var sheet = sheets[i];
            var routineName = sheets.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty;

            var scope = Scope.Build(_controller)
                .In(programName)
                .In(routineName)
                .Type(ScopeType.Sheet)
                .Named(i.ToString());

            _elements[scope] = sheet;
        }
    }

    /// <summary>
    /// Finds all elements with a data type attribute and indexes them into a local reference index for fast lookup.
    /// This will include technically any type, predefined, atomic, user defined, or add on instruction.
    /// </summary>
    private void IndexTagReferences()
    {
        var tags = _content.Descendants().Where(d => d.IsTagElement());
        var references = tags.SelectMany(CrossReference.In).ToList();

        foreach (var reference in references)
            AddReference(reference);
    }

    /// <summary>
    /// Finds all program elements and indexes the referenced routine names found in the program attributes.
    /// </summary>
    private void IndexRoutineReferences()
    {
        var programs = _content.Descendants(L5XName.Program);
        var references = programs.SelectMany(CrossReference.In).ToList();

        foreach (var reference in references)
            AddReference(reference);
    }

    /// <summary>
    /// Handles indexing the tags or component names of for all rungs in the L5X file.
    /// </summary>
    private void IndexRungReferences()
    {
        var rungs = _content.Descendants(L5XName.Program).Descendants(L5XName.Rung);
        var references = rungs.SelectMany(CrossReference.In).ToList();

        foreach (var reference in references)
            AddReference(reference);
    }

    /// <summary>
    /// Handles indexing the tags or component names of for all rungs in the L5X file.
    /// </summary>
    private void IndexLineReferences()
    {
        var elements = _content.Descendants(L5XName.Program).Descendants(L5XName.Line);
        var references = elements.SelectMany(CrossReference.In).ToList();

        foreach (var reference in references)
            AddReference(reference);
    }

    /// <summary>
    /// 
    /// </summary>
    private void IndexSheetReferences()
    {
        var elements = _content.Descendants(L5XName.Program).Descendants(L5XName.Sheet);
        var references = elements.SelectMany(CrossReference.In).ToList();

        foreach (var reference in references)
            AddReference(reference);
    }

    /// <summary>
    /// Creates the reference key and sdds the cross-reference to the internal dictionary.
    /// </summary>
    private void AddReference(CrossReference reference)
    {
        var key = $"{reference.Type.Name}:{reference.Reference.Root}";

        if (!_references.TryAdd(key, [reference]))
            _references[key].Add(reference);
    }

    /// <summary>
    /// Helper for returning the TryGet result and element based on the nullness of the input element.
    /// </summary>
    private static bool IsNull<TObject>(TObject? element, out TObject result) where TObject : LogixObject
    {
        if (element is null)
        {
            result = default!;
            return false;
        }

        result = element;
        return true;
    }

    #endregion
}