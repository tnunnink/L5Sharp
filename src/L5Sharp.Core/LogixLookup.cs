using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace L5Sharp.Core;

internal class LogixLookup(XElement content) : ILogixLookup
{
    /// <summary>
    /// The internal scope generator that can generate scope keys to be used in lookup operations.
    /// </summary>
    private readonly L5XScopeGenerator _generator = new(content);

    #region API

    public bool Contains(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = _generator.GenerateSingle(scope);

        var element = content.XPathSelectElement(key.ToXPath());
        return element is not null;
    }

    public bool Contains(Func<IScopeBuilder, Scope> builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(content.LogixName());
        var scope = builder(root);
        var key = _generator.GenerateSingle(scope);

        var element = content.XPathSelectElement(key.ToXPath());
        return element is not null;
    }

    public IEnumerable<LogixScoped> Find(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var results = new List<LogixScoped>();
        var scopes = _generator.GenerateAll(scope);

        foreach (var key in scopes)
        {
            var element = content.XPathSelectElement(key.ToXPath())?.Deserialize<LogixScoped>();
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
        var scopes = _generator.GenerateAll(scope, typeof(TScoped));

        foreach (var key in scopes)
        {
            var element = content.XPathSelectElement(key.ToXPath())?.Deserialize<TScoped>();
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

        var key = _generator.GenerateSingle(scope);
        var element = content.XPathSelectElement(key.ToXPath());

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided scope was found: {key}");

        var result = element.Deserialize<LogixScoped>();
        return result is Tag tag ? tag[scope.Name.Path] : result;
    }

    public TScoped Get<TScoped>(Scope scope) where TScoped : LogixScoped
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = _generator.GenerateSingle(scope, typeof(TScoped));
        var element = content.XPathSelectElement(key.ToXPath());

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided scope was found: {key}");

        var result = element.Deserialize<TScoped>();
        return result is Tag tag ? (TScoped)(LogixObject)tag[scope.Name.Path] : result;
    }

    public LogixScoped Get(Func<IScopeBuilder, Scope> builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(content.LogixName());
        var scope = builder(root);
        var key = _generator.GenerateSingle(scope);

        var element = content.XPathSelectElement(key.ToXPath());

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
        var key = _generator.GenerateSingle(scope, typeof(TScoped));

        var element = content.XPathSelectElement(key.ToXPath());

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided scope was found: {scope}");

        var result = element.Deserialize<TScoped>();
        return result is Tag tag ? (TScoped)(LogixObject)tag[scope.Name.Path] : result;
    }

    public bool TryGet(Scope scope, out LogixScoped element)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = _generator.GenerateSingle(scope);
        var result = content.XPathSelectElement(key.ToXPath())?.Deserialize<LogixScoped>();

        if (result is null)
        {
            element = default!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(scope.Name.Path) : result;
        return IsNull(target, out element);
    }

    public bool TryGet<TScoped>(Scope scope, out TScoped element) where TScoped : LogixScoped
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        var key = _generator.GenerateSingle(scope, typeof(TScoped));
        var result = content.XPathSelectElement(key.ToXPath())?.Deserialize<TScoped>();

        if (result is null)
        {
            element = default!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(scope.Name.Path)?.As<LogixObject>() : result;
        return IsNull(target as TScoped, out element);
    }

    public bool TryGet(Func<IScopeBuilder, Scope> builder, out LogixScoped element)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        var root = Scope.Build(content.LogixName());
        var scope = builder(root);
        var key = _generator.GenerateSingle(scope);

        var result = content.XPathSelectElement(key.ToXPath())?.Deserialize<LogixScoped>();

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
        var key = _generator.GenerateSingle(scope, typeof(TScoped));

        var result = content.XPathSelectElement(key.ToXPath())?.Deserialize<TScoped>();

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