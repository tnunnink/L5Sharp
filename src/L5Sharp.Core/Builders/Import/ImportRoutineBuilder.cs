namespace L5Sharp.Core;

internal class ImportRoutineBuilder(Import import) :
    ImportConfigBuilder<IImportRoutineBuilder>(import),
    IImportRoutineBuilder
{
    private readonly Import _import = import;
    protected override IImportRoutineBuilder GetBuilder() => this;

    public IImportRoutineBuilder InProgram(string programName)
    {
        _import.Operations.Add(new ModifyOperation(x =>
        {
            if (x is not Routine routine) return;
            if (!Use.Target.Equals(routine.Use)) return;
            if (!routine.TryGetDocument(out _)) return;
            if (routine.Program is null) return;
            if (routine.Program.Name == programName) return;
            //This is a bit of a hack but should work if we are just importing a single routine and then discarding
            //the source L5X (not saving).
            routine.Program.Name = programName;
        }));

        return this;
    }
}