using FluentAssertions;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentTasksTests
{
    [Test]
    public void ToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var tasks = content.Tasks.ToList();

        tasks.Should().NotBeEmpty();
    }
    
    [Test]
    public void Index_ValidIndex_ShouldNotBeNull()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tasks[0];

        result.Should().NotBeNull();
    }

    [Test]
    public void Index_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        var content = LogixContent.Load(Known.Test);

        FluentActions.Invoking(() => content.Tasks[100]).Should().Throw<ArgumentOutOfRangeException>();
    }
}