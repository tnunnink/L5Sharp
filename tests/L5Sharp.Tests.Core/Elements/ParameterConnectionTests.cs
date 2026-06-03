using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class ParameterConnectionTests
{
    [Test]
    public void New_Default_ShouldHaveExpectedDefaults()
    {
        var connection = new ParameterConnection();

        connection.EndPoint1.Should().BeEmpty();
        connection.EndPoint2.Should().BeEmpty();
    }

    [Test]
    public void New_Overridden_ShouldHaveExpectedValues()
    {
        var connection = new ParameterConnection
        {
            EndPoint1 = "Program:MainProgram.Tag1",
            EndPoint2 = "Program:MainProgram.Tag2"
        };

        connection.EndPoint1.Should().Be("Program:MainProgram.Tag1");
        connection.EndPoint2.Should().Be("Program:MainProgram.Tag2");
    }

    [Test]
    public Task New_Overridden_ShouldBeVerified()
    {
        var connection = new ParameterConnection
        {
            EndPoint1 = "Program:MainProgram.Tag1",
            EndPoint2 = "Program:MainProgram.Tag2"
        };

        return Verify(connection.Serialize().ToString());
    }
}
