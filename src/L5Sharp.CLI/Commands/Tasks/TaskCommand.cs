using CliFx.Attributes;
using Task = L5Sharp.Core.Task;

namespace L5Sharp.CLI.Commands.Tasks;

[Command("task", Description = "Lists tasks found in the target project.")]
public class TaskCommand : SchemaCommand<Task>;