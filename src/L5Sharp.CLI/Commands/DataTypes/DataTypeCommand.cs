using CliFx.Attributes;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;
using L5Sharp.Model;

namespace L5Sharp.CLI.Commands.DataTypes;

[Command("datatype", Description = "")]
public class DataTypeCommand : SchemaCommand<DataType>;