using CliFx.Infrastructure;
using L5Sharp.CLI.Commands.Programs;
using L5Sharp.Core;
using L5Sharp.Samples;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.CLI.Commands.ProgramCommands;

[TestFixture]
public class ProgramDeleteCommandTests
{
    [Test]
    public async Task Execute_EmptyFile_ShouldReturnExpectedOutput()
    {
        using var console = new FakeInMemoryConsole();
        var content = L5X.Empty();
        console.WriteInput(content.Serialize().ToString());
        
        var command = new ProgramDeleteCommand { Name = "Test" };

        await command.ExecuteAsync(console);

        await VerifyXml(console.ReadOutputString())
            .ScrubInlineDateTimes("ddd MMM d HH:mm:ss yyyy")
            .ScrubMember("Owner");
    }

    [Test]
    [TestCase("MainProgram")]
    [TestCase("*Program")]
    [TestCase("Folder*")]
    [TestCase("Fake")]
    public async Task Execute_TestFileByName_ShouldReturnExpectedOutput(string name)
    {
        using var console = new FakeInMemoryConsole();
        var content = await L5X.LoadAsync(Known.Test);
        console.WriteInput(content.Serialize().ToString());
        
        var command = new ProgramDeleteCommand { Name = name };

        await command.ExecuteAsync(console);

        await VerifyXml(console.ReadOutputString())
            .ScrubInlineDateTimes("ddd MMM d HH:mm:ss yyyy")
            .ScrubMember("Owner");
    }
}