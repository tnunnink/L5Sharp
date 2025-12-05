using CliFx.Attributes;
using L5Sharp.CLI.Records;

namespace L5Sharp.CLI.Commands.Modules;

[Command("module", Description = "")]
public class ModuleCommand : SchemaCommand<ModuleInfo>;