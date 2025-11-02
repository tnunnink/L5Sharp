using CliFx.Attributes;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands.Programs;

[Command("program get", Description = "")]
public class ProgramGetCommand : GetCommand<Program>;