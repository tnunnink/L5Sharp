using CliFx.Attributes;
using CliFx.Infrastructure;
using JetBrains.Annotations;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;
using L5Sharp.Model;

namespace L5Sharp.CLI.Commands.Programs;

[PublicAPI]
[Command("program push", Description = "Creates or updates programs from JSON configuration.")]
public class ProgramPushCommand : MutateCommand
{
    protected override void Mutate(L5X project)
    {
        throw new NotImplementedException();
        /*var cancellation = console.RegisterCancellationHandler();
        var project = await LoadProject(console, cancellation);
        var records = await console.ReadJson<ProgramInfo>(cancellation);

        foreach (var record in records)
        {
            if (project.TryGet<Program>(record.Name, out var program))
            {
                record.Update(program);
                continue;
            }

            project.Add(record);
        }

        SaveProject(console, project);
        console.WriteIfRedirected(records, Format, Select);*/
    }
}