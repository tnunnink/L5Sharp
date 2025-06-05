using System;

namespace L5Sharp.Core;

internal class ImportPlanBuilder : IImportPlanBuilder
{
    private readonly ImportPlan _plan = new();

    public ImportPlan Build()
    {
        return _plan;
    }

    public IImportPlanBuilder Overwrite()
    {
        _plan.AddOperation(new OverwriteOperation<LogixObject>(_ => true));
        return this;
    }

    public IImportPlanBuilder Overwrite<TComponent>(Func<TComponent, bool> predicate)
        where TComponent : LogixComponent
    {
        _plan.AddOperation(new OverwriteOperation<TComponent>(predicate));
        return this;
    }

    public IImportPlanBuilder Discard<TComponent>(Func<TComponent, bool> predicate)
        where TComponent : LogixComponent
    {
        _plan.AddOperation(new DiscardOperation<TComponent>(predicate));
        return this;
    }

    public IImportPlanBuilder Modify<TComponent>(Func<TComponent, bool> predicate, Action<TComponent> config)
        where TComponent : LogixComponent
    {
        _plan.AddOperation(new ModifyOperation<TComponent>(predicate, config));
        return this;
    }

    public IImportPlanBuilder Rename<TComponent>(string current, string updated)
        where TComponent : LogixComponent
    {
        _plan.AddOperation(new ModifyOperation<TComponent>(c => c.Name == current, c => c.Name = updated));
        return this;
    }
}