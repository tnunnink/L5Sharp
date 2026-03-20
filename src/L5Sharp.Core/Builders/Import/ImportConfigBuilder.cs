using System;

namespace L5Sharp.Core;

internal abstract class ImportConfigBuilder<TBuilder>(Import import) : IImportConfigBuilder<TBuilder>
{
    public TBuilder Rename(string name)
    {
        import.Operations.Add(new ModifyOperation(x =>
        {
            //Only apply to the import target
            if (x.Reference != import.Target) return;
            x.Name = name;
        }));

        return GetBuilder();
    }

    public TBuilder Rename<TComponent>(string current, string updated)
        where TComponent : LogixComponent<TComponent>
    {
        import.Operations.Add(new ModifyOperation(x =>
        {
            if (x is not TComponent component) return;
            if (component.Name != current) return;
            component.Name = updated;
        }));

        return GetBuilder();
    }

    public TBuilder Force()
    {
        import.Operations.Add(new OverwriteOperation(_ => true));

        return GetBuilder();
    }

    public TBuilder Overwrite<TComponent>(Func<TComponent, bool> predicate)
        where TComponent : LogixComponent<TComponent>
    {
        import.Operations.Add(new OverwriteOperation(x =>
        {
            if (x is not TComponent component) return false;
            return predicate.Invoke(component);
        }));

        return GetBuilder();
    }

    public TBuilder Discard<TComponent>(Func<TComponent, bool> predicate)
        where TComponent : LogixComponent<TComponent>
    {
        import.Operations.Add(new DiscardOperation(x =>
        {
            if (x is not TComponent component) return false;
            return predicate.Invoke(component);
        }));

        return GetBuilder();
    }

    public TBuilder Modify<TComponent>(Func<TComponent, bool> predicate, Action<TComponent> action)
        where TComponent : LogixComponent<TComponent>
    {
        import.Operations.Add(new ModifyOperation(x =>
        {
            if (x is not TComponent component) return;
            if (!predicate.Invoke(component)) return;
            action.Invoke(component);
        }));

        return GetBuilder();
    }

    protected abstract TBuilder GetBuilder();
}