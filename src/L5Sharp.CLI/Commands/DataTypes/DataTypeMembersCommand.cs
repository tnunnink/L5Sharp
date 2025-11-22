using CliFx.Attributes;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;
using L5Sharp.Model;

namespace L5Sharp.CLI.Commands.DataTypes;

[Command("datatype members", Description = "")]
public class DataTypeMembersCommand : ListCommand<DataTypeMemberInfo>
{
    [CommandOption("parent", Description = "The name of the type to get members for (supports wildcards).")]
    public string Parent { get; set; } = string.Empty;

    protected override IEnumerable<DataTypeMemberInfo> ExecuteQuery(L5X project)
    {
        return project.Query<DataType>()
            .SelectMany(d => d.Members)
            .ToInfo()
            .FilterByPattern(x => x.Name, Name)
            .FilterByPattern(x => x.Parent, Parent)
            .FilterByExpression(Where)
            .Take(Take);
    }
}