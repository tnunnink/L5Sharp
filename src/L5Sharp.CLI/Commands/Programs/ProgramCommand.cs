using CliFx.Attributes;
using L5Sharp.CLI.Records;

namespace L5Sharp.CLI.Commands.Programs;

[Command("program", Description = "Displays the schema information (properties and types) for program components.")]
public class ProgramCommand : SchemaCommand<ProgramInfo>;