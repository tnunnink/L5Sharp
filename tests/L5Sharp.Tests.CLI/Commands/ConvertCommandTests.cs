using CliFx.Infrastructure;
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
            Project = @"C:\Users\tnunnink\Documents\Rockwell\Empty.ACD",
            Force = true,
            Detailed = true
        };

        await command.ExecuteAsync(new SystemConsole());
    }
}