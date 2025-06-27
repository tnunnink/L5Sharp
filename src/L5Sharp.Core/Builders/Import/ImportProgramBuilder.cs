using System;

namespace L5Sharp.Core;

internal class ImportProgramBuilder(ImportConfig config) :
    ImportConfigBuilder<IImportProgramBuilder>(config),
    IImportProgramBuilder
{
    private readonly ImportConfig _config = config;
    protected override IImportProgramBuilder GetBuilder() => this;

    public IImportProgramBuilder ScheduleIn(string taskName)
    {
        _config.Operations.Add(new ConfigureOperation(x =>
        {
            if (x is not Program program) return;
            if (program.L5X is null) return;
            if (!program.L5X.TryGet<Task>(taskName, out var task))
                throw new InvalidOperationException(
                    $"No task with name '{taskName}' exists in the current project.");
            task.Schedule(program.Name);
        }));

        return this;
    }

    public IImportProgramBuilder WithParent(string programName)
    {
        _config.Operations.Add(new ConfigureOperation(x =>
        {
            if (x is not Program program) return;
            if (program.L5X is null) return;
            if (!program.L5X.TryGet<Program>(programName, out var parent))
                throw new InvalidOperationException(
                    $"No program with name '{programName}' exists in the current project.");
            parent.AddChild(program.Name);
        }));

        return this;
    }
}