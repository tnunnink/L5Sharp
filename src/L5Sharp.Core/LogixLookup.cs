using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace L5Sharp.Core;

internal class LogixLookup(XElement content) : ILogixLookup
{
    private readonly string _controller = content.LogixName();

    #region API

    public bool Contains(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = GenerateAbsolute(scope);

        var element = content.XPathSelectElement(key.ToXPath());
        return element is not null;
    }

    public bool Contains(Func<IScopeBuilder, Scope> builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(content.LogixName());
        var scope = builder(root);

        var element = content.XPathSelectElement(scope.ToXPath());
        return element is not null;
    }

    public IEnumerable<LogixScoped> Find(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var results = new List<LogixScoped>();
        var scopes = GenerateScopes(scope.Type, scope.Name);

        foreach (var path in scopes)
        {
            var element = content.XPathSelectElement(path.ToXPath())?.Deserialize<LogixScoped>();
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
        var type = scope.Type != ScopeType.Empty ? scope.Type : ScopeType.Parse(typeof(TScoped).L5XType());
        var scopes = GenerateScopes(type, scope.Name.Root);

        foreach (var path in scopes)
        {
            var element = content.XPathSelectElement(path.ToXPath())?.Deserialize<TScoped>();
            var result = element is Tag tag ? tag.Member(scope.Name.Path) as LogixObject : element;
            if (result is not TScoped typed) continue;
            results.Add(typed);
        }

        return results;
    }

    public LogixScoped Get(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = GenerateAbsolute(scope);
        var element = content.XPathSelectElement(key.ToXPath());

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided scope was found: {key}");

        var result = element.Deserialize<LogixScoped>();
        return result is Tag tag ? tag[key.Name.Path] : result;
    }

    public TScoped Get<TScoped>(Scope scope) where TScoped : LogixScoped
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = GenerateAbsolute(scope, typeof(TScoped));
        var element = content.XPathSelectElement(key.ToXPath());

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided scope was found: {key}");

        var result = element.Deserialize<TScoped>();
        return result is Tag tag ? (TScoped)(LogixObject)tag[key.Name.Path] : result;
    }

    public LogixScoped Get(Func<IScopeBuilder, Scope> builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(content.LogixName());
        var scope = builder(root);

        var element = content.XPathSelectElement(scope.ToXPath());

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided scope was found: {scope}");

        var result = element.Deserialize<LogixScoped>();
        return result is Tag tag ? tag[scope.Name.Path] : result;
    }

    public TScoped Get<TScoped>(Func<IScopeBuilder, Scope> builder) where TScoped : LogixScoped
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(content.LogixName());
        var scope = builder(root);

        var element = content.XPathSelectElement(scope.ToXPath());

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided scope was found: {scope}");

        var result = element.Deserialize<TScoped>();
        return result is Tag tag ? (TScoped)(LogixObject)tag[scope.Name.Path] : result;
    }

    public bool TryGet(Scope scope, out LogixScoped element)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = GenerateAbsolute(scope);

        var result = content.XPathSelectElement(key.ToXPath())?.Deserialize<LogixScoped>();

        if (result is null)
        {
            element = default!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(key.Name.Path) : result;
        return IsNull(target, out element);
    }

    public bool TryGet<TScoped>(Scope scope, out TScoped element) where TScoped : LogixScoped
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = GenerateAbsolute(scope, typeof(TScoped));

        var result = content.XPathSelectElement(key.ToXPath())?.Deserialize<TScoped>();

        if (result is null)
        {
            element = default!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(key.Name.Path)?.As<LogixObject>() : result;
        return IsNull(target as TScoped, out element);
    }

    public bool TryGet(Func<IScopeBuilder, Scope> builder, out LogixScoped element)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(content.LogixName());
        var scope = builder(root);

        var result = content.XPathSelectElement(scope.ToXPath())?.Deserialize<LogixScoped>();

        if (result is null)
        {
            element = default!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(scope.Name.Path) : result;
        return IsNull(target, out element);
    }

    public bool TryGet<TScoped>(Func<IScopeBuilder, Scope> builder, out TScoped element) where TScoped : LogixScoped
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(content.LogixName());
        var scope = builder(root);

        var result = content.XPathSelectElement(scope.ToXPath())?.Deserialize<TScoped>();

        if (result is null)
        {
            element = default!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(scope.Name.Path)?.As<LogixObject>() : result;
        return IsNull(target as TScoped, out element);
    }

    public IEnumerable<CrossReference> References(TagName name) => [];

    #endregion

    #region Internal

    /// <summary>
    /// Generates all potential scope paths for the provided type and name so that we can attempt to find all instances
    /// of an element across alls scopes.
    /// </summary>
    private List<Scope> GenerateScopes(ScopeType type, string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                "Unable to resolve scope path without a 'name' specified. Type is required to determine which element to retrieve.");

        if (string.IsNullOrEmpty(type))
            throw new ArgumentException(
                "Unable to resolve scope path without a 'type' specified. Type is required to determine which element to retrieve.");

        var scopes = new List<Scope>();

        //Add controller scope only if not a routine or code element.
        if (type is { InRoutine: false, Name: not L5XName.Routine })
            scopes.Add(Scope.Build(_controller).Type(type).Named(name));

        //Return early if we are a controller element.
        if (type.InController) return scopes;

        var programs = content.Descendants(L5XName.Program);

        foreach (var program in programs)
        {
            if (type.InProgram)
                scopes.Add(Scope.Build(_controller).In(program.LogixName()).Type(type).Named(name));

            if (!type.InRoutine) continue;

            var routines = program.Descendants(L5XName.Routine).Select(r =>
                Scope.Build(_controller).In(program.LogixName()).In(r.LogixName()).Type(type).Named(name)
            );

            scopes.AddRange(routines);
        }

        return scopes;
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

    /// <summary>
    /// Generates the full/absolute scope path to be used to find elements in the lookup. 
    /// </summary>
    private Scope GenerateAbsolute(Scope scope, Type? type = default)
    {
        if (!scope.IsRelative)
            return scope;

        if (scope.Type != ScopeType.Empty)
            return Scope.To($"{_controller}//").Append(scope);

        if (type is null)
            throw new ArgumentException(
                "Unable to resolve scope path without a 'type' specified. Type is required to determine which element to retrieve.");

        return Scope.To($"{_controller}/{scope.Program}/{scope.Routine}/{type.L5XType()}/{scope.Name}");
    }

    #endregion
}