using FluentAssertions;
using L5Sharp.Components;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixExtensionTests
{
    [Test]
    public void Tags_WhenCalled_GetsAllTagsEverywhereIncludingModuleAndAoi()
    {
        var content = LogixContent.Load(Known.Test);
        
        var tags = content.Tags().ToList();

        tags.Should().NotBeEmpty();
    }

    [Test]
    public Task AddModule_ValidModule_ShouldBeVerified()
    {
        var content = LogixContent.Load(Known.Test);

        var local = content.Modules.Local();
        
        local?.Add(new Module{ Name = "Test", CatalogNumber = "WhoCares", Revision = 1.3});

        var xml = content.Modules.Serialize().ToString();

        return Verify(xml);
    }
}