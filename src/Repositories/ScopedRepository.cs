using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories;

internal class ScopedRepository<TScoped> : ILogixScopedRepository<TScoped> where TScoped : ILogixComponent, ILogixScoped
{
    private readonly L5X _l5X;
    private readonly XName _name;
    private readonly ILogixSerializer<TScoped> _serializer;

    internal ScopedRepository(L5X l5X)
    {
        _l5X = l5X;
        _name = typeof(TScoped).GetLogixName();
        _serializer = LogixSerializer.GetSerializer<TScoped>();
    }

    public ILogixComponentRepository<TScoped> In(string container)
    {
        var target = _l5X.GetContainers(_name).FirstOrDefault(e => e.Ancestors().Any(a => a.LogixName() == container));

        if (target is null)
            throw new InvalidOperationException(
                $"No container named '{container}' for type {typeof(TScoped)} was found in the L5X content.");

        return new ComponentRepository<TScoped>(target);
    }

    public IEnumerator<TScoped> GetEnumerator() =>
        _l5X.Descendants(_name).Select(e => _serializer.Deserialize(e)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}