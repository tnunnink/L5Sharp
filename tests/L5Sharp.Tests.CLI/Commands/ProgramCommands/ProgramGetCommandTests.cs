using CliFx.Infrastructure;
using L5Sharp.CLI.Commands.Programs;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;
using L5Sharp.Samples;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.CLI.Commands.ProgramCommands;

[TestFixture]
public class ProgramGetCommandTests
{
    [Test]
    public async Task Execute_TestFileValidName_ShouldWriteExpectedOutput()
    {
        using var console = new FakeInMemoryConsole();
        var content = await L5X.LoadAsync(Known.Test);
        console.WriteInput(content.ToString());

        var command = new ProgramGetCommand
        {
            Name = "MainProgram",
            Format = Format.Json
        };

        await command.ExecuteAsync(console);

        await VerifyJson(console.ReadOutputString());
    }
}