using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class UnitTests
{
    [Test]
    public void New_NullTagName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new Unit(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_TagName_ShouldHaveExpectedValues()
    {
        var unit = new Unit(".Member", "rpm");

        unit.Operand.Should().Be(".Member");
        unit.Value.Should().Be("rpm");
    }

    [Test]
    public void SetValue_ValidValue_ShouldBeExpected()
    {
        var unit = new Unit(".Member");

        unit.Value = "degrees";

        unit.Value.Should().Be("degrees");
    }

    [Test]
    public Task Serialize_WhenCalled_ShouldBeVerified()
    {
        var unit = new Unit(".Member", "rpm");

        return Verify(unit.Serialize().ToString());
    }
}
