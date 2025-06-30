using System;
using System.Linq;

namespace L5Sharp.Core;

internal class ImportProgramBuilder(Import import) :
    ImportConfigBuilder<IImportProgramBuilder>(import),
    IImportProgramBuilder
{
    private readonly Import _import = import;
    protected override IImportProgramBuilder GetBuilder() => this;

    public IImportProgramBuilder ScheduleIn(string taskName, Action<Task>? taskConfig = null)
    {
        _import.Operations.Add(new ConfigureOperation(x =>
        {
            if (x is not Program program) return;
            if (program.Document is null) return;

            if (taskConfig is not null)
            {
                var newTask = new Task();
                taskConfig.Invoke(newTask);
                program.Document.Add(newTask);
            }

            //We can't rely on the index API since if this content was indexed upon loading,
            //then the previously added task will not be found.
            var task = program.Document.Tasks.FirstOrDefault(t => t.Name == taskName);
            if (task is null)
                throw new InvalidOperationException(
                    $"No task with name '{taskName}' exists in the current project.");

            task.Schedule(program.Name);
        }));

        return this;
    }

    public IImportProgramBuilder WithParent(string programName)
    {
        _import.Operations.Add(new ConfigureOperation(x =>
        {
            if (x is not Program program) return;
            if (program.Document is null) return;
            if (!program.Document.TryGet<Program>(programName, out var parent))
                throw new InvalidOperationException(
                    $"No program with name '{programName}' exists in the current project.");
            parent.AddChild(program.Name);
        }));

        return this;
    }
}