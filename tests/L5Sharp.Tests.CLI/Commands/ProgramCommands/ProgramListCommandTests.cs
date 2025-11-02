using CliFx.Infrastructure;
using L5Sharp.CLI.Commands.Programs;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;
using L5Sharp.Samples;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.CLI.Commands.ProgramCommands;

[TestFixture]
public class ProgramListCommandTests
{
    [Test]
    public async Task Execute_TestProject_ShouldHaveExpectedOutput()
    {
        using var console = new FakeInMemoryConsole();
        var content = await L5X.LoadAsync(Known.Test);
        console.WriteInput(content.Serialize().ToString());

        var command = new ProgramListCommand
        {
            Format = Format.Json
        };

        await command.ExecuteAsync(console);

        var output = console.ReadOutputString();
        await VerifyJson(output);
    }

    [Test]
    [TestCase("Main*")]
    [TestCase("Fake*")]
    [TestCase("*Program")]
    [TestCase("FolderProgram")]
    public async Task Execute_TestProjectNameContains_ShouldHaveExpectedOutput(string name)
    {
        using var console = new FakeInMemoryConsole();
        var content = await L5X.LoadAsync(Known.Test);
        console.WriteInput(content.Serialize().ToString());

        var command = new ProgramListCommand
        {
            Name = name,
            Format = Format.Json
        };

        await command.ExecuteAsync(console);

        await VerifyJson(console.ReadOutputString());
    }

    [Test]
    [TestCase("Disabled=false")]
    [TestCase("Folder=true")]
    [TestCase("MainRoutine=\"Main\"")]
    [TestCase("np(Description, \"\").Contains(\"Test\")")]
    public async Task Execute_TestProjectExpressionArg_ShouldHaveExpectedOutput(string expression)
    {
        using var console = new FakeInMemoryConsole();
        var content = await L5X.LoadAsync(Known.Test);
        console.WriteInput(content.Serialize().ToString());

        var command = new ProgramListCommand
        {
            Where = expression,
            Format = Format.Json
        };

        await command.ExecuteAsync(console);

        await VerifyJson(console.ReadOutputString());
    }

    [Test]
    [TestCase("Name")]
    [TestCase("Name Description MainRoutine Disabled")]
    [TestCase("")]
    public async Task Execute_TestProjectSelectProperties_ShouldHaveExpectedOutput(string select)
    {
        using var console = new FakeInMemoryConsole();
        var project = await L5X.LoadAsync(Known.Test);
        console.WriteInput(project.Serialize().ToString());

        var command = new ProgramListCommand
        {
            Select = select.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            Format = Format.Json
        };

        await command.ExecuteAsync(console);

        await VerifyJson(console.ReadOutputString());
    }
    
    [Test]
    [TestCase(1)]
    [TestCase(10)]
    [TestCase(0)]
    [TestCase(-1)]
    public async Task Execute_TestProjectSelectProperties_ShouldHaveExpectedOutput(int take)
    {
        using var console = new FakeInMemoryConsole();
        var project = await L5X.LoadAsync(Known.Test);
        console.WriteInput(project.Serialize().ToString());

        var command = new ProgramListCommand
        {
            Take = take,
            Format = Format.Json
        };

        await command.ExecuteAsync(console);

        await VerifyJson(console.ReadOutputString());
    }
}