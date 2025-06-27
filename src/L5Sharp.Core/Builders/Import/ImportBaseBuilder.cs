namespace L5Sharp.Core;

internal class ImportBaseBuilder(ImportConfig config) :
    ImportConfigBuilder<IImportBaseBuilder>(config),
    IImportBaseBuilder
{
    protected override IImportBaseBuilder GetBuilder() => this;
}