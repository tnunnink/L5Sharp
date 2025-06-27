using System;

namespace L5Sharp.Core;

internal class ImportModuleBuilder(ImportConfig config) :
    ImportConfigBuilder<IImportModuleBuilder>(config),
    IImportModuleBuilder
{
    protected override IImportModuleBuilder GetBuilder() => this;

    public IImportModuleBuilder ConnectTo(string parentName)
    {
        config.Operations.Add(new ConfigureOperation(x =>
        {
            if (x is not Module module) return;
            if (x.L5X is null) return;
            if (!x.L5X.TryGet<Module>(parentName, out var parent))
                throw new InvalidOperationException(
                    $"No module with name '{parentName}' found in the current project.");
            parent.Connect(module);
        }));

        return this;
    }
}