using CliFx.Attributes;
using CliFx.Exceptions;
using JetBrains.Annotations;
using L5Sharp.Core;
using Microsoft.VisualBasic.CompilerServices;

namespace L5Sharp.CLI.Commands.Programs;

[Command("program clone", Description = "Creates a copy of an existing program with a new name")]
public class ProgramCloneCommand : CloneCommand<Program>;