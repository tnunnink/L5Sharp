using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands.Projects;

[Command("new", Description = "")]
public class NewCommand : ICommand
{
    [CommandParameter(0, Name = "name", Description = "")]
    public string Name { get; set; } = string.Empty;

    [CommandParameter(1, Name = "processor", Description = "")]
    public string Processor { get; set; } = string.Empty;

    [CommandOption("revision", 'r', Description = "")]
    public ushort? Revision { get; set; }

    public ValueTask ExecuteAsync(IConsole console)
    {
        try
        {
            //Only evaluate revision when 
            var revision = Revision.HasValue ? new Revision(Revision.Value) : null;
            
            //The new method will internally handle module info lookups.
            var project = L5X.New(Name, Processor, revision);
            
            //Emit to the console so the caller can chain to the next command.
            console.WriteXml(project);
        }
        catch (Exception e)
        {
            throw new CommandException($"Command Failed: {e.Message}", ExitCodes.InternalError, false, e);
        }

        return ValueTask.CompletedTask;
    }
}