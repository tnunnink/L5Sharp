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
}