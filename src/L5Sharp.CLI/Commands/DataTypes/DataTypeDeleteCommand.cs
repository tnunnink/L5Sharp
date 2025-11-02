using CliFx.Attributes;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands.DataTypes;

[Command("datatype delete", Description = "")]
public class DataTypeDeleteCommand : DeleteCommand<DataType>;