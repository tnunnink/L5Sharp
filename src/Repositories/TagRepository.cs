using System;
using System.Linq;
using L5Sharp.Components;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Repositories;

internal class TagRepository : ComponentRepository<Tag>, ILogixTagRepository
{
    private readonly L5X _l5X;

    internal TagRepository(L5X l5X) : base(l5X)
    {
        _l5X = l5X;
    }

    public ILogixComponentRepository<Tag> In(string container)
    {
        var target = _l5X.GetContainers(L5XName.Tag)
            .FirstOrDefault(e => e.Ancestors(L5XName.Program).Any(a => a.LogixName() == container));

        if (target is null)
            throw new InvalidOperationException(
                $"No container named '{container}' for type {typeof(Tag)} was found in the L5X content.");

        return new ComponentRepository<Tag>(target);
    }
}