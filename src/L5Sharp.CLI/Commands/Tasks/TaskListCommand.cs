using CliFx.Attributes;
using L5Sharp.CLI.Internal;
using L5Sharp.CLI.Records;
using L5Sharp.Core;
using Task = L5Sharp.Core.Task;

namespace L5Sharp.CLI.Commands.Tasks;

[Command("task list", Description = "Lists tasks found in the target project.")]
public class TaskListCommand : ListCommand<TaskInfo>
{
    protected override IEnumerable<TaskInfo> ExecuteQuery(L5X project)
    {
        return project.Query<Task>().ToInfo()
            .FilterByPattern(x => x.Name, Name)
            .FilterByExpression(Where)
            .Take(Take);
    }
}