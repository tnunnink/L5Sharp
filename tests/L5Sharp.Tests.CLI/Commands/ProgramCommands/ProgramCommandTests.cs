using CliFx.Infrastructure;
using L5Sharp.CLI.Commands.Programs;

namespace L5Sharp.Tests.CLI.Commands.ProgramCommands;

[TestFixture]
public class ProgramCommandTests
{
    [Test]
    public async Task Execute_WhenCalled_ShouldOutputExpectedTable()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ProgramCommand();

        await command.ExecuteAsync(console);

        await Verify(console.ReadOutputString());
    }
}