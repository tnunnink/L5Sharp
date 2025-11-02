using CliFx.Attributes;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;
using L5Sharp.Model;

namespace L5Sharp.CLI.Commands.Modules;

[Command("module list", Description = "")]
public class ModuleListCommand : ListCommand<Module, ModuleInfo>
{
    protected override IEnumerable<ModuleInfo> ExecuteQuery(L5X project)
    {
        return project.Query<Module>().ToInfo()
            .FilterByPattern(x => x.Name, Name)
            .FilterByExpression(Where)
            .Take(Take);
    }
}