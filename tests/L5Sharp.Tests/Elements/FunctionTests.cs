using FluentAssertions;
using L5Sharp.Elements;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class FunctionTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var function = new Function();

        function.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveDefaultValues()
    {
        var function = new Function();

        function.ID.Should().Be(0);
        function.X.Should().Be(0);
        function.Y.Should().Be(0);
        function.Type.Should().BeNull();
        function.Sheet.Should().BeNull();
    }

    [Test]
    public void New_Overloaded_ShouldHaveExpectedValues()
    {
        var function = new Function
        {
            ID = 1, X = 123, Y = 123, Type = "ADD"
        };

        function.Should().NotBeNull();
        function.ID.Should().Be(1);
        function.X.Should().Be(123);
        function.Y.Should().Be(123);
        function.Type.Should().Be("ADD");
    }
    
    [Test]
    public Task Serialize_Default_ShouldBeVerified()
    {
        var function = new Function();

        return Verify(function.Serialize().ToString());
    }


    [Test]
    public Task Serialize_Overloaded_ShouldBeVerified()
    {
        var function = new Function
        {
            ID = 1, X = 123, Y = 123, Type = "SUB"
        };

        return Verify(function.Serialize().ToString());
    }
}