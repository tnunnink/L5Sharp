using FluentAssertions;


namespace L5Sharp.Tests.Core;

[TestFixture]
public class L5XTasksTests
{
    [Test]
    public void ToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var tasks = content.Tasks.ToList();

        tasks.Should().NotBeEmpty();
    }
    
    [Test]
    public void Index_ValidIndex_ShouldNotBeNull()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Tasks[0];

        result.Should().NotBeNull();
    }

    [Test]
    public void Index_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Tasks[100]).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Get_KnownTask_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Tasks.Get(Known.Task);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.Task);
    }
}