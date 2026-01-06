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
            Project = @"C:\Users\tnunnink\Documents\Projects\L5Sharp\tests\L5Sharp.Samples\Empty.ACD",
            Output = @"C:\Users\tnunnink\Documents\Projects\L5Sharp\tests\L5Sharp.Samples\Simple.L5X",
            Force = true,
            Detailed = true
        };

        await command.ExecuteAsync(new SystemConsole());
    }
}