using CliFx.Attributes;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands.Programs;

[Command("program delete", Description = "Deletes a program from the target project.")]
public class ProgramDeleteCommand : DeleteCommand<Program>;