using FluentAssertions;


namespace L5Sharp.Tests;

[TestFixture]
public class L5XTagTests
{
    [Test]
    public void ToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Tags.ToList();

        result.Should().NotBeEmpty();
    }

    [Test]
    public void AllTagsToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Tags.SelectMany(t => t.Members()).ToList();

        result.Should().NotBeEmpty();
    }

    [Test]
    public void Test()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Tags.Get("DateTimeNs");

        result.Should().NotBeNull();
        result.Value.Should().BeOfType<LINT>();
    }

    [Test]
    public void References_AgainstKnownTest_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);
        var tag = content.Tags.Get(Known.Tag);

        var references = tag.References().ToList();

        references.Should().NotBeEmpty();
    }

    [Test]
    public void References_AgainstAllTags_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Example);
        
        var tags = content.Query<Tag>().ToList();

        var references = tags.Select(t => new {t.TagName, Refernces = t.References()}).ToList();

        references.Should().NotBeEmpty();
    }
}