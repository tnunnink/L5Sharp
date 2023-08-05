using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Extensions.Tests.TestTypes;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Extensions.Tests;

[TestFixture]
public class TagExtensionsTests
{
    [Test]
    public void CreateTagLookupForAllTags()
    {
        var content = LogixContent.Load(Known.Test);

        var lookup = content.Tags().ToLookup(k => k.TagName, t => t);

        lookup.Should().NotBeEmpty();
    }
    
    [Test]
    public void CreateTagLookupForAllTagsAndTheirMembers()
    {
        var content = LogixContent.Load(Known.Test);

        var lookup = content.Tags().SelectMany(t => t.Members()).ToLookup(k => k.TagName, t => t);

        lookup.Should().NotBeEmpty();
    }


    
    [Test]
    public void Add_StructureType_ShouldThrowInvalidOperationException()
    {
        var tag = new Tag { Name = "Test", Value = new TIMER() };

        FluentActions.Invoking(() => tag.Add("Test", 123)).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Add_ComplexType_ShouldUpdateStructure()
    {
        var tag = new Tag { Name = "Test", Value = new MySimpleType() };

        tag.Add("Test", 123);

        tag["Test"].Should().NotBeNull();
        tag["Test"].Value.Should().BeOfType<DINT>();
        tag["Test"].Value.Should().Be(123);
    }

    [Test]
    public Task Add_ComplexType_ShouldBeVerified()
    {
        var tag = new Tag { Name = "Test", Value = new MySimpleType() };

        tag.Add("Test", 123);

        var xml = tag.Serialize().ToString();
        return Verify(xml);
    }

    [Test]
    public Task Remove_ValidTag_ShouldBeVerified()
    {
        var tag = new Tag { Name = "Test", Value = new MySimpleType() };

        tag.Remove("M3");

        var xml = tag.Serialize().ToString();
        return Verify(xml);
    }
}