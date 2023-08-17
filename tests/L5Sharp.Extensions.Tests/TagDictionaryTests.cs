using FluentAssertions;
using L5Sharp.Samples;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Extensions.Tests;

[TestFixture]
public class TagDictionaryTests
{
    [Test]
    public void Tags_WhenCalled_ShouldNotBeNull()
    {
        var content = LogixContent.Load(Known.Test);

        var dictionary = content.Tags();

        dictionary.Should().NotBeNull();
    }
    
    [Test]
    public void Find_ExistingTag_ShouldNotBeNull()
    {
        var content = LogixContent.Load(Known.Test);
        
        var tag = content.Tags().Find("TestSimpleTag.DintMember.1");

        tag.Should().NotBeNull();
        tag?.TagName.Should().Be("TestSimpleTag.DintMember.1");
        tag?.Value.Should().BeOfType<BOOL>();
    }

    [Test]
    public void Indexer_ExistingTag_ShouldNotBeNull()
    {
        var content = LogixContent.Load(Known.Test);

        var tag = content.Tags()["TestSimpleTag.DintMember.1"];

        tag.Should().NotBeNull();
        tag.TagName.Should().Be("TestSimpleTag.DintMember.1");
        tag.Value.Should().BeOfType<BOOL>();
    }

    [Test]
    public void FindAll_ExistingTag_ShouldHaveExpectedCount()
    {
        var content = LogixContent.Load(Known.Test);

        var tags = content.Tags().FindAll("TestSimpleTag.DintMember.1");

        tags.Should().HaveCount(1);
    }
    
    [Test]
    public void FindIn_NonExistingTag_ShouldBeNull()
    {
        var content = LogixContent.Load(Known.Test);

        var tag = content.Tags().In("TestProgram", "Fake.TagName.Member");

        tag.Should().BeNull();
    }
}