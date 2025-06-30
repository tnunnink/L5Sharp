namespace L5Sharp.Core;

internal class ImportDefaultBuilder(Import import) :
    ImportConfigBuilder<IImportDefaultBuilder>(import),
    IImportDefaultBuilder
{
    protected override IImportDefaultBuilder GetBuilder() => this;
}