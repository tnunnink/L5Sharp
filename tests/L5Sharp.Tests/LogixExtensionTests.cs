using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixExtensionTests
{
    [Test]
    public void ToArrayType_OneDimension_ShouldBeOfCorrectType()
    {
        var array = new DINT[] { 1, 2, 3, 4 };

        var type = array.ToArrayType<DINT>();

        type.Should().BeOfType<ArrayType<DINT>>();
    }

    [Test]
    public void ToArrayType_OperatorOverload_ShouldBeOfCorrectType_()
    {
        LogixType type = new DINT[] { 1, 2, 3, 4 };
        
        type.Should().BeOfType<ArrayType<DINT>>();
    }
    
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