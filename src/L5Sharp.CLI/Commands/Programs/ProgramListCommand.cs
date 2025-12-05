using CliFx.Attributes;
using L5Sharp.CLI.Internal;
using L5Sharp.CLI.Records;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands.Programs;

[Command("program list", Description = "Lists programs found in the target project.")]
public class ProgramListCommand : ListCommand<ProgramInfo>
{
    protected override IEnumerable<ProgramInfo> ExecuteQuery(L5X project)
    {
        return project.Query<Program>().ToInfo()
            .FilterByPattern(x => x.Name, Name)
            .FilterByExpression(Where)
            .Take(Take);
    }
}