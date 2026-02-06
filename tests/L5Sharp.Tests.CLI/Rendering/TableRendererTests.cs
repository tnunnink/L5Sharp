using CliFx.Infrastructure;
using L5Sharp.CLI.Common;
using L5Sharp.CLI.Rendering;
using L5Sharp.Core;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.CLI.Rendering;

[TestFixture]
public class TableRendererTests
{
    [Test]
    public Task Write_SingleItem_ShouldBeVerified()
    {
        var console = new FakeInMemoryConsole();
        var writer = new TableRenderer(null!);

        //writer.Write(console, [new Tag()]);

        return Verify(console.ReadOutputString());
    }
}