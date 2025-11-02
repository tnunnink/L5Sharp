using CliFx.Attributes;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;
using L5Sharp.Model;

namespace L5Sharp.CLI.Commands.DataTypes;

[Command("datatype list", Description = "Lists data types found in the target project.")]
public class DataTypeListCommand : ListCommand<DataType, DataTypeInfo>
{
    protected override IEnumerable<DataTypeInfo> ExecuteQuery(L5X project)
    {
        return project.Query<DataType>().ToInfo()
            .FilterByPattern(x => x.Name, Name)
            .FilterByExpression(Where)
            .Take(Take);
    }
}