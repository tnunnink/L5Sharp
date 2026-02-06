using FluentAssertions;
using L5Sharp.CLI.Commands;
using L5Sharp.Core;
using L5Sharp.Samples;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.CLI.Commands;

[TestFixture]
public class ListCommandTests
{
    [Test]
    public async Task Execute_ProgramToJsonFormat_ShouldHaveExpectedOutput()
    {
        var content = await L5X.LoadAsync(Known.Test);
        using var context = new CliTestContext();
        context.Console.WriteInput(content.ToString());
        var app = context.CreateApp<ListCommand>();

        var exitCode = await app.RunAsync(["list", "program", "--format", "json"]);

        exitCode.Should().Be(0);
        await VerifyJson(context.Console.ReadOutputString());
    }

    [Test]
    [TestCase("Main*")]
    [TestCase("Fake*")]
    [TestCase("*Program")]
    [TestCase("FolderProgram")]
    public async Task Execute_ProgramWithNameFilter_ShouldHaveExpectedOutput(string name)
    {
        var content = await L5X.LoadAsync(Known.Test);
        using var context = new CliTestContext();
        context.Console.WriteInput(content.ToString());
        var app = context.CreateApp<ListCommand>();

        var exitCode = await app.RunAsync(
        [
            "list",
            "program",
            "--where", $"Name.Contains({name})",
            "--format", "json"
        ]);

        exitCode.Should().Be(0);
        await VerifyJson(context.Console.ReadOutputString());
    }

    [Test]
    [TestCase("Disabled=false")]
    [TestCase("Folder=true")]
    [TestCase("MainRoutine=\"Main\"")]
    [TestCase("np(Description, \"\").Contains(\"Test\")")]
    public async Task Execute_TestProjectExpressionArg_ShouldHaveExpectedOutput(string expression)
    {
        var content = await L5X.LoadAsync(Known.Test);
        using var context = new CliTestContext();
        context.Console.WriteInput(content.ToString());
        var app = context.CreateApp<ListCommand>();

        var exitCode = await app.RunAsync(
        [
            "list",
            "program",
            "--where", $"{expression}",
            "--format", "json"
        ]);

        exitCode.Should().Be(0);
        await VerifyJson(context.Console.ReadOutputString());
    }

    [Test]
    [TestCase("Name")]
    [TestCase("Name Description MainRoutine Disabled")]
    [TestCase("")]
    public async Task Execute_TestProjectSelectProperties_ShouldHaveExpectedOutput(string select)
    {
        var content = await L5X.LoadAsync(Known.Test);
        using var context = new CliTestContext();
        context.Console.WriteInput(content.ToString());
        var app = context.CreateApp<ListCommand>();

        var exitCode = await app.RunAsync(
        [
            "list",
            "program",
            "--select", $"{select.Split(' ', StringSplitOptions.RemoveEmptyEntries)}",
            "--format", "json"
        ]);

        exitCode.Should().Be(0);
        await VerifyJson(context.Console.ReadOutputString());
    }

    [Test]
    [TestCase(1)]
    [TestCase(10)]
    [TestCase(0)]
    [TestCase(-1)]
    public async Task Execute_TestProjectSelectProperties_ShouldHaveExpectedOutput(int take)
    {
        var content = await L5X.LoadAsync(Known.Test);
        using var context = new CliTestContext();
        context.Console.WriteInput(content.ToString());
        var app = context.CreateApp<ListCommand>();

        var exitCode = await app.RunAsync(
        [
            "list",
            "program",
            "--take", $"{take}",
            "--format", "json"
        ]);

        exitCode.Should().Be(0);
        await VerifyJson(context.Console.ReadOutputString());
    }
}