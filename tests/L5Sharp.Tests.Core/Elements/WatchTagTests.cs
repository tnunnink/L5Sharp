using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class WatchTagTests
{
    [Test]
    public void New_Default_ShouldHaveExpectedDefaults()
    {
        var watchTag = new WatchTag();

        watchTag.Specifier.Should().Be(TagName.Empty);
        watchTag.Scope.Should().BeEmpty();
    }

    [Test]
    public void New_Overridden_ShouldHaveExpectedValues()
    {
        var watchTag = new WatchTag
        {
            Specifier = "TestTag",
            Scope = "MainProgram"
        };

        watchTag.Specifier.Should().Be("TestTag");
        watchTag.Scope.Should().Be("MainProgram");
    }

    [Test]
    public Task New_Overridden_ShouldBeVerified()
    {
        var watchTag = new WatchTag
        {
            Specifier = "TestTag",
            Scope = "MainProgram"
        };

        return Verify(watchTag.Serialize().ToString());
    }
}
