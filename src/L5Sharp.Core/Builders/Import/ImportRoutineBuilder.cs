namespace L5Sharp.Core;

internal class ImportRoutineBuilder(ImportConfig config) :
    ImportConfigBuilder<IImportRoutineBuilder>(config),
    IImportRoutineBuilder
{
    protected override IImportRoutineBuilder GetBuilder() => this;

    public IImportRoutineBuilder InProgram(string name)
    {
        throw new System.NotImplementedException();
    }
}