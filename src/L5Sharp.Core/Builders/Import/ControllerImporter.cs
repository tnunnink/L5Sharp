using System;
using System.Collections.Generic;

namespace L5Sharp.Core;

internal class ControllerImporter :
    IImportControllerBuilder,
    IImportFromBuilder,
    IImportConflictBuilder,
    IImportUpdateBuilder
{
    private string? _sourcePath;
    private Func<L5X, IEnumerable<LogixComponent>> _query = _ => [];
    private Action<LogixComponent, LogixComponent>? _resolution;
    private Dictionary<Scope, Action<LogixComponent, LogixComponent>> _handlers = [];
    private List<Action<L5X>> _modifications = [];

    public IImportFromBuilder DataType(string name)
    {
        _query = x => [x.Get<DataType>(name)];
        return this;
    }

    public IImportFromBuilder DataTypes(Func<DataType, bool> predicate)
    {
        _query = x => x.Query(predicate);
        return this;
    }

    public IImportFromBuilder Instruction(string name)
    {
        _query = x => [x.Get<AddOnInstruction>(name)];
        return this;
    }

    public IImportFromBuilder Instructions(Func<AddOnInstruction, bool> predicate)
    {
        _query = x => x.Query(predicate);
        return this;
    }

    public IImportFromBuilder Program(string name)
    {
        _query = x => [x.Get<Program>(name)];
        return this;
    }

    public IImportFromBuilder Programs(Func<Program, bool> predicate)
    {
        _query = x => x.Query(predicate);
        return this;
    }

    public IImportFromBuilder Tag(string name)
    {
        _query = x => [x.Get<Tag>(name)];
        return this;
    }

    public IImportFromBuilder Tags(Func<Tag, bool> predicate)
    {
        _query = x => x.Query(predicate);
        return this;
    }

    public IImportConflictBuilder From(string fileName)
    {
        _sourcePath = fileName;
        return this;
    }

    public IImportUpdateBuilder UseExisting()
    {
        _resolution = (_, _) => { };
        return this;
    }

    public IImportUpdateBuilder Overwrite()
    {
        _resolution = (target, source) => { target.Replace(source); };
        return this;
    }

    public IImportUpdateBuilder Abort()
    {
        _resolution = (target, source) => throw new InvalidOperationException(
            $"Aborting import due to conflict between target '{target} and source '{source}'");
        return this;
    }

    public IImportUpdateBuilder Discard(Scope scope)
    {
        _handlers[scope] = (_, _) => { };
        return this;
    }

    public IImportUpdateBuilder UseExisting(Scope scope)
    {
        _handlers[scope] = (_, _) => { };
        return this;
    }

    public IImportUpdateBuilder Overwrite(Scope scope)
    {
        _handlers[scope] = (target, source) => { target.Replace(source); };
        return this;
    }

    public IImportUpdateBuilder OnConflict(Scope scope, Action<LogixObject, LogixObject> handler)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        _handlers[scope] = handler ?? throw new ArgumentNullException(nameof(handler));
        return this;
    }

    public IImportUpdateBuilder Modify(Action<L5X> update)
    {
        _modifications.Add(update);
        return this;
    }

    public IImportUpdateBuilder Replace(string find, string replace)
    {
        throw new NotImplementedException();
    }

    public void Execute(Controller target)
    {
        var source = GetSourceContent();
        
        var components = _query.Invoke() 
    }
    
    private L5X GetSourceContent()
    {
        if (string.IsNullOrEmpty(_sourcePath))
            throw new ArgumentException("Source path can not be null or empty.");
        
        if (_query is null)
            throw new ArgumentException("Source path can not be null or empty.");

        var source = L5X.Load(_sourcePath);

        return source;
    }
}