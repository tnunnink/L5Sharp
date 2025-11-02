using CliFx.Infrastructure;
using L5Sharp.CLI.Commands;
using L5Sharp.CLI.Commands.Projects;

namespace L5Sharp.Tests.CLI.Commands;

[TestFixture]
public class ConvertCommandTests
{
    [Test]
    public async Task ShouldConvertFileSuccessfully()
    {
        var command = new ConvertCommand
        {
        };

        await command.ExecuteAsync(new SystemConsole());
    }
}