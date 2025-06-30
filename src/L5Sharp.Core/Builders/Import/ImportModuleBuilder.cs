using System;

namespace L5Sharp.Core;

internal class ImportModuleBuilder(Import import) :
    ImportConfigBuilder<IImportModuleBuilder>(import),
    IImportModuleBuilder
{
    private readonly Import _import = import;
    protected override IImportModuleBuilder GetBuilder() => this;

    public IImportModuleBuilder ConnectTo(string parentName)
    {
        _import.Operations.Add(new ConfigureOperation(x =>
        {
            if (x is not Module module) return;
            if (x.Document is null) return;
            if (!x.Document.TryGet<Module>(parentName, out var parent))
                throw new InvalidOperationException(
                    $"No module with name '{parentName}' found in the current project.");
            parent.Connect(module);
        }));

        return this;
    }
}